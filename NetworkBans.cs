using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBans
{
	public static List<NetworkBanned> bans;

	static NetworkBans()
	{
		NetworkBans.bans = new List<NetworkBanned>();
	}

	public NetworkBans()
	{
	}

	public static void ban(string name, string id)
	{
		NetworkBans.bans.Add(new NetworkBanned(name, id));
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
			}
		}
	}

	public static void save()
	{
		string empty = string.Empty;
		for (int i = 0; i < NetworkBans.bans.Count; i++)
		{
			empty = string.Concat(empty, NetworkBans.bans[i].name, ":");
			empty = string.Concat(empty, NetworkBans.bans[i].id, ":;");
		}
		PlayerPrefs.SetString("bans", empty);
	}

	public static void unban(int index)
	{
		NetworkBans.bans.RemoveAt(index);
		NetworkBans.save();
	}
}