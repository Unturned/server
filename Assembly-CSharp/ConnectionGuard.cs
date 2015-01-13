using System;
using UnityEngine;

class ConnectionGuard : MonoBehaviour
{
    void Awake() {
        NetworkEvents.onHosted += new NetworkEventDelegate(this.onServerStarted);
    }

    void onServerStarted() {
        GameObject go = GameObject.Find("network") as GameObject;
        InvokeRepeating("CheckConnections", 0, 10);
    }
    
    void CheckConnections() {
        /*
        if (NetworkUserList.users.Count != Network.connections.Length)
        {
            foreach (NetworkPlayer plr in Network.connections ) 
            {
                NetworkUser user = NetworkUserList.getUserFromPlayer(plr);
                
                if ( user == null )
                {
                    try {
                        Debug.Log("Cleared player: " + plr.ipAddress );
                        Network.DestroyPlayerObjects(plr);
                    } finally {
                        Network.CloseConnection(plr, true);
                    }
                }
            }
        }
        */
        
        UnityEngine.Object[] players = UnityEngine.Object.FindObjectsOfType(typeof(Player));
        if ( players.Length > Network.connections.Length ) 
        {
            Debug.LogError("There is more players than connections!!!");
        }
    }
}
