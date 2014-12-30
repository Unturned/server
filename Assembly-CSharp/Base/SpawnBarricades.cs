using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBarricades : MonoBehaviour
{
	private static SpawnBarricades tool;

	public static GameObject model;

	public static BarricadesRegion[,] regions;

	public SpawnBarricades()
	{
	}

	[RPC]
	public void askAllBarricades(NetworkPlayer player, int x, int y)
	{
		for (int i = 0; i < SpawnBarricades.regions[x, y].barricades.Count; i++)
		{
			ServerBarricade item = SpawnBarricades.regions[x, y].barricades[i];
			if (player != Network.player)
			{
				base.networkView.RPC("createBarricade", player, new object[] { item.id, item.position, item.rotation });
			}
			else
			{
				this.createBarricadePleaseStopKuniiAlsoLetMeKnowIfYouWantToHelpWithAnticheatInVersion3(item.id, item.position, item.rotation);
			}
		}
	}

	[RPC]
	public void createBarricade(int id, Vector3 position, Vector3 rotation, NetworkMessageInfo info)
	{
		if (info.sender.ToString() == "0" || info.sender.ToString() == "-1")
		{
			this.createBarricadePleaseStopKuniiAlsoLetMeKnowIfYouWantToHelpWithAnticheatInVersion3(id, position, rotation);
		}
	}

	public void createBarricadePleaseStopKuniiAlsoLetMeKnowIfYouWantToHelpWithAnticheatInVersion3(int id, Vector3 position, Vector3 rotation)
	{
		Point2 region = NetworkRegions.getRegion(position);
		GameObject str = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Barricades/", id)));
		str.name = id.ToString();
		str.transform.parent = SpawnBarricades.model.transform;
		str.transform.position = position;
		str.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
		SpawnBarricades.regions[region.x, region.y].models.Add(str);
	}

	public static void damage(Vector3 position, int amount)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			ServerBarricade item = SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer];
			item.health = item.health - amount;
			if (SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].health <= 0)
			{
				NetworkEffects.askEffect(BarricadeStats.getEffect(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id), position, Quaternion.Euler(-90f, 0f, 0f), -1f);
				NetworkSounds.askSound(BarricadeStats.getSound(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id), position, 1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
				SpawnBarricades.tool.networkView.RPC("destroyBarricade", RPCMode.All, new object[] { position });
			}
		}
	}

	[RPC]
	public void destroyBarricade(Vector3 position)
	{
		int indexFromPositionServer;
		ClientItem[,] crateItems;
		Point2 region = NetworkRegions.getRegion(position);
		bool flag = false;
		int item = -1;
		Vector3 vector3 = Vector3.zero;
		if (Network.isServer)
		{
			indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
			if (SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id == 16019 || SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id == 16025 || SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id == 16023)
			{
				if (SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id != 16019)
				{
					string[] strArrays = Packer.unpack(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state, '\u005F');
					crateItems = InteractionInterface.getCrateItems(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id, Sneaky.expose(strArrays[2]));
				}
				else
				{
					crateItems = InteractionInterface.getCrateItems(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id, Sneaky.expose(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state));
				}
				for (int i = 0; i < 2; i++)
				{
					for (int j = 0; j < BarricadeStats.getCapacity(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id); j++)
					{
						if (!ItemStackable.getStackable(crateItems[i, j].id))
						{
							SpawnItems.drop(crateItems[i, j].id, crateItems[i, j].amount, crateItems[i, j].state, position);
						}
						else
						{
							for (int k = 0; k < crateItems[i, j].amount; k++)
							{
								SpawnItems.drop(crateItems[i, j].id, 1, crateItems[i, j].state, position);
							}
						}
					}
				}
			}
			else if (ExplosiveStats.getDamage(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id) != 0)
			{
				flag = true;
				item = SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id;
				if (SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id != 16015)
				{
					vector3 = position + Vector3.up;
					NetworkEffects.askEffect("Effects/grenade", position, Quaternion.Euler(-90f, 0f, 0f), -1f);
					NetworkSounds.askSoundMax("Sounds/Projectiles/grenade", position, 1f, UnityEngine.Random.Range(0.95f, 1.05f), 4f, 64f);
				}
				else
				{
					vector3 = position + SpawnBarricades.regions[region.x, region.y].models[indexFromPositionServer].transform.up;
					NetworkEffects.askEffect("Effects/bomb", position, Quaternion.Euler(-90f, 0f, 0f), -1f);
					NetworkSounds.askSoundMax("Sounds/Projectiles/bomb", position, 1f, UnityEngine.Random.Range(0.95f, 1.05f), 4f, 64f);
				}
			}
			if (indexFromPositionServer != -1 && indexFromPositionServer < SpawnBarricades.regions[region.x, region.y].barricades.Count)
			{
				SpawnBarricades.regions[region.x, region.y].barricades.RemoveAt(indexFromPositionServer);
			}
		}
		indexFromPositionServer = SpawnBarricades.getIndexFromPositionClient(region, position);
		if (indexFromPositionServer != -1 && indexFromPositionServer < SpawnBarricades.regions[region.x, region.y].models.Count)
		{
			UnityEngine.Object.Destroy(SpawnBarricades.regions[region.x, region.y].models[indexFromPositionServer]);
			SpawnBarricades.regions[region.x, region.y].models.RemoveAt(indexFromPositionServer);
		}
		if (flag)
		{
			ExplosionTool.explode(vector3, (float)ExplosiveStats.getRange(item), ExplosiveStats.getDamage(item));
		}
	}

	public static ServerBarricade[] getBarricades(Vector3 point, float range)
	{
		Point2 region = NetworkRegions.getRegion(point);
		List<ServerBarricade> serverBarricades = new List<ServerBarricade>();
		for (int i = 0; i < SpawnBarricades.regions[region.x, region.y].barricades.Count; i++)
		{
			if (Mathf.Abs(point.x - SpawnBarricades.regions[region.x, region.y].barricades[i].position.x) < range && Mathf.Abs(point.y - SpawnBarricades.regions[region.x, region.y].barricades[i].position.y) < range && Mathf.Abs(point.z - SpawnBarricades.regions[region.x, region.y].barricades[i].position.z) < range)
			{
				serverBarricades.Add(SpawnBarricades.regions[region.x, region.y].barricades[i]);
			}
		}
		return serverBarricades.ToArray();
	}

	public static int getIndexFromPositionClient(Point2 region, Vector3 position)
	{
		for (int i = 0; i < SpawnBarricades.regions[region.x, region.y].models.Count; i++)
		{
			if (SpawnBarricades.regions[region.x, region.y].models[i].transform.position == position)
			{
				return i;
			}
		}
		return -1;
	}

	public static int getIndexFromPositionServer(Point2 region, Vector3 position)
	{
		for (int i = 0; i < SpawnBarricades.regions[region.x, region.y].barricades.Count; i++)
		{
			if (SpawnBarricades.regions[region.x, region.y].barricades[i].position == position)
			{
				return i;
			}
		}
		return -1;
	}

	public void onReady()
	{
		SpawnBarricades.tool = this;
		SpawnBarricades.model = GameObject.Find(Application.loadedLevelName).transform.FindChild("barricades").gameObject;
		SpawnBarricades.regions = new BarricadesRegion[NetworkRegions.REGION_X, NetworkRegions.REGION_Y];
		for (int i = 0; i < NetworkRegions.REGION_X; i++)
		{
			for (int j = 0; j < NetworkRegions.REGION_Y; j++)
			{
				SpawnBarricades.regions[i, j] = new BarricadesRegion();
			}
		}
		if (Network.isServer)
		{
			for (int k = 0; k < NetworkRegions.REGION_X; k++)
			{
				for (int l = 0; l < NetworkRegions.REGION_Y; l++)
				{
					string str = Savedata.loadBarricades(k, l);
					if (str != string.Empty)
					{
						string[] strArrays = Packer.unpack(str, ';');
						for (int m = 0; m < (int)strArrays.Length; m++)
						{
							string[] strArrays1 = Packer.unpack(strArrays[m], ':');
							int num = int.Parse(strArrays1[0]);
							ServerBarricade serverBarricade = new ServerBarricade(num, int.Parse(strArrays1[1]), strArrays1[2], new Vector3(float.Parse(strArrays1[3]), float.Parse(strArrays1[4]), float.Parse(strArrays1[5])), new Vector3(float.Parse(strArrays1[6]), float.Parse(strArrays1[7]), float.Parse(strArrays1[8])));
							SpawnBarricades.regions[k, l].barricades.Add(serverBarricade);
							this.createBarricadePleaseStopKuniiAlsoLetMeKnowIfYouWantToHelpWithAnticheatInVersion3(SpawnBarricades.regions[k, l].barricades[SpawnBarricades.regions[k, l].barricades.Count - 1].id, SpawnBarricades.regions[k, l].barricades[SpawnBarricades.regions[k, l].barricades.Count - 1].position, SpawnBarricades.regions[k, l].barricades[SpawnBarricades.regions[k, l].barricades.Count - 1].rotation);
						}
						SpawnBarricades.regions[k, l].edit = true;
					}
				}
			}
		}
	}

	public void onRegionUpdate()
	{
		if (!Network.isServer && Network.peerType != NetworkPeerType.Disconnected)
		{
			if (NetworkRegions.lastRegion.x != -1)
			{
				for (int i = 0; i < NetworkRegions.REGION_X; i++)
				{
					for (int j = 0; j < NetworkRegions.REGION_Y; j++)
					{
						if (SpawnBarricades.regions[i, j].models.Count > 0 && (!BarricadesRegion.acceptable(i, NetworkRegions.region.x) || !BarricadesRegion.acceptable(j, NetworkRegions.region.y)))
						{
							GameObject[] array = SpawnBarricades.regions[i, j].models.ToArray();
							for (int k = 0; k < (int)array.Length; k++)
							{
								UnityEngine.Object.Destroy(array[k]);
							}
							SpawnBarricades.regions[i, j].models.Clear();
						}
					}
				}
			}
			for (int l = NetworkRegions.region.x - 4; l < NetworkRegions.region.x + 5; l++)
			{
				for (int m = NetworkRegions.region.y - 4; m < NetworkRegions.region.y + 5; m++)
				{
					if (l >= 0 && m >= 0 && l < NetworkRegions.REGION_X && m < NetworkRegions.REGION_Y && SpawnBarricades.regions[l, m].models.Count == 0 && Time.realtimeSinceStartup - SpawnBarricades.regions[l, m].cooldown > 1f)
					{
						SpawnBarricades.regions[l, m].cooldown = Time.realtimeSinceStartup;
						if (!Network.isServer)
						{
							SpawnBarricades.tool.networkView.RPC("askAllBarricades", RPCMode.Server, new object[] { Network.player, l, m });
						}
						else
						{
							this.askAllBarricades(Network.player, l, m);
						}
					}
				}
			}
		}
	}

	public static void placeBarricade(int id, Vector3 position, Vector3 rotation, string state)
	{
		Point2 region = NetworkRegions.getRegion(position);
		SpawnBarricades.regions[region.x, region.y].barricades.Add(new ServerBarricade(id, BarricadeStats.getHealth(id), state, position, rotation));
		SpawnBarricades.tool.networkView.RPC("testBarricade", RPCMode.All, new object[] { id, region.x, region.y, position, rotation });
	}

	public static void @remove(Vector3 position)
	{
		if (SpawnBarricades.getIndexFromPositionServer(NetworkRegions.getRegion(position), position) != -1)
		{
			SpawnBarricades.tool.networkView.RPC("destroyBarricade", RPCMode.All, new object[] { position });
		}
	}

	public static void save(int x, int y)
	{
		if (SpawnBarricades.regions[x, y].barricades.Count > 0 || SpawnBarricades.regions[x, y].edit)
		{
			string empty = string.Empty;
			for (int i = 0; i < SpawnBarricades.regions[x, y].barricades.Count; i++)
			{
				ServerBarricade item = SpawnBarricades.regions[x, y].barricades[i];
				empty = string.Concat(empty, item.id, ":");
				empty = string.Concat(empty, item.health, ":");
				empty = string.Concat(empty, item.state, ":");
				empty = string.Concat(empty, Mathf.Floor(item.position.x * 100f) / 100f, ":");
				empty = string.Concat(empty, Mathf.Floor(item.position.y * 100f) / 100f, ":");
				empty = string.Concat(empty, Mathf.Floor(item.position.z * 100f) / 100f, ":");
				empty = string.Concat(empty, Mathf.Floor(item.rotation.x * 100f) / 100f, ":");
				empty = string.Concat(empty, Mathf.Floor(item.rotation.y * 100f) / 100f, ":");
				empty = string.Concat(empty, Mathf.Floor(item.rotation.z * 100f) / 100f, ":;");
			}
			Savedata.saveBarricades(x, y, empty);
		}
	}

	public void Start()
	{
		NetworkEvents.onReady += new NetworkEventDelegate(this.onReady);
		NetworkEvents.onRegionUpdate += new NetworkEventDelegate(this.onRegionUpdate);
	}

	[RPC]
	public void testBarricade(int id, int x, int y, Vector3 position, Vector3 rotation, NetworkMessageInfo info)
	{
		if ((info.sender.ToString() == "0" || info.sender.ToString() == "-1") && (Network.isServer || BarricadesRegion.acceptable(x, NetworkRegions.region.x) && BarricadesRegion.acceptable(y, NetworkRegions.region.y)))
		{
			GameObject str = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Barricades/", id)));
			str.name = id.ToString();
			str.transform.parent = SpawnBarricades.model.transform;
			str.transform.position = position;
			str.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
			SpawnBarricades.regions[x, y].models.Add(str);
		}
	}
}