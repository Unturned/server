using System;
using UnityEngine;

public class Animal : AI
{
	private static Vector3 run;

	public bool scared;

	private Vector3 escape = Vector3.zero;

	public Animal()
	{
	}

	public override void alert(bool setAngry)
	{
		if (!this.dead)
		{
			if (!Network.isServer)
			{
				base.networkView.RPC("askScare", RPCMode.Server, new object[] { Network.player });
			}
			else
			{
				this.askScare(Network.player);
			}
		}
	}

	[RPC]
	public void askAllAI(NetworkPlayer player)
	{
		if (player != Network.player)
		{
			base.networkView.RPC("tellAllAI", player, new object[] { this.moving, this.dead, this.scared });
		}
		else
		{
			this.tellAllAI(this.moving, this.dead, this.scared);
		}
	}

	[RPC]
	public void askScare(NetworkPlayer player)
	{
		if (!this.dead)
		{
			GameObject modelFromPlayer = NetworkUserList.getModelFromPlayer(player);
			if (modelFromPlayer != null)
			{
				if (base.transform.position.y >= Ocean.level)
				{
					Vector3 vector3 = base.transform.position;
					Vector3 vector31 = base.transform.position - modelFromPlayer.transform.position;
					Animal.run = vector3 + (vector31.normalized * (float)UnityEngine.Random.Range(20, 30));
				}
				else
				{
					Vector3 vector32 = base.transform.position;
					Vector3 vector33 = base.transform.position - modelFromPlayer.transform.position;
					Animal.run = vector32 + (vector33.normalized * (float)(-UnityEngine.Random.Range(20, 30)));
				}
				Animal.run = new Vector3(Animal.run.x, Ground.height(Animal.run) + 1f, Animal.run.z);
				this.escape = Animal.run;
				if (!this.scared)
				{
					base.networkView.RPC("tellScared", RPCMode.All, new object[] { true });
					this.tick();
				}
			}
		}
	}

	public override void attract(Vector3 point)
	{
		if (!this.dead)
		{
			if (!Network.isServer)
			{
				base.networkView.RPC("askScare", RPCMode.Server, new object[] { Network.player });
			}
			else
			{
				this.askScare(Network.player);
			}
		}
	}

	public override void die()
	{
		this.dead = true;
		if (this.agent != null)
		{
			UnityEngine.Object.Destroy(this.agent);
		}
		base.CancelInvoke("tick");
		this.anim.play(string.Empty);
		this.anim.stance("dead");
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("back").collider);
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("back").FindChild("frontLeft").collider);
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("back").FindChild("frontRight").collider);
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("back").FindChild("skull").collider);
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("backLeft").collider);
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("backRight").collider);
	}

	public override void setupClient()
	{
		base.Invoke("tick", UnityEngine.Random.@value);
		base.name = "animal";
	}

	public override void setupServer()
	{
	}

	[RPC]
	public void tellAllAI(bool setMoving, bool setDead, bool setScared)
	{
		this.moving = setMoving;
		this.dead = setDead;
		this.scared = setScared;
		if (this.dead)
		{
			this.die();
		}
	}

	[RPC]
	public void tellScared(bool setScared)
	{
		this.scared = setScared;
	}

	public void tick()
	{
		if (!this.dead)
		{
			if (Player.model != null && Player.life != null && !Player.life.dead && (!this.scared || (double)UnityEngine.Random.@value > 0.8) && Mathf.Abs(base.transform.position.x - Player.model.transform.position.x) < (float)Stance.range && Mathf.Abs(base.transform.position.z - Player.model.transform.position.z) < (float)Stance.range)
			{
				if (!Network.isServer)
				{
					base.networkView.RPC("askScare", RPCMode.Server, new object[] { Network.player });
				}
				else
				{
					this.askScare(Network.player);
				}
			}
			if (!Network.isServer)
			{
				base.Invoke("tick", UnityEngine.Random.Range(1.2f, 1.4f));
			}
			else if (this.agent.isOnOffMeshLink)
			{
				base.traverse();
				base.Invoke("tick", 0.03f);
			}
			else if (!this.scared)
			{
				if (this.agent.enabled)
				{
					this.agent.Stop();
					this.agent.enabled = false;
				}
				if (this.moving)
				{
					base.networkView.RPC("tellMoving", RPCMode.All, new object[] { false });
				}
				base.Invoke("tick", UnityEngine.Random.Range(1.2f, 1.4f));
			}
			else
			{
				if (!this.agent.enabled)
				{
					this.agent.enabled = true;
				}
				if (!this.moving)
				{
					base.networkView.RPC("tellMoving", RPCMode.All, new object[] { true });
				}
				this.agent.destination = this.escape;
				if (Mathf.Abs(this.agent.destination.x - base.transform.position.x) >= 0.75f || Mathf.Abs(this.agent.destination.z - base.transform.position.z) >= 0.75f)
				{
					Collider[] colliderArray = Physics.OverlapSphere(base.transform.position, 2f, RayMasks.BUILT);
					if ((int)colliderArray.Length > 0)
					{
						for (int i = 0; i < (int)colliderArray.Length; i++)
						{
							if (BarricadeStats.getDamage(int.Parse(colliderArray[i].transform.parent.name)) != 0 && (colliderArray[i].transform.parent.name != "16009" && colliderArray[i].transform.parent.name != "16021" || colliderArray[i].GetComponent<ElectricTrap>().powered))
							{
								base.damage(BarricadeStats.getDamage(int.Parse(colliderArray[i].transform.parent.name)));
								SpawnBarricades.damage(colliderArray[i].transform.parent.position, 10);
								NetworkSounds.askSound("Sounds/Impacts/flesh", base.transform.position + Vector3.up, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
								NetworkEffects.askEffect("Effects/flesh", base.transform.position + Vector3.up, Quaternion.identity, -1f);
							}
							else if (ExplosiveStats.getDamage(int.Parse(colliderArray[i].transform.parent.name)) != 0)
							{
								InteractionInterface.sendExplosiveTrap(colliderArray[i].transform.parent.position);
							}
						}
					}
				}
				else
				{
					base.networkView.RPC("tellScared", RPCMode.All, new object[] { false });
				}
				base.Invoke("tick", UnityEngine.Random.Range(0.25f, 0.35f));
			}
		}
		if (this.dead)
		{
			this.anim.stance("dead");
		}
		else if (!this.moving)
		{
			this.anim.stance("idle");
		}
		else
		{
			this.anim.stance("move");
		}
	}
}