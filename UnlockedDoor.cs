using System;
using UnityEngine;

public class UnlockedDoor : Interactable
{
	private bool state;

	public UnlockedDoor()
	{
	}

	public override string hint()
	{
		if (this.state)
		{
			return "Close";
		}
		return "Open";
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
		InteractionInterface.requestUnlockedDoor(base.transform.parent.position);
	}

	public override void trigger()
	{
		InteractionInterface.sendUnlockedDoor(base.transform.parent.position);
	}
}