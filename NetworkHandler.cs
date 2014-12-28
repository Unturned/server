using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NetworkHandler : MonoBehaviour
{
	public static NetworkHandler tool;

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
		else
		{
			NetworkHandler.tool.mark();
		}
	}

	[RPC]
	public void joinNetworkUser(string name, string nickname, string clan, string id, int status, NetworkPlayer player)
	{
		if (player != Network.player || !ServerSettings.dedicated)
		{
			NetworkChat.sendAlert(string.Concat(name, " connected."));
			string str = Savedata.loadReputation(id);
			int num = 0;
			if (str != string.Empty)
			{
				num = int.Parse(str);
			}
			base.networkView.RPC("addNetworkUser", RPCMode.All, new object[] { name, nickname, clan, id, status, num, player });
			base.StartCoroutine(this.liscence(name, id, status, player));
		}
	}

	[DebuggerHidden]
	public IEnumerator liscence(string name, string id, int status, NetworkPlayer player)
	{
		// TODO: research what was that
		return null;
	}

	[RPC]
	public void mark()
	{
		HUDGame.lastHitmarker = Time.realtimeSinceStartup;
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
		if (!Network.isServer)
		{
			base.networkView.RPC("joinNetworkUser", RPCMode.Server, new object[] { PlayerSettings.user, PlayerSettings.nickname, PlayerSettings.friendHash, PlayerSettings.id, PlayerSettings.status, Network.player });
		}
		else
		{
			this.joinNetworkUser(PlayerSettings.user, PlayerSettings.nickname, PlayerSettings.friendHash, PlayerSettings.id, PlayerSettings.status, Network.player);
		}
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
		if (ServerSettings.dedicated) {
			NetworkTools.save();
		}
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
		base.networkView.RPC("removeNetworkUser", RPCMode.All, new object[] { player });
	}

	public void onReady()
	{
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
				NetworkChat.sendAlert(string.Concat(NetworkUserList.users[indexFromPlayer].name, " disconnected."));
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
		if (player == Network.player)
		{
			if (reputation > 70)
			{
				SteamUserStats.SetAchievement("hero");
				SteamUserStats.StoreStats();
			}
			else if (reputation < -70)
			{
				SteamUserStats.SetAchievement("bandit");
				SteamUserStats.StoreStats();
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