using System;
using UnityEngine;

public class Equipment : MonoBehaviour
{
	public static Point2 equipped;

	public static Useable useable;

	public static GameObject model;

	public static int id;

	public static bool busy;

	public static bool setup;

	public static bool ticking;

	private static RaycastHit hit;

	private static float lastEquip;

	private static bool ready;

	private static float startedEquip;

	private bool swung;

	private bool swinging;

	private float lastSwing;

	private bool right;

	static Equipment()
	{
		Equipment.equipped = Point2.NONE;
		Equipment.useable = null;
		Equipment.model = null;
		Equipment.id = -1;
		Equipment.busy = false;
		Equipment.setup = true;
		Equipment.ticking = false;
		Equipment.lastEquip = Single.MinValue;
		Equipment.ready = false;
		Equipment.startedEquip = Single.MinValue;
	}

	public Equipment()
	{
	}

	public static void delete()
	{
		if (!Player.life.dead)
		{
			Player.inventory.sendDeleteItem(Equipment.equipped.x, Equipment.equipped.y);
			Equipment.dequip();
		}
	}

	public static void dequip()
	{
		Equipment.ticking = false;
		Equipment.startedEquip = Single.MinValue;
		if (Equipment.useable != null)
		{
			Equipment.useable.dequip();
			string[] animations = ItemAnimations.getAnimations(Equipment.id);
			for (int i = 0; i < (int)animations.Length; i++)
			{
				Viewmodel.model.animation.RemoveClip(animations[i]);
			}
			UnityEngine.Object.Destroy(Equipment.useable);
		}
		if (Equipment.model != null)
		{
			UnityEngine.Object.Destroy(Equipment.model);
		}
		Equipment.equipped = Point2.NONE;
		Equipment.id = -1;
		Equipment.busy = false;
		Equipment.setup = true;
		Player.clothes.changeItem(-1, string.Empty);
		if (!Network.isServer)
		{
			Player.model.networkView.RPC("equipServer", RPCMode.Server, new object[] { -1, -1, Equipment.id });
		}
	}

	public static void equip(int x, int y)
	{
		if (!Player.life.dead && !Equipment.busy && Equipment.setup && (double)(Time.realtimeSinceStartup - Equipment.lastEquip) > 0.2 && !Movement.isSwimming && !Movement.isClimbing && !Movement.isDriving)
		{
			Equipment.lastEquip = Time.realtimeSinceStartup;
			if ((Equipment.equipped.x != x || Equipment.equipped.y != y) && x >= 0 && x < Player.inventory.width && y >= 0 && y < Player.inventory.height && Player.inventory.items[x, y].id != -1 && ItemEquipable.getEquipable(Player.inventory.items[x, y].id))
			{
				Equipment.dequip();
				Equipment.equipped = new Point2(x, y);
				Equipment.id = Player.inventory.items[x, y].id;
				Player.clothes.changeItem(Equipment.id, Player.inventory.items[x, y].state);
				Equipment.model = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Viewmodels/", Equipment.id)));
				Equipment.model.name = Equipment.id.ToString();
				if (Equipment.id == 7004 || Equipment.id == 7014)
				{
					Equipment.model.transform.parent = Viewmodel.model.transform.FindChild("skeleton").FindChild("leftRoot").FindChild("leftShoulder").FindChild("leftUpper").FindChild("leftLower").FindChild("leftHand");
				}
				else
				{
					Equipment.model.transform.parent = Viewmodel.model.transform.FindChild("skeleton").FindChild("rightRoot").FindChild("rightShoulder").FindChild("rightUpper").FindChild("rightLower").FindChild("rightHand");
				}
				Equipment.model.transform.localPosition = Vector3.zero;
				Equipment.model.transform.localRotation = Quaternion.identity;
				Equipment.model.transform.localScale = new Vector3(1f, 1f, 1f);
				Equipment.useable = (Useable)Player.model.AddComponent(Equipment.model.GetComponent<Useable>().GetType());
				UnityEngine.Object.Destroy(Equipment.model.GetComponent<Useable>());
				GameObject gameObject = (GameObject)Resources.Load(string.Concat("Models/Animations/FirstPerson/Items/", ItemAnimations.getSource(Equipment.id), "/model"));
				string[] animations = ItemAnimations.getAnimations(Equipment.id);
				for (int i = 0; i < (int)animations.Length; i++)
				{
					Viewmodel.model.animation.AddClip(gameObject.animation.GetClip(animations[i]), animations[i]);
				}
				Equipment.startedEquip = Time.realtimeSinceStartup;
				Equipment.setup = false;
				Equipment.ready = false;
				Equipment.model.transform.FindChild("model").renderer.enabled = false;
				if (!Network.isServer)
				{
					Player.model.networkView.RPC("equipServer", RPCMode.Server, new object[] { x, y, Equipment.id });
				}
			}
			else
			{
				Equipment.dequip();
			}
		}
	}

