using System;
using UnityEngine;

public class Zombie : AI
{
	public readonly static int IDLES;

	public readonly static int ATTACKS;

	public readonly static int AGROS;

	private static NavMeshHit hit;

	public bool agro;

	public bool search;

	private GameObject target;

	private Vector3 moveTo = Vector3.zero;

	private GameObject destroy;

	private float lastServerAttack;

	private float lastClientAttack;

	private byte animset;

	static Zombie()
	{
		Zombie.IDLES = 7;
		Zombie.ATTACKS = 7;
		Zombie.AGROS = 7;
	}

	public Zombie()
	{
	}

	[RPC]
	public void askAgro(NetworkPlayer player)
	{
		if (!this.dead)
		{
			GameObject modelFromPlayer = NetworkUserList.getModelFromPlayer(player);
			if (modelFromPlayer != null && (this.target == null || modelFromPlayer != this.target && (modelFromPlayer.transform.position - base.transform.position).magnitude < (this.target.transform.position - base.transform.position).magnitude))
			{
				this.target = modelFromPlayer;
				if (this.search)
				{
					this.moveTo = Vector3.zero;
					base.networkView.RPC("tellSearch", RPCMode.All, new object[] { false });
				}
				if (!this.agro)
				{
					base.networkView.RPC("tellAgro", RPCMode.All, new object[] { true });
					this.tick();
					NetworkSounds.askSound(string.Concat("Sounds/Animals/Zombie/attack_", UnityEngine.Random.Range(0, Zombie.ATTACKS)), base.transform.position, 0.75f, UnityEngine.Random.Range(0.9f, 1.1f), 0.5f);
				}
			}
		}
	}

	[RPC]
	public void askAllAI(NetworkPlayer player)
	{
		if (player != Network.player)
		{
			NetworkView networkView = base.networkView;
			object[] objArray = new object[] { this.moving, this.agro, this.search, this.dead, null };
			objArray[4] = new byte[] { this.animset };
			networkView.RPC("tellAllAI", player, objArray);
		}
		else
		{
			this.tellAllAI(this.moving, this.agro, this.search, this.dead, new byte[] { this.animset });
		}
	}

	[RPC]
	public void askAttract(Vector3 setMoveTo)
	{
		if (!this.dead && !this.agro && (this.moveTo == Vector3.zero || !this.search || (setMoveTo - base.transform.position).magnitude < (this.moveTo - base.transform.position).magnitude))
		{
			this.moveTo = setMoveTo;
			this.tick();
			if (!this.search)
			{
				base.networkView.RPC("tellSearch", RPCMode.All, new object[] { true });
			}
		}
	}

