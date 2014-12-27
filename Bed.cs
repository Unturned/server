using System;
using UnityEngine;

public class Bed : Interactable
{
	private string state = string.Empty;

	public Bed()
	{
	}

	public override string hint()
	{
		if (this.state == string.Empty)
		{
			return "Claim";
		}
		if (this.state == PlayerSettings.id)
		{
			return "Unclaim";
		}
		return string.Empty;
	}

	public void setState(string setState)
	{
		if (setState != this.state)
		{
			this.state = setState;
		}
	}

	public void Start()
	{
		InteractionInterface.requestBed(base.transform.parent.position);
	}

	public override void trigger()
	{
		InteractionInterface.sendBed(base.transform.parent.position);
	}
}