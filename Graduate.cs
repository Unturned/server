using Steamworks;
using System;
using UnityEngine;

public class Graduate : MonoBehaviour
{
	public Graduate()
	{
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			SteamUserStats.SetAchievement("graduate");
			SteamUserStats.StoreStats();
		}
	}
}