	[RPC]
	public void askMaul(NetworkPlayer player)
	{
		if (!this.dead && this.target != null && this.destroy == null)
		{
			GameObject modelFromPlayer = NetworkUserList.getModelFromPlayer(player);
			if (modelFromPlayer != null && modelFromPlayer == this.target)
			{
				float armor = 10f * (1f - modelFromPlayer.GetComponent<Skills>().warrior() * 0.4f);
				if (modelFromPlayer.GetComponent<Clothes>().vest != -1)
				{
					armor = armor * ArmorStats.getArmor(modelFromPlayer.GetComponent<Clothes>().vest);
				}
				modelFromPlayer.GetComponent<Life>().damage((int)armor, "You were mauled by a zombie!", -20, "");
				if (ServerSettings.mode != 2)
				{
					modelFromPlayer.GetComponent<Life>().infect(4);
				}
				else
				{
					modelFromPlayer.GetComponent<Life>().infect(12);
				}
				base.networkView.RPC("swing", RPCMode.All, new object[0]);
				Transform transforms = base.transform;
				float single = modelFromPlayer.transform.position.z - base.transform.position.z;
				float single1 = modelFromPlayer.transform.position.x;
				Vector3 vector3 = base.transform.position;
				transforms.rotation = Quaternion.Euler(0f, 90f - Mathf.Atan2(single, single1 - vector3.x) * 57.29578f, 0f);
				NetworkSounds.askSound(string.Concat("Sounds/Animals/Zombie/attack_", UnityEngine.Random.Range(0, Zombie.ATTACKS)), base.transform.position, 0.75f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
				NetworkEffects.askEffect("Effects/flesh", modelFromPlayer.transform.position + Vector3.up, Quaternion.identity, -1f);
			}
		}
	}

	public override void attract(Vector3 point)
	{
		if (!this.dead && !this.agro)
		{
			if (!Network.isServer)
			{
				base.networkView.RPC("askAttract", RPCMode.Server, new object[] { point });
			}
			else
			{
				this.askAttract(point);
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
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("spine").collider);
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("spine").FindChild("neck").collider);
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("spine").FindChild("leftShoulder").FindChild("leftArmUpper").collider);
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("spine").FindChild("leftShoulder").FindChild("leftArmUpper").FindChild("leftArmLower").collider);
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("spine").FindChild("rightShoulder").FindChild("rightArmUpper").collider);
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("spine").FindChild("rightShoulder").FindChild("rightArmUpper").FindChild("rightArmLower").collider);
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("leftHip").FindChild("leftLegUpper").collider);
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("leftHip").FindChild("leftLegUpper").FindChild("leftLegLower").collider);
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("rightHip").FindChild("rightLegUpper").collider);
		UnityEngine.Object.Destroy(base.transform.FindChild("model").FindChild("character").FindChild("skeleton").FindChild("rightHip").FindChild("rightLegUpper").FindChild("rightLegLower").collider);
	}

	public override void setupClient()
	{
		base.Invoke("tick", UnityEngine.Random.@value);
		base.name = "zombie";
	}

	public override void setupServer()
	{
		if ((double)UnityEngine.Random.@value <= 0.8)
		{
			this.animset = (byte)UnityEngine.Random.Range(0, 4);
		}
		else
		{
			this.animset = (byte)UnityEngine.Random.Range(4, 6);
		}
	}

	[RPC]
	public void swing()
	{
		this.anim.play(string.Concat("attack_", this.animset));
	}

	[RPC]
	public void tellAgro(bool setAgro)
	{
		this.agro = setAgro;
	}

	[RPC]
	public void tellAllAI(bool setMoving, bool setAgro, bool setSearch, bool setDead, byte[] setAnimset)
	{
		this.animset = setAnimset[0];
		this.moving = setMoving;
		this.agro = setAgro;
		this.search = setSearch;
		this.dead = setDead;
		if (this.dead)
		{
			this.die();
		}
	}

	[RPC]
	public void tellSearch(bool setSearch)
	{
		this.search = setSearch;
	}

	public void tick()
	{
		if (!this.dead)
		{
			if (Player.model != null && Player.life != null && !Player.life.dead)
			{
				if (Mathf.Abs(base.transform.position.x - Player.model.transform.position.x) >= (float)Stance.range || Mathf.Abs(base.transform.position.z - Player.model.transform.position.z) >= (float)Stance.range || Mathf.Abs(base.transform.position.y - Player.model.transform.position.y) >= (float)(Stance.range / 2))
				{
					this.lastClientAttack = Time.realtimeSinceStartup;
				}
				else
				{
					if ((!this.agro && !this.search || (double)UnityEngine.Random.@value > 0.8) && RayMasks.isVisible(base.transform.position + Vector3.up, Player.model.transform.position + Vector3.up))
					{
						if (!Network.isServer)
						{
							base.networkView.RPC("askAgro", RPCMode.Server, new object[] { Network.player });
						}
						else
						{
							this.askAgro(Network.player);
						}
					}
					if (!(Movement.vehicle == null) || Mathf.Abs(base.transform.position.x - Player.model.transform.position.x) >= 1.25f || Mathf.Abs(base.transform.position.z - Player.model.transform.position.z) >= 1.25f || Mathf.Abs(base.transform.position.y - Player.model.transform.position.y) >= 2f)
					{
						this.lastClientAttack = Time.realtimeSinceStartup;
					}
					else if (Time.realtimeSinceStartup - this.lastClientAttack > 0.5f)
					{
						this.lastClientAttack = Time.realtimeSinceStartup;
						if (RayMasks.isVisible(base.transform.position + Vector3.up, Player.model.transform.position + Vector3.up))
						{
							if (!Network.isServer)
							{
								base.networkView.RPC("askMaul", RPCMode.Server, new object[] { Network.player });
							}
							else
							{
								this.askMaul(Network.player);
							}
						}
					}
				}
			}
			if (Network.isServer)
			{
				if (!this.agent.isOnOffMeshLink)
				{
					if (!this.agro && !this.search)
					{
						if ((double)UnityEngine.Random.@value > 0.975)
						{
							NetworkSounds.askSound(string.Concat("Sounds/Animals/Zombie/idle_", UnityEngine.Random.Range(0, Zombie.IDLES)), base.transform.position, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.5f);
						}
					}
					else if ((double)UnityEngine.Random.@value > 0.975)
					{
						NetworkSounds.askSound(string.Concat("Sounds/Animals/Zombie/agro_", UnityEngine.Random.Range(0, Zombie.AGROS)), base.transform.position, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.5f);
					}
					if (this.agro && this.target != null)
					{
						if (Mathf.Abs(base.transform.position.x - this.target.transform.position.x) > 64f || Mathf.Abs(base.transform.position.z - this.target.transform.position.z) > 64f || this.target.GetComponent<Life>().dead)
						{
							if (this.agent.enabled)
							{
								this.agent.Stop();
								this.agent.enabled = false;
							}
							this.target = null;
							this.destroy = null;
							if (this.agro)
							{
								base.networkView.RPC("tellAgro", RPCMode.All, new object[] { false });
							}
						}
						else if (this.destroy == null)
						{
							if (!this.agent.enabled)
							{
								this.agent.enabled = true;
							}
							this.agent.destination = this.target.transform.position + this.target.GetComponent<Player>().prediction;
							if (Mathf.Abs(this.agent.destination.x - base.transform.position.x) >= 0.75f || Mathf.Abs(this.agent.destination.z - base.transform.position.z) >= 0.75f)
							{
								if (!this.moving)
								{
									base.networkView.RPC("tellMoving", RPCMode.All, new object[] { true });
								}
								if (this.target.GetComponent<Player>().vehicle == null)
								{
									Collider[] colliderArray = Physics.OverlapSphere(base.transform.position, 2f, RayMasks.BUILT);
									if ((int)colliderArray.Length > 0)
									{
										for (int i = 0; i < (int)colliderArray.Length; i++)
										{
											if (!BarricadeStats.getMaulable(int.Parse(colliderArray[i].transform.parent.name)))
											{
												if (BarricadeStats.getBarrier(int.Parse(colliderArray[i].transform.parent.name)))
												{
													this.destroy = colliderArray[i].gameObject;
												}
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
											else
											{
												this.destroy = colliderArray[i].gameObject;
											}
										}
									}
								}
								else
								{
									this.destroy = this.target.GetComponent<Player>().vehicle.gameObject;
								}
							}
							else
							{
								if (this.moving)
								{
									base.networkView.RPC("tellMoving", RPCMode.All, new object[] { false });
								}
								if (this.search)
								{
									base.networkView.RPC("tellSearch", RPCMode.All, new object[] { false });
								}
							}
						}
						else
						{
							if (!this.agent.enabled)
							{
								this.agent.enabled = true;
							}
							this.agent.destination = this.destroy.transform.position;
							if (this.destroy.tag == "Barricade")
							{
								if (Time.realtimeSinceStartup - this.lastServerAttack > 0.5f)
								{
									this.lastServerAttack = Time.realtimeSinceStartup;
									if (RayMasks.isVisible(base.transform.position + Vector3.up, this.destroy.transform.position + Vector3.up))
									{
										SpawnBarricades.damage(this.destroy.transform.parent.position, 10);
										base.networkView.RPC("swing", RPCMode.All, new object[0]);
										Transform transforms = base.transform;
										Vector3 vector3 = this.destroy.transform.position;
										float single = vector3.z - base.transform.position.z;
										float single1 = this.destroy.transform.position.x;
										Vector3 vector31 = base.transform.position;
										transforms.rotation = Quaternion.Euler(0f, 90f - Mathf.Atan2(single, single1 - vector31.x) * 57.29578f, 0f);
										NetworkSounds.askSound(string.Concat("Sounds/Animals/Zombie/attack_", UnityEngine.Random.Range(0, Zombie.ATTACKS)), base.transform.position, 0.75f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
									}
								}
								if (this.moving)
								{
									base.networkView.RPC("tellMoving", RPCMode.All, new object[] { false });
								}
							}
							else if (this.destroy.tag == "Structure")
							{
								if (Time.realtimeSinceStartup - this.lastServerAttack > 0.5f)
								{
									this.lastServerAttack = Time.realtimeSinceStartup;
                                    // FIXME: zombie can't destroy
									//SpawnStructures.damage(this.destroy.transform.parent.position, 10);
									base.networkView.RPC("swing", RPCMode.All, new object[0]);
									Transform transforms1 = base.transform;
									Vector3 vector32 = this.destroy.transform.position;
									float single2 = vector32.z - base.transform.position.z;
									float single3 = this.destroy.transform.position.x;
									Vector3 vector33 = base.transform.position;
									transforms1.rotation = Quaternion.Euler(0f, 90f - Mathf.Atan2(single2, single3 - vector33.x) * 57.29578f, 0f);
									NetworkSounds.askSound(string.Concat("Sounds/Animals/Zombie/attack_", UnityEngine.Random.Range(0, Zombie.ATTACKS)), base.transform.position, 0.75f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
								}
								if (this.moving)
								{
									base.networkView.RPC("tellMoving", RPCMode.All, new object[] { false });
								}
							}
							else if (this.destroy.tag == "Vehicle")
							{
								if (Mathf.Abs(base.transform.position.x - this.destroy.transform.position.x) > 6f || Mathf.Abs(base.transform.position.y - this.destroy.transform.position.y) > 6f || Mathf.Abs(base.transform.position.z - this.destroy.transform.position.z) > 6f || this.target == null || this.target.GetComponent<Player>().vehicle == null)
								{
									this.destroy = null;
								}
								else
								{
									if (Time.realtimeSinceStartup - this.lastServerAttack > 0.5f)
									{
										this.lastServerAttack = Time.realtimeSinceStartup;
										this.destroy.GetComponent<Vehicle>().damage(2);
										base.networkView.RPC("swing", RPCMode.All, new object[0]);
										Transform transforms2 = base.transform;
										Vector3 vector34 = this.destroy.transform.position;
										float single4 = vector34.z - base.transform.position.z;
										float single5 = this.destroy.transform.position.x;
										Vector3 vector35 = base.transform.position;
										transforms2.rotation = Quaternion.Euler(0f, 90f - Mathf.Atan2(single4, single5 - vector35.x) * 57.29578f, 0f);
										if (this.destroy.GetComponent<Vehicle>().health == 0)
										{
											this.destroy = null;
										}
										NetworkSounds.askSound(string.Concat("Sounds/Animals/Zombie/attack_", UnityEngine.Random.Range(0, Zombie.ATTACKS)), base.transform.position, 0.75f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
									}
									if (this.moving)
									{
										base.networkView.RPC("tellMoving", RPCMode.All, new object[] { false });
									}
								}
							}
						}
						base.Invoke("tick", UnityEngine.Random.Range(0.25f, 0.35f));
					}
					else if (!this.search || !(this.moveTo != Vector3.zero))
					{
						if (this.agent.enabled)
						{
							this.agent.Stop();
							this.agent.enabled = false;
						}
						if (this.target != null)
						{
							this.target = null;
							if (this.agro)
							{
								base.networkView.RPC("tellAgro", RPCMode.All, new object[] { false });
							}
						}
						if (this.moveTo != Vector3.zero)
						{
							this.moveTo = Vector3.zero;
							if (this.search)
							{
								base.networkView.RPC("tellSearch", RPCMode.All, new object[] { false });
							}
						}
						this.destroy = null;
						if (this.moving)
						{
							base.networkView.RPC("tellMoving", RPCMode.All, new object[] { false });
						}
						base.Invoke("tick", UnityEngine.Random.Range(2.2f, 2.6f));
					}
					else
					{
						if (this.destroy == null)
						{
							if (!this.agent.enabled)
							{
								this.agent.enabled = true;
							}
							this.agent.destination = this.moveTo;
							if (Mathf.Abs(this.agent.destination.x - base.transform.position.x) >= 0.75f || Mathf.Abs(this.agent.destination.z - base.transform.position.z) >= 0.75f)
							{
								if (!this.moving)
								{
									base.networkView.RPC("tellMoving", RPCMode.All, new object[] { true });
								}
								Collider[] colliderArray1 = Physics.OverlapSphere(base.transform.position, 2f, RayMasks.BUILT);
								if ((int)colliderArray1.Length > 0)
								{
									for (int j = 0; j < (int)colliderArray1.Length; j++)
									{
										if (!BarricadeStats.getMaulable(int.Parse(colliderArray1[j].transform.parent.name)))
										{
											if (BarricadeStats.getBarrier(int.Parse(colliderArray1[j].transform.parent.name)))
											{
												this.destroy = colliderArray1[j].gameObject;
											}
											if (BarricadeStats.getDamage(int.Parse(colliderArray1[j].transform.parent.name)) != 0 && (colliderArray1[j].transform.parent.name != "16009" && colliderArray1[j].transform.parent.name != "16021" || colliderArray1[j].GetComponent<ElectricTrap>().powered))
											{
												base.damage(BarricadeStats.getDamage(int.Parse(colliderArray1[j].transform.parent.name)));
												SpawnBarricades.damage(colliderArray1[j].transform.parent.position, 10);
												NetworkSounds.askSound("Sounds/Impacts/flesh", base.transform.position + Vector3.up, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
												NetworkEffects.askEffect("Effects/flesh", base.transform.position + Vector3.up, Quaternion.identity, -1f);
											}
											else if (ExplosiveStats.getDamage(int.Parse(colliderArray1[j].transform.parent.name)) != 0)
											{
												InteractionInterface.sendExplosiveTrap(colliderArray1[j].transform.parent.position);
											}
										}
										else
										{
											this.destroy = colliderArray1[j].gameObject;
										}
									}
								}
							}
							else
							{
								this.moveTo = Vector3.zero;
								if (this.moving)
								{
									base.networkView.RPC("tellMoving", RPCMode.All, new object[] { false });
								}
								if (this.search)
								{
									base.networkView.RPC("tellSearch", RPCMode.All, new object[] { false });
								}
							}
						}
						else
						{
							if (!this.agent.enabled)
							{
								this.agent.enabled = true;
							}
							this.agent.destination = this.destroy.transform.position;
							if (this.destroy.tag == "Barricade")
							{
								if (Time.realtimeSinceStartup - this.lastServerAttack > 0.5f)
								{
									this.lastServerAttack = Time.realtimeSinceStartup;
									if (RayMasks.isVisible(base.transform.position + Vector3.up, this.destroy.transform.position + Vector3.up))
									{
										SpawnBarricades.damage(this.destroy.transform.parent.position, 10);
										base.networkView.RPC("swing", RPCMode.All, new object[0]);
										Transform transforms3 = base.transform;
										Vector3 vector36 = this.destroy.transform.position;
										float single6 = vector36.z - base.transform.position.z;
										float single7 = this.destroy.transform.position.x;
										Vector3 vector37 = base.transform.position;
										transforms3.rotation = Quaternion.Euler(0f, 90f - Mathf.Atan2(single6, single7 - vector37.x) * 57.29578f, 0f);
										NetworkSounds.askSound(string.Concat("Sounds/Animals/Zombie/attack_", UnityEngine.Random.Range(0, Zombie.ATTACKS)), base.transform.position, 0.75f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
									}
								}
								if (this.moving)
								{
									base.networkView.RPC("tellMoving", RPCMode.All, new object[] { false });
								}
							}
							else if (this.destroy.tag == "Structure")
							{
								if (Time.realtimeSinceStartup - this.lastServerAttack > 0.5f)
								{
									this.lastServerAttack = Time.realtimeSinceStartup;
                                    // FIXME: zombie can't destroy
									//SpawnStructures.damage(this.destroy.transform.parent.position, 10);
									base.networkView.RPC("swing", RPCMode.All, new object[0]);
									Transform transforms4 = base.transform;
									Vector3 vector38 = this.destroy.transform.position;
									float single8 = vector38.z - base.transform.position.z;
									float single9 = this.destroy.transform.position.x;
									Vector3 vector39 = base.transform.position;
									transforms4.rotation = Quaternion.Euler(0f, 90f - Mathf.Atan2(single8, single9 - vector39.x) * 57.29578f, 0f);
									NetworkSounds.askSound(string.Concat("Sounds/Animals/Zombie/attack_", UnityEngine.Random.Range(0, Zombie.ATTACKS)), base.transform.position, 0.75f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
								}
								if (this.moving)
								{
									base.networkView.RPC("tellMoving", RPCMode.All, new object[] { false });
								}
							}
							else if (this.destroy.tag == "Vehicle")
							{
								if (Mathf.Abs(base.transform.position.x - this.destroy.transform.position.x) > 6f || Mathf.Abs(base.transform.position.y - this.destroy.transform.position.y) > 6f || Mathf.Abs(base.transform.position.z - this.destroy.transform.position.z) > 6f || this.target == null || this.target.GetComponent<Player>().vehicle == null)
								{
									this.destroy = null;
								}
								else
								{
									if (Time.realtimeSinceStartup - this.lastServerAttack > 0.5f)
									{
										this.lastServerAttack = Time.realtimeSinceStartup;
										this.destroy.GetComponent<Vehicle>().damage(2);
										base.networkView.RPC("swing", RPCMode.All, new object[0]);
										Transform transforms5 = base.transform;
										Vector3 vector310 = this.destroy.transform.position;
										float single10 = vector310.z - base.transform.position.z;
										float single11 = this.destroy.transform.position.x;
										Vector3 vector311 = base.transform.position;
										transforms5.rotation = Quaternion.Euler(0f, 90f - Mathf.Atan2(single10, single11 - vector311.x) * 57.29578f, 0f);
										if (this.destroy.GetComponent<Vehicle>().health == 0)
										{
											this.destroy = null;
										}
										NetworkSounds.askSound(string.Concat("Sounds/Animals/Zombie/attack_", UnityEngine.Random.Range(0, Zombie.ATTACKS)), base.transform.position, 0.75f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
									}
									if (this.moving)
									{
										base.networkView.RPC("tellMoving", RPCMode.All, new object[] { false });
									}
								}
							}
						}
						base.Invoke("tick", UnityEngine.Random.Range(0.25f, 0.35f));
					}
				}
				else
				{
					base.traverse();
					base.Invoke("tick", 0.03f);
				}
			}
			else if (this.agro || this.search)
			{
				base.Invoke("tick", UnityEngine.Random.Range(0.25f, 0.35f));
			}
			else
			{
				base.Invoke("tick", UnityEngine.Random.Range(2.5f, 2.6f));
			}
		}
		if (this.dead)
		{
			this.anim.play("dead");
			this.anim.stance(string.Empty);
		}
		else if (!this.moving)
		{
			this.anim.stance(string.Concat("idle_", this.animset));
		}
		else
		{
			this.anim.stance(string.Concat("move_", this.animset));
		}
	}
}