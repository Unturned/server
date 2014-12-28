using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBans
{
	public static List<NetworkBanned> bans;
	public static Dictionary<String, String> bannedPlayers;

	static NetworkBans() {
		NetworkBans.bans = new List<NetworkBanned>();
		bannedPlayers = new Dictionary<string, string>();
	}

	public NetworkBans() {	}

	public static void ban(string name, string id) {
		NetworkBans.bans.Add(new NetworkBanned(name, id));
		bannedPlayers.Add(id, name);
		NetworkBans.save();
	}

	public static void load()
	{
		NetworkBans.bans.Clear();
		string str = PlayerPrefs.GetString("bans");
		if (str != string.Empty)
		{
			string[] strArrays = Packer.unpack(str, ';');
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				string[] strArrays1 = Packer.unpack(strArrays[i], ':');
				NetworkBans.bans.Add(new NetworkBanned(strArrays1[0], strArrays1[1]));
				bannedPlayers.Add(strArrays1[1], strArrays1[0]); // ID, Name
			}
			
			Logger.LogBan("Loaded " + strArrays.Length + " ban entry"); 
		}
	}

	public static void save()
	{
		string banString = string.Empty;
		for (int i = 0; i < NetworkBans.bans.Count; i++)
		{
			banString = string.Concat(banString, NetworkBans.bans[i].name, ":");
			banString = string.Concat(banString, NetworkBans.bans[i].id, ":;");
		}
		PlayerPrefs.SetString("bans", banString);
	}

	public static void unban(int index) {
		NetworkBans.bans.RemoveAt(index);
		NetworkBans.save();
	}
	
	public static Boolean isBanned(String steamId) {
		String playerName = "";
		return NetworkBans.bannedPlayers.TryGetValue(steamId, out playerName);
	}
}