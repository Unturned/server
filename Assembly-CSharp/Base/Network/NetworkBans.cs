using System;
using System.Collections.Generic;
using UnityEngine;
using DataHolder;
using Unturned;

public class NetworkBans {
	private static Dictionary<String, INetworkBanned> bannedPlayers;

	public static void ban(string name, string id, string reason, string bannedBy) {
		bannedPlayers.Add(id, new NetworkBanned(name, id, reason, bannedBy, System.DateTime.Now));
		// Save moved to /save command
	}

	public static void load() {
		bannedPlayers = Database.provider.LoadBans();
	}
	
	public static void unban(int offset) {
		// TODO: implement remove by offset
		Debug.LogWarning("unban by offset not implemented!");
	}
	
	public static void unban(String steamId) {
		NetworkBans.bannedPlayers.Remove(steamId);
	}
	
	public static Boolean isBanned(String steamId) {
        if ( bannedPlayers == null ) 
            return false; // I hope just in server starts

		INetworkBanned bannedPlayer;
		return NetworkBans.bannedPlayers.TryGetValue(steamId, out bannedPlayer);
	}
	
	public static Dictionary<String, INetworkBanned> GetBannedPlayers() {
		return bannedPlayers;
	}
}