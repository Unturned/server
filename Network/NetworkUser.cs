using System;
using UnityEngine;

public class NetworkUser
{
	public string name;

	public string nickname;

	public string friend;

	public string id;

	public int status;

	public int reputation;

	public NetworkPlayer player;

	public GameObject model;

	public float spawned;

	public bool muted;

	public NetworkUser(string setName, string setNickname, string setFriend, string setID, int setStatus, int setReputation, NetworkPlayer setPlayer)
	{
		this.name = setName;
		this.nickname = setNickname;
		this.friend = setFriend;
		this.id = setID;
		this.status = setStatus;
		this.reputation = setReputation;
		this.player = setPlayer;
		this.spawned = Single.MinValue;
		if (SpawnPlayers.model != null && SpawnPlayers.model.transform.FindChild("models") != null)
		{
			int num = 0;
			while (num < SpawnPlayers.model.transform.FindChild("models").childCount)
			{
				GameObject child = SpawnPlayers.model.transform.FindChild("models").GetChild(num).gameObject;
				if (child.networkView.owner != this.player)
				{
					num++;
				}
				else
				{
					child.name = this.name;
					child.GetComponent<Player>().owner = this;
					this.model = child;
					break;
				}
			}
		}
	}

	public void toggleMute()
	{
		if (this.model != null)
		{
			this.model.audio.mute = !this.model.audio.mute;
			this.muted = this.model.audio.mute;
		}
	}
}