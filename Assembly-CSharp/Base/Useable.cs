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
        killerUser.model.networkView.RPC("killedPlayer", killer, new object[0]);

        int credit = 0;

        if (victim.reputation > 20)
            credit = -10;
        else if (victim.reputation < -70) // bandit
            credit = 15;
        else if (victim.reputation < -50)
            credit = 12;
        else if (victim.reputation < -30)
            credit = 10;
        else if (victim.reputation < -20)
            credit = 9;
        else if (victim.reputation < -10)
            credit = 7;
        else
            credit = 4;

        String icon = "Textures/Icons/gold";
        String text = "";

        if (credit < 0)
            text = String.Format("You have lost {0} credit", credit);
        else
            text = String.Format("You have earned {0} credit", credit);

        NetworkManager.error(text, icon, killer);

        GameObject playerGameObject = NetworkUserList.getModelFromPlayer(killer);
        playerGameObject.GetComponentInChildren<Player>().credit += credit;

        //NetworkManager.tool.networkView.RPC("openError", killer, new object[] { text, icon });
    }
}