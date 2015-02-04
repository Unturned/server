using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unturned;

public class NetworkBans {
	private static Dictionary<String, IBanEntry> bannedPlayers;

	public static void ban(string name, string id, string reason, string bannedBy) {
        BanEntry entry = new BanEntry(name, id, reason, bannedBy, System.DateTime.Now);
        bannedPlayers.Add(id, entry);
        Database.provider.AddBan(entry);

        // Reload
        NetworkBans.Load();
		// Save moved to /save command
	}

    public static void Load() 
    {
		bannedPlayers = Database.provider.LoadBans();
#if DEBUG
		Console.WriteLine("Loaded bans with " + bannedPlayers.Count + " count");
#endif
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

		IBanEntry bannedPlayer;
		return NetworkBans.bannedPlayers.TryGetValue(steamId, out bannedPlayer);
	}
	
	public static Dictionary<String, IBanEntry> GetBannedPlayers() {
		return bannedPlayers;
	}
}