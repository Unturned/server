using UnityEngine;
using System.Collections;

public class DedicatedServer : MonoBehaviour
{
    // Checks the ingame world
    public static bool CheckPlayer(NetworkPlayer player, string reference) {
        try {
            if ( player == null) {
                throw new UnityException();
            }

            if ( player.ipAddress == null || player.guid == null ) {
                throw new UnityException();
            }

            //if (NetworkUserList.getUserFromPlayer(player) == null) 
            //    throw new UnityException("Player not found but conenction exists. Dropping!");

        } catch {
            Debug.LogError("Kicked no-network player! " + reference);
            NetworkTools.kick(player, "No network connection... Kicking...");
            return false;
        }

        return true;
    }

    public IEnumerator fixFrameRate()
    {
        yield return new WaitForFixedUpdate();
        Application.targetFrameRate = 10;
        Network.sendRate = 5;
    }

    public void Start()
    {
            StartCoroutine(fixFrameRate());
    
            PlayerSettings.id = "Server";
            PlayerSettings.status = 21;
            if (Epoch.serverTime == -1)
            {
                Epoch.serverTime = (int)0;//(SteamUtils.GetServerRealTime() - 1401854099);
            }

        ServerSettings.map = 1;
        ServerSettings.name = "Unturned Server";
        ServerSettings.save = true;
        ServerSettings.dedicated = true;

        NetworkTools.host(32, 25444, string.Empty);
    }
}

