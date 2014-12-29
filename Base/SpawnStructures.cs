using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStructures : MonoBehaviour
{
	private static SpawnStructures tool;

	public static GameObject model;

	public static List<ServerStructure> structures;

	public static List<GameObject> models;

	public SpawnStructures()
	{
	}

	[RPC]
	public void askAllStructures(NetworkPlayer player)
	{
		for (int i = 0; i < SpawnStructures.structures.Count; i++)
		{
			ServerStructure item = SpawnStructures.structures[i];
			if (player != Network.player)
			{
				base.networkView.RPC("createStructure", player, new object[] { item.id, item.position, item.rotation });
			}
			else
			{
				this.createStructurePleaseStopKuniiAlsoLetMeKnowIfYouWantToHelpWithAnticheatInVersion3(item.id, item.position, item.rotation);
			}
		}
	}

	[RPC]
	public void createStructure(int id, Vector3 position, int rotation, NetworkMessageInfo info)
	{
		if (info.sender.ToString() == "0" || info.sender.ToString() == "-1")
		{
			this.createStructurePleaseStopKuniiAlsoLetMeKnowIfYouWantToHelpWithAnticheatInVersion3(id, position, rotation);
		}
	}

	public void createStructurePleaseStopKuniiAlsoLetMeKnowIfYouWantToHelpWithAnticheatInVersion3(int id, Vector3 position, int rotation)
	{
		GameObject str = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Structures/", id)));
		str.name = id.ToString();
		str.transform.parent = SpawnStructures.model.transform;
		str.transform.position = position;
		str.transform.rotation = Quaternion.Euler(0f, (float)rotation, 0f);
		SpawnStructures.models.Add(str);
		if (StructureStats.isFoundation(id))
		{
			Ground.clear(position, 5);
		}
	}

	public static void damage(Vector3 position, int amount)
	{
		int num = 0;
		while (num < SpawnStructures.structures.Count)
		{
			if (SpawnStructures.structures[num].position != position)
			{
				num++;
			}
			else
			{
				ServerStructure item = SpawnStructures.structures[num];
				item.health = item.health - amount;
				if (SpawnStructures.structures[num].health <= 0)
				{
					NetworkEffects.askEffect("Effects/debrisWood", position, Quaternion.Euler(-90f, 0f, 0f), -1f);
					NetworkSounds.askSound("Sounds/Barricades/debrisWood", position, 1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
					SpawnStructures.tool.networkView.RPC("destroyStructure", RPCMode.All, new object[] { num });
				}
				break;
			}
		}
	}

	[RPC]
	public void destroyStructure(int index)
	{
		if (SpawnStructures.models.Count > 0)
		{
			UnityEngine.Object.Destroy(SpawnStructures.models[index]);
			SpawnStructures.models.RemoveAt(index);
		}
		if (Network.isServer)
		{
			SpawnStructures.structures.RemoveAt(index);
		}
	}

	public void onReady()
	{
		SpawnStructures.tool = this;
		SpawnStructures.model = GameObject.Find(Application.loadedLevelName).transform.FindChild("structures").gameObject;
		SpawnStructures.structures = new List<ServerStructure>();
		SpawnStructures.models = new List<GameObject>();
		if (!Network.isServer)
		{
			base.networkView.RPC("askAllStructures", RPCMode.Server, new object[] { Network.player });
		}
		else
		{
			string str = Savedata.loadStructures();
			if (str != string.Empty)
			{
				string[] strArrays = Packer.unpack(str, ';');
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					string[] strArrays1 = Packer.unpack(strArrays[i], ':');
					SpawnStructures.structures.Add(
						new ServerStructure(
						int.Parse(strArrays1[0]), 
						int.Parse(strArrays1[1]), 
						strArrays1[2], 
						new Vector3(
							float.Parse(strArrays1[3]), 
							float.Parse(strArrays1[4]), 
							float.Parse(strArrays1[5])), 
						int.Parse(strArrays1[6]))
					);
					
					this.createStructurePleaseStopKuniiAlsoLetMeKnowIfYouWantToHelpWithAnticheatInVersion3(
						SpawnStructures.structures[SpawnStructures.structures.Count - 1].id,
						SpawnStructures.structures[SpawnStructures.structures.Count - 1].position,
						SpawnStructures.structures[SpawnStructures.structures.Count - 1].rotation
					);
				}
			}
		}
	}

	public static void placeStructure(int id, Vector3 position, int rotation, string state) {
		SpawnStructures.structures.Add(new ServerStructure(id, StructureStats.getHealth(id), state, position, rotation));
		SpawnStructures.tool.networkView.RPC("createStructure", RPCMode.All, new object[] { id, position, rotation });
	}

	public static void save() {
		string empty = string.Empty;
		for (int i = 0; i < SpawnStructures.structures.Count; i++)
		{
			ServerStructure item = SpawnStructures.structures[i];
			empty = string.Concat(empty, item.id, ":");
			empty = string.Concat(empty, item.health, ":");
			empty = string.Concat(empty, item.state, ":");
			empty = string.Concat(empty, Mathf.Floor(item.position.x * 100f) / 100f, ":");
			empty = string.Concat(empty, Mathf.Floor(item.position.y * 100f) / 100f, ":");
			empty = string.Concat(empty, Mathf.Floor(item.position.z * 100f) / 100f, ":");
			empty = string.Concat(empty, item.rotation, ":;");
		}
		Savedata.saveStructures(empty);
	}

	public void Start()
	{
		NetworkEvents.onReady += new NetworkEventDelegate(this.onReady);
	}
}