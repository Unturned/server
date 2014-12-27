using System;
using UnityEngine;

public class Throw : Useable
{
	private float startedUse = Single.MaxValue;

	private bool done;

	public Throw()
	{
	}

	public override void equip()
	{
		Viewmodel.play("equip");
	}

	public override void startPrimary()
	{
		Equipment.busy = true;
		this.startedUse = Time.realtimeSinceStartup;
		NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/use"), Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
		Viewmodel.play("use");
	}

	public override void tick()
	{
		if (Time.realtimeSinceStartup - this.startedUse > Viewmodel.model.animation["use"].length && !this.done)
		{
			this.done = true;
			SpawnProjectiles.throwProjectile(Equipment.id, Camera.main.transform.position + (Camera.main.transform.forward * -0.5f), Camera.main.transform.forward * 1500f);
			Equipment.use();
		}
	}
}