using Steamworks;
using System;
using UnityEngine;

public class ADayInTheSun : MonoBehaviour
{
	public ADayInTheSun()
	{
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			SteamUserStats.SetAchievement("adayinthesun");
			SteamUserStats.StoreStats();
		}
	}
}