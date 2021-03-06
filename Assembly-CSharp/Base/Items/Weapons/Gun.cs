using System;
using System.Collections.Generic;
using UnityEngine;
using Unturned.Log;

public class Gun : Useable
{
	public static bool aiming;

	public static bool dualrender;

	public static float spread;

	private bool sprinting;

	private bool firing;

	private bool reloading;

	private bool cocking;

	private bool cocked;

	private bool attaching;

	private float lastShot;

	private float lastServerShot;

	private int pelletCount;

	private float lastReload;

	private float lastCock;

	private float recoil_x = 1f;

	private float recoil_y = 1f;

	private float accuracy = 1f;

	private float lastTactical;

	private int ammo = -1;

	private int capacity;

	private int magazine;

	private int firemode = -1;

	private int firetype = -1;

	private bool on;

	private int tactical;

	private int barrel;

	private int sight;

	private GameObject magazineModel;

	private GameObject tacticalModel;

	private GameObject barrelModel;

	private GameObject sightModel;

	private Transform magazinePoint;

	private Transform tacticalPoint;

	private Transform barrelPoint;

	private Transform sightPoint;

	private Transform ejectPoint;

	private Vector3 scopeOffset;

	private List<Point2> magazineAttach;

	private List<Point2> tacticalAttach;

	private List<Point2> barrelAttach;

	private List<Point2> sightAttach;

	private GameObject laser;

	private LineRenderer line;

	private GameObject anchor_0;

	private GameObject anchor_1;

	private GameObject anchor_2;

	private GameObject streak;

	private Vector3 flyFromPos;

	private Vector3 flyToPos;

	private float flyStart;

	private float flyTime;

	private RaycastHit hit;

	static Gun()
	{
	}

	public Gun()
	{
	}

