using System;
using UnityEngine;

public class Flashlight : Useable
{
	private bool swung;

	private bool swinging;

	private float lastSwing;

	private bool on;

	private static RaycastHit hit;

	public Flashlight()
	{
	}

	public override void equip()
	{
		Viewmodel.play("equip");
		this.sync();
	}

	public override void startPrimary()
	{
		this.swinging = true;
	}

	public override void startSecondary()
	{
		if (!Network.isServer)
		{
			base.networkView.RPC("toggleFlashlight", RPCMode.Server, new object[] { Equipment.equipped.x, Equipment.equipped.y });
		}
		else
		{
			this.toggleFlashlight(Equipment.equipped.x, Equipment.equipped.y);
		}
		NetworkSounds.askSound("Sounds/General/firemode", base.transform.position, 0.1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
	}

	[RPC]
	public void swingAnimal(NetworkViewID id, int limb)
	{
		if (!base.GetComponent<Life>().dead)
		{
			GameObject gameObject = NetworkView.Find(id).gameObject;
			if (gameObject != null && !gameObject.GetComponent<AI>().dead)
			{
				gameObject.GetComponent<AI>().damage((int)((float)MeleeStats.getDamage(base.GetComponent<Clothes>().item) * (1f + base.GetComponent<Skills>().warrior() * 0.4f) * DamageMultiplier.getMultiplierZombie(limb)));
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
	}

	[RPC]
	public void swingPlayer(string id, int limb)
	{
		if (!base.GetComponent<Life>().dead && ServerSettings.pvp)
		{
			NetworkUser userFromID = NetworkUserList.getUserFromID(id);
			if (userFromID != null && userFromID.model != null && userFromID.model != base.gameObject && !userFromID.model.GetComponent<Life>().dead && (base.GetComponent<Player>().owner.friend == string.Empty || base.GetComponent<Player>().owner.friend != userFromID.friend) && (userFromID.model.transform.position - base.transform.position).magnitude < 3f)
			{
				float damage = (float)MeleeStats.getDamage(base.GetComponent<Clothes>().item) * DamageMultiplier.getMultiplierPlayer(limb);
				if ((limb == 0 || limb == 1) && userFromID.model.GetComponent<Clothes>().pants != -1)
				{
					damage = damage * ArmorStats.getArmor(userFromID.model.GetComponent<Clothes>().pants);
				}
				if ((limb == 2 || limb == 3 || limb == 5) && userFromID.model.GetComponent<Clothes>().shirt != -1)
				{
					damage = damage * ArmorStats.getArmor(userFromID.model.GetComponent<Clothes>().shirt);
				}
				if (limb == 5 && userFromID.model.GetComponent<Clothes>().vest != -1)
				{
					damage = damage * ArmorStats.getArmor(userFromID.model.GetComponent<Clothes>().vest);
				}
				if (limb == 4 && userFromID.model.GetComponent<Clothes>().hat != -1)
				{
					damage = damage * ArmorStats.getArmor(userFromID.model.GetComponent<Clothes>().hat);
				}
				damage = damage * (1f + base.GetComponent<Skills>().warrior() * 0.4f);
				damage = damage * (1f - userFromID.model.GetComponent<Skills>().warrior() * 0.4f);
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
				userFromID.model.GetComponent<Life>().damage((int)damage, string.Concat(new string[] { "You were wacked in the ", empty, " with the ", ItemName.getName(base.GetComponent<Clothes>().item), " by ", base.GetComponent<Player>().owner.name, "!" }));
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

	[RPC]
	public void sync()
	{
		this.on = Player.inventory.items[Equipment.equipped.x, Equipment.equipped.y].state == "b";
		Equipment.model.transform.FindChild("model").transform.FindChild("light_0").light.enabled = this.on;
		Equipment.model.transform.FindChild("model").transform.FindChild("light_1").light.enabled = this.on;
		Player.clothes.changeItem(Equipment.id, (!this.on ? "d" : "b"));
	}

	public override void tick()
	{
		if (this.swung && Time.realtimeSinceStartup - this.lastSwing > Viewmodel.model.animation["swingWeak"].length / 2f)
		{
			NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/use"), Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.25f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
			this.swung = false;
			Physics.Raycast(Camera.main.transform.position + (Camera.main.transform.forward * -0.5f), Camera.main.transform.forward, out Flashlight.hit, MeleeStats.getRange(Equipment.id) + 0.5f, RayMasks.DAMAGE);
			if (Flashlight.hit.collider != null)
			{
				if (Flashlight.hit.point.y < Ocean.level)
				{
					NetworkSounds.askSound("Sounds/Impacts/rock", Flashlight.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/bubbles", Flashlight.hit.point + (Flashlight.hit.normal * 0.05f), Quaternion.LookRotation(Flashlight.hit.normal), -1f);
				}
				else if (Flashlight.hit.collider.gameObject.name == "ground" || Flashlight.hit.collider.material.name.ToLower() == "rock (instance)" || Flashlight.hit.collider.material.name.ToLower() == "ground (instance)")
				{
					NetworkSounds.askSound("Sounds/Impacts/rock", Flashlight.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/rock", Flashlight.hit.point + (Flashlight.hit.normal * 0.05f), Quaternion.LookRotation(Flashlight.hit.normal), -1f);
				}
				else if (Flashlight.hit.collider.material.name.ToLower() == "cloth (instance)")
				{
					NetworkSounds.askSound("Sounds/Impacts/concrete", Flashlight.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/cloth", Flashlight.hit.point + (Flashlight.hit.normal * 0.05f), Quaternion.LookRotation(Flashlight.hit.normal), -1f);
				}
				else if (Flashlight.hit.collider.material.name.ToLower() == "wood (instance)")
				{
					NetworkSounds.askSound("Sounds/Impacts/wood", Flashlight.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/splinters", Flashlight.hit.point + (Flashlight.hit.normal * 0.05f), Quaternion.LookRotation(Flashlight.hit.normal), -1f);
				}
				else if (Flashlight.hit.collider.material.name.ToLower() == "tile (instance)")
				{
					NetworkSounds.askSound("Sounds/Impacts/concrete", Flashlight.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/tile", Flashlight.hit.point + (Flashlight.hit.normal * 0.05f), Quaternion.LookRotation(Flashlight.hit.normal), -1f);
				}
				else if (Flashlight.hit.collider.material.name.ToLower() == "concrete (instance)")
				{
					NetworkSounds.askSound("Sounds/Impacts/concrete", Flashlight.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/concrete", Flashlight.hit.point + (Flashlight.hit.normal * 0.05f), Quaternion.LookRotation(Flashlight.hit.normal), -1f);
				}
				else if (Flashlight.hit.collider.material.name.ToLower() == "metal (instance)" || Flashlight.hit.collider.material.name.ToLower() == "iron (instance)")
				{
					NetworkSounds.askSound("Sounds/Impacts/metal", Flashlight.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/sparks", Flashlight.hit.point + (Flashlight.hit.normal * 0.05f), Quaternion.LookRotation(Flashlight.hit.normal), -1f);
				}
				else if (Flashlight.hit.collider.material.name.ToLower() == "flesh (instance)")
				{
					NetworkSounds.askSound("Sounds/Impacts/flesh", Flashlight.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/flesh", Flashlight.hit.point + (Flashlight.hit.normal * 0.05f), Quaternion.LookRotation(Flashlight.hit.normal), -1f);
				}
				if (Flashlight.hit.collider.name == "ground" || Flashlight.hit.collider.tag == "Prop" || Flashlight.hit.collider.tag == "World" || Flashlight.hit.collider.tag == "Environment" || Flashlight.hit.collider.tag == "Global")
				{
					NetworkEffects.askEffect("Effects/hole", Flashlight.hit.point + (Flashlight.hit.normal * UnityEngine.Random.Range(0.04f, 0.06f)), Quaternion.LookRotation(Flashlight.hit.normal) * Quaternion.Euler(0f, 0f, (float)UnityEngine.Random.Range(0, 360)), 20f);
				}
				if (Flashlight.hit.collider.tag == "Enemy" && ServerSettings.pvp)
				{
					int limb = OwnerFinder.getLimb(Flashlight.hit.collider.gameObject);
					GameObject owner = OwnerFinder.getOwner(Flashlight.hit.collider.gameObject);
					if (owner != null && owner.GetComponent<Player>().action != 4 && (PlayerSettings.friend == string.Empty || PlayerSettings.friendHash != owner.GetComponent<Player>().owner.friend))
					{
						HUDGame.lastHitmarker = Time.realtimeSinceStartup;
						if (!Network.isServer)
						{
							base.networkView.RPC("swingPlayer", RPCMode.Server, new object[] { owner.GetComponent<Player>().owner.id, limb });
						}
						else
						{
							this.swingPlayer(owner.GetComponent<Player>().owner.id, limb);
						}
					}
				}
				else if (Flashlight.hit.collider.tag == "Animal")
				{
					int num = OwnerFinder.getLimb(Flashlight.hit.collider.gameObject);
					GameObject gameObject = OwnerFinder.getOwner(Flashlight.hit.collider.gameObject);
					if (gameObject != null && !gameObject.GetComponent<AI>().dead)
					{
						HUDGame.lastHitmarker = Time.realtimeSinceStartup;
						if (!Network.isServer)
						{
							base.networkView.RPC("swingAnimal", RPCMode.Server, new object[] { gameObject.networkView.viewID, num });
						}
						else
						{
							this.swingAnimal(gameObject.networkView.viewID, num);
						}
					}
				}
			}
		}
		if (!this.swinging || Time.realtimeSinceStartup - this.lastSwing <= Viewmodel.model.animation["swingWeak"].length)
		{
			this.swinging = false;
		}
		else
		{
			this.swinging = false;
			this.lastSwing = Time.realtimeSinceStartup;
			this.swung = true;
			Viewmodel.play("swingWeak");
			if (Stance.state == 0)
			{
				Player.play("standMelee");
			}
			else if (Stance.state == 1)
			{
				Player.play("crouchMelee");
			}
			else if (Stance.state == 2)
			{
				Player.play("proneMelee");
			}
		}
	}

	[RPC]
	public void toggleFlashlight(int slot_x, int slot_y)
	{
		if (!base.GetComponent<Life>().dead)
		{
			Inventory component = base.GetComponent<Inventory>();
			if (component.items[slot_x, slot_y].state != "b")
			{
				component.items[slot_x, slot_y].state = "b";
			}
			else
			{
				component.items[slot_x, slot_y].state = "d";
			}
			component.syncItem(slot_x, slot_y);
			if (!base.networkView.isMine)
			{
				base.networkView.RPC("sync", base.networkView.owner, new object[0]);
			}
			else
			{
				this.sync();
			}
		}
	}
}