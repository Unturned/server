using Steamworks;
using System;
using UnityEngine;

public class ABridgeTooFar : MonoBehaviour
{
	public ABridgeTooFar()
	{
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			SteamUserStats.SetAchievement("abridgetoofar");
			SteamUserStats.StoreStats();
		}
	}
}