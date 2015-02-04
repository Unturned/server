using System;
using UnityEngine;

public class Axe : Useable
{
	private bool swung;

	private bool swinging;

	private float lastSwing;

	private bool strong;

	private bool lastStrong;

	private static RaycastHit hit;

	public Axe()
	{
	}

	public override void equip()
	{
		Viewmodel.play("equip");
	}

	public override void startPrimary()
	{
		if (!this.swinging)
		{
			this.swinging = true;
			this.strong = false;
		}
	}

	public override void startSecondary()
	{
		if (!this.swinging)
		{
			this.swinging = true;
			this.strong = true;
		}
	}

	[RPC]
	public void swingAnimal(NetworkViewID id, int limb, bool extra)
	{
		if (!base.GetComponent<Life>().dead)
		{
			GameObject gameObject = NetworkView.Find(id).gameObject;
			if (gameObject != null && !gameObject.GetComponent<AI>().dead)
			{
				gameObject.GetComponent<AI>().damage((int)((float)MeleeStats.getDamage(base.GetComponent<Clothes>().item) * (1f + base.GetComponent<Skills>().warrior() * 0.4f) * DamageMultiplier.getMultiplierZombie(limb) * (!extra ? 1f : 1.5f)));
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
	public void swingBarricade(Vector3 position, bool extra)
	{
		if (!base.GetComponent<Life>().dead && (position - base.transform.position).magnitude < 5f)
		{
			SpawnBarricades.damage(position, (int)((float)MeleeStats.getDamage(base.GetComponent<Clothes>().item) * (1f + base.GetComponent<Skills>().warrior() * 0.4f) * 0.25f * (!extra ? 1f : 1.5f)));
		}
	}

	[RPC]
	public void swingPlayer(string id, int limb, bool extra)
	{
		if (!base.GetComponent<Life>().dead && ServerSettings.pvp)
		{
			NetworkUser userFromID = NetworkUserList.getUserFromID(id);
			if (userFromID != null && userFromID.model != null && userFromID.model != base.gameObject && !userFromID.model.GetComponent<Life>().dead && (base.GetComponent<Player>().owner.friend == string.Empty || base.GetComponent<Player>().owner.friend != userFromID.friend) && (userFromID.model.transform.position - base.transform.position).magnitude < 3f)
			{
				float damage = (float)MeleeStats.getDamage(base.GetComponent<Clothes>().item) * DamageMultiplier.getMultiplierPlayer(limb);
				damage = damage * (1f + base.GetComponent<Skills>().warrior() * 0.4f);
				damage = damage * (1f - userFromID.model.GetComponent<Skills>().warrior() * 0.4f);
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
				damage = damage * (!extra ? 1f : 1.5f);
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
				userFromID.model.GetComponent<Life>().damage((int)damage, string.Concat(new string[] { "You were chopped in the ", empty, " with the ", ItemName.getName(base.GetComponent<Clothes>().item), " by ", base.GetComponent<Player>().owner.name, "!" }));
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
						//base.networkView.RPC("killedPlayer", base.networkView.owner, new object[0]);
                        base.sendKilledPlayer(userFromID, base.networkView.owner);
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
	public void swingResource(int index, bool extra)
	{
		if (!base.GetComponent<Life>().dead)
		{
			GameObject child = SpawnResources.model.transform.FindChild(string.Concat("resource_", index)).GetChild(0).gameObject;
			if (child != null && child.name.Substring(0, 4) != "rock")
			{
				SpawnResources.chop(index, (int)((float)((!extra ? 10 : 15)) * (1f + base.GetComponent<Skills>().miner() * 0.5f)), base.gameObject);
			}
		}
	}

	[RPC]
	public void swingStructure(Vector3 position, bool extra, NetworkMessageInfo info)
	{
		if (!base.GetComponent<Life>().dead && (position - base.transform.position).magnitude < 5f)
		{
            SpawnStructures.damage(position, (int)((float)MeleeStats.getDamage(base.GetComponent<Clothes>().item) * (1f + base.GetComponent<Skills>().warrior() * 0.4f) * 0.25f * (!extra ? 1f : 1.5f)), info.sender);
		}
	}

	[RPC]
	public void swingVehicle(NetworkViewID id, bool extra)
	{
		if (!base.GetComponent<Life>().dead && ServerSettings.pvp)
		{
			GameObject gameObject = NetworkView.Find(id).gameObject;
			if (gameObject != null)
			{
				gameObject.GetComponent<Vehicle>().damage((int)((float)MeleeStats.getDamage(base.GetComponent<Clothes>().item) * (1f + base.GetComponent<Skills>().warrior() * 0.4f) * 0.2f * (!extra ? 1f : 1.5f)));
			}
		}
	}

	public override void tick()
	{
		if (this.swung)
		{
			if (Time.realtimeSinceStartup - this.lastSwing > (!this.lastStrong ? Viewmodel.model.animation["swingWeak"].length / 2f : Viewmodel.model.animation["swingStrong"].length / 2f))
			{
				NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/use"), Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.25f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
				this.swung = false;
				Physics.Raycast(Camera.main.transform.position + (Camera.main.transform.forward * -0.5f), Camera.main.transform.forward, out Axe.hit, MeleeStats.getRange(Equipment.id) + 0.5f, RayMasks.DAMAGE);
				if (Axe.hit.collider != null)
				{
					if (Axe.hit.point.y < Ocean.level)
					{
						NetworkSounds.askSound("Sounds/Impacts/rock", Axe.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/bubbles", Axe.hit.point + (Axe.hit.normal * 0.05f), Quaternion.LookRotation(Axe.hit.normal), -1f);
					}
					else if (Axe.hit.collider.gameObject.name == "ground" || Axe.hit.collider.material.name.ToLower() == "rock (instance)" || Axe.hit.collider.material.name.ToLower() == "ground (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/rock", Axe.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/rock", Axe.hit.point + (Axe.hit.normal * 0.05f), Quaternion.LookRotation(Axe.hit.normal), -1f);
					}
					else if (Axe.hit.collider.material.name.ToLower() == "cloth (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/rock", Axe.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/cloth", Axe.hit.point + (Axe.hit.normal * 0.05f), Quaternion.LookRotation(Axe.hit.normal), -1f);
					}
					else if (Axe.hit.collider.material.name.ToLower() == "wood (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/wood", Axe.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/splinters", Axe.hit.point + (Axe.hit.normal * 0.05f), Quaternion.LookRotation(Axe.hit.normal), -1f);
					}
					else if (Axe.hit.collider.material.name.ToLower() == "tile (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/concrete", Axe.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/tile", Axe.hit.point + (Axe.hit.normal * 0.05f), Quaternion.LookRotation(Axe.hit.normal), -1f);
					}
					else if (Axe.hit.collider.material.name.ToLower() == "concrete (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/concrete", Axe.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/concrete", Axe.hit.point + (Axe.hit.normal * 0.05f), Quaternion.LookRotation(Axe.hit.normal), -1f);
					}
					else if (Axe.hit.collider.material.name.ToLower() == "metal (instance)" || Axe.hit.collider.material.name.ToLower() == "iron (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/metal", Axe.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/sparks", Axe.hit.point + (Axe.hit.normal * 0.05f), Quaternion.LookRotation(Axe.hit.normal), -1f);
					}
					else if (Axe.hit.collider.material.name.ToLower() == "flesh (instance)")
					{
						NetworkSounds.askSound("Sounds/Impacts/flesh", Axe.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
						NetworkEffects.askEffect("Effects/flesh", Axe.hit.point + (Axe.hit.normal * 0.05f), Quaternion.LookRotation(Axe.hit.normal), -1f);
					}
					if (Axe.hit.collider.name == "ground" || Axe.hit.collider.tag == "Prop" || Axe.hit.collider.tag == "World" || Axe.hit.collider.tag == "Environment" || Axe.hit.collider.tag == "Global")
					{
						NetworkEffects.askEffect("Effects/hole", Axe.hit.point + (Axe.hit.normal * UnityEngine.Random.Range(0.04f, 0.06f)), Quaternion.LookRotation(Axe.hit.normal) * Quaternion.Euler(0f, 0f, (float)UnityEngine.Random.Range(0, 360)), 20f);
					}
					if (Axe.hit.collider.tag == "Resource")
					{
						if (Axe.hit.collider.name.Substring(0, 4) != "rock")
						{
							int num = int.Parse(Axe.hit.collider.transform.parent.name.Substring(Axe.hit.collider.transform.parent.name.IndexOf("_") + 1));
							if (!Network.isServer)
							{
								base.networkView.RPC("swingResource", RPCMode.Server, new object[] { num, this.lastStrong });
							}
							else
							{
								this.swingResource(num, this.lastStrong);
							}
						}
					}
					else if (Axe.hit.collider.tag == "Barricade")
					{
						if (!Network.isServer)
						{
							base.networkView.RPC("swingBarricade", RPCMode.Server, new object[] { Axe.hit.collider.transform.parent.position, this.lastStrong });
						}
						else
						{
							this.swingBarricade(Axe.hit.collider.transform.parent.position, this.lastStrong);
						}
					}
					else if (Axe.hit.collider.tag == "Structure")
					{
						if (!Network.isServer)
						{
							base.networkView.RPC("swingStructure", RPCMode.Server, new object[] { Axe.hit.collider.transform.parent.position, this.lastStrong });
						}
						else
						{
                            // FIXME: client stuff?
							//this.swingStructure(Axe.hit.collider.transform.parent.position, this.lastStrong);
						}
					}
					else if (Axe.hit.collider.tag == "Enemy" && ServerSettings.pvp)
					{
						int limb = OwnerFinder.getLimb(Axe.hit.collider.gameObject);
						GameObject owner = OwnerFinder.getOwner(Axe.hit.collider.gameObject);
						if (owner != null && owner.GetComponent<Player>().action != 4 && (PlayerSettings.friend == string.Empty || PlayerSettings.friendHash != owner.GetComponent<Player>().owner.friend))
						{
							if (!Network.isServer)
							{
								base.networkView.RPC("swingPlayer", RPCMode.Server, new object[] { owner.GetComponent<Player>().owner.id, limb, this.lastStrong });
							}
							else
							{
								this.swingPlayer(owner.GetComponent<Player>().owner.id, limb, this.lastStrong);
							}
						}
					}
					else if (Axe.hit.collider.tag == "Animal")
					{
						int limb1 = OwnerFinder.getLimb(Axe.hit.collider.gameObject);
						GameObject gameObject = OwnerFinder.getOwner(Axe.hit.collider.gameObject);
						if (gameObject != null && !gameObject.GetComponent<AI>().dead)
						{
							if (!Network.isServer)
							{
								base.networkView.RPC("swingAnimal", RPCMode.Server, new object[] { gameObject.networkView.viewID, limb1, this.lastStrong });
							}
							else
							{
								this.swingAnimal(gameObject.networkView.viewID, limb1, this.lastStrong);
							}
						}
					}
					else if (Axe.hit.collider.tag == "Vehicle" && Axe.hit.collider.GetComponent<Vehicle>().health > 0 && ServerSettings.pvp)
					{
						if (!Network.isServer)
						{
							base.networkView.RPC("swingVehicle", RPCMode.Server, new object[] { Axe.hit.collider.networkView.viewID, this.lastStrong });
						}
						else
						{
							this.swingVehicle(Axe.hit.collider.networkView.viewID, this.lastStrong);
						}
					}
				}
			}
		}
		if (this.swinging && (!this.strong || Player.life.stamina >= 10 - (int)(Player.skills.endurance() * 10f)))
		{
			if (Time.realtimeSinceStartup - this.lastSwing <= (!this.lastStrong ? Viewmodel.model.animation["swingWeak"].length : Viewmodel.model.animation["swingStrong"].length))
			{
				this.swinging = false;
				return;
			}
			if (this.strong)
			{
				Player.life.exhaust(10 - (int)(Player.skills.endurance() * 10f));
			}
			this.lastStrong = this.strong;
			this.swinging = false;
			this.lastSwing = Time.realtimeSinceStartup;
			this.swung = true;
			if (!this.strong)
			{
				Viewmodel.play("swingWeak");
			}
			else
			{
				Viewmodel.play("swingStrong");
			}
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
			return;
		}
		this.swinging = false;
	}
}