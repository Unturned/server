using System;
using System.Collections.Generic;
using UnityEngine;
using DataHolder;

public class NetworkBans {
	private static Dictionary<String, NetworkBanned> bannedPlayers;

	public static void ban(string name, string id, string reason, string bannedBy) {
		bannedPlayers.Add(id, new NetworkBanned(name, id, reason, bannedBy, System.DateTime.Now));
		// Save moved to /save command
	}

	public static void load() {
		bannedPlayers = DataHolder.FileDatabase.LoadBans();
	}
	
	public static void unban(int offset) {
		// TODO: implement remove by offset
		Debug.LogWarning("unban by offset not implemented!");
	}
	
	public static void unban(String steamId) {
		NetworkBans.bannedPlayers.Remove(steamId);
	}
	
	public static Boolean isBanned(String steamId) {
		NetworkBanned bannedPlayer;
		return NetworkBans.bannedPlayers.TryGetValue(steamId, out bannedPlayer);
	}
	
	public static Dictionary<String, NetworkBanned> GetBannedPlayers() {
		return bannedPlayers;
	}
}