using System;
using UnityEngine;

public class Consumeable : Useable
{
	private float startedUse = Single.MaxValue;

	private bool yum;

	private bool done;

	public Consumeable()
	{
	}

	[RPC]
	public void askConsume()
	{
		if (!base.GetComponent<Life>().dead)
		{
			base.GetComponent<Life>().eat(ConsumeableStats.getFood(base.GetComponent<Clothes>().item));
			base.GetComponent<Life>().drink(ConsumeableStats.getWater(base.GetComponent<Clothes>().item));
			base.GetComponent<Life>().infect(ConsumeableStats.getSickness(base.GetComponent<Clothes>().item));
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
		if (!this.yum && Time.realtimeSinceStartup - this.startedUse > Viewmodel.model.animation["use"].length * 0.1f)
		{
			this.yum = true;
			NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/use"), Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
		}
		if (Time.realtimeSinceStartup - this.startedUse > Viewmodel.model.animation["use"].length && !this.done)
		{
			this.done = true;
			if (!Network.isServer)
			{
				base.networkView.RPC("askConsume", RPCMode.Server, new object[0]);
			}
			else
			{
				this.askConsume();
			}
			base.GetComponent<Life>().rest(ConsumeableStats.getStamina(Equipment.id));
			Equipment.use();
		}
	}
}