using Steamworks;
using System;
using UnityEngine;

public class PizzaDatabase : MonoBehaviour
{
	public readonly static string code;

	private static bool welcomed;

	static PizzaDatabase()
	{
		PizzaDatabase.code = "_P1Zz4 [}";
	}

	public PizzaDatabase()
	{
	}

	public void Start()
	{
		if (!PizzaDatabase.welcomed)
		{
			PizzaDatabase.welcomed = true;
			
			if (Application.internetReachability == NetworkReachability.NotReachable) {
				MenuRegister.openInfo(Texts.ERROR_INTERNET, "Textures/Icons/error");
			} else if (!Steam.initialized || !SteamAPI.IsSteamRunning()) {
				MenuRegister.openInfo(Texts.ERROR_STEAMWORKS, "Textures/Icons/error");
			} else if (SteamApps.BIsVACBanned()) {
				MenuRegister.openInfo(Texts.ERROR_BANNED, "Textures/Icons/error");
			} else {
				PlayerSettings.user = SteamFriends.GetPersonaName();
				
				if (!Application.isEditor) {
					PlayerSettings.id = SteamUser.GetSteamID().ToString();
				} else {
					PlayerSettings.id = "Editor";
				}
				
				//PlayerSettings.status = (!SteamApps.BIsSubscribedApp(new AppId_t(306460)) ? 0 : 21);
				PlayerSettings.status = 21;
				if (PlayerSettings.id == string.Empty) {
					MenuRegister.openInfo(Texts.ERROR_INTERNET, "Textures/Icons/error");
				} else {
					if (Epoch.serverTime == -1) {
						Epoch.serverTime = (int)(SteamUtils.GetServerRealTime() - 1401854099);
					}
					
					ServerSettings.cycle = (int)(SteamUtils.GetServerRealTime() - 1401854099);
					ServerSettings.offset = 0f;
					ServerSettings.time = (int)((float)ServerSettings.cycle % Sun.COURSE);
					Sun.tick = 0f;
					Sun.lastTick = Time.realtimeSinceStartup;
					Sun.tool.cycle();
					
					if (PlayerPrefs.GetInt("played2") != 0) {
						MenuRegister.openError("Welcome back to Unturned!", "Textures/Icons/go");
					} else {
						PlayerPrefs.SetInt("played2", 1);
						MenuRegister.openError("Welcome to Unturned!", "Textures/Icons/go");
					}
					
					MenuRegister.close();
				}
			}
		}
	}
}