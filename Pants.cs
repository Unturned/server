using System;
using UnityEngine;

public class Pants : Useable
{
	private float startedUse = Single.MaxValue;

	private bool wear;

	private bool done;

	public Pants()
	{
	}

	public override void dequip()
	{
		if (this.wear)
		{
			Player.clothes.changePants(Equipment.id);
		}
	}

	public override void equip()
	{
		Viewmodel.play("equip");
	}

	public override void startPrimary()
	{
		Equipment.busy = true;
		this.startedUse = Time.realtimeSinceStartup;
		Viewmodel.play("use");
	}

	public override void tick()
	{
		if (Time.realtimeSinceStartup - this.startedUse > Viewmodel.model.animation["use"].length && !this.done)
		{
			this.done = true;
			this.wear = true;
			Equipment.use();
		}
	}
}