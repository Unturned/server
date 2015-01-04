using System;
using UnityEngine;

public class Chainsaw : Useable
{
	private bool swinging;

	private float lastSwing;

	private static RaycastHit hit;

	public Chainsaw()
	{
	}

	public override void equip()
	{
		Viewmodel.play("equip");
	}

	public override void startPrimary()
	{
		this.swinging = true;
		Viewmodel.play("start");
	}

	public override void stopPrimary()
	{
		this.swinging = false;
		Viewmodel.play("stop");
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
	public void swingBarricade(Vector3 position)
	{
		if (!base.GetComponent<Life>().dead && (position - base.transform.position).magnitude < 5f)
		{
			SpawnBarricades.damage(position, (int)((float)MeleeStats.getDamage(base.GetComponent<Clothes>().item) * (1f + base.GetComponent<Skills>().warrior() * 0.4f) * 0.25f));
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
	public void swingResource(int index)
	{
		if (!base.GetComponent<Life>().dead)
		{
			GameObject child = SpawnResources.model.transform.FindChild(string.Concat("resource_", index)).GetChild(0).gameObject;
			if (child != null && child.name.Substring(0, 4) != "rock")
			{
				SpawnResources.chop(index, 3, base.gameObject);
			}
		}
	}

	[RPC]
	public void swingStructure(Vector3 position)
	{
		if (!base.GetComponent<Life>().dead && (position - base.transform.position).magnitude < 5f)
		{
			SpawnStructures.damage(position, (int)((float)MeleeStats.getDamage(base.GetComponent<Clothes>().item) * (1f + base.GetComponent<Skills>().warrior() * 0.4f) * 0.25f));
		}
	}

	[RPC]
	public void swingVehicle(NetworkViewID id)
	{
		if (!base.GetComponent<Life>().dead && ServerSettings.pvp)
		{
			GameObject gameObject = NetworkView.Find(id).gameObject;
			if (gameObject != null)
			{
				gameObject.GetComponent<Vehicle>().damage((int)((float)MeleeStats.getDamage(base.GetComponent<Clothes>().item) * (1f + base.GetComponent<Skills>().warrior() * 0.4f) * 0.2f));
			}
		}
	}

	public override void tick()
	{
		if (this.swinging && Time.realtimeSinceStartup - this.lastSwing > 0.1f)
		{
			Viewmodel.offset_x = UnityEngine.Random.Range(-0.02f, 0.02f);
			Viewmodel.offset_y = UnityEngine.Random.Range(-0.02f, 0.02f);
			Viewmodel.offset_z = UnityEngine.Random.Range(-0.02f, 0.02f);
			this.lastSwing = Time.realtimeSinceStartup;
			NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/use"), Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.25f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
			Physics.Raycast(Camera.main.transform.position + (Camera.main.transform.forward * -0.5f), Camera.main.transform.forward, out Chainsaw.hit, MeleeStats.getRange(Equipment.id) + 0.5f, RayMasks.DAMAGE);
			if (Chainsaw.hit.collider != null)
			{
				if (Chainsaw.hit.point.y < Ocean.level)
				{
					NetworkSounds.askSound("Sounds/Impacts/rock", Chainsaw.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/bubbles", Chainsaw.hit.point + (Chainsaw.hit.normal * 0.05f), Quaternion.LookRotation(Chainsaw.hit.normal), -1f);
				}
				else if (Chainsaw.hit.collider.gameObject.name == "ground" || Chainsaw.hit.collider.material.name.ToLower() == "rock (instance)" || Chainsaw.hit.collider.material.name.ToLower() == "ground (instance)")
				{
					NetworkSounds.askSound("Sounds/Impacts/rock", Chainsaw.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/rock", Chainsaw.hit.point + (Chainsaw.hit.normal * 0.05f), Quaternion.LookRotation(Chainsaw.hit.normal), -1f);
				}
				else if (Chainsaw.hit.collider.material.name.ToLower() == "cloth (instance)")
				{
					NetworkSounds.askSound("Sounds/Impacts/concrete", Chainsaw.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/cloth", Chainsaw.hit.point + (Chainsaw.hit.normal * 0.05f), Quaternion.LookRotation(Chainsaw.hit.normal), -1f);
				}
				else if (Chainsaw.hit.collider.material.name.ToLower() == "wood (instance)")
				{
					NetworkSounds.askSound("Sounds/Impacts/wood", Chainsaw.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/splinters", Chainsaw.hit.point + (Chainsaw.hit.normal * 0.05f), Quaternion.LookRotation(Chainsaw.hit.normal), -1f);
				}
				else if (Chainsaw.hit.collider.material.name.ToLower() == "tile (instance)")
				{
					NetworkSounds.askSound("Sounds/Impacts/concrete", Chainsaw.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/tile", Chainsaw.hit.point + (Chainsaw.hit.normal * 0.05f), Quaternion.LookRotation(Chainsaw.hit.normal), -1f);
				}
				else if (Chainsaw.hit.collider.material.name.ToLower() == "concrete (instance)")
				{
					NetworkSounds.askSound("Sounds/Impacts/concrete", Chainsaw.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/concrete", Chainsaw.hit.point + (Chainsaw.hit.normal * 0.05f), Quaternion.LookRotation(Chainsaw.hit.normal), -1f);
				}
				else if (Chainsaw.hit.collider.material.name.ToLower() == "metal (instance)" || Chainsaw.hit.collider.material.name.ToLower() == "iron (instance)")
				{
					NetworkSounds.askSound("Sounds/Impacts/metal", Chainsaw.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/sparks", Chainsaw.hit.point + (Chainsaw.hit.normal * 0.05f), Quaternion.LookRotation(Chainsaw.hit.normal), -1f);
				}
				else if (Chainsaw.hit.collider.material.name.ToLower() == "flesh (instance)")
				{
					NetworkSounds.askSound("Sounds/Impacts/flesh", Chainsaw.hit.point, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/flesh", Chainsaw.hit.point + (Chainsaw.hit.normal * 0.05f), Quaternion.LookRotation(Chainsaw.hit.normal), -1f);
				}
				if (Chainsaw.hit.collider.tag == "Resource")
				{
					if (Chainsaw.hit.collider.name.Substring(0, 4) != "rock")
					{
						//HUDGame.lastHitmarker = Time.realtimeSinceStartup;
						int num = int.Parse(Chainsaw.hit.collider.transform.parent.name.Substring(Chainsaw.hit.collider.transform.parent.name.IndexOf("_") + 1));
						if (!Network.isServer)
						{
							base.networkView.RPC("swingResource", RPCMode.Server, new object[] { num });
						}
						else
						{
							this.swingResource(num);
						}
					}
				}
				else if (Chainsaw.hit.collider.tag == "Barricade")
				{
					//HUDGame.lastStructmarker = Time.realtimeSinceStartup;
					if (!Network.isServer)
					{
						base.networkView.RPC("swingBarricade", RPCMode.Server, new object[] { Chainsaw.hit.collider.transform.parent.position });
					}
					else
					{
						this.swingBarricade(Chainsaw.hit.collider.transform.parent.position);
					}
				}
				else if (Chainsaw.hit.collider.tag == "Structure")
				{
					//HUDGame.lastStructmarker = Time.realtimeSinceStartup;
					if (!Network.isServer)
					{
						base.networkView.RPC("swingStructure", RPCMode.Server, new object[] { Chainsaw.hit.collider.transform.parent.position });
					}
					else
					{
						this.swingStructure(Chainsaw.hit.collider.transform.parent.position);
					}
				}
				else if (Chainsaw.hit.collider.tag == "Enemy" && ServerSettings.pvp)
				{
					int limb = OwnerFinder.getLimb(Chainsaw.hit.collider.gameObject);
					GameObject owner = OwnerFinder.getOwner(Chainsaw.hit.collider.gameObject);
					if (owner != null && owner.GetComponent<Player>().action != 4 && (PlayerSettings.friend == string.Empty || PlayerSettings.friendHash != owner.GetComponent<Player>().owner.friend))
					{
						//HUDGame.lastHitmarker = Time.realtimeSinceStartup;
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
				else if (Chainsaw.hit.collider.tag == "Animal")
				{
					int limb1 = OwnerFinder.getLimb(Chainsaw.hit.collider.gameObject);
					GameObject gameObject = OwnerFinder.getOwner(Chainsaw.hit.collider.gameObject);
					if (gameObject != null && !gameObject.GetComponent<AI>().dead)
					{
						//HUDGame.lastHitmarker = Time.realtimeSinceStartup;
						if (!Network.isServer)
						{
							base.networkView.RPC("swingAnimal", RPCMode.Server, new object[] { gameObject.networkView.viewID, limb1 });
						}
						else
						{
							this.swingAnimal(gameObject.networkView.viewID, limb1);
						}
					}
				}
				else if (Chainsaw.hit.collider.tag == "Vehicle" && Chainsaw.hit.collider.GetComponent<Vehicle>().health > 0 && ServerSettings.pvp)
				{
					//HUDGame.lastHitmarker = Time.realtimeSinceStartup;
					if (!Network.isServer)
					{
						base.networkView.RPC("swingVehicle", RPCMode.Server, new object[] { Chainsaw.hit.collider.networkView.viewID });
					}
					else
					{
						this.swingVehicle(Chainsaw.hit.collider.networkView.viewID);
					}
				}
			}
		}
	}
}