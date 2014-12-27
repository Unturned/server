using System;
using UnityEngine;

public class Repair : Useable
{
	private bool swinging;

	private float lastSwing;

	private static RaycastHit hit;

	public Repair()
	{
	}

	public override void equip()
	{
		Viewmodel.play("equip");
	}

	public override void startPrimary()
	{
		this.swinging = true;
		Equipment.model.transform.FindChild("bluefire").GetComponent<ParticleSystem>().Play();
		Viewmodel.play("start");
	}

	public override void stopPrimary()
	{
		this.swinging = false;
		Equipment.model.transform.FindChild("bluefire").GetComponent<ParticleSystem>().Stop();
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
	public void swingPlayer(string id, int limb)
	{
		if (!base.GetComponent<Life>().dead && ServerSettings.pvp)
		{
			NetworkUser userFromID = NetworkUserList.getUserFromID(id);
			if (userFromID != null && userFromID.model != null && userFromID.model != base.gameObject && !userFromID.model.GetComponent<Life>().dead && (base.GetComponent<Player>().owner.friend == string.Empty || base.GetComponent<Player>().owner.friend != userFromID.friend) && (userFromID.model.transform.position - base.transform.position).magnitude < 10f)
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
				userFromID.model.GetComponent<Life>().damage((int)damage, string.Concat(new string[] { "You were burned in the ", empty, " with the ", ItemName.getName(base.GetComponent<Clothes>().item).ToLower(), " by ", base.GetComponent<Player>().owner.name, "!" }));
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
	public void swingVehicle(NetworkViewID id)
	{
		if (!base.GetComponent<Life>().dead)
		{
			GameObject gameObject = NetworkView.Find(id).gameObject;
			if (gameObject != null && !gameObject.GetComponent<Vehicle>().exploded)
			{
				gameObject.GetComponent<Vehicle>().heal(1);
			}
		}
	}

	public override void tick()
	{
		if (this.swinging && Time.realtimeSinceStartup - this.lastSwing > 0.1f)
		{
			this.lastSwing = Time.realtimeSinceStartup;
			NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/use"), Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.25f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
			Physics.Raycast(Camera.main.transform.position + (Camera.main.transform.forward * -0.5f), Camera.main.transform.forward, out Repair.hit, MeleeStats.getRange(Equipment.id) + 0.5f, RayMasks.DAMAGE);
			if (Repair.hit.collider != null)
			{
				NetworkEffects.askEffect("Effects/repair", Repair.hit.point + (Repair.hit.normal * 0.05f), Quaternion.LookRotation(Repair.hit.normal), -1f);
				if (Repair.hit.collider.tag == "Enemy" && ServerSettings.pvp)
				{
					int limb = OwnerFinder.getLimb(Repair.hit.collider.gameObject);
					GameObject owner = OwnerFinder.getOwner(Repair.hit.collider.gameObject);
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
				else if (Repair.hit.collider.tag == "Animal")
				{
					int num = OwnerFinder.getLimb(Repair.hit.collider.gameObject);
					GameObject gameObject = OwnerFinder.getOwner(Repair.hit.collider.gameObject);
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
				else if (Repair.hit.collider.tag == "Vehicle" && Repair.hit.collider.GetComponent<Vehicle>().health < Repair.hit.collider.GetComponent<Vehicle>().maxHealth && !Repair.hit.collider.GetComponent<Vehicle>().exploded)
				{
					HUDGame.lastHitmarker = Time.realtimeSinceStartup;
					if (!Network.isServer)
					{
						base.networkView.RPC("swingVehicle", RPCMode.Server, new object[] { Repair.hit.collider.networkView.viewID });
					}
					else
					{
						this.swingVehicle(Repair.hit.collider.networkView.viewID);
					}
				}
			}
		}
	}
}