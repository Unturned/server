using System;
using UnityEngine;

public class Crate : Interactable
{
	private string state = string.Empty;

	private ClientItem[,] items;

	public Crate()
	{
	}

	public override string hint()
	{
		return "Storage";
	}

	public void setState(string setState)
	{
		if (setState != this.state)
		{
			this.state = setState;
		}
		this.items = InteractionInterface.getCrateItems(int.Parse(base.transform.parent.name), Sneaky.expose(this.state));
		if (Interact.edit == base.gameObject)
		{
			HUDInteract.crate(int.Parse(base.transform.parent.name), this.items);
		}
	}

	public void Start()
	{
		InteractionInterface.requestCrate(base.transform.parent.position);
	}

	public override void trigger()
	{
		HUDInteract.crate(int.Parse(base.transform.parent.name), this.items);
		Interact.interact(base.gameObject);
	}
}