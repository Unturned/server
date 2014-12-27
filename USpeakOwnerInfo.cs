using System;
using System.Collections.Generic;
using UnityEngine;

public class USpeakOwnerInfo : MonoBehaviour
{
	public static Dictionary<USpeakOwnerInfo, USpeaker> USpeakerMap;

	public static Dictionary<string, USpeakOwnerInfo> USpeakPlayerMap;

	private USpeaker m_speaker;

	private USpeakPlayer m_Owner;

	public USpeakPlayer Owner
	{
		get
		{
			return this.m_Owner;
		}
	}

	public USpeaker Speaker
	{
		get
		{
			if (this.m_speaker == null)
			{
				this.m_speaker = USpeaker.Get(this);
			}
			return this.m_speaker;
		}
	}

	static USpeakOwnerInfo()
	{
		USpeakOwnerInfo.USpeakerMap = new Dictionary<USpeakOwnerInfo, USpeaker>();
		USpeakOwnerInfo.USpeakPlayerMap = new Dictionary<string, USpeakOwnerInfo>();
	}

	public USpeakOwnerInfo()
	{
	}

	public void DeInit()
	{
		USpeakOwnerInfo.USpeakPlayerMap.Remove(this.m_Owner.PlayerID);
		USpeakOwnerInfo.USpeakerMap.Remove(this);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public static USpeakOwnerInfo FindPlayerByID(string PlayerID)
	{
		return USpeakOwnerInfo.USpeakPlayerMap[PlayerID];
	}

	public void Init(USpeakPlayer owner)
	{
		this.m_Owner = owner;
		USpeakOwnerInfo.USpeakPlayerMap.Add(owner.PlayerID, this);
		USpeakOwnerInfo.USpeakerMap.Add(this, base.GetComponent<USpeaker>());
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}
}