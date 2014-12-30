using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LocalUSpeakSender : MonoBehaviour
{
	private bool recording;

	private float jitter;

	private float ping;

	private float packetLoss;

	public LocalUSpeakSender()
	{
	}

	[DebuggerHidden]
	private IEnumerator fakeSendPacket(byte[] data)
	{
		//LocalUSpeakSender.<fakeSendPacket>c__Iterator1 variable = null;
		return null;
	}

	private void OnGUI()
	{
		GUILayout.Label(string.Concat("Ping - ", Mathf.Round(this.ping), "ms"), new GUILayoutOption[0]);
		this.ping = GUILayout.HorizontalSlider(this.ping, 0f, 100f, new GUILayoutOption[] { GUILayout.Width(200f) });
		GUILayout.Label(string.Concat("Ping Jitter - ", Mathf.Round(this.jitter), "ms"), new GUILayoutOption[0]);
		this.jitter = GUILayout.HorizontalSlider(this.jitter, 0f, 100f, new GUILayoutOption[] { GUILayout.Width(200f) });
		GUILayout.Label(string.Concat("Packet Loss - ", Mathf.Round(this.packetLoss), "%"), new GUILayoutOption[0]);
		this.packetLoss = GUILayout.HorizontalSlider(this.packetLoss, 0f, 10f, new GUILayoutOption[] { GUILayout.Width(200f) });
		GUILayout.Label(string.Concat("Using Microphone: ", Microphone.devices[0]), new GUILayoutOption[0]);
		GUILayout.Space(10f);
		if (this.recording)
		{
			if (GUILayout.Button("Stop Recording", new GUILayoutOption[0]))
			{
				this.recording = false;
			}
		}
		else if (GUILayout.Button("Start Recording", new GUILayoutOption[0]))
		{
			this.recording = true;
		}
	}

	public void OnInspectorGUI()
	{
	}

	public bool ShouldSend()
	{
		return this.recording;
	}

	public void USpeakInitializeSettings(int data)
	{
		USpeaker.Get(this).InitializeSettings(data);
	}

	public void USpeakOnSerializeAudio(byte[] data)
	{
		base.StartCoroutine(this.fakeSendPacket(data));
	}
}