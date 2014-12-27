using System;
using UnityEngine;

public class Plant : Useable
{
	private float startedUse = Single.MaxValue;

	private GameObject help;

	private bool safe;

	private bool ready;

	private int rotation;

	private bool done;

	private static RaycastHit hit;

	public Plant()
	{
	}

	[RPC]
	public void askBuild(int slot_x, int slot_y, Vector3 position, Vector3 rotation, string state)
	{
		if (!base.GetComponent<Life>().dead)
		{
			Inventory component = base.GetComponent<Inventory>();
			if (ItemType.getType(component.items[slot_x, slot_y].id) == 22)
			{
				SpawnBarricades.placeBarricade(component.items[slot_x, slot_y].id, position, rotation, state);
			}
		}
	}

	public void bad()
	{
		if (this.safe || !this.ready)
		{
			this.ready = true;
			this.help.transform.FindChild("model").renderer.material = (Material)Resources.Load("Materials/Help/bad");
		}
		this.safe = false;
	}

	public override void dequip()
	{
		UnityEngine.Object.Destroy(this.help);
		Interact.hint = string.Empty;
		Interact.icon = string.Empty;
	}

	public override void equip()
	{
		Viewmodel.play("equip");
		this.help = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Barricades/", Equipment.id)), Vector3.zero, base.transform.rotation);
		this.help.name = "help";
		UnityEngine.Object.Destroy(this.help.transform.FindChild("model").collider);
		this.help.transform.FindChild("model").renderer.castShadows = false;
		this.help.transform.FindChild("model").renderer.receiveShadows = false;
		this.help.tag = "Untagged";
		this.help.layer = 2;
		this.help.transform.FindChild("model").tag = "Untagged";
		this.help.transform.FindChild("model").gameObject.layer = 2;
		if (this.help.transform.FindChild("model").GetComponent<Interactable>() != null)
		{
			UnityEngine.Object.Destroy(this.help.transform.FindChild("model").GetComponent<Interactable>());
		}
		if (this.help.transform.FindChild("nav"))
		{
			UnityEngine.Object.Destroy(this.help.transform.FindChild("nav").gameObject);
		}
		this.rotation = BarricadeStats.getRotation(Equipment.id);
		Transform transforms = this.help.transform;
		transforms.rotation = transforms.rotation * Quaternion.Euler(0f, (float)this.rotation, 0f);
	}

	public void good()
	{
		if (!this.safe || !this.ready)
		{
			this.ready = true;
			this.help.transform.FindChild("model").renderer.material = (Material)Resources.Load("Materials/Help/good");
		}
		this.safe = true;
	}

	public override void startPrimary()
	{
		if (this.safe)
		{
			Equipment.busy = true;
			this.startedUse = Time.realtimeSinceStartup;
			Viewmodel.play("use");
		}
	}

	public override void tick()
	{
		if (this.startedUse == Single.MaxValue)
		{
			Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out Plant.hit, 5f, RayMasks.PLACEABLE);
			if (Plant.hit.collider == null)
			{
				this.bad();
				Interact.hint = "Cannot Reach";
				Interact.icon = "Textures/Icons/error";
				this.help.transform.position = Vector3.zero;
			}
			else
			{
				this.help.transform.position = Plant.hit.point;
				Transform transforms = this.help.transform;
				Quaternion quaternion = this.help.transform.rotation;
				Vector3 vector3 = base.transform.rotation.eulerAngles;
				transforms.rotation = Quaternion.Lerp(quaternion, Quaternion.Euler(0f, vector3.y + (float)this.rotation, 0f), 4f * Time.deltaTime);
				if ((double)Plant.hit.normal.y <= 0.5 && !BarricadeStats.getLinked(Equipment.id))
				{
					this.bad();
					Interact.hint = "Too Steep";
					Interact.icon = "Textures/Icons/error";
				}
				else if (Plant.hit.point.y <= Ocean.level)
				{
					this.bad();
					Interact.hint = "Not Waterproof";
					Interact.icon = "Textures/Icons/error";
				}
				else if ((int)Physics.OverlapSphere(this.help.transform.position + new Vector3(0f, 1.25f, 0f), 0.75f, RayMasks.ERROR).Length != 0 || (int)Physics.OverlapSphere(this.help.transform.position + new Vector3(0f, 0.1f, 0f), 0.05f, RayMasks.ERROR).Length != 0)
				{
					this.bad();
					Interact.hint = "No Space";
					Interact.icon = "Textures/Icons/error";
				}
				else if ((!(Plant.hit.collider.name == "ground") || Ground.material(Plant.hit.point) != 0 && Ground.material(Plant.hit.point) != 7 && Ground.material(Plant.hit.point) != 8) && !(Plant.hit.collider.transform.parent.name == "17006") && !(Plant.hit.collider.transform.parent.name == "17007"))
				{
					this.bad();
					Interact.hint = "Infertile Soil";
					Interact.icon = "Textures/Icons/error";
				}
				else
				{
					this.good();
					Interact.hint = string.Empty;
					Interact.icon = string.Empty;
				}
			}
			if (Input.GetMouseButtonDown(1))
			{
				Plant plant = this;
				plant.rotation = plant.rotation + 45;
			}
		}
		if (Time.realtimeSinceStartup - this.startedUse > Viewmodel.model.animation["use"].length && !this.done)
		{
			this.done = true;
			if (!Network.isServer)
			{
				NetworkView networkView = base.networkView;
				object[] state = new object[] { Equipment.equipped.x, Equipment.equipped.y, this.help.transform.position, null, null };
				Quaternion quaternion1 = this.help.transform.rotation;
				state[3] = quaternion1.eulerAngles;
				state[4] = BarricadeStats.getState(Equipment.id);
				networkView.RPC("askBuild", RPCMode.Server, state);
			}
			else
			{
				int num = Equipment.equipped.x;
				int num1 = Equipment.equipped.y;
				Vector3 vector31 = this.help.transform.position;
				Quaternion quaternion2 = this.help.transform.rotation;
				this.askBuild(num, num1, vector31, quaternion2.eulerAngles, BarricadeStats.getState(Equipment.id));
			}
			NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/use"), Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
			Equipment.use();
		}
	}
}