using System;
using UnityEngine;

public class Voice : MonoBehaviour
{
	public static bool sending;

	public Voice()
	{
	}

	public void OnInspectorGUI()
	{
	}

	public bool ShouldSend()
	{
		return Voice.sending;
	}

	public void Start()
	{
		if (!GameSettings.voice && base.networkView.isMine)
		{
			base.GetComponent<USpeaker>().enabled = false;
		}
	}

	public void Update()
	{
		if (!GameSettings.voice)
		{
			Voice.sending = false;
		}
		else if (Input.GetKeyDown(InputSettings.voiceKey) && !Voice.sending)
		{
			Voice.sending = true;
		}
		else if (!Input.GetKey(InputSettings.voiceKey) && Voice.sending)
		{
			Voice.sending = false;
		}
	}
}