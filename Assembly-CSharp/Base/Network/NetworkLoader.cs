using System;
using UnityEngine;

public class NetworkLoader : MonoBehaviour
{
	public static AsyncOperation load;

	public static int epoch;

	public NetworkLoader()
	{
	}

	[RPC]
	public void askTick(NetworkPlayer player)
	{
		base.networkView.RPC("tellTick", player, new object[] { Sun.tick });
	}

	public void onDisconnected()
	{
		AudioListener.volume = 0f;
		NetworkLoader.load = Application.LoadLevelAsync("menu");
	}

	public void onHosted()
	{
		base.networkView.RPC("tellState", RPCMode.OthersBuffered, new object[] { ServerSettings.map, ServerSettings.pvp, ServerSettings.save, ServerSettings.version, ServerSettings.time, Epoch.getSeconds(), ServerSettings.mode });
		AudioListener.volume = 0f;
		UnityEngine.Object.Destroy(GameObject.Find("menu"));
		NetworkLoader.load = Application.LoadLevelAsync(Maps.getFile(ServerSettings.map));
	}

	public void OnLevelWasLoaded()
	{
		if (Application.loadedLevel != 0)
		{
			Network.isMessageQueueRunning = true;
			NetworkEvents.triggerOnReady();
		}
	}

	public void onReady()
	{
		if (!Network.isServer)
		{
			base.networkView.RPC("askTick", RPCMode.Server, new object[] { Network.player });
		}
	}

	public void Start()
	{
		NetworkEvents.onHosted += new NetworkEventDelegate(this.onHosted);
		NetworkEvents.onDisconnected += new NetworkEventDelegate(this.onDisconnected);
		NetworkEvents.onReady += new NetworkEventDelegate(this.onReady);
	}

	[RPC]
	public void tellState(int map, bool pvp, bool save, string version, int time, int seconds, int mode)
	{
		Network.isMessageQueueRunning = false;
		if (ServerSettings.version != version || mode == 3 && PlayerSettings.status != 21)
		{
			NetworkChat.notification = "Sorry: Version Mismatch";
			NetworkTools.disconnect();
		}
		else
		{
			ServerSettings.map = map;
			ServerSettings.pvp = pvp;
			ServerSettings.save = save;
			ServerSettings.time = time;
			NetworkLoader.epoch = seconds;
			ServerSettings.mode = mode;
			AudioListener.volume = 0f;
			UnityEngine.Object.Destroy(GameObject.Find("menu"));
			NetworkLoader.load = Application.LoadLevelAsync(Maps.getFile(map));
		}
	}

	[RPC]
	public void tellTick(float tick)
	{
		Sun.tick = tick;
		Sun.lastTick = Time.realtimeSinceStartup;
		Sun.tool.cycle();
	}
}