	[RPC]
	public void equipServer(int slotX, int slotY, int itemId)
	{
        Inventory inventory = base.GetComponent<Inventory>();
		if ((slotX != -1 || itemId != -1) && inventory.items[slotX, slotY].id != itemId) // WTF?
		{
			NetworkTools.kick(base.networkView.owner, string.Concat("Kicking ", base.name, " for hacking items."));
		}
		else
		{
			Useable useable = base.GetComponent<Useable>();
			if (useable != null)
			{
				UnityEngine.Object.Destroy(useable);
			}
			if (itemId != -1)
			{
				GameObject gameObject = (GameObject)Resources.Load(string.Concat("Prefabs/Viewmodels/", itemId));
				base.gameObject.AddComponent(gameObject.GetComponent<Useable>().GetType());
			}
		}
	}

	[RPC]
	public void punchAnimal(NetworkViewID id, int limb)
	{
		if (!base.GetComponent<Life>().dead)
		{
			GameObject gameObject = NetworkView.Find(id).gameObject;
			if (gameObject != null && !gameObject.GetComponent<AI>().dead)
			{
				gameObject.GetComponent<AI>().damage((int)(14f * (1f + base.GetComponent<Skills>().warrior() * 0.4f) * DamageMultiplier.getMultiplierZombie(limb)));
				if (gameObject.GetComponent<AI>().dead)
				{
					base.GetComponent<Skills>().learn(UnityEngine.Random.Range(2, 4));
				}
			}
		}
	}

