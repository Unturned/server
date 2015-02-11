using System;
using UnityEngine;

public class Refillable : Useable
{
	private float startedUse = Single.MaxValue;

	private bool self;

	private bool done;

	private static RaycastHit hit;

	public Refillable()
	{
	}

	public override void dequip()
	{
		Interact.hint = string.Empty;
	}

	public override void equip()
	{
		Viewmodel.play("equip");
	}

	public override void startPrimary()
	{
		if (Player.inventory.items[Equipment.equipped.x, Equipment.equipped.y].state != "f")
		{
			Physics.Raycast(Camera.main.transform.position + (Camera.main.transform.forward * -0.5f), Camera.main.transform.forward, out Refillable.hit, 5f, -29365511);
			if (Refillable.hit.collider != null && (Refillable.hit.point.y < Ocean.level || Refillable.hit.collider.name == "well_0"))
			{
				this.self = false;
				Equipment.busy = true;
				this.startedUse = Time.realtimeSinceStartup;
				Viewmodel.play("use");
			}
		}
		else
		{
			this.self = true;
			NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/use"), Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
			Equipment.busy = true;
			this.startedUse = Time.realtimeSinceStartup;
			Viewmodel.play("drink");
		}
	}

	public override void tick()
	{
		bool item;
		if (Player.inventory.items[Equipment.equipped.x, Equipment.equipped.y].state != "f")
		{
			Interact.hint = "Empty";
		}
		else
		{
			Interact.hint = "Full";
		}
		if (!this.self)
		{
			item = (Time.realtimeSinceStartup - this.startedUse <= Viewmodel.model.animation["use"].length ? false : !this.done);
		}
		else
		{
			item = Time.realtimeSinceStartup - this.startedUse > Viewmodel.model.animation["drink"].length;
		}
		if (item)
		{
			this.done = true;
			if (this.self)
			{
				if (!Network.isServer)
				{
					base.networkView.RPC("useSelf", RPCMode.Server, new object[] { Equipment.equipped.x, Equipment.equipped.y });
				}
				else
				{
					this.useSelf(Equipment.equipped.x, Equipment.equipped.y);
				}
			}
			else if (!Network.isServer)
			{
				base.networkView.RPC("useRefill", RPCMode.Server, new object[] { Equipment.equipped.x, Equipment.equipped.y });
			}
			else
			{
				this.useRefill(Equipment.equipped.x, Equipment.equipped.y);
			}
			Equipment.dequip();
		}
	}

	[RPC]
	public void useRefill(int slot_x, int slot_y)
	{
		if (!base.GetComponent<Life>().dead)
		{
			Inventory component = base.GetComponent<Inventory>();
			if (component.items[slot_x, slot_y].id == 26000 && component.items[slot_x, slot_y].state == "e")
			{
				component.items[slot_x, slot_y].state = "f";
				component.syncItem(slot_x, slot_y);
			}
		}
	}

	[RPC]
	public void useSelf(int slot_x, int slot_y)
	{
		if (!base.GetComponent<Life>().dead)
		{
			Inventory component = base.GetComponent<Inventory>();
			if (component.items[slot_x, slot_y].id == 26000 && component.items[slot_x, slot_y].state == "f")
			{
				base.GetComponent<Life>().drink(ConsumeableStats.getWater(component.items[slot_x, slot_y].id));
				component.items[slot_x, slot_y].state = "e";
				component.syncItem(slot_x, slot_y);
			}
		}
	}
}