using Steamworks;
using System;
using UnityEngine;

public class Chess : MonoBehaviour
{
	public Chess()
	{
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			SteamUserStats.SetAchievement("chess");
			SteamUserStats.StoreStats();
		}
	}
}