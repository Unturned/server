using System;
using UnityEngine;

public class Fertilizer : Useable
{
	private float startedUse = Single.MaxValue;

	private GameObject target;

	private bool done;

	private static RaycastHit hit;

	public Fertilizer()
	{
	}

	public override void equip()
	{
		Viewmodel.play("equip");
	}

	public override void startPrimary()
	{
		Physics.Raycast(Camera.main.transform.position + (Camera.main.transform.forward * -0.5f), Camera.main.transform.forward, out Fertilizer.hit, 5f, -29365511);
		if (Fertilizer.hit.collider != null && Fertilizer.hit.collider.tag == "Barricade" && ItemType.getType(int.Parse(Fertilizer.hit.collider.transform.parent.name)) == 22)
		{
			this.target = Fertilizer.hit.collider.gameObject;
			NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/use"), Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
			Equipment.busy = true;
			this.startedUse = Time.realtimeSinceStartup;
			Viewmodel.play("use");
		}
	}

	public override void tick()
	{
		if (Time.realtimeSinceStartup - this.startedUse > Viewmodel.model.animation["use"].length && !this.done)
		{
			this.done = true;
			if (this.target != null)
			{
				InteractionInterface.sendFertilize(this.target.transform.parent.position);
			}
			Equipment.use();
		}
	}
}