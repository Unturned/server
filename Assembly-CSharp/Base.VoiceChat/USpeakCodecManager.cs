using System;
using UnityEngine;

public class USpeakCodecManager : ScriptableObject
{
	public string[] CodecNames = new string[0];

	public string[] FriendlyNames = new string[0];

	public static USpeakCodecManager Instance
	{
		get
		{
			return null;
		}
	}

	public USpeakCodecManager()
	{
	}
}