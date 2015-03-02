//using Steamworks;
using System;
using System.Collections;
using UnityEngine;
using Unturned;

public class Database : MonoBehaviour
{
    public readonly static string code;

    private static bool welcomed;

    private static GameObject connectionGuard;

    public static IDataHolder provider;

	public const String SERVER_VERSION = "4.3";

    static Database()
    {
        Database.code = "_P1Zz4 [}";
    }

    public Database()
    {
    }

    public IEnumerator fixFrameRate()
    {
        yield return new WaitForFixedUpdate();
        Application.targetFrameRate = 10;
        Network.sendRate = 5;
    }

    public void Start()
    {
        //provider = new FileDatabase();
        //provider = new MysqlDatabase.Database();
        provider = new Unturned.RemoteDatabase();
        provider.Init();
        
        
        connectionGuard = new GameObject();
        UnityEngine.Object.DontDestroyOnLoad(Database.connectionGuard);
        connectionGuard.name = "Connection Guard";
        connectionGuard.AddComponent<ConnectionGuard>();

        if (!Database.welcomed)
        {
            StartCoroutine(fixFrameRate());
            Database.welcomed = true;
			
            PlayerSettings.id = "Server";
            PlayerSettings.status = 21;
            if (Epoch.serverTime == -1)
            {
				Epoch.serverTime = (int)(GetServerTime() - 1401854099);
            }
					
            ServerSettings.cycle = (int)(GetServerTime() - 1401854099);
            ServerSettings.offset = 0f;
            ServerSettings.time = (int)((float)ServerSettings.cycle % Sun.COURSE);
            Sun.tick = 0f;
            Sun.lastTick = Time.realtimeSinceStartup;
            Sun.tool.cycle();
        }
    }

	/// <summary>
	/// returns Unix time in seconds
	/// </summary>
	/// <returns>The server time.</returns>
	public static int GetServerTime()
	{
		return (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
	}
}