	[RPC]
	public void attach()
	{
		this.stopAttach();
		string[] strArrays = Packer.unpack(Player.inventory.items[Equipment.equipped.x, Equipment.equipped.y].state, '\u005F');
		int num = int.Parse(strArrays[1]);
		int num1 = int.Parse(strArrays[2]);
		int num2 = int.Parse(strArrays[3]);
		int num3 = int.Parse(strArrays[4]);
		int num4 = int.Parse(strArrays[5]);
		bool flag = strArrays[6] == "y";
		bool flag1 = false;
		if (this.firemode != num4)
		{
			this.firemode = num4;
		}
		if (this.on != flag)
		{
			this.on = flag;
			flag1 = true;
		}
		this.recoil_x = 1f;
		this.recoil_y = 1f;
		this.accuracy = 1f;
		if (this.magazine == 25002)
		{
			Gun gun = this;
			gun.accuracy = gun.accuracy * 0.15f;
		}
		if (this.tactical == 11000)
		{
			Gun recoilX = this;
			recoilX.recoil_x = recoilX.recoil_x * 0.9f;
			Gun recoilY = this;
			recoilY.recoil_y = recoilY.recoil_y * 0.5f;
			Gun gun1 = this;
			gun1.accuracy = gun1.accuracy * 0.9f;
		}
		else if (this.tactical == 11001)
		{
			Gun recoilX1 = this;
			recoilX1.recoil_x = recoilX1.recoil_x * 0.5f;
			Gun recoilY1 = this;
			recoilY1.recoil_y = recoilY1.recoil_y * 0.9f;
			Gun gun2 = this;
			gun2.accuracy = gun2.accuracy * 0.9f;
		}
		if (this.magazine != num || this.reloading)
		{
			this.ammo = int.Parse(strArrays[0]);
		}
		this.magazinePoint = Equipment.model.transform.FindChild("model").transform.FindChild("magazine");
		if (this.magazine != num)
		{
			this.magazine = num;
			if (this.magazineModel != null)
			{
				UnityEngine.Object.Destroy(this.magazineModel);
			}
			if (this.magazine == -1)
			{
				this.capacity = -1;
			}
			else
			{
				this.magazineModel = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Viewmodels/", this.magazine)));
				this.magazineModel.name = "model";
				this.magazineModel.transform.parent = this.magazinePoint;
				this.magazineModel.transform.localPosition = Vector3.zero;
				this.magazineModel.transform.localRotation = Quaternion.identity;
				this.magazineModel.transform.localScale = new Vector3(1f, 1f, 1f);
				if (ItemType.getType(this.magazine) == 25 && this.ammo == 0 || this.reloading)
				{
					this.magazineModel.transform.FindChild("model").renderer.enabled = false;
				}
				this.capacity = AmmoStats.getCapacity(Equipment.id, this.magazine);
			}
			flag1 = true;
		}
		this.tacticalPoint = Equipment.model.transform.FindChild("model").transform.FindChild("tactical");
		if (this.tactical != num1)
		{
			this.tactical = num1;
			if (this.tacticalModel != null)
			{
				UnityEngine.Object.Destroy(this.tacticalModel);
			}
			if (this.tactical != -1)
			{
				this.tacticalModel = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Viewmodels/", this.tactical)));
				this.tacticalModel.name = "model";
				this.tacticalModel.transform.parent = this.tacticalPoint;
				this.tacticalModel.transform.localPosition = Vector3.zero;
				this.tacticalModel.transform.localRotation = Quaternion.identity;
				this.tacticalModel.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			flag1 = true;
		}
		this.barrelPoint = Equipment.model.transform.FindChild("model").transform.FindChild("barrel");
		if (this.barrel != num2)
		{
			this.barrel = num2;
			if (this.barrelModel != null)
			{
				UnityEngine.Object.Destroy(this.barrelModel);
			}
			if (this.barrel != -1)
			{
				this.barrelModel = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Viewmodels/", this.barrel)));
				this.barrelModel.name = "model";
				this.barrelModel.transform.parent = this.barrelPoint;
				this.barrelModel.transform.localPosition = Vector3.zero;
				this.barrelModel.transform.localRotation = Quaternion.identity;
				this.barrelModel.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			flag1 = true;
		}
		this.sightPoint = Equipment.model.transform.FindChild("model").transform.FindChild("sight");
		if (this.sight != num3)
		{
			this.sight = num3;
			if (this.sightModel != null)
			{
				UnityEngine.Object.Destroy(this.sightModel);
			}
			if (this.sight == -1)
			{
				this.scopeOffset = Vector3.zero;
				Look.zoom.farClipPlane = 0.17f;
				Gun.dualrender = false;
			}
			else
			{
				this.sightModel = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Viewmodels/", this.sight)));
				this.sightModel.name = "model";
				this.sightModel.transform.parent = this.sightPoint;
				this.sightModel.transform.localPosition = Vector3.zero;
				this.sightModel.transform.localRotation = Quaternion.identity;
				this.sightModel.transform.localScale = new Vector3(1f, 1f, 1f);
				Viewmodel.model.SampleAnimation(Viewmodel.model.animation.GetClip("startAim"), 1f);
				this.scopeOffset = Viewmodel.model.transform.InverseTransformPoint(this.sightModel.transform.FindChild("model").FindChild("aim").position);
				if (this.sightModel.transform.FindChild("model").FindChild("aim").FindChild("zoom") == null)
				{
					Look.zoom.farClipPlane = 0.17f;
					Gun.dualrender = false;
				}
				else
				{
					if (PlayerSettings.arm)
					{
						Transform vector3 = this.sightModel.transform.FindChild("model").FindChild("aim").FindChild("scope").transform;
						Vector3 vector31 = this.sightModel.transform.FindChild("model").FindChild("aim").FindChild("scope").transform.localScale;
						float single = vector31.x;
						Vector3 vector32 = this.sightModel.transform.FindChild("model").FindChild("aim").FindChild("scope").transform.localScale;
						Vector3 vector33 = this.sightModel.transform.FindChild("model").FindChild("aim").FindChild("scope").transform.localScale;
						vector3.localScale = new Vector3(single, vector32.y * -1f, vector33.z);
						Transform transforms = this.sightModel.transform.FindChild("model").FindChild("aim").FindChild("zoom").transform;
						Vector3 vector34 = this.sightModel.transform.FindChild("model").FindChild("aim").FindChild("zoom").transform.localScale;
						Vector3 vector35 = this.sightModel.transform.FindChild("model").FindChild("aim").FindChild("zoom").transform.localScale;
						float single1 = vector35.y;
						Vector3 vector36 = this.sightModel.transform.FindChild("model").FindChild("aim").FindChild("zoom").transform.localScale;
						transforms.localScale = new Vector3(vector34.x * -1f, single1, vector36.z);
					}
					Look.zoom.fieldOfView = GunStats.getRange(this.sight);
					Look.zoom.farClipPlane = 8192f;
					Gun.dualrender = true;
				}
			}
			flag1 = true;
		}
		if (this.laser != null)
		{
			UnityEngine.Object.Destroy(this.laser);
		}
		if (this.tactical == 11002)
		{
			this.tacticalModel.transform.FindChild("model").FindChild("light").light.enabled = this.on;
			if (this.on)
			{
				this.laser = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Effects/laser"), Vector3.zero, Quaternion.identity);
				this.laser.name = "laser";
			}
		}
		else if (this.tactical == 11003)
		{
			this.tacticalModel.transform.FindChild("model").FindChild("light_0").light.enabled = this.on;
			this.tacticalModel.transform.FindChild("model").FindChild("light_1").light.enabled = this.on;
		}
		this.ejectPoint = Equipment.model.transform.FindChild("model").transform.FindChild("eject");
		if (flag1)
		{
			Player.clothes.changeItem(Equipment.id, Player.inventory.items[Equipment.equipped.x, Equipment.equipped.y].state);
		}
	}

	public override void dequip()
	{
		if (this.streak != null)
		{
			UnityEngine.Object.Destroy(this.streak);
		}
		Gun.aiming = false;
		Gun.dualrender = false;
		Look.fov = 0f;
		Look.zoom.farClipPlane = 0.17f;
		if (this.laser != null)
		{
			UnityEngine.Object.Destroy(this.laser);
		}
		Interact.hint = string.Empty;
	}

	public override void equip()
	{
		Viewmodel.offset_z = 0f;
		this.firetype = GunStats.getFiretype(Equipment.id);
		if (Equipment.model.transform.FindChild("model").FindChild("string") != null)
		{
			this.line = Equipment.model.transform.FindChild("model").FindChild("string").GetComponent<LineRenderer>();
			this.anchor_0 = Equipment.model.transform.FindChild("model").FindChild("string").FindChild("anchor_0").gameObject;
			this.anchor_1 = Equipment.model.transform.FindChild("model").FindChild("string").FindChild("anchor_1").gameObject;
			if (Equipment.id != 7007)
			{
				this.anchor_2 = Viewmodel.model.transform.FindChild("skeleton").FindChild("rightRoot").FindChild("rightShoulder").FindChild("rightUpper").FindChild("rightLower").FindChild("rightHand").gameObject;
			}
			else
			{
				this.anchor_2 = Equipment.model.transform.FindChild("model").FindChild("string").gameObject;
			}
		}
		Gun.aiming = false;
		Gun.dualrender = false;
		Gun.spread = 0f;
		Viewmodel.play("equip");
		this.attach();
	}

	[RPC]
	public void landArrow(Vector3 point)
	{
		SpawnItems.spawnItem(25001, 1, point);
	}

	[RPC]
	public void reload()
	{
		if (!base.GetComponent<Life>().dead)
		{
			Equipment.busy = true;
			this.cocked = (this.ammo == 0 ? false : this.magazine != -1);
			this.reloading = true;
			this.lastReload = Time.realtimeSinceStartup;
			Viewmodel.play("reload");
			NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/reload"), base.transform.position, 0.5f, UnityEngine.Random.Range(0.95f, 1.05f), 1f);
			if (this.magazineModel != null)
			{
				this.magazineModel.transform.FindChild("model").renderer.enabled = false;
			}
		}
	}

	[RPC]
	public void setBarrel(int slot_x, int slot_y, int new_x, int new_y)
	{
		if (!base.GetComponent<Life>().dead)
		{
			Inventory component = base.GetComponent<Inventory>();
			string[] strArrays = Packer.unpack(component.items[slot_x, slot_y].state, '\u005F');
			if (strArrays[3] != "-1")
			{
				string[] strArrays1 = new string[] { strArrays[0], "_", strArrays[1], "_", strArrays[2], "_-1_", strArrays[4], "_", strArrays[5], "_", strArrays[6], "_" };
				component.items[slot_x, slot_y].state = string.Concat(strArrays1);
				component.tryAddItem(int.Parse(strArrays[3]), 1);
			}
			if (new_x != -1 && ItemType.getType(component.items[new_x, new_y].id) == 12)
			{
				object[] objArray = new object[] { strArrays[0], "_", strArrays[1], "_", strArrays[2], "_", component.items[new_x, new_y].id, "_", strArrays[4], "_", strArrays[5], "_", strArrays[6], "_" };
				component.items[slot_x, slot_y].state = string.Concat(objArray);
				component.deleteItem(new_x, new_y);
			}
			component.syncItem(slot_x, slot_y);
			if (new_x != -1)
			{
				component.syncItem(new_x, new_y);
			}
			if (!base.networkView.isMine)
			{
				base.networkView.RPC("attach", base.networkView.owner, new object[0]);
			}
			else
			{
				this.attach();
			}
		}
	}

	[RPC]
	public void setFiremode(int slot_x, int slot_y, int firemode)
	{
		if (!base.GetComponent<Life>().dead)
		{
			Inventory component = base.GetComponent<Inventory>();
			string[] strArrays = Packer.unpack(component.items[slot_x, slot_y].state, '\u005F');
			object[] objArray = new object[] { strArrays[0], "_", strArrays[1], "_", strArrays[2], "_", strArrays[3], "_", strArrays[4], "_", firemode, "_", strArrays[6], "_" };
			component.items[slot_x, slot_y].state = string.Concat(objArray);
			component.syncItem(slot_x, slot_y);
			if (!base.networkView.isMine)
			{
				base.networkView.RPC("attach", base.networkView.owner, new object[0]);
			}
			else
			{
				this.attach();
			}
		}
	}

	[RPC]
	public void setMagazine(int slot_x, int slot_y, int new_x, int new_y)
	{
		if (!base.GetComponent<Life>().dead)
		{
			Inventory component = base.GetComponent<Inventory>();
			string[] strArrays = Packer.unpack(component.items[slot_x, slot_y].state, '\u005F');
			int num = int.Parse(strArrays[0]);
			if (new_x == -1 || strArrays[1] == "-1" || num < AmmoStats.getCapacity(component.items[slot_x, slot_y].id, int.Parse(strArrays[1])) || int.Parse(strArrays[1]) != component.items[new_x, new_y].id || component.items[new_x, new_y].amount != num + 1)
			{
				if (new_x == -1)
				{
					if (strArrays[1] != "-1")
					{
						string[] strArrays1 = new string[] { "0_-1_", strArrays[2], "_", strArrays[3], "_", strArrays[4], "_", strArrays[5], "_", strArrays[6], "_" };
						component.items[slot_x, slot_y].state = string.Concat(strArrays1);
						if (ItemType.getType(int.Parse(strArrays[1])) == 25)
						{
							if (num > 0)
							{
								component.tryAddItem(int.Parse(strArrays[1]), num, string.Empty);
							}
						}
						else if (num <= 0)
						{
							SpawnItems.drop(int.Parse(strArrays[1]), 1, string.Empty, base.transform.position);
						}
						else
						{
							component.tryAddItem(int.Parse(strArrays[1]), num + 1, string.Empty);
						}
					}
				}
				else if (ItemType.getType(component.items[new_x, new_y].id) == 10)
				{
					if (strArrays[1] != "-1")
					{
						string[] strArrays2 = new string[] { "0_-1_", strArrays[2], "_", strArrays[3], "_", strArrays[4], "_", strArrays[5], "_", strArrays[6], "_" };
						component.items[slot_x, slot_y].state = string.Concat(strArrays2);
						if (ItemType.getType(int.Parse(strArrays[1])) == 25)
						{
							if (num > 0)
							{
								component.tryAddItem(int.Parse(strArrays[1]), num, string.Empty);
							}
						}
						else if (num <= 0)
						{
							SpawnItems.drop(int.Parse(strArrays[1]), 1, string.Empty, base.transform.position);
						}
						else
						{
							component.tryAddItem(int.Parse(strArrays[1]), num + 1, string.Empty);
						}
					}
					if (ItemType.getType(component.items[new_x, new_y].id) == 10)
					{
						object[] objArray = new object[] { component.items[new_x, new_y].amount - 1, "_", component.items[new_x, new_y].id, "_", strArrays[2], "_", strArrays[3], "_", strArrays[4], "_", strArrays[5], "_", strArrays[6], "_" };
						component.items[slot_x, slot_y].state = string.Concat(objArray);
						component.deleteItem(new_x, new_y);
					}
				}
				else if (ItemType.getType(component.items[new_x, new_y].id) == 25)
				{
					if (int.Parse(strArrays[1]) != component.items[new_x, new_y].id)
					{
						if (num > 0)
						{
							component.tryAddItem(int.Parse(strArrays[1]), num, string.Empty);
						}
						object[] objArray1 = new object[] { 1, "_", component.items[new_x, new_y].id, "_", strArrays[2], "_", strArrays[3], "_", strArrays[4], "_", strArrays[5], "_", strArrays[6], "_" };
						component.items[slot_x, slot_y].state = string.Concat(objArray1);
						component.useItem(new_x, new_y);
					}
					else if (num < AmmoStats.getCapacity(component.items[slot_x, slot_y].id, component.items[new_x, new_y].id))
					{
						object[] objArray2 = new object[] { num + 1, "_", component.items[new_x, new_y].id, "_", strArrays[2], "_", strArrays[3], "_", strArrays[4], "_", strArrays[5], "_", strArrays[6], "_" };
						component.items[slot_x, slot_y].state = string.Concat(objArray2);
						component.useItem(new_x, new_y);
					}
				}
				component.syncItem(slot_x, slot_y);
				if (new_x != -1)
				{
					component.syncItem(new_x, new_y);
				}
				if (!base.networkView.isMine)
				{
					base.networkView.RPC("reload", base.networkView.owner, new object[0]);
					base.networkView.RPC("attach", base.networkView.owner, new object[0]);
				}
				else
				{
					this.reload();
					this.attach();
				}
			}
		}
	}

	[RPC]
	public void setOn(int slot_x, int slot_y, bool on)
	{
		if (!base.GetComponent<Life>().dead)
		{
			Inventory component = base.GetComponent<Inventory>();
			string[] strArrays = Packer.unpack(component.items[slot_x, slot_y].state, '\u005F');
			string[] strArrays1 = new string[] { strArrays[0], "_", strArrays[1], "_", strArrays[2], "_", strArrays[3], "_", strArrays[4], "_", strArrays[5], "_", null, null };
			strArrays1[12] = (!on ? "n" : "y");
			strArrays1[13] = "_";
			component.items[slot_x, slot_y].state = string.Concat(strArrays1);
			component.syncItem(slot_x, slot_y);
			if (!base.networkView.isMine)
			{
				base.networkView.RPC("attach", base.networkView.owner, new object[0]);
			}
			else
			{
				this.attach();
			}
		}
	}

	[RPC]
	public void setSight(int slot_x, int slot_y, int new_x, int new_y)
	{
		if (!base.GetComponent<Life>().dead)
		{
			Inventory component = base.GetComponent<Inventory>();
			string[] strArrays = Packer.unpack(component.items[slot_x, slot_y].state, '\u005F');
			if (strArrays[4] != "-1")
			{
				string[] strArrays1 = new string[] { strArrays[0], "_", strArrays[1], "_", strArrays[2], "_", strArrays[3], "_-1_", strArrays[5], "_", strArrays[6], "_" };
				component.items[slot_x, slot_y].state = string.Concat(strArrays1);
				component.tryAddItem(int.Parse(strArrays[4]), 1);
			}
			if (new_x != -1 && ItemType.getType(component.items[new_x, new_y].id) == 9)
			{
				object[] objArray = new object[] { strArrays[0], "_", strArrays[1], "_", strArrays[2], "_", strArrays[3], "_", component.items[new_x, new_y].id, "_", strArrays[5], "_", strArrays[6], "_" };
				component.items[slot_x, slot_y].state = string.Concat(objArray);
				component.deleteItem(new_x, new_y);
			}
			component.syncItem(slot_x, slot_y);
			if (new_x != -1)
			{
				component.syncItem(new_x, new_y);
			}
			if (!base.networkView.isMine)
			{
				base.networkView.RPC("attach", base.networkView.owner, new object[0]);
			}
			else
			{
				this.attach();
			}
		}
	}

	[RPC]
	public void setTactical(int slot_x, int slot_y, int new_x, int new_y)
	{
		if (!base.GetComponent<Life>().dead)
		{
			Inventory component = base.GetComponent<Inventory>();
			string[] strArrays = Packer.unpack(component.items[slot_x, slot_y].state, '\u005F');
			if (strArrays[2] != "-1")
			{
				string[] strArrays1 = new string[] { strArrays[0], "_", strArrays[1], "_-1_", strArrays[3], "_", strArrays[4], "_", strArrays[5], "_", strArrays[6], "_" };
				component.items[slot_x, slot_y].state = string.Concat(strArrays1);
				component.tryAddItem(int.Parse(strArrays[2]), 1);
			}
			if (new_x != -1 && ItemType.getType(component.items[new_x, new_y].id) == 11)
			{
				object[] objArray = new object[] { strArrays[0], "_", strArrays[1], "_", component.items[new_x, new_y].id, "_", strArrays[3], "_", strArrays[4], "_", strArrays[5], "_", strArrays[6], "_" };
				component.items[slot_x, slot_y].state = string.Concat(objArray);
				component.deleteItem(new_x, new_y);
			}
			component.syncItem(slot_x, slot_y);
			if (new_x != -1)
			{
				component.syncItem(new_x, new_y);
			}
			if (!base.networkView.isMine)
			{
				base.networkView.RPC("attach", base.networkView.owner, new object[0]);
			}
			else
			{
				this.attach();
			}
		}
	}

	public bool shoot(int slot_x, int slot_y)
	{
		Inventory component = base.GetComponent<Inventory>();
		string[] strArrays = Packer.unpack(component.items[slot_x, slot_y].state, '\u005F');
		bool type = ItemType.getType(int.Parse(strArrays[1])) == 25;
		if (type)
		{
			Gun gun = this;
			gun.pelletCount = gun.pelletCount + 1;
			if (this.pelletCount > GunStats.getPellets(int.Parse(strArrays[1])) - 1)
			{
				this.pelletCount = 0;
			}
		}
		if (type && this.pelletCount != 0)
		{
			return true;
		}
		if (int.Parse(strArrays[0]) <= 0)
		{
			return false;
		}
		this.lastServerShot = Time.realtimeSinceStartup;
		object[] objArray = new object[] { int.Parse(strArrays[0]) - 1, "_", strArrays[1], "_", strArrays[2], "_", strArrays[3], "_", strArrays[4], "_", strArrays[5], "_", strArrays[6], "_" };
		component.items[slot_x, slot_y].state = string.Concat(objArray);
		component.syncItem(slot_x, slot_y);
		if (!base.networkView.isMine)
		{
			base.networkView.RPC("attach", base.networkView.owner, new object[0]);
		}
		else
		{
			this.attach();
		}
		return true;
	}

	[RPC]
	public void shootAnimal(int slot_x, int slot_y, NetworkViewID id, int limb)
	{
		if (!base.GetComponent<Life>().dead)
		{
			if (this.shoot(slot_x, slot_y))
			{
				GameObject gameObject = NetworkView.Find(id).gameObject;
				if (gameObject != null && !gameObject.GetComponent<AI>().dead)
				{
					float damage = (float)GunStats.getDamage(base.GetComponent<Clothes>().item) * DamageMultiplier.getMultiplierZombie(limb);
					if (Packer.unpack(base.GetComponent<Inventory>().items[slot_x, slot_y].state, '\u005F')[1] == "25002")
					{
						damage = damage * 10f;
					}
					gameObject.GetComponent<AI>().damage((int)damage);
					if (gameObject.GetComponent<AI>().dead)
					{
						base.GetComponent<Skills>().learn(UnityEngine.Random.Range(gameObject.GetComponent<AI>().xp - 1, gameObject.GetComponent<AI>().xp + 2));
						if (gameObject.name == "zombie")
						{
							if (!base.networkView.isMine)
							{
								base.networkView.RPC("killedZombie", base.networkView.owner, new object[0]);
							}
							else
							{
								base.GetComponent<Player>().killedZombie();
							}
						}
						else if (!base.networkView.isMine)
						{
							base.networkView.RPC("killedAnimal", base.networkView.owner, new object[0]);
						}
						else
						{
							base.GetComponent<Player>().killedAnimal();
						}
					}
				}
			}
			else
			{
				NetworkTools.kick(base.networkView.owner, string.Concat("Kicking ", base.name, " for cheating their ammo."));
			}
		}
	}

	[RPC]
	public void shootBarricade(int slot_x, int slot_y, Vector3 position)
	{
		if (!base.GetComponent<Life>().dead)
		{
			if (this.shoot(slot_x, slot_y))
			{
				float damage = (float)GunStats.getDamage(base.GetComponent<Clothes>().item) * 0.5f;
				if (Packer.unpack(base.GetComponent<Inventory>().items[slot_x, slot_y].state, '\u005F')[1] == "25002")
				{
					damage = damage * 10f;
				}
				SpawnBarricades.damage(position, (int)damage);
			}
			else
			{
				NetworkTools.kick(base.networkView.owner, string.Concat("Kicking ", base.name, " for cheating their ammo."));
			}
		}
	}

	[RPC]
	public void shootNothing(int slot_x, int slot_y)
	{
		if (!base.GetComponent<Life>().dead && !this.shoot(slot_x, slot_y))
		{
			NetworkTools.kick(base.networkView.owner, string.Concat("Kicking ", base.name, " for cheating their ammo."));
		}
	}

	[RPC]
	public void shootPlayer(int slot_x, int slot_y, string id, int limb)
	{
		if (!base.GetComponent<Life>().dead && ServerSettings.pvp)
		{
			if (this.shoot(slot_x, slot_y))
			{
				Clothes cloth = base.GetComponent<Clothes>();
				String owner = base.GetComponent<Player>().owner.name;
				String weapon = ItemName.getName(cloth.item);

                NetworkUser user = NetworkUserList.getUserFromID(id);
				if (user != null && user.model != null && user.model != base.gameObject && !user.model.GetComponent<Life>().dead && (base.GetComponent<Player>().owner.friend == string.Empty || base.GetComponent<Player>().owner.friend != user.friend))
				{
					// Range check
					float weaponRange = GunStats.getEffectiveness(cloth.item);
					float hitDistance = (user.model.transform.position - base.transform.position).magnitude;
					if ((user.model.transform.position - base.transform.position).magnitude > weaponRange)
					{
						Logger.LogSecurity(user.id, user.name, "Long range detected with weapon: " + weapon + " Owner: " + owner);
						return;
					}

					RaycastHit hitCheck;
					Physics.Raycast(base.transform.position, user.model.transform.position, out hitCheck, weaponRange, RayMasks.DAMAGE);
					float rayDistance = (user.model.transform.position - hitCheck.point).magnitude;
					if ( Math.Abs( rayDistance - hitDistance ) > 5 )
					{
						Logger.LogSecurity(user.id, user.name, "This fag can shoot through walls: " + owner + " with " + weapon + " Distance (d/ray): " + hitDistance + "/" + rayDistance );
					}

					float damage = (float)GunStats.getDamage(base.GetComponent<Clothes>().item) * DamageMultiplier.getMultiplierPlayer(limb);
					if (Packer.unpack(base.GetComponent<Inventory>().items[slot_x, slot_y].state, '\u005F')[1] == "25002")
					{
						damage = damage * 10f;
					}
					if ((limb == 0 || limb == 1) && user.model.GetComponent<Clothes>().pants != -1)
					{
						damage = damage * ArmorStats.getArmor(user.model.GetComponent<Clothes>().pants);
					}
					if ((limb == 2 || limb == 3 || limb == 5) && user.model.GetComponent<Clothes>().shirt != -1)
					{
						damage = damage * ArmorStats.getArmor(user.model.GetComponent<Clothes>().shirt);
					}
					if (limb == 5 && user.model.GetComponent<Clothes>().vest != -1)
					{
						damage = damage * ArmorStats.getArmor(user.model.GetComponent<Clothes>().vest);
					}
					if (limb == 4 && user.model.GetComponent<Clothes>().hat != -1)
					{
						damage = damage * ArmorStats.getArmor(user.model.GetComponent<Clothes>().hat);
					}
					string bone = string.Empty;
					if (limb == 0)
					{
						bone = "shin";
					}
					else if (limb == 1)
					{
						bone = "thigh";
					}
					else if (limb == 2)
					{
						bone = "arm";
					}
					else if (limb == 3)
					{
						bone = "shoulder";
					}
					else if (limb == 4)
					{
						bone = "head";
					}
					else if (limb == 5)
					{
                        bone = "chest";
					}

					int itemId = base.GetComponent<Clothes>().item;
					string steamID = base.GetComponent<Player>().owner.id;

					var killString = string.Concat (new string[] {
						"You were shot in the ",
						bone,
						" with the ",
						weapon,
						" by ",
						owner.Length > 10 ? owner.Substring (0, 10) : owner,
						" from ",
						hitDistance.ToString ("F"),
						"meters!"
					});
					user.model.GetComponent<Life> ().damage (
                        (int)damage, 
                        killString,
						itemId,
						steamID,
						hitDistance);

					if (user.model.GetComponent<Life>().dead && Time.realtimeSinceStartup - user.model.GetComponent<Player>().owner.spawned > (float)Reputation.SPAWN_DELAY)
					{
						if (user.model.GetComponent<Player>().owner.reputation >= 0)
						{
							NetworkHandler.offsetReputation(base.networkView.owner, -1);
						}
						else
						{
							NetworkHandler.offsetReputation(base.networkView.owner, 1);
						}
						if (!base.networkView.isMine)
						{
                            base.sendKilledPlayer(user, base.networkView.owner);
						}
						else
						{
							base.GetComponent<Player>().killedPlayer();
						}
					}
				}
			}
			else
			{
				NetworkTools.kick(base.networkView.owner, string.Concat("Kicking ", base.name, " for cheating their ammo."));
			}
		}
	}

	[RPC]
	public void shootStructure(int slot_x, int slot_y, Vector3 position, NetworkMessageInfo info)
	{
		if (!base.GetComponent<Life>().dead)
		{
			if (this.shoot(slot_x, slot_y))
			{
				float damage = (float)GunStats.getDamage(base.GetComponent<Clothes>().item) * 0.5f;
				if (Packer.unpack(base.GetComponent<Inventory>().items[slot_x, slot_y].state, '\u005F')[1] == "25002")
				{
					damage = damage * 10f;
				}
                SpawnStructures.damage(position, (int)damage, info.sender);
			}
			else
			{
				NetworkTools.kick(base.networkView.owner, string.Concat("Kicking ", base.name, " for cheating their ammo."));
			}
		}
	}

	[RPC]
	public void shootVehicle(int slot_x, int slot_y, NetworkViewID id)
	{
		if (!base.GetComponent<Life>().dead && ServerSettings.pvp)
		{
			if (this.shoot(slot_x, slot_y))
			{
				GameObject gameObject = NetworkView.Find(id).gameObject;
				if (gameObject != null)
				{
					float damage = (float)GunStats.getDamage(base.GetComponent<Clothes>().item) * 0.4f;
					if (Packer.unpack(base.GetComponent<Inventory>().items[slot_x, slot_y].state, '\u005F')[1] == "25002")
					{
						damage = damage * 10f;
					}
					gameObject.GetComponent<Vehicle>().damage((int)damage);
				}
			}
			else
			{
				NetworkTools.kick(base.networkView.owner, string.Concat("Kicking ", base.name, " for cheating their ammo."));
			}
		}
	}

	[RPC]
	public void stabAnimal(NetworkViewID id, int limb)
	{
		if (!base.GetComponent<Life>().dead)
		{
			GameObject gameObject = NetworkView.Find(id).gameObject;
			if (gameObject != null && !gameObject.GetComponent<AI>().dead)
			{
				gameObject.GetComponent<AI>().damage((int)(50f * (1f + base.GetComponent<Skills>().warrior() * 0.4f) * DamageMultiplier.getMultiplierZombie(limb)));
				if (gameObject.GetComponent<AI>().dead)
				{
					base.GetComponent<Skills>().learn(UnityEngine.Random.Range(2, 4));
					if (gameObject.name == "zombie")
					{
						if (!base.networkView.isMine)
						{
							base.networkView.RPC("killedZombie", base.networkView.owner, new object[0]);
						}
						else
						{
							base.GetComponent<Player>().killedZombie();
						}
					}
					else if (!base.networkView.isMine)
					{
						base.networkView.RPC("killedAnimal", base.networkView.owner, new object[0]);
					}
					else
					{
						base.GetComponent<Player>().killedAnimal();
					}
				}
			}
		}
	}

	[RPC]
	public void stabPlayer(string id, int limb)
	{
		if (!base.GetComponent<Life>().dead && ServerSettings.pvp)
		{
			NetworkUser userFromID = NetworkUserList.getUserFromID(id);
			if (userFromID != null && userFromID.model != null && userFromID.model != base.gameObject && !userFromID.model.GetComponent<Life>().dead && (base.GetComponent<Player>().owner.friend == string.Empty || base.GetComponent<Player>().owner.friend != userFromID.friend) && (userFromID.model.transform.position - base.transform.position).magnitude < 5f)
			{
				float multiplierPlayer = 50f * DamageMultiplier.getMultiplierPlayer(limb);
				multiplierPlayer = multiplierPlayer * (1f + base.GetComponent<Skills>().warrior() * 0.4f);
				multiplierPlayer = multiplierPlayer * (1f - userFromID.model.GetComponent<Skills>().warrior() * 0.4f);
				if ((limb == 0 || limb == 1) && userFromID.model.GetComponent<Clothes>().pants != -1)
				{
					multiplierPlayer = multiplierPlayer * ArmorStats.getArmor(userFromID.model.GetComponent<Clothes>().pants);
				}
				if ((limb == 2 || limb == 3 || limb == 5) && userFromID.model.GetComponent<Clothes>().shirt != -1)
				{
					multiplierPlayer = multiplierPlayer * ArmorStats.getArmor(userFromID.model.GetComponent<Clothes>().shirt);
				}
				if (limb == 5 && userFromID.model.GetComponent<Clothes>().vest != -1)
				{
					multiplierPlayer = multiplierPlayer * ArmorStats.getArmor(userFromID.model.GetComponent<Clothes>().vest);
				}
				if (limb == 4 && userFromID.model.GetComponent<Clothes>().hat != -1)
				{
					multiplierPlayer = multiplierPlayer * ArmorStats.getArmor(userFromID.model.GetComponent<Clothes>().hat);
				}
				string bone = string.Empty;
				if (limb == 0)
				{
                    bone = "shin";
				}
				else if (limb == 1)
				{
					bone = "thigh";
				}
				else if (limb == 2)
				{
					bone = "arm";
				}
				else if (limb == 3)
				{
					bone = "shoulder";
				}
				else if (limb == 4)
				{
					bone = "head";
				}
				else if (limb == 5)
				{
					bone = "chest";
				}

				int itemId = base.GetComponent<Clothes>().item;
				string steamID = base.GetComponent<Player>().owner.id;

				userFromID.model.GetComponent<Life>().damage((int)multiplierPlayer, string.Concat(new string[] { "You were stabbed in the ", bone, " by ", base.GetComponent<Player>().owner.name, "!" }),
				itemId, steamID);
				if (userFromID.model.GetComponent<Life>().dead && Time.realtimeSinceStartup - userFromID.model.GetComponent<Player>().owner.spawned > (float)Reputation.SPAWN_DELAY)
				{
					if (userFromID.model.GetComponent<Player>().owner.reputation >= 0)
					{
						NetworkHandler.offsetReputation(base.networkView.owner, -1);
					}
					else
					{
						NetworkHandler.offsetReputation(base.networkView.owner, 1);
					}
					if (!base.networkView.isMine)
					{
						base.networkView.RPC("killedPlayer", base.networkView.owner, new object[0]);
					}
					else
					{
						base.GetComponent<Player>().killedPlayer();
					}
				}
			}
		}
	}

	public override void startPrimary()
	{
		this.firing = true;
	}

	public override void startSecondary()
	{
		if (!InputSettings.aimToggle)
		{
			if (!Gun.aiming && !this.sprinting && !this.reloading && !this.cocking && !this.attaching)
			{
				Gun.aiming = true;
				if (this.firetype == 2)
				{
					NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/aim"), base.transform.position, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
				}
				Viewmodel.play("startAim");
			}
		}
		else if (Gun.aiming)
		{
			Gun.aiming = false;
			Viewmodel.play("stopAim");
		}
		else if (!this.sprinting && !this.reloading && !this.cocking && !this.attaching)
		{
			Gun.aiming = true;
			if (this.firetype == 2)
			{
				NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/aim"), base.transform.position, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
			}
			Viewmodel.play("startAim");
		}
	}

	public void stopAttach()
	{
		if (this.attaching)
		{
			this.attaching = false;
			if (!this.reloading && !this.cocking)
			{
				Viewmodel.play("stopAttach");
			}
		}
	}

	public override void stopPrimary()
	{
		this.firing = false;
	}

	public override void stopSecondary()
	{
		if (!InputSettings.aimToggle && Gun.aiming)
		{
			Gun.aiming = false;
			Viewmodel.play("stopAim");
		}
	}

	public override void tick()
	{
		Vector3 vector3;

		if (this.magazine == -1)
		{
			Interact.hint = string.Concat("Ammo [", InputSettings.attachmentKey, "]");
		}
		else if (this.ammo == 0)
		{
			Interact.hint = string.Concat("Reload [", InputSettings.reloadKey, "]");
		}
		else if (this.firemode != 0)
		{
			Interact.hint = string.Empty;
		}
		else
		{
			Interact.hint = string.Concat("Safety [", InputSettings.firemodeKey, "]");
		}
		if (Input.GetKeyDown(InputSettings.interactKey) && Interact.focus == null && Screen.lockCursor)
		{
			if (this.tactical == 11002 || this.tactical == 11003)
			{
				bool flag = !this.on;
				if (!Network.isServer)
				{
					base.networkView.RPC("setOn", RPCMode.Server, new object[] { Equipment.equipped.x, Equipment.equipped.y, flag });
				}
				else
				{
					this.setOn(Equipment.equipped.x, Equipment.equipped.y, flag);
				}
				NetworkSounds.askSound("Sounds/General/firemode", base.transform.position, 0.1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
			}
			else if (this.tactical == 11005 && (double)(Time.realtimeSinceStartup - this.lastTactical) > 0.3 && !this.sprinting)
			{
				this.lastTactical = Time.realtimeSinceStartup;
				Viewmodel.offset_z = 0.5f;
				NetworkSounds.askSound("Sounds/Items/8001/use", Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.25f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
				Physics.Raycast(Camera.main.transform.position + (Camera.main.transform.forward * -0.5f), Camera.main.transform.forward, out hit, 2f, RayMasks.DAMAGE);
				if (hit.collider != null)
				{
					if (hit.point.y < Ocean.level)
					{
						NetworkSounds.askSound("Sounds/Impacts/rock", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/bubbles", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
					}
					else if (hit.collider.gameObject.name == "ground" || hit.collider.material.name.ToLower() == "rock (instance)" || hit.collider.material.name.ToLower() == "ground (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/rock", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/rock", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
					}
					else if (hit.collider.material.name.ToLower() == "cloth (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/concrete", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/cloth", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
					}
					else if (hit.collider.material.name.ToLower() == "wood (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/wood", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/splinters", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
					}
					else if (hit.collider.material.name.ToLower() == "tile (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/concrete", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/tile", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
					}
					else if (hit.collider.material.name.ToLower() == "concrete (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/concrete", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/concrete", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
					}
					else if (hit.collider.material.name.ToLower() == "metal (instance)" || hit.collider.material.name.ToLower() == "iron (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/metal", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/sparks", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
					}
					else if (hit.collider.material.name.ToLower() == "flesh (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/flesh", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/flesh", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
					}
					if (hit.collider.name == "ground" || hit.collider.tag == "Prop" || hit.collider.tag == "World" || hit.collider.tag == "Environment")
					{
						NetworkEffects.askEffect("Effects/hole", hit.point + (hit.normal * UnityEngine.Random.Range(0.04f, 0.06f)), Quaternion.LookRotation(hit.normal) * Quaternion.Euler(0f, 0f, (float)UnityEngine.Random.Range(0, 360)), 20f);
					}
					if (hit.collider.tag == "Enemy" && ServerSettings.pvp)
					{
						int limb = OwnerFinder.getLimb(hit.collider.gameObject);
						if (limb != -1)
						{
							GameObject owner = OwnerFinder.getOwner(hit.collider.gameObject);
							if (owner != null && owner.GetComponent<Player>() != null && owner.GetComponent<Player>().action != 4 && (PlayerSettings.friend == string.Empty || PlayerSettings.friendHash != owner.GetComponent<Player>().owner.friend))
							{
								if (!Network.isServer)
								{
									base.networkView.RPC("stabPlayer", RPCMode.Server, new object[] { owner.GetComponent<Player>().owner.id, limb });
								}
								else
								{
									this.stabPlayer(owner.GetComponent<Player>().owner.id, limb);
								}
							}
						}
					}
					else if (hit.collider.tag == "Animal")
					{
						int num = OwnerFinder.getLimb(hit.collider.gameObject);
						GameObject gameObject = OwnerFinder.getOwner(hit.collider.gameObject);
						if (gameObject != null && !gameObject.GetComponent<AI>().dead)
						{
							if (!Network.isServer)
							{
								base.networkView.RPC("stabAnimal", RPCMode.Server, new object[] { gameObject.networkView.viewID, num });
							}
							else
							{
								this.stabAnimal(gameObject.networkView.viewID, num);
							}
						}
					}
				}
			}
		}
		Viewmodel.offset_z = Mathf.Lerp(Viewmodel.offset_z, 0f, 8f * Time.deltaTime);
		if (Input.GetKeyDown(InputSettings.firemodeKey) && Screen.lockCursor)
		{
			int num1 = this.firemode;
			if (num1 == 0)
			{
				if (GunStats.hasSingle(Equipment.id))
				{
					num1 = 1;
				}
				else if (GunStats.hasAuto(Equipment.id))
				{
					num1 = 2;
				}
			}
			else if (num1 != 1)
			{
				if (num1 == 2)
				{
					if (GunStats.hasSafety(Equipment.id))
					{
						num1 = 0;
					}
					else if (GunStats.hasSingle(Equipment.id))
					{
						num1 = 1;
					}
				}
			}
			else if (GunStats.hasAuto(Equipment.id))
			{
				num1 = 2;
			}
			else if (GunStats.hasSafety(Equipment.id))
			{
				num1 = 0;
			}
			if (!Network.isServer)
			{
				base.networkView.RPC("setFiremode", RPCMode.Server, new object[] { Equipment.equipped.x, Equipment.equipped.y, num1 });
			}
			else
			{
				this.setFiremode(Equipment.equipped.x, Equipment.equipped.y, num1);
			}
			NetworkSounds.askSound("Sounds/General/firemode", base.transform.position, 0.1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
		}
		if (Movement.isSprinting && !Gun.aiming && !this.sprinting && !this.reloading && !this.cocking && !this.attaching)
		{
			this.sprinting = true;
			Viewmodel.play("startSprint");
		}
		else if (!Movement.isSprinting && this.sprinting)
		{
			this.sprinting = false;
			Viewmodel.play("stopSprint");
		}
		if (!Gun.aiming)
		{
			Viewmodel.offset_x = 0f;
			Viewmodel.offset_y = 0f;
			Look.fov = 0f;
		}
		else
		{
			if (!PlayerSettings.arm)
			{
				Viewmodel.offset_x = -this.scopeOffset.z;
			}
			else
			{
				Viewmodel.offset_x = this.scopeOffset.z;
			}
			Viewmodel.offset_y = -this.scopeOffset.y;
			if (this.sight == -1)
			{
				Look.fov = 0f;
			}
			else
			{
				Look.fov = GunStats.getZoom(this.sight);
			}
		}
		if (this.cocking && Time.realtimeSinceStartup - this.lastCock > Viewmodel.model.animation["cock"].length)
		{
			this.cocking = false;
			Equipment.busy = false;
		}
		if (this.reloading && Time.realtimeSinceStartup - this.lastReload > Viewmodel.model.animation["reload"].length)
		{
			if (this.magazineModel != null)
			{
				this.magazineModel.transform.FindChild("model").renderer.enabled = true;
			}
			this.reloading = false;
			if (this.cocked)
			{
				Equipment.busy = false;
			}
			else if (!GunStats.hasHammer(Equipment.id))
			{
				this.cocked = true;
				Equipment.busy = false;
			}
			else
			{
				this.cocking = true;
				this.lastCock = Time.realtimeSinceStartup;
				Viewmodel.play("cock");
				NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/cock"), base.transform.position, 0.5f, UnityEngine.Random.Range(0.95f, 1.05f), 1f);
			}
		}
		if (Input.GetKeyDown(InputSettings.reloadKey) && (this.magazine == -1 || this.ammo < this.capacity) && !this.sprinting && !this.reloading && !this.cocking && !this.firing && !Gun.aiming && !this.attaching && Screen.lockCursor)
		{
			bool flag1 = false;
			int num2 = 0;
			while (num2 < Player.inventory.height)
			{
				if (!flag1)
				{
					int num3 = 0;
					while (num3 < Player.inventory.width)
					{
						if (!flag1)
						{
							if ((Player.inventory.items[num3, num2].amount > 1 || ItemType.getType(Player.inventory.items[num3, num2].id) == 25) && AmmoStats.getGunCompatible(Equipment.id, Player.inventory.items[num3, num2].id))
							{
								if (!Network.isServer)
								{
									base.networkView.RPC("setMagazine", RPCMode.Server, new object[] { Equipment.equipped.x, Equipment.equipped.y, num3, num2 });
								}
								else
								{
									this.setMagazine(Equipment.equipped.x, Equipment.equipped.y, num3, num2);
								}
								flag1 = true;
							}
							num3++;
						}
						else
						{
							break;
						}
					}
					num2++;
				}
				else
				{
					break;
				}
			}
		}
		if (Input.GetKeyDown(InputSettings.attachmentKey) && !this.sprinting && !this.reloading && !this.cocking && !this.firing && !Gun.aiming && Screen.lockCursor)
		{
			this.attaching = true;
			if (this.magazinePoint != null)
			{
				this.magazineAttach = new List<Point2>();
				for (int i = 0; i < Player.inventory.height; i++)
				{
					for (int j = 0; j < Player.inventory.width; j++)
					{
						if ((Player.inventory.items[j, i].amount > 1 || ItemType.getType(Player.inventory.items[j, i].id) == 25) && AmmoStats.getGunCompatible(Equipment.id, Player.inventory.items[j, i].id))
						{
							this.magazineAttach.Add(new Point2(j, i));
						}
					}
				}
			}
			if (this.tacticalPoint != null)
			{
				this.tacticalAttach = new List<Point2>();
				for (int k = 0; k < Player.inventory.height; k++)
				{
					for (int l = 0; l < Player.inventory.width; l++)
					{
						if (ItemType.getType(Player.inventory.items[l, k].id) == 11)
						{
							this.tacticalAttach.Add(new Point2(l, k));

						}
					}
				}
			}
			if (this.barrelPoint != null)
			{
				this.barrelAttach = new List<Point2>();
				for (int m = 0; m < Player.inventory.height; m++)
				{
					for (int n = 0; n < Player.inventory.width; n++)
					{
						if (ItemType.getType(Player.inventory.items[n, m].id) == 12)
						{
							this.barrelAttach.Add(new Point2(n, m));

						}
					}
				}
			}
			if (this.sightPoint != null)
			{
				this.sightAttach = new List<Point2>();
				for (int o = 0; o < Player.inventory.height; o++)
				{
					for (int p = 0; p < Player.inventory.width; p++)
					{
						if (ItemType.getType(Player.inventory.items[p, o].id) == 9)
						{
							this.sightAttach.Add(new Point2(p, o));

						}
					}
				}
			}
			Viewmodel.play("startAttach");
		}
		else if (!Input.GetKey(InputSettings.attachmentKey) && this.attaching)
		{
			this.stopAttach();
		}
		if (this.attaching)
		{
		}

		Gun.spread = Mathf.Lerp(Gun.spread, 1f, 4f * Time.deltaTime);
		if (this.firing && (this.firetype != 2 || Gun.aiming))
		{
			if (this.sprinting || this.reloading || this.cocking || this.attaching || this.firemode == 0)
			{
				this.firing = false;
			}
			else if (Time.realtimeSinceStartup - this.lastShot > GunStats.getROF(Equipment.id))
			{
				this.lastShot = Time.realtimeSinceStartup;
				if (this.firemode == 1)
				{
					this.firing = false;
				}
				if (this.ammo <= 0 || this.magazine == -1)
				{
					this.firing = false;
					if (this.firetype != 2)
					{
						NetworkSounds.askSound("Sounds/General/firemode", base.transform.position, 0.1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
					}
				}
				else
				{
					Gun gun = this;
					gun.ammo = gun.ammo - 1;
					for (int q = 0; q < GunStats.getPellets(this.magazine); q++)
					{
						if (!Gun.aiming)
						{
							Vector3 spread = ((Camera.main.transform.forward * 10f) + ((((Camera.main.transform.right * GunStats.getSpread(Equipment.id)) * Gun.spread) * this.accuracy) * (1f - Player.skills.marksman() / 2f))) + ((((Camera.main.transform.up * GunStats.getSpread(Equipment.id)) * Gun.spread) * this.accuracy) * (1f - Player.skills.marksman() / 2f));
							vector3 = spread.normalized;
						}
						else
						{
							Vector3 spread1 = ((Camera.main.transform.forward * 10f) + (((((Camera.main.transform.right * GunStats.getSpread(Equipment.id)) * Gun.spread) * this.accuracy) * GunStats.getADS(Equipment.id)) * (1f - Player.skills.marksman()))) + (((((Camera.main.transform.up * GunStats.getSpread(Equipment.id)) * Gun.spread) * this.accuracy) * GunStats.getADS(Equipment.id)) * (1f - Player.skills.marksman()));
							vector3 = spread1.normalized;
						}
						Vector3 vector32 = Camera.main.transform.position + (Camera.main.transform.forward * -0.5f);
						Physics.Raycast(vector32, vector3, out hit, GunStats.getEffectiveness(Equipment.id), RayMasks.DAMAGE);
						if (this.magazine == 25001)
						{
							this.magazineModel.transform.FindChild("model").renderer.enabled = false;
							this.streak = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Projectiles/25001"));
							this.streak.name = "streak";
							this.streak.transform.parent = NetworkEffects.model.transform;
							this.streak.transform.position = this.magazineModel.transform.position;
							this.streak.transform.rotation = this.magazineModel.transform.rotation;
							this.flyFromPos = this.streak.transform.position;
							this.flyStart = Time.realtimeSinceStartup;
							if (hit.collider == null)
							{
								this.streak.transform.rotation = Quaternion.LookRotation(vector3) * Quaternion.Euler(0f, 90f, 0f);
								this.flyToPos = vector32 + (vector3 * 100f);
								this.flyTime = 4f;
							}
							else
							{
								Transform transforms = this.streak.transform;
								Vector3 vector33 = hit.point - vector32;
								transforms.rotation = Quaternion.LookRotation(vector33.normalized) * Quaternion.Euler(0f, 90f, 0f);
								this.flyToPos = hit.point;
								Vector3 vector34 = hit.point - vector32;
								this.flyTime = vector34.magnitude / 25f;
							}
							UnityEngine.Object.Destroy(this.streak, this.flyTime);
						}
						else if (this.barrelPoint != null)
						{
							if (AmmoStats.getTracer(this.magazine))
							{
								if (hit.collider == null)
								{
									NetworkEffects.askEffect("Effects/tracerRed", vector32, Quaternion.LookRotation(vector3), 4f);
								}
								else
								{
									Quaternion quaternion = Quaternion.LookRotation(vector3);
									Vector3 vector35 = hit.point - vector32;
									NetworkEffects.askEffect("Effects/tracerRed", vector32, quaternion, vector35.magnitude / 300f);
								}
							}
							else if (hit.collider == null)
							{
								NetworkEffects.askEffect("Effects/tracer", vector32, Quaternion.LookRotation(vector3), 4f);
							}
							else
							{
								Quaternion quaternion1 = Quaternion.LookRotation(vector3);
								Vector3 vector36 = hit.point - vector32;
								NetworkEffects.askEffect("Effects/tracer", vector32, quaternion1, vector36.magnitude / 400f);
							}
						}
						if (hit.collider != null)
						{
							if (AmmoStats.getTracer(this.magazine))
							{
								NetworkEffects.askEffect("Effects/sparksRed", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
							}
							if (hit.point.y < Ocean.level)
							{
								NetworkSounds.askSound("Sounds/Impacts/rock", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
								NetworkEffects.askEffect("Effects/bubbles", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
							}
							else if (hit.collider.gameObject.name == "ground" || hit.collider.material.name.ToLower() == "rock (instance)" || hit.collider.material.name.ToLower() == "ground (instance)")
							{
								NetworkSounds.askSound("Sounds/Impacts/rock", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
								NetworkEffects.askEffect("Effects/rock", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
							}
							else if (hit.collider.material.name.ToLower() == "cloth (instance)")
							{
								NetworkSounds.askSound("Sounds/Impacts/concrete", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
								NetworkEffects.askEffect("Effects/cloth", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
							}
							else if (hit.collider.material.name.ToLower() == "wood (instance)")
							{
								NetworkSounds.askSound("Sounds/Impacts/wood", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
								NetworkEffects.askEffect("Effects/splinters", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
							}
							else if (hit.collider.material.name.ToLower() == "tile (instance)")
							{
								NetworkSounds.askSound("Sounds/Impacts/concrete", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
								NetworkEffects.askEffect("Effects/tile", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
							}
							else if (hit.collider.material.name.ToLower() == "concrete (instance)")
							{
								NetworkSounds.askSound("Sounds/Impacts/concrete", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
								NetworkEffects.askEffect("Effects/concrete", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
							}
							else if (hit.collider.material.name.ToLower() == "metal (instance)" || hit.collider.material.name.ToLower() == "iron (instance)")
							{
								NetworkSounds.askSound("Sounds/Impacts/metal", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
								NetworkEffects.askEffect("Effects/sparks", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
							}
							else if (hit.collider.material.name.ToLower() == "flesh (instance)")
							{
								NetworkSounds.askSound("Sounds/Impacts/flesh", hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
								NetworkEffects.askEffect("Effects/flesh", hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), -1f);
							}
							if (hit.collider.name == "ground" || hit.collider.tag == "Prop" || hit.collider.tag == "World" || hit.collider.tag == "Environment" || hit.collider.tag == "Global")
							{
								NetworkEffects.askEffect("Effects/hole", hit.point + (hit.normal * UnityEngine.Random.Range(0.04f, 0.06f)), Quaternion.LookRotation(hit.normal) * Quaternion.Euler(0f, 0f, (float)UnityEngine.Random.Range(0, 360)), 20f);
							}
							if (hit.collider.tag == "Barricade")
							{
								if (!Network.isServer)
								{
									base.networkView.RPC("shootBarricade", RPCMode.Server, new object[] { Equipment.equipped.x, Equipment.equipped.y, hit.collider.transform.parent.position });
								}
								else
								{
									this.shootBarricade(Equipment.equipped.x, Equipment.equipped.y, hit.collider.transform.parent.position);
								}
							}
							else if (hit.collider.tag == "Structure")
							{
								if (!Network.isServer)
								{
									base.networkView.RPC("shootStructure", RPCMode.Server, new object[] { Equipment.equipped.x, Equipment.equipped.y, hit.collider.transform.parent.position });
								}
								else
								{
                                    // FIXME: client stuff?
									//this.shootStructure(Equipment.equipped.x, Equipment.equipped.y, hit.collider.transform.parent.position);
								}
							}
							else if (hit.collider.tag == "Enemy" && ServerSettings.pvp)
							{
								int limb1 = OwnerFinder.getLimb(hit.collider.gameObject);
								if (limb1 != -1)
								{
									GameObject owner1 = OwnerFinder.getOwner(hit.collider.gameObject);
									if (owner1 != null && owner1.GetComponent<Player>() != null && owner1.GetComponent<Player>().action != 4 && (PlayerSettings.friend == string.Empty || PlayerSettings.friendHash != owner1.GetComponent<Player>().owner.friend))
									{
										if (!Network.isServer)
										{
											// Shooting player
											base.networkView.RPC("shootPlayer", RPCMode.Server, new object[] { 
												Equipment.equipped.x, 
												Equipment.equipped.y, 
												owner1.GetComponent<Player>().owner.id, 
												limb1 
											});
										}
										else
										{
											this.shootPlayer(Equipment.equipped.x, Equipment.equipped.y, owner1.GetComponent<Player>().owner.id, limb1);
										}
									}
									else if (!Network.isServer)
									{
										base.networkView.RPC("shootNothing", RPCMode.Server, new object[] { Equipment.equipped.x, Equipment.equipped.y });
									}
									else
									{
										this.shootNothing(Equipment.equipped.x, Equipment.equipped.y);
									}
								}
							}
							else if (hit.collider.tag == "Animal")
							{
								int limb2 = OwnerFinder.getLimb(hit.collider.gameObject);
								GameObject gameObject1 = OwnerFinder.getOwner(hit.collider.gameObject);
								if (gameObject1 != null && !gameObject1.GetComponent<AI>().dead)
								{
									if (!Network.isServer)
									{
										base.networkView.RPC("shootAnimal", RPCMode.Server, new object[] { Equipment.equipped.x, Equipment.equipped.y, gameObject1.networkView.viewID, limb2 });
									}
									else
									{
										this.shootAnimal(Equipment.equipped.x, Equipment.equipped.y, gameObject1.networkView.viewID, limb2);
									}
									gameObject1.GetComponent<AI>().attract(Player.model.transform.position);
								}
								else if (!Network.isServer)
								{
									base.networkView.RPC("shootNothing", RPCMode.Server, new object[] { Equipment.equipped.x, Equipment.equipped.y });
								}
								else
								{
									this.shootNothing(Equipment.equipped.x, Equipment.equipped.y);
								}
							}
							else if (!(hit.collider.tag == "Vehicle") || hit.collider.GetComponent<Vehicle>().health <= 0 || !ServerSettings.pvp)
							{
								if (!Network.isServer)
								{
									base.networkView.RPC("shootNothing", RPCMode.Server, new object[] { Equipment.equipped.x, Equipment.equipped.y });
								}
								else
								{
									this.shootNothing(Equipment.equipped.x, Equipment.equipped.y);
								}
								if (this.magazine == 25001)
								{
									if (!Network.isServer)
									{
										base.networkView.RPC("landArrow", RPCMode.Server, new object[] { hit.point });
									}
									else
									{
										this.landArrow(hit.point);
									}
								}
							}
							else
							{
								if (!Network.isServer)
								{
									base.networkView.RPC("shootVehicle", RPCMode.Server, new object[] { Equipment.equipped.x, Equipment.equipped.y, hit.collider.networkView.viewID });
								}
								else
								{
									this.shootVehicle(Equipment.equipped.x, Equipment.equipped.y, hit.collider.networkView.viewID);
								}
							}
						}
						else if (!Network.isServer)
						{
							base.networkView.RPC("shootNothing", RPCMode.Server, new object[] { Equipment.equipped.x, Equipment.equipped.y });
						}
						else
						{
							this.shootNothing(Equipment.equipped.x, Equipment.equipped.y);
						}
					}
					Gun.spread = Gun.spread + GunStats.getSpray(Equipment.id);
					if (this.tactical != 11004 || Stance.state != 2)
					{
						Look.recoil(GunStats.getRecoil_X(Equipment.id) * this.recoil_x * (1f - Player.skills.marksman() * 0.25f), -GunStats.getRecoil_Y(Equipment.id) * this.recoil_y * (1f - Player.skills.marksman() * 0.25f));
					}
					else
					{
						Look.recoil(GunStats.getRecoil_X(Equipment.id) * this.recoil_x * 0.25f * (1f - Player.skills.marksman() * 0.25f), -GunStats.getRecoil_Y(Equipment.id) * this.recoil_y * 0.25f * (1f - Player.skills.marksman() * 0.25f));
					}
					if (this.firetype != 2)
					{
						if (!Gun.aiming)
						{
							Viewmodel.play("shootHip");
						}
						else
						{
							Viewmodel.play("shootAim");
						}
					}
					if (this.barrel == 12000)
					{
						NetworkSounds.askSound("Sounds/General/silencer", base.transform.position, 1f, UnityEngine.Random.Range(0.95f, 1.05f), 1f);
					}
					else if (this.barrel == 12002)
					{
						SpawnAnimals.attract(base.transform.position, 8f);
						NetworkSounds.askSound("Sounds/General/sneakybeaky", base.transform.position, 1f, UnityEngine.Random.Range(0.95f, 1.05f), 1f);
					}
					else if (this.magazine == 25001 || this.magazine == 10012)
					{
						NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/shoot"), base.transform.position, 1f, UnityEngine.Random.Range(0.95f, 1.05f), 1f);
					}
					else
					{
						SpawnAnimals.attract(base.transform.position, 64f);
						NetworkSounds.askSoundMax(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/shoot"), base.transform.position, 1f, UnityEngine.Random.Range(0.95f, 1.05f), 16f, 1024f);
					}
					if (this.barrelPoint != null && this.barrel != 12001)
					{
						NetworkEffects.askEffect("Effects/muzzle", this.barrelPoint.position, Quaternion.identity, -1f);
					}
					if (this.ejectPoint != null)
					{
						NetworkEffects.askEffect("Effects/bullet", this.ejectPoint.position, this.ejectPoint.rotation, -1f);
						NetworkSounds.askSound("Sounds/General/casing", base.transform.position, 0.1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
					}
					if (this.firetype == 2)
					{
						Gun.aiming = false;
						Viewmodel.play("stopAim");
					}
					else if (this.firetype == 1)
					{
						Gun.aiming = false;
						this.cocking = true;
						this.lastCock = Time.realtimeSinceStartup;
						Viewmodel.play("cock");
						NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/cock"), base.transform.position, 0.5f, UnityEngine.Random.Range(0.95f, 1.05f), 1f);
					}
				}
			}
		}
	}

	public void Update()
	{
		if (!Network.isServer)
		{
			if (base.networkView.isMine)
			{
				if (this.laser != null)
				{
					if (this.sightModel == null)
					{
						Physics.Raycast(this.tacticalModel.transform.position, -this.tacticalModel.transform.right, out hit, 128f, RayMasks.DAMAGE);
					}
					else
					{
						Physics.Raycast(this.sightModel.transform.position + (this.sightModel.transform.forward * this.scopeOffset.y), -this.sightModel.transform.right, out hit, 128f, RayMasks.DAMAGE);
					}
					if (hit.collider == null)
					{
						this.laser.transform.position = Vector3.zero;
					}
					else
					{
						this.laser.transform.position = hit.point;
						this.laser.transform.rotation = Quaternion.LookRotation(this.tacticalModel.transform.right);
					}
				}
				if (this.line != null)
				{
					this.line.SetPosition(0, this.anchor_0.transform.position);
					if (Equipment.id == 7007)
					{
						if (this.reloading || this.ammo == 0)
						{
							this.line.SetPosition(1, this.anchor_0.transform.position);
						}
						else
						{
							this.line.SetPosition(1, this.anchor_2.transform.position);
						}
					}
					else if (this.reloading || this.attaching || this.sprinting || this.ammo == 0 && !Gun.aiming)
					{
						this.line.SetPosition(1, this.anchor_0.transform.position);
					}
					else
					{
						this.line.SetPosition(1, this.anchor_2.transform.position);
					}
					this.line.SetPosition(2, this.anchor_1.transform.position);
					if (!this.line.enabled)
					{
						this.line.enabled = true;
					}
				}
				if (this.streak != null)
				{
					this.streak.transform.position = Vector3.Lerp(this.flyFromPos, this.flyToPos, (Time.realtimeSinceStartup - this.flyStart) / this.flyTime);
				}
			}
		}
	}
}