using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTools
{
	public static Server[] servers;

	public static Server[] cleared;

	public static int currentPort;

	static NetworkTools() {
		NetworkTools.servers = new Server[0];
		NetworkTools.cleared = NetworkTools.servers;
	}

	public NetworkTools() {
	}

	public static void ban(NetworkPlayer player, string name, string id, string reason) {
		NetworkBans.ban(name, id);
		NetworkTools.kick(player, reason);
	}

	public static void connectGUID(string guid, string password) {
		NetworkEvents.triggerOnConnecting();
		if (Network.Connect(guid, password).ToString().ToLower() != "noerror") {
			NetworkTools.disconnect();
			NetworkEvents.triggerOnFailed(0);
		}
		
		MenuRegister.buttonCancel.visible = true;
	}

	public static void connectIP(string ip, int port, string password) {
		NetworkEvents.triggerOnConnecting();
		if (Network.Connect(ip, port, password).ToString().ToLower() != "noerror") {
			NetworkTools.disconnect();
			NetworkEvents.triggerOnFailed(0);
		}
		MenuRegister.buttonCancel.visible = true;
	}

	public static void disconnect() {
		if (Network.isServer && ServerSettings.open) {
			NetworkTools.save();
		}
		
		NetworkEvents.triggerOnDisconnecting();
		Network.Disconnect();
		MenuRegister.buttonCancel.visible = false;
	}

	public static void host(int players, int port, string password) {
		NetworkTools.currentPort = port;
		NetworkEvents.triggerOnHosting();
		Network.InitializeSecurity();
		Network.incomingPassword = password;
		
		string lower = Network.InitializeServer((!ServerSettings.dedicated ? players - 1 : players), port, false).ToString().ToLower();
		
		ServerSettings.passworded = password != string.Empty;
		NetworkBans.load();
		if (lower == "noerror")
		{
			if (!ServerSettings.open)
				System.Console.WriteLine("Server opened with no error!");
		} else {
			NetworkTools.disconnect();
			NetworkEvents.triggerOnFailed(0);
		}
		
		MenuRegister.buttonCancel.visible = true;
	}

	public static void kick(NetworkPlayer player, string reason) {
		// TODO server notify about kick
		//NetworkChat.sendAlert(reason);
		NetworkChat.sendNotification(player, reason);
		
		if (player != Network.player) {
			Network.CloseConnection(player, true);
		} else {
			NetworkTools.disconnect();
		}
	}

	public static void refresh() {
		if (!NetworkMasterServer.STEAM_MASTER_SERVER) {
			MasterServer.RequestHostList(string.Concat("unturned2_v", Texts.VERSION_ID));
		}
		MenuServers.search();
	}

	public static void save() {
		if (!ServerSettings.save) {
			for (int i = 0; i < NetworkUserList.users.Count; i++) {
				if (NetworkUserList.users[i].model != null) {
					NetworkUserList.users[i].model.GetComponent<Player>().save();
				}
			}
		}
		
		for (int j = 0; j < NetworkRegions.REGION_X; j++) {
			for (int k = 0; k < NetworkRegions.REGION_Y; k++) {
				SpawnBarricades.save(j, k);
			}
		}
		
		SpawnStructures.save();
		SpawnVehicles.save();
		PlayerPrefs.Save();
	}

	public static void search(string name, int mode, int host, int save, int players, bool ping, int type, int map, bool nopass) {
		List<Server> servers = new List<Server>();
		for (int i = 0; i < (int)NetworkTools.servers.Length; i++) {
			Server server = NetworkTools.servers[i];
			bool flag = true;
			if (nopass == server.passworded) {
				flag = false;
			}
			else if (Texts.VERSION_ID != server.version) {
				flag = false;
			}
			else if (map != 0 && server.map != map) {
				flag = false;
			}
			else if (ping && server.ping > 100)
			{
				flag = false;
			}
			else if (mode != 0 && (mode == 1 && !server.pvp || mode == 2 && server.pvp))
			{
				flag = false;
			}
			else if (save != 0 && (save == 1 && server.save || save == 2 && !server.save))
			{
				flag = false;
			}
			else if (host != 0 && (host == 1 && !server.dedicated || host == 2 && server.dedicated))
			{
				flag = false;
			}
			else if (players == 0 && server.players >= server.max)
			{
				flag = false;
			}
			else if (players == 1 && server.players != 0)
			{
				flag = false;
			}
			else if (type == 0 && server.mode != 0)
			{
				flag = false;
			}
			else if (type == 1 && server.mode != 1)
			{
				flag = false;
			}
			else if (type == 2 && server.mode != 2)
			{
				flag = false;
			}
			else if (type == 3 && server.mode != 3)
			{
				flag = false;
			}
			else if (name != string.Empty && !server.name.ToLower().Contains(name.ToLower()))
			{
				flag = false;
			}
			if (flag)
			{
				server.reference = i;
				servers.Add(server);
			}
		}
		
		NetworkTools.cleared = servers.ToArray();
	}

	public static void updateServers() {
		if (!NetworkMasterServer.STEAM_MASTER_SERVER) {
			NetworkTools.servers = new Server[(int)MasterServer.PollHostList().Length];
			for (int i = 0; i < (int)NetworkTools.servers.Length; i++)
			{
				NetworkTools.servers[i] = new Server(MasterServer.PollHostList()[i]);
				NetworkTools.servers[i].ping = 2147483647;
			}
		}
		
		bool flag = false;
		while (!flag) {
			bool flag1 = false;
			for (int j = 0; j < (int)NetworkTools.servers.Length - 1; j++) {
				Server server = NetworkTools.servers[j];
				Server server1 = NetworkTools.servers[j + 1];
				if (server.players < server1.players) {
					NetworkTools.servers[j + 1] = server;
					NetworkTools.servers[j] = server1;
					flag1 = true;
				}
			}
			
			if (flag1) {
				continue;
			}
			flag = true;
		}
		
		MenuServers.search();
		MenuServers.load();
	}
}