	[RPC]
	public void punchPlayer(string id, int limb)
	{
		if (!base.GetComponent<Life>().dead && ServerSettings.pvp)
		{
			NetworkUser userFromID = NetworkUserList.getUserFromID(id);
			if (userFromID != null && userFromID.model != null && userFromID.model != base.gameObject && !userFromID.model.GetComponent<Life>().dead && (base.GetComponent<Player>().owner.friend == string.Empty || base.GetComponent<Player>().owner.friend != userFromID.friend) && (userFromID.model.transform.position - base.transform.position).magnitude < 3f)
			{
				float multiplierPlayer = 8f * DamageMultiplier.getMultiplierPlayer(limb);
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
				string empty = string.Empty;
				if (limb == 0)
				{
					empty = "shin";
				}
				else if (limb == 1)
				{
					empty = "thigh";
				}
				else if (limb == 2)
				{
					empty = "arm";
				}
				else if (limb == 3)
				{
					empty = "shoulder";
				}
				else if (limb == 4)
				{
					empty = "head";
				}
				else if (limb == 5)
				{
					empty = "chest";
				}
				userFromID.model.GetComponent<Life>().damage((int)multiplierPlayer, string.Concat(new string[] { "You were punched in the ", empty, " by ", base.GetComponent<Player>().owner.name, "!" }));
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
				}
			}
		}
	}

	public void Start()
	{
		if (base.networkView.isMine)
		{
			Equipment.equipped = Point2.NONE;
			Equipment.useable = null;
			Equipment.model = null;
			Equipment.id = -1;
			Equipment.busy = false;
			Equipment.ticking = false;
			Equipment.lastEquip = Single.MinValue;
			Equipment.ready = true;
			Equipment.setup = true;
		}
	}

	public void Update()
	{
		if (base.networkView.isMine)
		{
			if ((Player.life.dead || Movement.isSwimming || Movement.isClimbing || Movement.isDriving) && Equipment.model != null)
			{
				Equipment.dequip();
			}
			if (Equipment.model != null)
			{
				if (!Equipment.ready)
				{
					Equipment.ready = true;
					Equipment.model.transform.FindChild("model").renderer.enabled = true;
					Equipment.useable.equip();
					Equipment.ticking = true;
				}
				else if (Equipment.ticking)
				{
					if (Equipment.setup)
					{
						Equipment.useable.tick();
					}
					else if (Time.realtimeSinceStartup - Equipment.startedEquip > Viewmodel.model.animation["equip"].length)
					{
						Equipment.setup = true;
					}
				}
				if (Input.GetKeyDown(InputSettings.dropKey))
				{
					int num = Equipment.equipped.x;
					int num1 = Equipment.equipped.y;
					Equipment.dequip();
					SpawnItems.dropItem(num, num1, Input.GetKey(InputSettings.otherKey));
				}
				else if (Input.GetKeyDown(InputSettings.itemKey))
				{
					Equipment.dequip();
				}
			}
			if (Screen.lockCursor && !Player.life.dead)
			{
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					Equipment.equip(0, 0);
				}
				if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					Equipment.equip(1, 0);
				}
				if (Input.GetKeyDown(KeyCode.Alpha3))
				{
					Equipment.equip(2, 0);
				}
				if (Input.GetKeyDown(KeyCode.Alpha4))
				{
					Equipment.equip(3, 0);
				}
				if (Input.GetKeyDown(KeyCode.Alpha5))
				{
					Equipment.equip(4, 0);
				}
				if (Input.GetKeyDown(KeyCode.Alpha6))
				{
					Equipment.equip(5, 0);
				}
				if (!Equipment.busy && Equipment.setup)
				{
					if (Equipment.useable != null)
					{
						if (Input.GetKeyDown(InputSettings.shootKey))
						{
							Equipment.useable.startPrimary();
						}
						if (Input.GetKeyUp(InputSettings.shootKey))
						{
							Equipment.useable.stopPrimary();
						}
						if (Input.GetKeyDown(InputSettings.aimKey))
						{
							Equipment.useable.startSecondary();
						}
						if (Input.GetKeyUp(InputSettings.aimKey))
						{
							Equipment.useable.stopSecondary();
						}
					}
					else if (!Player.life.dead && !Movement.isSwimming && !Movement.isClimbing && Movement.vehicle == null && Stance.state != 2)
					{
						if (Input.GetKeyDown(InputSettings.shootKey) && !this.swinging)
						{
							this.swinging = true;
							this.right = false;
						}
						if (Input.GetKeyDown(InputSettings.aimKey) && !this.swinging)
						{
							this.swinging = true;
							this.right = true;
						}
					}
				}
			}
			if (this.swung && Time.realtimeSinceStartup - this.lastSwing > Viewmodel.model.animation["punchRight"].length / 2f)
			{
				NetworkSounds.askSound("Sounds/Items/8001/use", Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.25f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
				this.swung = false;
				Physics.Raycast(Camera.main.transform.position + (Camera.main.transform.forward * -0.5f), Camera.main.transform.forward, out Equipment.hit, 2f, RayMasks.DAMAGE);
				if (Equipment.hit.collider != null)
				{
					if (Equipment.hit.point.y < Ocean.level)
					{
						NetworkSounds.askSound("Sounds/Impacts/rock", Equipment.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/bubbles", Equipment.hit.point + (Equipment.hit.normal * 0.05f), Quaternion.LookRotation(Equipment.hit.normal), -1f);
					}
					else if (Equipment.hit.collider.gameObject.name == "ground" || Equipment.hit.collider.material.name.ToLower() == "rock (instance)" || Equipment.hit.collider.material.name.ToLower() == "ground (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/rock", Equipment.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/rock", Equipment.hit.point + (Equipment.hit.normal * 0.05f), Quaternion.LookRotation(Equipment.hit.normal), -1f);
					}
					else if (Equipment.hit.collider.material.name.ToLower() == "cloth (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/concrete", Equipment.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/cloth", Equipment.hit.point + (Equipment.hit.normal * 0.05f), Quaternion.LookRotation(Equipment.hit.normal), -1f);
					}
					else if (Equipment.hit.collider.material.name.ToLower() == "wood (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/wood", Equipment.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/splinters", Equipment.hit.point + (Equipment.hit.normal * 0.05f), Quaternion.LookRotation(Equipment.hit.normal), -1f);
					}
					else if (Equipment.hit.collider.material.name.ToLower() == "tile (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/concrete", Equipment.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/tile", Equipment.hit.point + (Equipment.hit.normal * 0.05f), Quaternion.LookRotation(Equipment.hit.normal), -1f);
					}
					else if (Equipment.hit.collider.material.name.ToLower() == "concrete (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/concrete", Equipment.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/concrete", Equipment.hit.point + (Equipment.hit.normal * 0.05f), Quaternion.LookRotation(Equipment.hit.normal), -1f);
					}
					else if (Equipment.hit.collider.material.name.ToLower() == "metal (instance)" || Equipment.hit.collider.material.name.ToLower() == "iron (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/metal", Equipment.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/sparks", Equipment.hit.point + (Equipment.hit.normal * 0.05f), Quaternion.LookRotation(Equipment.hit.normal), -1f);
					}
					else if (Equipment.hit.collider.material.name.ToLower() == "flesh (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/flesh", Equipment.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/flesh", Equipment.hit.point + (Equipment.hit.normal * 0.05f), Quaternion.LookRotation(Equipment.hit.normal), -1f);
					}
					if (Equipment.hit.collider.tag == "Enemy" && ServerSettings.pvp)
					{
						int limb = OwnerFinder.getLimb(Equipment.hit.collider.gameObject);
						GameObject owner = OwnerFinder.getOwner(Equipment.hit.collider.gameObject);
						if (owner != null && owner.GetComponent<Player>().action != 4 && (PlayerSettings.friend == string.Empty || PlayerSettings.friendHash != owner.GetComponent<Player>().owner.friend))
						{
							if (!Network.isServer)
							{
								base.networkView.RPC("punchPlayer", RPCMode.Server, new object[] { owner.GetComponent<Player>().owner.id, limb });
							}
							else
							{
								this.punchPlayer(owner.GetComponent<Player>().owner.id, limb);
							}
						}
					}
					else if (Equipment.hit.collider.tag == "Animal")
					{
						int limb1 = OwnerFinder.getLimb(Equipment.hit.collider.gameObject);
						GameObject gameObject = OwnerFinder.getOwner(Equipment.hit.collider.gameObject);
						if (gameObject != null && !gameObject.GetComponent<AI>().dead)
						{
							if (!Network.isServer)
							{
								base.networkView.RPC("punchAnimal", RPCMode.Server, new object[] { gameObject.networkView.viewID, limb1 });
							}
							else
							{
								this.punchAnimal(gameObject.networkView.viewID, limb1);
							}
						}
					}
				}
			}
			if (!this.swinging || Time.realtimeSinceStartup - this.lastSwing <= Viewmodel.model.animation["punchRight"].length)
			{
				this.swinging = false;
			}
			else
			{
				this.swinging = false;
				this.lastSwing = Time.realtimeSinceStartup;
				this.swung = true;
				if (!this.right)
				{
					Viewmodel.play("punchLeft");
					if (Stance.state == 0)
					{
						Player.play("standPunchLeft");
					}
					else if (Stance.state == 1)
					{
						Player.play("crouchPunchLeft");
					}
				}
				else
				{
					Viewmodel.play("punchRight");
					if (Stance.state == 0)
					{
						Player.play("standPunchRight");
					}
					else if (Stance.state == 1)
					{
						Player.play("crouchPunchRight");
					}
				}
			}
		}
	}

	public static void use()
	{
		if (!Player.life.dead)
		{
			if (!ItemStackable.getStackable(Equipment.id) || Player.inventory.items[Equipment.equipped.x, Equipment.equipped.y].amount <= 1)
			{
				Player.inventory.sendUseItem(Equipment.equipped.x, Equipment.equipped.y);
				Equipment.dequip();
			}
			else
			{
				Player.inventory.sendUseItem(Equipment.equipped.x, Equipment.equipped.y);
				int num = Equipment.equipped.x;
				int num1 = Equipment.equipped.y;
				Equipment.dequip();
				Equipment.equip(num, num1);
			}
		}
	}
}