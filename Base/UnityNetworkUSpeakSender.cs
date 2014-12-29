using MoPhoGames.USpeak.Interface;
using System;
using UnityEngine;

public class UnityNetworkUSpeakSender : MonoBehaviour, ISpeechDataHandler
{
	public UnityNetworkUSpeakSender()
	{
	}

	[RPC]
	private void init(int data)
	{
		USpeaker.Get(this).InitializeSettings(data);
	}

	private void Start()
	{
		if (!base.networkView.isMine)
		{
			USpeaker.Get(this).SpeakerMode = SpeakerMode.Remote;
		}
	}

	public void USpeakInitializeSettings(int data)
	{
		base.networkView.RPC("init", RPCMode.AllBuffered, new object[] { data });
	}

	public void USpeakOnSerializeAudio(byte[] data)
	{
		base.networkView.RPC("vc", RPCMode.All, new object[] { data });
	}

	[RPC]
	private void vc(byte[] data)
	{
		USpeaker.Get(this).ReceiveAudio(data);
	}
}