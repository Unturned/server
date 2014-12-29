using MoPhoGames.USpeak.Codec;
using System;
using UnityEngine;

public class USpeakCodecManager : ScriptableObject
{
	private static USpeakCodecManager instance;

	public ICodec[] Codecs;

	public string[] CodecNames = new string[0];

	public string[] FriendlyNames = new string[0];

	public static USpeakCodecManager Instance
	{
		get
		{
			if (USpeakCodecManager.instance == null)
			{
				USpeakCodecManager.instance = (USpeakCodecManager)Resources.Load("CodecManager");
				if (Application.isPlaying)
				{
					USpeakCodecManager.instance.Codecs = new ICodec[(int)USpeakCodecManager.instance.CodecNames.Length];
					for (int i = 0; i < (int)USpeakCodecManager.instance.Codecs.Length; i++)
					{
						USpeakCodecManager.instance.Codecs[i] = (ICodec)Activator.CreateInstance(Type.GetType(USpeakCodecManager.instance.CodecNames[i]));
					}
				}
			}
			return USpeakCodecManager.instance;
		}
	}

	public USpeakCodecManager()
	{
	}
}