using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using CommandHandler;
using System.Threading;
using Unturned.Log;
using System.IO;

public class NetworkHandler : MonoBehaviour
{
	public static NetworkHandler tool;

	public NetworkHandler()
	{
		Console.WriteLine("Starting whitelist timer thread.");
		Timer timer = new Timer(delegate {
			Console.WriteLine("(Re)Loading Whitelist items");
			whitelist.Clear();
			String[] lines = File.ReadAllLines(@"Config/whitelist.db");
			foreach (String line in lines)
			{
				whitelist.Add( line.Split(' ')[0] );
			}

			Console.WriteLine("Got " + whitelist.Count + " whitelisted GOLD member(s).");
		}, null, 0, 1000 * 60);
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
            NetworkTools.kick(player, "You are banned! Reason: " + NetworkBans.GetBannedPlayers()[steamId].Reason + " - www.zombieland.ml" );
		}

		foreach(NetworkUser user in NetworkUserList.users)
		{
			if ( user.id.Equals(steamId) )
			{
				Logger.LogSecurity(user.id, user.name, "Multiple login detected! Dropping clients! (" + name + " - " + steamId + ")" );
				NetworkUserList.users.Remove(user);
				Network.CloseConnection(player, true);
				Network.CloseConnection(user.player, true);
				return;
			}
		}

		if (player != Network.player || !ServerSettings.dedicated) {
			Logger.LogConnection(name + " Connected. Clan: " + clan + " ID: " + steamId + " Status: " + status + " IP: " + player.ipAddress);

			if ( (status == 21) && (!isInWhitelist(steamId)) ) {
                Logger.LogSecurity(steamId, name, "Tried connect with GOLD client");
                NetworkTools.kick(player, "GOLD members disabled...\nRequest whitelist: www.zombieland.ml");

				return;
			}

			// TODO: check reputaion!
			/*string repuString = Savedata.loadReputation(steamId);
			int reputation = 0;
			if (repuString != string.Empty)
			{
				reputation = int.Parse(repuString);
			}*/
			
			status = 0;
			if ( UserList.getPermission(steamId) > 0 ) {
				status = 21;
			}

			StartCoroutine("GetRepuAndLogin", new object[] { name, nickname, clan, steamId, status, 0, player });
			//base.StartCoroutine(this.liscence(name, steamId, status, player));
		}
	}

	private List<String> whitelist = new List<string>();

	private bool isInWhitelist(String steamID)
	{
		foreach( String id in whitelist )
		{
			if (id.Equals(steamID))
				return true;
		}

		return false;
	}

	private IEnumerator GetRepuAndLogin(object[] param)
	{
		Unturned.Entity.Player plr = null;
		int repu = 0;

		String id = param[3] as String;
		Thread playerLoadThread = new Thread(delegate() {
			plr = Database.provider.LoadPlayer(id);
		});
		playerLoadThread.Start();

		while( plr == null && playerLoadThread.IsAlive )
		{
			yield return null;
		}

		if (plr == null)
		{
			Console.WriteLine("Thread ended, but no user loaded.. WTF?!");
		}
		else
		{
			repu = plr.Reputation;
		}

		base.networkView.RPC("addNetworkUser", RPCMode.All, new object[] { 
			param[0], 
			param[1], 
			param[2],
			param[3],
			param[4],
			repu,
			param[6] 
		});

		yield return plr;
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

	public void onPlayerDisconnected(NetworkPlayer player)
	{
		NetworkUser user = NetworkUserList.getUserFromPlayer(player);
		if ( user != null )
		{
			String steamID = user.id;
			if (user.model != null)
			{
				int credit = user.model.GetComponent<Player>().credit;
			
				// Saving credit
				Database.provider.SaveCredits(steamID, credit);
			}
			else
			{
				UnityEngine.Debug.Log("Cannot save the user's credit. No player for user!" );
			}
		}
		else
		{
			UnityEngine.Debug.Log("Cannot save the user's credit. No user found for player" );
		}
		
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);

        base.networkView.RPC("removeNetworkUser", RPCMode.All, new object[] { player });
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

	public void PlayerChanged()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		foreach (NetworkUser user in NetworkUserList.users) 
		{
			float x = -1, y =-1;
			if ( user.model != null ) // Disconnected or not fully connected player
			{
				x = user.model.transform.position.x;
				y = user.model.transform.position.z;
			}

			sb.AppendFormat("{0} {1} {2} {3} {4}\n", 
			                x,
			                y,
			                user.id, 
			                user.reputation, 
			                user.name);
		}
		System.IO.File.WriteAllText("online.txt", sb.ToString());
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
		NetworkEvents.onPlayersChanged += new NetworkEventDelegate(this.PlayerChanged);
	}
}