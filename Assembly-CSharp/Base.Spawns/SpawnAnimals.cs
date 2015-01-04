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

	public void onReady() {
		SpawnAnimals.model = GameObject.Find(Application.loadedLevelName).transform.FindChild("animals").gameObject;
		SpawnAnimals.reset();
	}

    private static bool SpawnAnimal(Vector3 location) {
        if (ServerSettings.mode == 0 && UnityEngine.Random.@value > Loot.NORMAL_ANIMAL_CHANCE || ServerSettings.mode == 1 && UnityEngine.Random.@value > Loot.BAMBI_ANIMAL_CHANCE || ServerSettings.mode == 2 && UnityEngine.Random.@value > Loot.HARDCORE_ANIMAL_CHANCE || ServerSettings.mode == 3 && UnityEngine.Random.@value > Loot.GOLD_ANIMAL_CHANCE)
        {
            if ((double)UnityEngine.Random.@value <= 0.5) { // 50% chance to deer or pig
                Network.Instantiate(Resources.Load("Prefabs/Game/deer"), location, Quaternion.identity, 0); 
            } else {
                Network.Instantiate(Resources.Load("Prefabs/Game/pig"), location, Quaternion.identity, 0);
            }
            return true;
        }

        return false;
    }

	public static void reset() {
        Debug.Log("Temporary disabled zombie spawning");
        return;

		Transform[] child = new Transform[SpawnAnimals.model.transform.FindChild("models").childCount];
		for (int i = 0; i < (int)child.Length; i++) {
			child[i] = SpawnAnimals.model.transform.FindChild("models").GetChild(i);
		}

        // Destroying
		for (int j = 0; j < (int)child.Length; j++)
		{
			Network.RemoveRPCs(child[j].networkView.viewID);
			Network.Destroy(child[j].networkView.viewID);
		}
		
        int spawnCount = 0;

		for (int k = 0; k < SpawnAnimals.model.transform.FindChild("spawns").childCount; k++)
		{
            Transform npc = SpawnAnimals.model.transform.FindChild("spawns").GetChild(k);
            Vector3 spawnLocation = npc.transform.position + new Vector3(UnityEngine.Random.Range(-0.25f, 0.25f), 1f, UnityEngine.Random.Range(-0.25f, 0.25f));
			
            if (npc.name == "animal") {
                if ( SpawnAnimal(spawnLocation) )
                    spawnCount++;
			} else if ( // Spawn chance
                ServerSettings.mode == 0 && UnityEngine.Random.@value > Loot.NORMAL_ZOMBIE_CHANCE || 
                ServerSettings.mode == 1 && UnityEngine.Random.@value > Loot.BAMBI_ZOMBIE_CHANCE || 
                ServerSettings.mode == 2 && UnityEngine.Random.@value > Loot.HARDCORE_ZOMBIE_CHANCE || 
                ServerSettings.mode == 3 && UnityEngine.Random.@value > Loot.GOLD_ZOMBIE_CHANCE)
			{
				if (npc.name == "civilianZombie")
				{
					Network.Instantiate(Resources.Load(string.Concat("Prefabs/Game/civilianZombie_", UnityEngine.Random.Range(0, 10))), spawnLocation, Quaternion.identity, 0);
				}
				else if (npc.name == "farmerZombie")
				{
					Network.Instantiate(Resources.Load(string.Concat("Prefabs/Game/farmerZombie_", UnityEngine.Random.Range(0, 2))), spawnLocation, Quaternion.identity, 0);
				}
				else if (npc.name == "firemanZombie")
				{
					Network.Instantiate(Resources.Load(string.Concat("Prefabs/Game/firemanZombie_", UnityEngine.Random.Range(0, 2))), spawnLocation, Quaternion.identity, 0);
				}
				else if (npc.name == "militaryZombie")
				{
					Network.Instantiate(Resources.Load(string.Concat("Prefabs/Game/militaryZombie_", UnityEngine.Random.Range(0, 2))), spawnLocation, Quaternion.identity, 0);
				}
				else if (npc.name == "policeZombie")
				{
					Network.Instantiate(Resources.Load(string.Concat("Prefabs/Game/policeZombie_", UnityEngine.Random.Range(0, 2))), spawnLocation, Quaternion.identity, 0);
				}
				spawnCount++;
			}
		}
	}

	public static void Respawn(GameObject npc)
	{
		Network.RemoveRPCs(npc.networkView.viewID);
		Network.Destroy(npc.networkView.viewID);
		SpawnAnimals.Spawn();
	}

	private static void Spawn() {
        Debug.Log("Spawn temporary disabled.");
        return;

		Transform child = SpawnAnimals.model.transform.FindChild("spawns").GetChild(
            UnityEngine.Random.Range(0, SpawnAnimals.model.transform.FindChild("spawns").childCount)
        );
		
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