//using Steamworks;
using System;
using System.Collections;
using UnityEngine;

public class Database : MonoBehaviour
{
	public readonly static string code;

	private static bool welcomed;

	static Database()
	{
		Database.code = "_P1Zz4 [}";
	}

	public Database()
	{
	}

	public IEnumerator fixFrameRate() {
		yield return new WaitForFixedUpdate();
		Application.targetFrameRate = 10;
		Network.sendRate = 5;
	}

	public void Start() {
        StartCoroutine(fixFrameRate());

		if (!Database.welcomed)
		{
			Database.welcomed = true;
			
				PlayerSettings.id = "Server";
				PlayerSettings.status = 21;
				
                if (PlayerSettings.id == string.Empty) {
					MenuRegister.openInfo(Texts.ERROR_INTERNET, "Textures/Icons/error");
				} else {
					if (Epoch.serverTime == -1) {
                        Epoch.serverTime = (int)0;//(SteamUtils.GetServerRealTime() - 1401854099);
					}
					
                    ServerSettings.cycle = (int)0;//(SteamUtils.GetServerRealTime() - 1401854099);
					ServerSettings.offset = 0f;
					ServerSettings.time = (int)((float)ServerSettings.cycle % Sun.COURSE);
					Sun.tick = 0f;
					Sun.lastTick = Time.realtimeSinceStartup;
					Sun.tool.cycle();
					
					MenuRegister.close();
				}
			
		}
	}
}