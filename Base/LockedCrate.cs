using System;
using UnityEngine;

public class LockedCrate : Interactable
{
	private string state = string.Empty;

	private string player = string.Empty;

	private string friend = string.Empty;

	private ClientItem[,] items;

	public LockedCrate()
	{
	}

	public override string hint()
	{
		if (!(this.player == PlayerSettings.id) && (!(PlayerSettings.friendHash != string.Empty) || !(PlayerSettings.friendHash == this.friend)))
		{
			return string.Empty;
		}
		return "Storage";
	}

	public void setOwner(string setPlayer, string setFriend)
	{
		this.player = setPlayer;
		this.friend = setFriend;
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
		InteractionInterface.requestLockedCrate(base.transform.parent.position);
	}

	public override void trigger()
	{
		HUDInteract.crate(int.Parse(base.transform.parent.name), this.items);
		Interact.interact(base.gameObject);
	}
}