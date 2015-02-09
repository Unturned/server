using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
	public static GameObject ground;

	public static RaycastHit hit;

	private static float lastStep;

	private int footstep = -1;
	
	private static Dictionary<string, int> FootStepMap;

	static Footsteps()
	{
		Footsteps.lastStep = Single.MinValue;
	}

	public Footsteps()
	{
	}

	public int getFootstep(int max)
	{
		this.footstep = UnityEngine.Random.Range(0, max + 1);
		return this.footstep;
	}

	public void Start()
	{
		Footsteps.ground = null;
		Footsteps.lastStep = Single.MinValue;
	}

	public void Update()
	{
		// TODO: this is server...
		/*
		int num;
		Physics.Raycast(base.transform.position + new Vector3(0f, 0.5f, 0f), Vector3.down, out Footsteps.hit, 1f);
		if (Footsteps.hit.collider == null)
		{
			Footsteps.ground = null;
		}
		else
		{
			Footsteps.ground = Footsteps.hit.collider.gameObject;
		}
		if (Time.realtimeSinceStartup - Footsteps.lastStep > 1f / Movement.speed * 1.5f && Movement.isMoving && !Movement.isDriving && Stance.state != 2 && (Footsteps.ground != null && Movement.isGrounded && Footsteps.ground.name != "land" || Movement.isClimbing || Movement.isSwimming))
		{
			string empty = string.Empty;
			if (Movement.isSwimming)
			{
				empty = "Sounds/Footsteps/swim";
			}
			else if (base.transform.position.y < Ocean.level)
			{
				empty = "Sounds/Footsteps/splash";
			}
			else if (Movement.isClimbing)
			{
				empty = string.Concat("Sounds/Footsteps/wood_", this.getFootstep(5));
			}
			else if (Footsteps.ground.name != "ground")
			{
				string lower = Footsteps.hit.collider.material.name.ToLower();
				if (lower != null)
				{
					if (Footsteps.FootStepMap == null) {
						Dictionary<string, int> footStepMap = new Dictionary<string, int>();
						footStepMap.Add("cloth (instance)", 0 );
						footStepMap.Add("concrete (instance)", 1 );
						                footStepMap.Add("iron (instance)", 2 );
						                footStepMap.Add("metal (instance)", 3 );
						                footStepMap.Add("rock (instance)", 4 );
						                footStepMap.Add("tile (instance)", 5 );
						                footStepMap.Add("wood (instance)", 6 );
						                footStepMap.Add("ground (instance)", 7 );
						Footsteps.FootStepMap = footStepMap;
					}
					
					if (Footsteps.FootStepMap.TryGetValue(lower, out num)) {
						switch (num)
						{
							case 0:
							{
								empty = string.Concat("Sounds/Footsteps/road_", this.getFootstep(2));
								break;
							}
							case 1:
							{
								empty = string.Concat("Sounds/Footsteps/stone_", this.getFootstep(2));
								break;
							}
							case 2:
							{
								empty = string.Concat("Sounds/Footsteps/road_", this.getFootstep(2));
								break;
							}
							case 3:
							{
								empty = string.Concat("Sounds/Footsteps/metal_", this.getFootstep(1));
								break;
							}
							case 4:
							{
								empty = string.Concat("Sounds/Footsteps/stone_", this.getFootstep(2));
								break;
							}
							case 5:
							{
								empty = string.Concat("Sounds/Footsteps/tile_", this.getFootstep(2));
								break;
							}
							case 6:
							{
								empty = string.Concat("Sounds/Footsteps/wood_", this.getFootstep(5));
								break;
							}
							case 7:
							{
								empty = string.Concat("Sounds/Footsteps/gravel_", this.getFootstep(2));
								break;
							}
						}
					}
				}
			}
			else if (ServerSettings.map == 1 || ServerSettings.map == 2)
			{
				num = Ground.material(base.transform.position);
				switch (num)
				{
					case 0:
					{
						empty = string.Concat("Sounds/Footsteps/grass_", this.getFootstep(2));
						break;
					}
					case 1:
					{
						empty = string.Concat("Sounds/Footsteps/sand_", this.getFootstep(1));
						break;
					}
					case 2:
					{
						empty = string.Concat("Sounds/Footsteps/stone_", this.getFootstep(2));
						break;
					}
					case 3:
					{
						empty = string.Concat("Sounds/Footsteps/sand_", this.getFootstep(1));
						break;
					}
					case 4:
					{
						empty = string.Concat("Sounds/Footsteps/road_", this.getFootstep(2));
						break;
					}
					case 5:
					{
						empty = string.Concat("Sounds/Footsteps/gravel_", this.getFootstep(2));
						break;
					}
					case 6:
					{
						empty = string.Concat("Sounds/Footsteps/road_", this.getFootstep(2));
						break;
					}
					case 7:
					{
						empty = string.Concat("Sounds/Footsteps/grass_", this.getFootstep(2));
						break;
					}
					case 8:
					{
						empty = string.Concat("Sounds/Footsteps/grass_", this.getFootstep(2));
						break;
					}
					case 9:
					{
						empty = string.Concat("Sounds/Footsteps/grass_", this.getFootstep(2));
						break;
					}
					case 10:
					{
						empty = string.Concat("Sounds/Footsteps/sand_", this.getFootstep(1));
						break;
					}
				}
			}
			else if (ServerSettings.map == 3)
			{
				num = Ground.material(base.transform.position);
				switch (num)
				{
					case 0:
					{
						empty = string.Concat("Sounds/Footsteps/grass_", this.getFootstep(2));
						break;
					}
					case 1:
					{
						empty = string.Concat("Sounds/Footsteps/sand_", this.getFootstep(1));
						break;
					}
					case 2:
					{
						empty = string.Concat("Sounds/Footsteps/stone_", this.getFootstep(2));
						break;
					}
					case 3:
					{
						empty = string.Concat("Sounds/Footsteps/sand_", this.getFootstep(1));
						break;
					}
					case 4:
					{
						empty = string.Concat("Sounds/Footsteps/road_", this.getFootstep(2));
						break;
					}
					case 5:
					{
						empty = string.Concat("Sounds/Footsteps/gravel_", this.getFootstep(2));
						break;
					}
					case 6:
					{
						empty = string.Concat("Sounds/Footsteps/road_", this.getFootstep(2));
						break;
					}
					case 7:
					{
						empty = string.Concat("Sounds/Footsteps/gravel_", this.getFootstep(2));
						break;
					}
				}
			}
			Footsteps.lastStep = Time.realtimeSinceStartup;
			if (empty != string.Empty)
			{
				if (Stance.state != 0)
				{
					NetworkSounds.askSound(empty, base.transform.position, 0.07f * (1f - Player.skills.sneakybeaky()), UnityEngine.Random.Range(0.8f, 1.2f), 1f);
				}
				else
				{
					NetworkSounds.askSound(empty, base.transform.position, 0.15f * (1f - Player.skills.sneakybeaky()), UnityEngine.Random.Range(0.8f, 1.2f), 1f);
				}
			}
		}
		*/
	}
}