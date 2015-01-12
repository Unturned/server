using System;
using UnityEngine;

public class Useable : MonoBehaviour
{
	public Useable()
	{
	}

	public virtual void dequip()
	{
	}

	public virtual void equip()
	{
	}

	public virtual void startPrimary()
	{
	}

	public virtual void startSecondary()
	{
	}

	public virtual void stopPrimary()
	{
	}

	public virtual void stopSecondary()
	{
	}

	public virtual void tick()
	{
	}

    public void sendKilledPlayer(NetworkUser victim, NetworkPlayer killer) {
        //NetworkManager.tool.networkView.RPC("openError", player, new object[] { text, icon });
        NetworkUser killerUser = NetworkUserList.getUserFromPlayer(killer);

        // Informing player
        NetworkManager.tool.networkView.RPC("killedPlayer", killer, new object[0]);

        int credit = 0;

        if (victim.reputation == 0)
            return; // No more calculation, no announce
        else if (victim.reputation > 20)
            credit = -5;
        else if (victim.reputation > 10)
            credit = -2;
        else if (victim.reputation > 0)
            credit = 1;
        else if (victim.reputation < -70) // bandit
            credit = 15;
        else if (victim.reputation < -50)
            credit = 10;
        else if (victim.reputation < -30)
            credit = 8;
        else if (victim.reputation < -20)
            credit = 6;
        else if (victim.reputation < -10)
            credit = 3;
        else if (victim.reputation < 0)
            credit = 2;

        String icon = "Textures/Icons/gold";
        String text = "";

        if (credit < 0)
            text = String.Format("You have lost {0} credit", credit);
        else
            text = String.Format("You have earned {0} credit", credit);

        NetworkManager.error(text, icon, killer);

        GameObject playerGameObject = NetworkUserList.getModelFromPlayer(killer);
        playerGameObject.GetComponent<Skills>().credit += credit;

        //NetworkManager.tool.networkView.RPC("openError", killer, new object[] { text, icon });
    }
}