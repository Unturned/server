using System;
using UnityEngine;

public class Door : Interactable
{
	private bool state;

	private string player = string.Empty;

	private string friend = string.Empty;

	public Door()
	{
	}

	public override string hint()
	{
		if (!(this.player == PlayerSettings.id) && (!(PlayerSettings.friendHash != string.Empty) || !(PlayerSettings.friendHash == this.friend)))
		{
			return string.Empty;
		}
		if (this.state)
		{
			return "Close";
		}
		return "Open";
	}

	public void setOwner(string setPlayer, string setFriend)
	{
		this.player = setPlayer;
		this.friend = setFriend;
	}

	public void setState(bool setState)
	{
		if (setState != this.state)
		{
			this.state = setState;
			if (!this.state)
			{
				base.animation.Play("close");
			}
			else
			{
				base.animation.Play("open");
			}
		}
	}

	public void Start()
	{
		InteractionInterface.requestDoor(base.transform.parent.position);
	}

	public override void trigger()
	{
		InteractionInterface.sendDoor(base.transform.parent.position);
	}
}