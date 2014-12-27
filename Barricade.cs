using System;
using UnityEngine;

public class Barricade : Useable
{
	private float startedUse = Single.MaxValue;

	private GameObject help;

	private bool safe;

	private bool ready;

	private int rotation;

	private bool done;

	private static RaycastHit hit;

	public Barricade()
	{
	}

	[RPC]
	public void askBuild(int slot_x, int slot_y, Vector3 position, Vector3 rotation, string state)
	{
		if (!base.GetComponent<Life>().dead)
		{
			Inventory component = base.GetComponent<Inventory>();
			if (ItemType.getType(component.items[slot_x, slot_y].id) == 16)
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
		if (this.help.transform.FindChild("nav2"))
		{
			UnityEngine.Object.Destroy(this.help.transform.FindChild("nav2").gameObject);
		}
		if (this.help.transform.FindChild("model").FindChild("clip"))
		{
			UnityEngine.Object.Destroy(this.help.transform.transform.FindChild("model").FindChild("clip").gameObject);
		}
		if (BarricadeStats.getElectric(Equipment.id))
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Help/electricity"));
			gameObject.name = "electricity";
			gameObject.transform.parent = this.help.transform.FindChild("model");
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
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
			Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out Barricade.hit, 5f, RayMasks.PLACEABLE);
			if (Barricade.hit.collider == null)
			{
				this.bad();
				Interact.hint = "Cannot Reach";
				Interact.icon = "Textures/Icons/error";
				this.help.transform.position = Vector3.zero;
			}
			else
			{
				this.help.transform.position = Barricade.hit.point;
				if (BarricadeStats.getOriented(Equipment.id) || BarricadeStats.getFocused(Equipment.id) && Mathf.Abs(Barricade.hit.normal.y) < 0.1f)
				{
					this.help.transform.rotation = Quaternion.Lerp(this.help.transform.rotation, (Quaternion.LookRotation(Barricade.hit.normal) * Quaternion.Euler(0f, -90f, -90f)) * Quaternion.Euler(0f, (float)this.rotation, 0f), 4f * Time.deltaTime);
				}
				else
				{
					Transform transforms = this.help.transform;
					Quaternion quaternion = this.help.transform.rotation;
					Vector3 vector3 = base.transform.rotation.eulerAngles;
					transforms.rotation = Quaternion.Lerp(quaternion, Quaternion.Euler(0f, vector3.y + (float)this.rotation, 0f), 4f * Time.deltaTime);
				}
				if ((double)Barricade.hit.normal.y <= 0.5 && !BarricadeStats.getLinked(Equipment.id))
				{
					this.bad();
					Interact.hint = "Too Steep";
					Interact.icon = "Textures/Icons/error";
				}
				else if (Barricade.hit.point.y <= Ocean.level)
				{
					this.bad();
					Interact.hint = "Not Waterproof";
					Interact.icon = "Textures/Icons/error";
				}
				else if (BarricadeStats.getLinked(Equipment.id) || BarricadeStats.getOriented(Equipment.id) && (int)Physics.OverlapSphere(this.help.transform.position + (Barricade.hit.normal * 0.1f), 0.05f, RayMasks.ERROR).Length == 0 || (int)Physics.OverlapSphere(this.help.transform.position + new Vector3(0f, 1.25f, 0f), 0.75f, RayMasks.ERROR).Length == 0 && (int)Physics.OverlapSphere(this.help.transform.position + new Vector3(0f, 0.1f, 0f), 0.05f, RayMasks.ERROR).Length == 0)
				{
					this.good();
					Interact.hint = string.Empty;
					Interact.icon = string.Empty;
				}
				else
				{
					this.bad();
					Interact.hint = "No Space";
					Interact.icon = "Textures/Icons/error";
				}
			}
			if (Input.GetMouseButtonDown(1))
			{
				Barricade barricade = this;
				barricade.rotation = barricade.rotation + 45;
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
			int source = ItemSounds.getSource(Equipment.id);
			if (source != -1)
			{
				NetworkSounds.askSound(string.Concat("Sounds/Items/", source, "/use"), Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
			}
			Equipment.use();
		}
	}
}