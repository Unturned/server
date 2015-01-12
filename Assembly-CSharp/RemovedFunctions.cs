using UnityEngine;
using System.Collections;

public class RemovedFunctions : MonoBehaviour
{
    // Hooking up to Network object
    public static void hook(GameObject networkObject) {
        networkObject.AddComponent<RemovedFunctions>();
    }

    void Awake() {
        Debug.Log("RemovedFunctions.cs Waking up!");
    }
        
    [RPC]
    public void mark()
    {
        // Completly client function to mark at gui
        //Debug.LogWarning("mark received, but removed from server!");
    }
    
    [RPC]
    public void openError(string text, string icon) 
    {
        // HUDGame.openerror(); // Just for clients..
        //Debug.LogWarning("openError received, but removed from server!");
    }
}

