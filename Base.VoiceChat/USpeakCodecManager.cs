using System;
using UnityEngine;

public class USpeakCodecManager : ScriptableObject
{
	private static USpeakCodecManager instance;

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