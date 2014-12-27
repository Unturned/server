using System;
using UnityEngine;

public class Medicine : Useable
{
	private float startedUse = Single.MaxValue;

	private bool done;

	public Medicine()
	{
	}

	[RPC]
	public void askHeal()
	{
		if (!base.GetComponent<Life>().dead)
		{
			base.GetComponent<Life>().heal(MedicalStats.getHealth(base.GetComponent<Clothes>().item), MedicalStats.getBleeding(base.GetComponent<Clothes>().item), MedicalStats.getBones(base.GetComponent<Clothes>().item));
			base.GetComponent<Life>().disinfect(MedicalStats.getSickness(base.GetComponent<Clothes>().item));
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
		NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/use"), Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
		Viewmodel.play("use");
	}

	public override void tick()
	{
		if (Time.realtimeSinceStartup - this.startedUse > Viewmodel.model.animation["use"].length && !this.done)
		{
			this.done = true;
			if (!Network.isServer)
			{
				base.networkView.RPC("askHeal", RPCMode.Server, new object[0]);
			}
			else
			{
				this.askHeal();
			}
			base.GetComponent<Life>().rest(MedicalStats.getStamina(Equipment.id));
			HUDGame.pain = HUDGame.pain + (float)MedicalStats.getPain(Equipment.id);
			Equipment.use();
		}
	}
}