using System;
using UnityEngine;

public class SpawnResources : MonoBehaviour
{
	private static SpawnResources tool;

	public static GameObject model;

	public static int[] health;

	public SpawnResources()
	{
	}

	public static void chop(int index, int amount, GameObject killer)
	{
		if (SpawnResources.health[index] > 0)
		{
			SpawnResources.health[index] = SpawnResources.health[index] - amount;
			if (SpawnResources.health[index] <= 0)
			{
				if (killer != null)
				{
					killer.GetComponent<Skills>().learn(UnityEngine.Random.Range(2, 5));
					if (!killer.networkView.isMine)
					{
						killer.networkView.RPC("collectedResource", killer.networkView.owner, new object[0]);
					}
					else
					{
						killer.GetComponent<Player>().collectedResource();
					}
				}
				NetworkEffects.askEffect(string.Concat("Effects/", SpawnResources.model.transform.GetChild(index).GetChild(0).name), SpawnResources.model.transform.GetChild(index).position + new Vector3(0f, 2f, 0f), Quaternion.Euler(-90f, 0f, 0f), -1f);
				string child = SpawnResources.model.transform.GetChild(index).GetChild(0).name;
				Vector3 vector3 = SpawnResources.model.transform.GetChild(index).position;
				SpawnResources.tool.delete(index);
				SpawnResources.tool.networkView.RPC("delete", RPCMode.OthersBuffered, new object[] { index });
				if (child.Substring(0, 4) != "rock")
				{
					NetworkSounds.askSound("Sounds/Resources/timber", vector3, 1f, UnityEngine.Random.Range(0.9f, 1.1f), 2f);
					for (int i = 0; i < UnityEngine.Random.Range(6, 10); i++)
					{
						float single = UnityEngine.Random.Range(0f, 5.28f);
						float single1 = UnityEngine.Random.@value;
						if ((double)single1 > 0.975)
						{
							SpawnItems.dropItem(8018, vector3 + new Vector3(Mathf.Sin(single) * 2.5f, 1f, Mathf.Cos(single) * 2.5f));
						}
						else if ((double)single1 <= 0.5)
						{
							SpawnItems.dropItem(18003, vector3 + new Vector3(Mathf.Sin(single) * 2.5f, 1f, Mathf.Cos(single) * 2.5f));
						}
						else
						{
							SpawnItems.dropItem(18004, vector3 + new Vector3(Mathf.Sin(single) * 2.5f, 1f, Mathf.Cos(single) * 2.5f));
						}
					}
					if (child == "palm_0")
					{
						for (int j = 0; j < UnityEngine.Random.Range(2, 4); j++)
						{
							float single2 = UnityEngine.Random.Range(0f, 5.28f);
							SpawnItems.dropItem(14031, vector3 + new Vector3(Mathf.Sin(single2) * 2.5f, 1f, Mathf.Cos(single2) * 2.5f));
						}
					}
				}
				else
				{
					NetworkSounds.askSound("Sounds/Resources/smash", vector3, 1f, UnityEngine.Random.Range(0.9f, 1.1f), 2f);
					for (int k = 0; k < UnityEngine.Random.Range(6, 10); k++)
					{
						float single3 = UnityEngine.Random.Range(0f, 5.28f);
						if ((double)UnityEngine.Random.@value <= 0.75)
						{
							SpawnItems.dropItem(18019, vector3 + new Vector3(Mathf.Sin(single3) * 4f, 1f, Mathf.Cos(single3) * 4f));
						}
						else
						{
							SpawnItems.dropItem(18015, vector3 + new Vector3(Mathf.Sin(single3) * 4f, 1f, Mathf.Cos(single3) * 4f));
						}
					}
				}
			}
		}
	}

	[RPC]
	public void delete(int index)
	{
		GameObject child = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Environment/", SpawnResources.model.transform.GetChild(index).GetChild(0).name, "Stump")));
		GameObject gameObject = SpawnResources.model.transform.GetChild(index).GetChild(0).gameObject;
		child.name = "stump";
		child.transform.parent = SpawnResources.model.transform.GetChild(index);
		child.transform.localPosition = Vector3.zero;
		child.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
		UnityEngine.Object.Destroy(gameObject);
	}

	public void onReady()
	{
		SpawnResources.tool = this;
		SpawnResources.model = GameObject.Find(Application.loadedLevelName).transform.FindChild("resources").gameObject;
		SpawnResources.health = new int[SpawnResources.model.transform.childCount];
		for (int i = 0; i < (int)SpawnResources.health.Length; i++)
		{
			SpawnResources.health[i] = UnityEngine.Random.Range(100, 110);
		}
	}

	public void Start()
	{
		NetworkEvents.onReady += new NetworkEventDelegate(this.onReady);
	}
}