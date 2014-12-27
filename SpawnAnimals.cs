using System;
using UnityEngine;

public class SpawnAnimals : MonoBehaviour
{
	public static GameObject model;

	public SpawnAnimals()
	{
	}

	public static void attract(Vector3 position, float range)
	{
		for (int i = 0; i < SpawnAnimals.model.transform.FindChild("models").childCount; i++)
		{
			Transform child = SpawnAnimals.model.transform.FindChild("models").GetChild(i);
			if ((child.transform.position - position).magnitude < range)
			{
				child.GetComponent<AI>().attract(position);
			}
		}
	}

	public void onReady()
	{
		SpawnAnimals.model = GameObject.Find(Application.loadedLevelName).transform.FindChild("animals").gameObject;
		if (Network.isServer)
		{
			SpawnAnimals.reset();
		}
	}

	public static void reset()
	{
		Transform[] child = new Transform[SpawnAnimals.model.transform.FindChild("models").childCount];
		for (int i = 0; i < (int)child.Length; i++)
		{
			child[i] = SpawnAnimals.model.transform.FindChild("models").GetChild(i);
		}
		for (int j = 0; j < (int)child.Length; j++)
		{
			Network.RemoveRPCs(child[j].networkView.viewID);
			Network.Destroy(child[j].networkView.viewID);
		}
		int num = 0;
		for (int k = 0; k < SpawnAnimals.model.transform.FindChild("spawns").childCount; k++)
		{
			Transform transforms = SpawnAnimals.model.transform.FindChild("spawns").GetChild(k);
			Vector3 vector3 = transforms.transform.position + new Vector3(UnityEngine.Random.Range(-0.25f, 0.25f), 1f, UnityEngine.Random.Range(-0.25f, 0.25f));
			if (transforms.name == "animal")
			{
				if (ServerSettings.mode == 0 && UnityEngine.Random.@value > Loot.NORMAL_ANIMAL_CHANCE || ServerSettings.mode == 1 && UnityEngine.Random.@value > Loot.BAMBI_ANIMAL_CHANCE || ServerSettings.mode == 2 && UnityEngine.Random.@value > Loot.HARDCORE_ANIMAL_CHANCE || ServerSettings.mode == 3 && UnityEngine.Random.@value > Loot.GOLD_ANIMAL_CHANCE)
				{
					if ((double)UnityEngine.Random.@value <= 0.5)
					{
						Network.Instantiate(Resources.Load("Prefabs/Game/deer"), vector3, Quaternion.identity, 0);
					}
					else
					{
						Network.Instantiate(Resources.Load("Prefabs/Game/pig"), vector3, Quaternion.identity, 0);
					}
					num++;
				}
			}
			else if (ServerSettings.mode == 0 && UnityEngine.Random.@value > Loot.NORMAL_ZOMBIE_CHANCE || ServerSettings.mode == 1 && UnityEngine.Random.@value > Loot.BAMBI_ZOMBIE_CHANCE || ServerSettings.mode == 2 && UnityEngine.Random.@value > Loot.HARDCORE_ZOMBIE_CHANCE || ServerSettings.mode == 3 && UnityEngine.Random.@value > Loot.GOLD_ZOMBIE_CHANCE)
			{
				if (transforms.name == "civilianZombie")
				{
					Network.Instantiate(Resources.Load(string.Concat("Prefabs/Game/civilianZombie_", UnityEngine.Random.Range(0, 10))), vector3, Quaternion.identity, 0);
				}
				else if (transforms.name == "farmerZombie")
				{
					Network.Instantiate(Resources.Load(string.Concat("Prefabs/Game/farmerZombie_", UnityEngine.Random.Range(0, 2))), vector3, Quaternion.identity, 0);
				}
				else if (transforms.name == "firemanZombie")
				{
					Network.Instantiate(Resources.Load(string.Concat("Prefabs/Game/firemanZombie_", UnityEngine.Random.Range(0, 2))), vector3, Quaternion.identity, 0);
				}
				else if (transforms.name == "militaryZombie")
				{
					Network.Instantiate(Resources.Load(string.Concat("Prefabs/Game/militaryZombie_", UnityEngine.Random.Range(0, 2))), vector3, Quaternion.identity, 0);
				}
				else if (transforms.name == "policeZombie")
				{
					Network.Instantiate(Resources.Load(string.Concat("Prefabs/Game/policeZombie_", UnityEngine.Random.Range(0, 2))), vector3, Quaternion.identity, 0);
				}
				num++;
			}
		}
	}

	public static void respawn(GameObject animal)
	{
		Network.RemoveRPCs(animal.networkView.viewID);
		Network.Destroy(animal.networkView.viewID);
		SpawnAnimals.spawn();
	}

	public static void spawn()
	{
		Transform child = SpawnAnimals.model.transform.FindChild("spawns").GetChild(UnityEngine.Random.Range(0, SpawnAnimals.model.transform.FindChild("spawns").childCount));
		Vector3 vector3 = child.transform.position + new Vector3(UnityEngine.Random.Range(-0.25f, 0.25f), 1f, UnityEngine.Random.Range(-0.25f, 0.25f));
		if (child.name == "animal")
		{
			Network.Instantiate(Resources.Load("Prefabs/Game/deer"), vector3, Quaternion.identity, 0);
		}
		else if (child.name == "civilianZombie")
		{
			Network.Instantiate(Resources.Load(string.Concat("Prefabs/Game/civilianZombie_", UnityEngine.Random.Range(0, 10))), vector3, Quaternion.identity, 0);
		}
		else if (child.name == "farmerZombie")
		{
			Network.Instantiate(Resources.Load(string.Concat("Prefabs/Game/farmerZombie_", UnityEngine.Random.Range(0, 2))), vector3, Quaternion.identity, 0);
		}
		else if (child.name == "firemanZombie")
		{
			Network.Instantiate(Resources.Load(string.Concat("Prefabs/Game/firemanZombie_", UnityEngine.Random.Range(0, 2))), vector3, Quaternion.identity, 0);
		}
		else if (child.name == "militaryZombie")
		{
			Network.Instantiate(Resources.Load(string.Concat("Prefabs/Game/militaryZombie_", UnityEngine.Random.Range(0, 2))), vector3, Quaternion.identity, 0);
		}
		else if (child.name == "policeZombie")
		{
			Network.Instantiate(Resources.Load(string.Concat("Prefabs/Game/policeZombie_", UnityEngine.Random.Range(0, 2))), vector3, Quaternion.identity, 0);
		}
	}

	public void Start()
	{
		NetworkEvents.onReady += new NetworkEventDelegate(this.onReady);
	}
}