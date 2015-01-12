using System;
using UnityEngine;
using CommandHandler;

public class Structure : Useable
{
	private float startedUse = Single.MaxValue;

	private GameObject help;

	private bool safe;

	private bool ready;

	private int rotation;

	private bool done;

	private static RaycastHit hit;

	[RPC]
	public void askBuild(int slot_x, int slot_y, Vector3 position, int rotation, string state) {
		//UserList.getUserFromPlayer(base.GetComponent<NetworkPlayer>());ű
		//TODO: disallowing build
		return;
		
		if (!base.GetComponent<Life>().dead) { // Dead hack
			Inventory inventory = base.GetComponent<Inventory>();
						
			if (ItemType.getType(inventory.items[slot_x, slot_y].id) == 17) {
				SpawnStructures.placeStructure(inventory.items[slot_x, slot_y].id, position, rotation, state);
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

	public override void dequip() {
		UnityEngine.Object.Destroy(this.help);
		Interact.hint = string.Empty;
		Interact.icon = string.Empty;
	}

	public override void equip()
	{
		Viewmodel.play("equip");
		this.help = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Structures/", Equipment.id)), Vector3.zero, base.transform.rotation);
		this.help.name = "help";
		UnityEngine.Object.Destroy(this.help.transform.FindChild("model").collider);
		this.help.transform.FindChild("model").renderer.castShadows = false;
		this.help.transform.FindChild("model").renderer.receiveShadows = false;
		this.help.tag = "Untagged";
		this.help.layer = 2;
		this.help.transform.FindChild("model").tag = "Untagged";
		this.help.transform.FindChild("model").gameObject.layer = 2;
		if (this.help.transform.FindChild("nav"))
		{
			UnityEngine.Object.Destroy(this.help.transform.FindChild("nav").gameObject);
		}
		if (StructureStats.isLadder(Equipment.id))
		{
			UnityEngine.Object.Destroy(this.help.transform.FindChild("model").FindChild("ladder").gameObject);
		}
		this.rotation = StructureStats.getRotation(Equipment.id);
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
			Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out Structure.hit, 10f, RayMasks.PLACEABLE);
			if (Structure.hit.collider == null)
			{
				this.bad();
				Interact.hint = "Cannot Reach";
				Interact.icon = "Textures/Icons/error";
				this.help.transform.position = Vector3.zero;
			}
			else
			{
				this.help.transform.position = Structure.hit.point;
				Transform transforms = this.help.transform;
				Quaternion quaternion = this.help.transform.rotation;
				Vector3 vector3 = base.transform.rotation.eulerAngles;
				transforms.rotation = Quaternion.Lerp(quaternion, Quaternion.Euler(0f, vector3.y + (float)this.rotation, 0f), 4f * Time.deltaTime);
				if (Structure.hit.point.y <= Ocean.level)
				{
					this.bad();
					Interact.hint = "Not Waterproof";
					Interact.icon = "Textures/Icons/error";
				}
				else if (StructureStats.isFloor(Equipment.id))
				{
					if (!(Structure.hit.collider.tag == "Structure") || !StructureStats.isFoundation(int.Parse(Structure.hit.collider.transform.parent.name)) && !StructureStats.isFloor(int.Parse(Structure.hit.collider.transform.parent.name)))
					{
						this.bad();
						Interact.hint = "No Base";
						Interact.icon = "Textures/Icons/error";
					}
					else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 6f)) + new Vector3(0f, -0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
					{
						this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 6f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation * Quaternion.Euler(0f, (float)(this.rotation * 2), 0f);
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -6f)) + new Vector3(0f, -0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
					{
						this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -6f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation * Quaternion.Euler(0f, (float)(this.rotation * 2), 0f);
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * 3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * 6f)) + new Vector3(0f, -0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
					{
						this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * 6f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation * Quaternion.Euler(0f, (float)(this.rotation * 2), 0f);
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * -3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * -6f)) + new Vector3(0f, -0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
					{
						this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * -6f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation * Quaternion.Euler(0f, (float)(this.rotation * 2), 0f);
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if ((int)Physics.OverlapSphere(Structure.hit.collider.transform.position + new Vector3(0f, 3f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length != 0 || (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length <= 0 || (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length <= 0 || (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length <= 0 || (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length <= 0)
					{
						this.bad();
						Interact.hint = "No Supports";
						Interact.icon = "Textures/Icons/error";
					}
					else
					{
						this.help.transform.position = Structure.hit.collider.transform.position + new Vector3(0f, 3f, 0f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation * Quaternion.Euler(0f, (float)(this.rotation * 2), 0f);
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
				}
				else if (StructureStats.isPillar(Equipment.id))
				{
					if (!(Structure.hit.collider.tag == "Structure") || !StructureStats.isFoundation(int.Parse(Structure.hit.collider.transform.parent.name)) && !StructureStats.isFloor(int.Parse(Structure.hit.collider.transform.parent.name)))
					{
						this.bad();
						Interact.hint = "No Base";
						Interact.icon = "Textures/Icons/error";
					}
					else if (Structure.hit.normal.y > 0f)
					{
						if ((((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * 3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
						{
							this.help.transform.position = (Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * 3f);
							this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
							this.good();
							Interact.hint = string.Empty;
							Interact.icon = string.Empty;
						}
						else if ((((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * -3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
						{
							this.help.transform.position = (Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * -3f);
							this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
							this.good();
							Interact.hint = string.Empty;
							Interact.icon = string.Empty;
						}
						else if ((((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * 3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
						{
							this.help.transform.position = (Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * 3f);
							this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
							this.good();
							Interact.hint = string.Empty;
							Interact.icon = string.Empty;
						}
						else if ((((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * -3f)) - Structure.hit.point).magnitude >= 1f || (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length != 0)
						{
							this.bad();
							Interact.hint = "No Corners";
							Interact.icon = "Textures/Icons/error";
						}
						else
						{
							this.help.transform.position = (Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * -3f);
							this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
							this.good();
							Interact.hint = string.Empty;
							Interact.icon = string.Empty;
						}
					}
					else if ((((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * 3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, -1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
					{
						this.help.transform.position = ((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, -3f, 0f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if ((((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * -3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, -1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
					{
						this.help.transform.position = ((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, -3f, 0f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if ((((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * 3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, -1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
					{
						this.help.transform.position = ((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, -3f, 0f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if ((((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * -3f)) - Structure.hit.point).magnitude >= 1f || (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, -1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length != 0)
					{
						this.bad();
						Interact.hint = "No Corners";
						Interact.icon = "Textures/Icons/error";
					}
					else
					{
						this.help.transform.position = ((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, -3f, 0f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
				}
				else if (StructureStats.isPost(Equipment.id))
				{
					if (!(Structure.hit.collider.tag == "Structure") || !StructureStats.isFoundation(int.Parse(Structure.hit.collider.transform.parent.name)) && !StructureStats.isFloor(int.Parse(Structure.hit.collider.transform.parent.name)))
					{
						this.bad();
						Interact.hint = "No Base";
						Interact.icon = "Textures/Icons/error";
					}
					else if ((((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * 3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
					{
						this.help.transform.position = (Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * 3f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if ((((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * -3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
					{
						this.help.transform.position = (Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * -3f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if ((((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * 3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
					{
						this.help.transform.position = (Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * 3f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if ((((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * -3f)) - Structure.hit.point).magnitude >= 1f || (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length != 0)
					{
						this.bad();
						Interact.hint = "No Corners";
						Interact.icon = "Textures/Icons/error";
					}
					else
					{
						this.help.transform.position = (Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * -3f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
				}
				else if (StructureStats.isWall(Equipment.id))
				{
					if (!(Structure.hit.collider.tag == "Structure") || !StructureStats.isFoundation(int.Parse(Structure.hit.collider.transform.parent.name)) && !StructureStats.isFloor(int.Parse(Structure.hit.collider.transform.parent.name)))
					{
						this.bad();
						Interact.hint = "No Base";
						Interact.icon = "Textures/Icons/error";
					}
					else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0 && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length > 0 && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length > 0)
					{
						this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0 && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length > 0 && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length > 0)
					{
						this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * 3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0 && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length > 0 && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length > 0)
					{
						this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * 3f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation * Quaternion.Euler(0f, 90f, 0f);
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * -3f)) - Structure.hit.point).magnitude >= 1f || (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length != 0 || (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length <= 0 || (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 1.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length <= 0)
					{
						this.bad();
						Interact.hint = "No Pillars";
						Interact.icon = "Textures/Icons/error";
					}
					else
					{
						this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * -3f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation * Quaternion.Euler(0f, 90f, 0f);
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
				}
				else if (StructureStats.isRampart(Equipment.id))
				{
					if (!(Structure.hit.collider.tag == "Structure") || !StructureStats.isFoundation(int.Parse(Structure.hit.collider.transform.parent.name)) && !StructureStats.isFloor(int.Parse(Structure.hit.collider.transform.parent.name)))
					{
						this.bad();
						Interact.hint = "No Base";
						Interact.icon = "Textures/Icons/error";
					}
					else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0 && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length > 0 && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length > 0)
					{
						this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0 && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length > 0 && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length > 0)
					{
						this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * 3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0 && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length > 0 && (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * 3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length > 0)
					{
						this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * 3f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation * Quaternion.Euler(0f, 90f, 0f);
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
					else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * -3f)) - Structure.hit.point).magnitude >= 1f || (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length != 0 || (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length <= 0 || (int)Physics.OverlapSphere(((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) + (Structure.hit.collider.transform.right * -3f)) + new Vector3(0f, 0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length <= 0)
					{
						this.bad();
						Interact.hint = "No Posts";
						Interact.icon = "Textures/Icons/error";
					}
					else
					{
						this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * -3f);
						this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation * Quaternion.Euler(0f, 90f, 0f);
						this.good();
						Interact.hint = string.Empty;
						Interact.icon = string.Empty;
					}
				}
				else if (!StructureStats.isFoundation(Equipment.id))
				{
					if (StructureStats.isLadder(Equipment.id))
					{
						if (!(Structure.hit.collider.tag == "Structure") || !(Structure.hit.collider.transform.parent.tag == "Structure") || !StructureStats.isLadder(int.Parse(Structure.hit.collider.transform.parent.name)))
						{
							this.help.transform.position = Structure.hit.point;
							this.good();
							Interact.hint = string.Empty;
							Interact.icon = string.Empty;
						}
						else if ((int)Physics.OverlapSphere(Structure.hit.collider.transform.position + new Vector3(0f, 4.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length != 0)
						{
							this.bad();
							Interact.hint = "No Space";
							Interact.icon = "Textures/Icons/error";
						}
						else
						{
							this.help.transform.position = Structure.hit.collider.transform.position + new Vector3(0f, 3f, 0f);
							this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
							this.good();
							Interact.hint = string.Empty;
							Interact.icon = string.Empty;
						}
					}
				}
				else if ((double)Structure.hit.normal.y <= 0.5)
				{
					this.bad();
					Interact.hint = "Too Steep";
					Interact.icon = "Textures/Icons/error";
				}
				else if (Structure.hit.collider.name == "ground")
				{
					this.help.transform.position = Structure.hit.point + new Vector3(0f, 0.3f, 0f);
					this.good();
					Interact.hint = string.Empty;
					Interact.icon = string.Empty;
				}
				else if (!(Structure.hit.collider.tag == "Structure") || !StructureStats.isFoundation(int.Parse(Structure.hit.collider.transform.parent.name)))
				{
					this.bad();
					Interact.hint = "No Base";
					Interact.icon = "Textures/Icons/error";
				}
				else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 6f)) + new Vector3(0f, -0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
				{
					this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * 6f);
					this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
					this.good();
					Interact.hint = string.Empty;
					Interact.icon = string.Empty;
				}
				else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -6f)) + new Vector3(0f, -0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
				{
					this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.up * -6f);
					this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
					this.good();
					Interact.hint = string.Empty;
					Interact.icon = string.Empty;
				}
				else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * 3f)) - Structure.hit.point).magnitude < 1f && (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * 6f)) + new Vector3(0f, -0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length == 0)
				{
					this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * 6f);
					this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
					this.good();
					Interact.hint = string.Empty;
					Interact.icon = string.Empty;
				}
				else if (((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * -3f)) - Structure.hit.point).magnitude >= 1f || (int)Physics.OverlapSphere((Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * -6f)) + new Vector3(0f, -0.5f, 0f), 0.1f, RayMasks.ERRORSTRUCT).Length != 0)
				{
					this.bad();
					Interact.hint = "No Edges";
					Interact.icon = "Textures/Icons/error";
				}
				else
				{
					this.help.transform.position = Structure.hit.collider.transform.position + (Structure.hit.collider.transform.right * -6f);
					this.help.transform.rotation = Structure.hit.collider.transform.parent.rotation;
					this.good();
					Interact.hint = string.Empty;
					Interact.icon = string.Empty;
				}
			}
			if (Input.GetMouseButtonDown(1))
			{
				Structure structure = this;
				structure.rotation = structure.rotation + 45;
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
				state[3] = (int)quaternion1.eulerAngles.y;
				state[4] = StructureStats.getState(Equipment.id);
				networkView.RPC("askBuild", RPCMode.Server, state);
			}
			else
			{
				int num = Equipment.equipped.x;
				int num1 = Equipment.equipped.y;
				Vector3 vector31 = this.help.transform.position;
				Vector3 vector32 = this.help.transform.rotation.eulerAngles;
				this.askBuild(num, num1, vector31, (int)vector32.y, StructureStats.getState(Equipment.id));
			}
			NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/use"), Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
			Equipment.use();
		}
	}
}