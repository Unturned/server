using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USpeakUtilities
{
	public static string USpeakerPrefabPath;

	static USpeakUtilities()
	{
		USpeakUtilities.USpeakerPrefabPath = "USpeakerPrefab";
	}

	public USpeakUtilities()
	{
	}

	public static void Clear()
	{
		foreach (string key in USpeakOwnerInfo.USpeakPlayerMap.Keys)
		{
			USpeakOwnerInfo.USpeakPlayerMap[key].DeInit();
		}
	}

	public static void ListPlayers(IEnumerable<string> PlayerIDs)
	{
		IEnumerator<string> enumerator = PlayerIDs.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				USpeakUtilities.PlayerJoined(enumerator.Current);
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
	}

	public static void PlayerJoined(string PlayerID)
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("USpeakerPrefab"));
		USpeakOwnerInfo uSpeakOwnerInfo = gameObject.AddComponent<USpeakOwnerInfo>();

	}

	public static void PlayerLeft(string PlayerID)
	{
		USpeakOwnerInfo.FindPlayerByID(PlayerID).DeInit();
	}
}