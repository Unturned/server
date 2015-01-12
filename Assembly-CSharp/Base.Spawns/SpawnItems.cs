using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
	private static SpawnItems tool;

	public static GameObject model;

	public static ItemsRegion[,] regions;

	private static RaycastHit hit;

	public SpawnItems()
	{
	}

	[RPC]
	public void askAllItems(NetworkPlayer player, int x, int y)
	{
		for (int i = 0; i < SpawnItems.regions[x, y].items.Count; i++)
		{
			ServerItem item = SpawnItems.regions[x, y].items[i];
			if (player != Network.player)
			{
				base.networkView.RPC("createItem", player, new object[] { item.id, item.position });
			}
			else
			{
				this.createItemNotRPC(item.id, item.position);
			}
		}
	}

	[RPC]
	public void askDrop(int x, int y, bool all, NetworkPlayer player)
	{
        GameObject playerModel = NetworkUserList.getModelFromPlayer(player);
		if (playerModel != null)
		{
            Inventory inventory = playerModel.GetComponent<Inventory>();
			if (x >= 0 && y >= 0 && x < inventory.width && y < inventory.height && inventory.items[x, y].amount > 0)
			{
				Vector3 position = inventory.transform.position;
				if (inventory.transform.GetComponent<Player>().vehicle != null)
				{
					position = inventory.transform.GetComponent<Player>().vehicle.getPosition();
				}
				if (!ItemStackable.getStackable(inventory.items[x, y].id))
				{
					SpawnItems.drop(inventory.items[x, y].id, inventory.items[x, y].amount, inventory.items[x, y].state, position);
					inventory.useItem(x, y);
				}
				else if (!all)
				{
					SpawnItems.drop(inventory.items[x, y].id, 1, inventory.items[x, y].state, position);
					inventory.useItem(x, y);
				}
				else
				{
					for (int i = 0; i < inventory.items[x, y].amount; i++)
					{
						SpawnItems.drop(inventory.items[x, y].id, 1, inventory.items[x, y].state, position);
					}
					inventory.deleteItem(x, y);
				}
				inventory.syncItem(x, y);
				NetworkSounds.askSound("Sounds/General/drop", position, 0.2f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
			}
		}
	}

	[RPC]
	public void askItem(Vector3 position, NetworkPlayer player)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnItems.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			GameObject modelFromPlayer = NetworkUserList.getModelFromPlayer(player);
			if (modelFromPlayer != null)
			{
				Inventory component = modelFromPlayer.GetComponent<Inventory>();
				if (component.hasSpace(SpawnItems.regions[region.x, region.y].items[indexFromPositionServer]) == 0)
				{
					component.addItem(SpawnItems.regions[region.x, region.y].items[indexFromPositionServer]);
					base.networkView.RPC("destroyItem", RPCMode.All, new object[] { position });
					NetworkSounds.askSound("Sounds/General/take", component.transform.position, 0.1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
					NetworkManager.error(Texts.ALERT_ITEM_ADDED, string.Empty, player);
					if (!modelFromPlayer.networkView.isMine)
					{
						modelFromPlayer.networkView.RPC("tookItem", player, new object[0]);
					}
					else
					{
						modelFromPlayer.GetComponent<Player>().tookItem();
					}
				}
			}
		}
	}

	[RPC]
	public void createItem(int id, Vector3 position, NetworkMessageInfo info)
	{
		if (info.sender.ToString() == "0" || info.sender.ToString() == "-1")
		{
			this.createItemNotRPC(id, position);
		}
	}

	public void createItemNotRPC(int id, Vector3 position)
	{
		if (ItemWeight.getWeight(id) != -1000)
		{
			GameObject str = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Items/", id)));
			str.name = id.ToString();
			str.transform.parent = SpawnItems.model.transform.FindChild("models");
			str.transform.position = position;
			str.transform.rotation = Quaternion.Euler(-90f, (float)UnityEngine.Random.Range(0, 360), 0f);
			Point2 region = NetworkRegions.getRegion(position);
			SpawnItems.regions[region.x, region.y].models.Add(str);
		}
	}

	[RPC]
	public void destroyItem(Vector3 position)
	{
		int indexFromPositionServer;
		Point2 region = NetworkRegions.getRegion(position);
		if (Network.isServer)
		{
			indexFromPositionServer = SpawnItems.getIndexFromPositionServer(region, position);
			if (indexFromPositionServer != -1 && indexFromPositionServer < SpawnItems.regions[region.x, region.y].items.Count)
			{
				SpawnItems.regions[region.x, region.y].items.RemoveAt(indexFromPositionServer);
			}
		}
		indexFromPositionServer = SpawnItems.getIndexFromPositionClient(region, position);
		if (indexFromPositionServer != -1 && indexFromPositionServer < SpawnItems.regions[region.x, region.y].models.Count)
		{
			UnityEngine.Object.Destroy(SpawnItems.regions[region.x, region.y].models[indexFromPositionServer]);
			SpawnItems.regions[region.x, region.y].models.RemoveAt(indexFromPositionServer);
		}
	}

	public static void drop(int id, int amount, string state, Vector3 position)
	{
		if (!ItemState.getMarket(id) && (ItemType.getType(id) != 7 || state != string.Empty))
		{
			position = position + new Vector3(UnityEngine.Random.Range(-0.75f, 0.75f), 0f, UnityEngine.Random.Range(-0.75f, 0.75f));
			Physics.Raycast(position + new Vector3(0f, 2f, 0f), Vector3.down, out SpawnItems.hit, 16f, RayMasks.STATIC);
			position = SpawnItems.hit.point + new Vector3(0f, 0.25f, 0f);
			SpawnItems.spawn(id, amount, state, position);
		}
	}

	public static void dropItem(int id, Vector3 position)
	{
		SpawnItems.drop(id, ItemAmount.getAmount(id), ItemState.getState(id), position);
	}

	public static void dropItem(int x, int y, bool all)
	{
		if (!Network.isServer)
		{
			SpawnItems.tool.networkView.RPC("askDrop", RPCMode.Server, new object[] { x, y, all, Network.player });
		}
		else
		{
			SpawnItems.tool.askDrop(x, y, all, Network.player);
		}
	}

	public static int getIndexFromPositionClient(Point2 region, Vector3 position)
	{
		for (int i = 0; i < SpawnItems.regions[region.x, region.y].models.Count; i++)
		{
			if (SpawnItems.regions[region.x, region.y].models[i].transform.position == position)
			{
				return i;
			}
		}
		return -1;
	}

	public static int getIndexFromPositionServer(Point2 region, Vector3 position)
	{
		for (int i = 0; i < SpawnItems.regions[region.x, region.y].items.Count; i++)
		{
			if (SpawnItems.regions[region.x, region.y].items[i].position == position)
			{
				return i;
			}
		}
		return -1;
	}

	public static ServerItem[] getItems(Vector3 point, float range)
	{
		Point2 region = NetworkRegions.getRegion(point);
		List<ServerItem> serverItems = new List<ServerItem>();
		for (int i = 0; i < SpawnItems.regions[region.x, region.y].items.Count; i++)
		{
			if (Mathf.Abs(point.x - SpawnItems.regions[region.x, region.y].items[i].position.x) < range && Mathf.Abs(point.y - SpawnItems.regions[region.x, region.y].items[i].position.y) < range && Mathf.Abs(point.z - SpawnItems.regions[region.x, region.y].items[i].position.z) < range)
			{
				serverItems.Add(SpawnItems.regions[region.x, region.y].items[i]);
			}
		}
		return serverItems.ToArray();
	}

	public void onReady()
	{
		SpawnItems.tool = this;
		SpawnItems.model = GameObject.Find(Application.loadedLevelName).transform.FindChild("items").gameObject;
		SpawnItems.regions = new ItemsRegion[NetworkRegions.REGION_X, NetworkRegions.REGION_Y];
		for (int i = 0; i < NetworkRegions.REGION_X; i++)
		{
			for (int j = 0; j < NetworkRegions.REGION_Y; j++)
			{
				SpawnItems.regions[i, j] = new ItemsRegion();
			}
		}
		if (Network.isServer)
		{
			int num = 0;
			for (int k = 0; k < SpawnItems.model.transform.FindChild("spawns").childCount; k++)
			{
				if (ServerSettings.mode == 0 && UnityEngine.Random.@value > Loot.NORMAL_ITEM_CHANCE || ServerSettings.mode == 1 && UnityEngine.Random.@value > Loot.BAMBI_ITEM_CHANCE || ServerSettings.mode == 2 && UnityEngine.Random.@value > Loot.HARDCORE_ITEM_CHANCE || ServerSettings.mode == 3 && UnityEngine.Random.@value > Loot.GOLD_ITEM_CHANCE)
				{
					Transform child = SpawnItems.model.transform.FindChild("spawns").GetChild(k);
					Point2 region = NetworkRegions.getRegion(child.position);
					int loot = Loot.getLoot(child.name);
					int type = ItemType.getType(loot);
					if (ItemWeight.getWeight(loot) != -1000 || type == 30)
					{
						if (type == 10)
						{
							SpawnItems.regions[region.x, region.y].items.Add(new ServerItem(loot, UnityEngine.Random.Range(1, ItemAmount.getAmount(loot) + 1), ItemState.getState(loot), child.position));
							num++;
						}
						else if (type == 25)
						{
							for (int l = 0; l < UnityEngine.Random.Range(3, 6); l++)
							{
								SpawnItems.regions[region.x, region.y].items.Add(new ServerItem(loot, ItemAmount.getAmount(loot), ItemState.getState(loot), child.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), 0f, UnityEngine.Random.Range(-0.5f, 0.5f))));
								num++;
							}
						}
						else if (loot == 30000)
						{
							SpawnItems.regions[region.x, region.y].items.Add(new ServerItem(11, 1, ItemState.getState(11), child.position));
							SpawnItems.regions[region.x, region.y].items.Add(new ServerItem(4017, 1, ItemState.getState(4017), child.position));
							SpawnItems.regions[region.x, region.y].items.Add(new ServerItem(5017, 1, ItemState.getState(5017), child.position));
							num = num + 3;
						}
						else if (loot != 30001)
						{
							SpawnItems.regions[region.x, region.y].items.Add(new ServerItem(loot, ItemAmount.getAmount(loot), ItemState.getState(loot), child.position));
							num++;
						}
						else
						{
							SpawnItems.regions[region.x, region.y].items.Add(new ServerItem(12, 1, ItemState.getState(12), child.position));
							SpawnItems.regions[region.x, region.y].items.Add(new ServerItem(4018, 1, ItemState.getState(4018), child.position));
							SpawnItems.regions[region.x, region.y].items.Add(new ServerItem(5018, 1, ItemState.getState(5018), child.position));
							num = num + 3;
						}
					}
				}
			}
			if (ServerSettings.map != 0)
			{
				base.InvokeRepeating("respawn", 5f, Loot.getRespawnRate() * (ServerSettings.mode != 3 ? 1f : 0.5f));
			}
		}
	}

	public void onRegionUpdate()
	{
		if (Network.peerType != NetworkPeerType.Disconnected)
		{
			if (NetworkRegions.lastRegion.x != -1)
			{
				for (int i = 0; i < NetworkRegions.REGION_X; i++)
				{
					for (int j = 0; j < NetworkRegions.REGION_Y; j++)
					{
						if (SpawnItems.regions[i, j].models.Count > 0 && (!ItemsRegion.acceptable(i, NetworkRegions.region.x) || !ItemsRegion.acceptable(j, NetworkRegions.region.y)))
						{
							GameObject[] array = SpawnItems.regions[i, j].models.ToArray();
							for (int k = 0; k < (int)array.Length; k++)
							{
								UnityEngine.Object.Destroy(array[k]);
							}
							SpawnItems.regions[i, j].models.Clear();
						}
					}
				}
			}
			for (int l = NetworkRegions.region.x - 1; l < NetworkRegions.region.x + 2; l++)
			{
				for (int m = NetworkRegions.region.y - 1; m < NetworkRegions.region.y + 2; m++)
				{
					if (l >= 0 && m >= 0 && l < NetworkRegions.REGION_X && m < NetworkRegions.REGION_Y && SpawnItems.regions[l, m].models.Count == 0 && Time.realtimeSinceStartup - SpawnItems.regions[l, m].cooldown > 1f)
					{
						SpawnItems.regions[l, m].cooldown = Time.realtimeSinceStartup;
						if (!Network.isServer)
						{
							SpawnItems.tool.networkView.RPC("askAllItems", RPCMode.Server, new object[] { Network.player, l, m });
						}
						else
						{
							this.askAllItems(Network.player, l, m);
						}
					}
				}
			}
		}
	}

	public static void reset()
	{
		for (int i = 0; i < NetworkRegions.REGION_X; i++)
		{
			for (int j = 0; j < NetworkRegions.REGION_Y; j++)
			{
				Vector3[] item = new Vector3[SpawnItems.regions[i, j].items.Count];
				for (int k = 0; k < (int)item.Length; k++)
				{
					item[k] = SpawnItems.regions[i, j].items[k].position;
				}
				for (int l = 0; l < (int)item.Length; l++)
				{
					SpawnItems.tool.networkView.RPC("destroyItem", RPCMode.All, new object[] { item[l] });
				}
			}
		}
		for (int m = 0; m < SpawnItems.model.transform.FindChild("spawns").childCount; m++)
		{
			if ((ServerSettings.mode == 0 || ServerSettings.mode == 1) && UnityEngine.Random.@value > Loot.NORMAL_ITEM_CHANCE || ServerSettings.mode == 2 && UnityEngine.Random.@value > Loot.HARDCORE_ITEM_CHANCE || ServerSettings.mode == 3 && UnityEngine.Random.@value > Loot.GOLD_ITEM_CHANCE)
			{
				Transform child = SpawnItems.model.transform.FindChild("spawns").GetChild(m);
				int loot = Loot.getLoot(child.name);
				if (ItemWeight.getWeight(loot) != -1000)
				{
					int type = ItemType.getType(loot);
					if (type == 10)
					{
						SpawnItems.spawnItem(loot, UnityEngine.Random.Range(1, ItemAmount.getAmount(loot) + 1), child.position);
					}
					else if (type != 25)
					{
						SpawnItems.spawnItem(loot, ItemAmount.getAmount(loot), child.position);
					}
					else
					{
						for (int n = 0; n < UnityEngine.Random.Range(1, 4); n++)
						{
							SpawnItems.spawnItem(loot, ItemAmount.getAmount(loot), child.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), 0f, UnityEngine.Random.Range(-0.5f, 0.5f)));
						}
					}
				}
			}
		}
	}

	public void respawn()
	{
		Transform child = SpawnItems.model.transform.FindChild("spawns").GetChild(UnityEngine.Random.Range(0, SpawnItems.model.transform.FindChild("spawns").childCount));
		if ((int)SpawnItems.getItems(child.position, 2f).Length == 0)
		{
			int loot = Loot.getLoot(child.name);
			int type = ItemType.getType(loot);
			if (ItemWeight.getWeight(loot) != -1000 || type == 30)
			{
				if (type == 10)
				{
					SpawnItems.spawnItem(loot, UnityEngine.Random.Range(1, ItemAmount.getAmount(loot) + 1), child.position);
				}
				else if (type == 25)
				{
					for (int i = 0; i < UnityEngine.Random.Range(1, 4); i++)
					{
						SpawnItems.spawnItem(loot, ItemAmount.getAmount(loot), child.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), 0f, UnityEngine.Random.Range(-0.5f, 0.5f)));
					}
				}
				else if (loot == 30000)
				{
					SpawnItems.spawnItem(11, 1, child.position);
					SpawnItems.spawnItem(4017, 1, child.position);
					SpawnItems.spawnItem(5017, 1, child.position);
				}
				else if (loot != 30001)
				{
					SpawnItems.spawnItem(loot, ItemAmount.getAmount(loot), child.position);
				}
				else
				{
					SpawnItems.spawnItem(12, 1, child.position);
					SpawnItems.spawnItem(4018, 1, child.position);
					SpawnItems.spawnItem(5018, 1, child.position);
				}
			}
		}
	}

	public static void spawn(int id, int amount, string state, Vector3 position)
	{
		Point2 region = NetworkRegions.getRegion(position);
		SpawnItems.regions[region.x, region.y].items.Add(new ServerItem(id, amount, state, position));
		SpawnItems.tool.networkView.RPC("testItem", RPCMode.All, new object[] { id, region.x, region.y, position });
	}

	public static void spawnItem(int id, int amount, Vector3 position)
	{
		SpawnItems.spawn(id, amount, ItemState.getState(id), position);
	}

	public void Start()
	{
		NetworkEvents.onReady += new NetworkEventDelegate(this.onReady);
		NetworkEvents.onRegionUpdate += new NetworkEventDelegate(this.onRegionUpdate);
	}

	public static void takeItem(GameObject item)
	{
		if (!Network.isServer)
		{
			SpawnItems.tool.networkView.RPC("askItem", RPCMode.Server, new object[] { item.transform.position, Network.player });
		}
		else
		{
			SpawnItems.tool.askItem(item.transform.position, Network.player);
		}
	}

	[RPC]
	public void testItem(int id, int x, int y, Vector3 position, NetworkMessageInfo info)
	{
        if (info.sender != Network.player)
        {
            Logger.LogSecurity(info.sender, "Item spawn hack detected!");
            NetworkTools.kick(info.sender, "VAC: item spawn hack detected. Incident reported!");
            return;
        }

		if ((info.sender.ToString() == "0" || info.sender.ToString() == "-1") && ItemsRegion.acceptable(x, NetworkRegions.region.x) && ItemsRegion.acceptable(y, NetworkRegions.region.y))
		{
			this.createItemNotRPC(id, position);
		}
	}
}