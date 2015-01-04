using System;
using UnityEngine;

public class Note : Interactable
{
	private string state = string.Empty;

	public Note()
	{
	}

	public override string hint()
	{
		return "Write";
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
		InteractionInterface.requestNote(base.transform.parent.position);
	}

	public override void trigger()
	{
		//HUDInteract.note(Sneaky.expose(this.state));
		Interact.interact(base.gameObject);
	}

	public void write(string note)
	{
		InteractionInterface.sendNote(base.transform.parent.position, note);
	}
}