using Steamworks;
using System;
using UnityEngine;

public class Steam : MonoBehaviour
{
	public static AppId_t APP_ID;

	public static bool initialized;

	private Callback<SteamShutdown_t> steamShutdown;

	static Steam()
	{
		Steam.APP_ID = new AppId_t(304930);
	}

	public Steam()
	{
	}

	private void Awake()
	{
		if (SteamAPI.RestartAppIfNecessary(Steam.APP_ID))
		{
			Debug.LogError("Failed to run game through Steam.");
			Application.Quit();
			return;
		}
		if (Steam.initialized)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			Steam.initialized = true;
			if (!SteamAPI.Init())
			{
				Debug.LogError("Failed to initialize the Steam API.");
				Application.Quit();
				return;
			}
			this.steamShutdown = Callback<SteamShutdown_t>.Create(new Callback<SteamShutdown_t>.DispatchDelegate(this.onSteamShutdown));
			base.name = "steam";
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			SteamUserStats.RequestUserStats(SteamUser.GetSteamID());
		}
	}

	private void Cleanup()
	{
		if (!Steam.initialized)
		{
			return;
		}
		Steam.initialized = false;
		SteamAPI.Shutdown();
	}

	private void OnApplicationQuit()
	{
		this.Cleanup();
	}

	private void OnDestroy()
	{
		if (base.name == "steam")
		{
			this.Cleanup();
		}
	}

	private void onSteamShutdown(SteamShutdown_t callback)
	{
		Application.Quit();
	}

	private void Update()
	{
		if (!Steam.initialized)
		{
			return;
		}
		SteamAPI.RunCallbacks();
	}
}