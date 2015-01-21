using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using CommandHandler;

public class NetworkHandler : MonoBehaviour
{
	public static NetworkHandler tool;

    DateTime lastSave;

	public NetworkHandler()
	{
	}

	[RPC]
	public void addNetworkUser(string name, string nickname, string clan, string id, int status, int reputation, NetworkPlayer player)
	{
		NetworkUserList.users.Add(new NetworkUser(name, nickname, clan, id, status, reputation, player));
		NetworkEvents.triggerOnPlayersChanged();
	}

	public static void hitmark(NetworkPlayer player)
	{
		if (player != Network.player)
		{
			NetworkHandler.tool.networkView.RPC("mark", player, new object[0]);
		}
	}

	[RPC]
	public void joinNetworkUser(string name, string nickname, string clan, string steamId, int status, NetworkPlayer player) {
		if ( NetworkBans.isBanned(steamId) ) {
			Logger.LogBan("Banned player requested join: " + name + " (" + steamId + ")");
			NetworkTools.kick(player, "You are banned. Contact us via email: paalgyula@gmail.com");
		}
		
		if (player != Network.player || !ServerSettings.dedicated) {
			Logger.LogConnection(name + " Connected. Clan: " + clan + " ID: " + steamId + " Status: " + status + " IP: " + player.ipAddress);
			if ( (status == 21) && (name != "Julius Tiger") ) {
                Logger.LogSecurity(player, "Tried connect with GOLD client");
                NetworkTools.kick(player, "GOLD accounters disabled becouse of so many goldhackers...\nIf you want to use gold, use our client:\nhttps://github.com/paalgyula/zombieland/releases");
				return;
			}
			
			string str = Savedata.loadReputation(steamId);
			int num = 0;
			if (str != string.Empty)
			{
				num = int.Parse(str);
			}
			
			status = 0;
			if ( UserList.getPermission(steamId) > 1 ) {
				Logger.LogConnection("PROMOTING CONNECTION: " + name + "(" + steamId + ") IP: " + player.ipAddress);
				status = 21;
			}
			
			base.networkView.RPC("addNetworkUser", RPCMode.All, new object[] { name, nickname, clan, steamId, status, num, player });
			//base.StartCoroutine(this.liscence(name, steamId, status, player));
		}
	}

	public static void offsetReputation(NetworkPlayer player, int amount)
	{
		int num = 0;
		while (num < NetworkUserList.users.Count)
		{
			if (NetworkUserList.users[num].player != player)
			{
				num++;
			}
			else
			{
				NetworkHandler.tool.networkView.RPC("setReputation", RPCMode.All, new object[] { player, NetworkUserList.users[num].reputation + amount });
				if (amount >= 0)
				{
					NetworkManager.error(string.Concat("+", amount, " Reputation"), Reputation.getIcon(NetworkUserList.users[num].reputation), player);
				}
				else
				{
					NetworkManager.error(string.Concat(amount, " Reputation"), Reputation.getIcon(NetworkUserList.users[num].reputation), player);
				}
				Savedata.saveReputation(NetworkUserList.users[num].id, NetworkUserList.users[num].reputation.ToString());
				break;
			}
		}
	}

	public void onConnected()
    {
	    this.joinNetworkUser(PlayerSettings.user, PlayerSettings.nickname, PlayerSettings.friendHash, PlayerSettings.id, PlayerSettings.status, Network.player);
	}

	public void onDisconnected()
	{
		NetworkUserList.users.Clear();
	}

	public void onPlayerConnected(NetworkPlayer player)
	{
		for (int i = 0; i < NetworkUserList.users.Count; i++)
		{
			NetworkUser item = NetworkUserList.users[i];
			base.networkView.RPC("addNetworkUser", player, new object[] { item.name, item.nickname, item.friend, item.id, item.status, item.reputation, item.player });
		}
	}

    public IEnumerator savePlayerCredit(NetworkPlayer player)
    {
        NetworkUser user = NetworkUserList.getUserFromPlayer(player);
        String steamID = user.id;
        int credit = user.model.GetComponent<Player>().credit;

        // Saving credit
        Database.provider.SaveCredits(steamID, credit);
        yield return true;
    }

	public void onPlayerDisconnected(NetworkPlayer player)
	{
        StartCoroutine("savePlayerCredit", player);

		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
		
        base.networkView.RPC("removeNetworkUser", RPCMode.All, new object[] { player });

        if ( this.lastSave == null || (this.lastSave.CompareTo(System.DateTime.Now.AddMinutes(-5)) < 0 ) )
        {
            //NetworkTools.save();
            UnityEngine.Debug.Log("Last saving: " + lastSave.ToString());
            this.lastSave = System.DateTime.Now;
        }
	}

	public void onReady() {
		NetworkHandler.tool = this;
	}

	[RPC]
	public void removeNetworkUser(NetworkPlayer player)
	{
		int indexFromPlayer = NetworkUserList.getIndexFromPlayer(player);
		if (indexFromPlayer != -1)
		{
			if (Network.isServer)
			{
				Logger.LogConnection(NetworkUserList.users[indexFromPlayer].name + " Disconnected.");
				// Server alert disabled..
				// NetworkChat.sendAlert(string.Concat(NetworkUserList.users[indexFromPlayer].name, " disconnected."));
			}
			
			NetworkUserList.users.RemoveAt(indexFromPlayer);
			NetworkEvents.triggerOnPlayersChanged();
		}
	}

	[RPC]
	public void setReputation(NetworkPlayer player, int reputation)
	{
		int num = 0;
		while (num < NetworkUserList.users.Count)
		{
			if (NetworkUserList.users[num].player != player)
			{
				num++;
			}
			else
			{
				NetworkUserList.users[num].reputation = reputation;
				break;
			}
		}
	}

	public void Start()
	{
		NetworkEvents.onConnected += new NetworkEventDelegate(this.onConnected);
		NetworkEvents.onDisconnected += new NetworkEventDelegate(this.onDisconnected);
		NetworkEvents.onHosted += new NetworkEventDelegate(this.onConnected);
		NetworkEvents.onDisconnected += new NetworkEventDelegate(this.onDisconnected);
		NetworkEvents.onPlayerConnected += new NetworkPlayerDelegate(this.onPlayerConnected);
		NetworkEvents.onPlayerDisconnected += new NetworkPlayerDelegate(this.onPlayerDisconnected);
		NetworkEvents.onReady += new NetworkEventDelegate(this.onReady);
	}
}