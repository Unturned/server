using System;
using UnityEngine;

public class Gas : Useable
{
	private float startedUse = Single.MaxValue;

	private GameObject target;

	private bool done;

	private static RaycastHit hit;

	public Gas()
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
		Physics.Raycast(Camera.main.transform.position + (Camera.main.transform.forward * -0.5f), Camera.main.transform.forward, out Gas.hit, 5f, -29365511);
		if (Gas.hit.collider != null && (Player.inventory.items[Equipment.equipped.x, Equipment.equipped.y].state == "f" && Gas.hit.collider.tag == "Vehicle" && Gas.hit.collider.GetComponent<Vehicle>().fuel < Gas.hit.collider.GetComponent<Vehicle>().maxFuel || Player.inventory.items[Equipment.equipped.x, Equipment.equipped.y].state == "e" && Gas.hit.collider.tag == "Vehicle" && Gas.hit.collider.GetComponent<Vehicle>().fuel >= 50 || Player.inventory.items[Equipment.equipped.x, Equipment.equipped.y].state == "e" && (Gas.hit.collider.name == "gasTank" || Gas.hit.collider.name == "gasPump")))
		{
			this.target = Gas.hit.collider.gameObject;
			NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/use"), Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
			Equipment.busy = true;
			this.startedUse = Time.realtimeSinceStartup;
			Viewmodel.play("use");
		}
	}

	public override void tick()
	{
		if (Player.inventory.items[Equipment.equipped.x, Equipment.equipped.y].state != "f")
		{
			Interact.hint = "Empty";
		}
		else
		{
			Interact.hint = "Full";
		}
		if (Time.realtimeSinceStartup - this.startedUse > Viewmodel.model.animation["use"].length && !this.done)
		{
			this.done = true;
			if (this.target != null)
			{
				if (this.target.tag == "Vehicle")
				{
					if (!Network.isServer)
					{
						base.networkView.RPC("useVehicle", RPCMode.Server, new object[] { this.target.networkView.viewID, Equipment.equipped.x, Equipment.equipped.y });
					}
					else
					{
						this.useVehicle(this.target.networkView.viewID, Equipment.equipped.x, Equipment.equipped.y);
					}
				}
				else if (this.target.name == "gasTank" || this.target.name == "gasPump")
				{
					if (!Network.isServer)
					{
						base.networkView.RPC("useTank", RPCMode.Server, new object[] { Equipment.equipped.x, Equipment.equipped.y });
					}
					else
					{
						this.useTank(Equipment.equipped.x, Equipment.equipped.y);
					}
				}
			}
			Equipment.dequip();
		}
	}

	[RPC]
	public void useTank(int slot_x, int slot_y)
	{
		if (!base.GetComponent<Life>().dead)
		{
			Inventory component = base.GetComponent<Inventory>();
			if (component.items[slot_x, slot_y].id == 20000 && component.items[slot_x, slot_y].state == "e")
			{
				component.items[slot_x, slot_y].state = "f";
				component.syncItem(slot_x, slot_y);
			}
		}
	}

	[RPC]
	public void useVehicle(NetworkViewID id, int slot_x, int slot_y)
	{
		if (!base.GetComponent<Life>().dead)
		{
			Inventory component = base.GetComponent<Inventory>();
			if (component.items[slot_x, slot_y].id == 20000)
			{
				if (component.items[slot_x, slot_y].state == "f")
				{
					GameObject gameObject = NetworkView.Find(id).gameObject;
					if (gameObject != null)
					{
						gameObject.GetComponent<Vehicle>().fill(50);
						component.items[slot_x, slot_y].state = "e";
						component.syncItem(slot_x, slot_y);
					}
				}
				else if (component.items[slot_x, slot_y].state == "e")
				{
					GameObject gameObject1 = NetworkView.Find(id).gameObject;
					if (gameObject1 != null && gameObject1.GetComponent<Vehicle>().fuel >= 50)
					{
						gameObject1.GetComponent<Vehicle>().burn(50);
						component.items[slot_x, slot_y].state = "f";
						component.syncItem(slot_x, slot_y);
					}
				}
			}
		}
	}
}