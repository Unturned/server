using System;
using UnityEngine;

public class UnityNetworkExample : MonoBehaviour
{
	public GameObject PlayerObject;

	private string remoteIP = "127.0.0.1";

	private int remotePort = 25000;

	private int listenPort = 25000;

	public UnityNetworkExample()
	{
	}

	private void OnConnectedToServer()
	{
		Network.Instantiate(this.PlayerObject, UnityEngine.Random.insideUnitSphere * 5f, Quaternion.identity, 0);
	}

	private void OnGUI()
	{
		if (Network.peerType != NetworkPeerType.Disconnected)
		{
			if (GUI.Button(new Rect(10f, 10f, 100f, 50f), "Disconnect"))
			{
				Network.Disconnect(200);
			}
			GUILayout.BeginArea(new Rect(10f, 60f, 200f, 500f));
			int num = 0;
			string[] strArrays = Microphone.devices;
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				string str = strArrays[i];
				if (GUILayout.Button(str, new GUILayoutOption[] { GUILayout.Width(200f) }))
				{
					USpeaker.SetInputDevice(num);
				}
				num++;
			}
			GUILayout.EndArea();
		}
		else
		{
			if (GUI.Button(new Rect(10f, 10f, 100f, 30f), "Connect"))
			{
				Network.Connect(this.remoteIP, this.remotePort);
			}
			if (GUI.Button(new Rect(10f, 50f, 100f, 30f), "Start Server"))
			{
				Network.InitializeServer(32, this.listenPort, true);
			}
			this.remoteIP = GUI.TextField(new Rect(120f, 10f, 100f, 20f), this.remoteIP);
			this.remotePort = int.Parse(GUI.TextField(new Rect(230f, 10f, 40f, 20f), this.remotePort.ToString()));
		}
	}

	private void OnServerInitialized()
	{
		Network.Instantiate(this.PlayerObject, UnityEngine.Random.insideUnitSphere * 5f, Quaternion.identity, 0);
	}
}