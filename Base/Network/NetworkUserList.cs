using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkUserList
{
	public static List<NetworkUser> users;

	static NetworkUserList()
	{
		NetworkUserList.users = new List<NetworkUser>();
	}

	public NetworkUserList()
	{
	}

	public static int getIndexFromID(string id)
	{
		for (int i = 0; i < NetworkUserList.users.Count; i++)
		{
			if (NetworkUserList.users[i].id == id)
			{
				return i;
			}
		}
		return -1;
	}

	public static int getIndexFromPlayer(NetworkPlayer player)
	{
		for (int i = 0; i < NetworkUserList.users.Count; i++)
		{
			if (NetworkUserList.users[i].player == player)
			{
				return i;
			}
		}
		return -1;
	}

	public static GameObject getModelFromPlayer(NetworkPlayer player)
	{
		for (int i = 0; i < NetworkUserList.users.Count; i++)
		{
			if (NetworkUserList.users[i].player == player)
			{
				return NetworkUserList.users[i].model;
			}
		}
		return null;
	}

	public static NetworkUser getUserFromID(string id)
	{
		for (int i = 0; i < NetworkUserList.users.Count; i++)
		{
			if (NetworkUserList.users[i].id == id)
			{
				return NetworkUserList.users[i];
			}
		}
		return null;
	}

	public static NetworkUser getUserFromPlayer(NetworkPlayer player)
	{
		for (int i = 0; i < NetworkUserList.users.Count; i++)
		{
			if (NetworkUserList.users[i].player == player)
			{
				return NetworkUserList.users[i];
			}
		}
		return null;
	}
}