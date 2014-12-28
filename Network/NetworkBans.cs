using System;
using System.Collections.Generic;
using UnityEngine;
using Database;

public class NetworkBans {
	public static List<NetworkBanned> bans;
	private static Dictionary<String, String> bannedPlayers;

	static NetworkBans() {
		bannedPlayers = new Dictionary<string, NetworkBanned>();
	}

	public NetworkBans() {
	}

	public static void ban(string name, string id, string reason, string bannedBy) {
		bannedPlayers.Add(id, new NetworkBanned(name, id, reason, bannedBy, System.DateTime.Now));
		// Save moved to /save command
	}

	public static void load() {
		bannedPlayers = Database.FileDatabase.LoadBans();
	}

	public static void unban(String steamId) {
		NetworkBans.bannedPlayers.Remove(steamId);
	}
	
	public static Boolean isBanned(String steamId) {
		String playerName = "";
		return NetworkBans.bannedPlayers.TryGetValue(steamId, out playerName);
	}
}