using System;
using UnityEngine;

public class UnityNetworkUSpeakSender : MonoBehaviour
{
    [RPC]
    private void init(int data) 
    {
        // TODO: notify client that Voice is disabled
        //NetworkUserList.getUserFromPlayer(player);
    }
}