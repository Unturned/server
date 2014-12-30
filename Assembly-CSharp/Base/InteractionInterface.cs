using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractionInterface : MonoBehaviour
{
	public static InteractionInterface tool;

	public InteractionInterface()
	{
	}

	[RPC]
	public void askBed(Vector3 position, NetworkPlayer player)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			NetworkUser userFromPlayer = NetworkUserList.getUserFromPlayer(player);
			if (userFromPlayer != null)
			{
				if (SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state == string.Empty)
				{
					for (int i = 0; i < NetworkRegions.REGION_X; i++)
					{
						for (int j = 0; j < NetworkRegions.REGION_Y; j++)
						{
							for (int k = 0; k < SpawnBarricades.regions[i, j].barricades.Count; k++)
							{
								if (SpawnBarricades.regions[i, j].barricades[k].state == userFromPlayer.id)
								{
									SpawnBarricades.regions[region.x, region.y].barricades[k].state = string.Empty;
									InteractionInterface.tool.networkView.RPC("tellBedState", RPCMode.All, new object[] { SpawnBarricades.regions[region.x, region.y].barricades[k].position, string.Empty });
								}
							}
						}
					}
					SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = userFromPlayer.id;
					InteractionInterface.tool.networkView.RPC("tellBedState", RPCMode.All, new object[] { position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state });
				}
				else if (SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state == userFromPlayer.id)
				{
					SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = string.Empty;
					InteractionInterface.tool.networkView.RPC("tellBedState", RPCMode.All, new object[] { position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state });
				}
			}
		}
	}

	[RPC]
	public void askBedState(Vector3 position, NetworkPlayer player)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			if (player != Network.player)
			{
				InteractionInterface.tool.networkView.RPC("tellBedState", player, new object[] { position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state });
			}
			else
			{
				InteractionInterface.tool.tellBedState(position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state);
			}
		}
	}

	[RPC]
	public void askCampfire(Vector3 position)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			if (SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state != "t")
			{
				SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = "t";
				InteractionInterface.tool.networkView.RPC("tellCampfireState", RPCMode.All, new object[] { position, true });
			}
			else
			{
				SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = "f";
				InteractionInterface.tool.networkView.RPC("tellCampfireState", RPCMode.All, new object[] { position, false });
			}
		}
	}

	[RPC]
	public void askCampfireState(Vector3 position, NetworkPlayer player)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			if (player != Network.player)
			{
				InteractionInterface.tool.networkView.RPC("tellCampfireState", player, new object[] { position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state == "t" });
			}
			else
			{
				InteractionInterface.tool.tellCampfireState(position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state == "t");
			}
		}
	}

	[RPC]
	public void askCrateAdd(Vector3 position, NetworkPlayer player, int x_0, int y_0, int x_1, int y_1, bool all)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			GameObject modelFromPlayer = NetworkUserList.getModelFromPlayer(player);
			if (modelFromPlayer != null)
			{
				Inventory component = modelFromPlayer.GetComponent<Inventory>();
				if (component.items[x_0, y_0].id != -1)
				{
					if (ItemState.getMarket(component.items[x_0, y_0].id) || ItemType.getType(component.items[x_0, y_0].id) == 7 && component.items[x_0, y_0].state == string.Empty)
					{
						component.deleteItem(x_0, y_0);
						component.syncItem(x_0, y_0);
					}
					else
					{
						ClientItem[,] crateItems = InteractionInterface.getCrateItems(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id, Sneaky.expose(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state));
						if (crateItems[x_1, y_1].id == -1 || ItemStackable.getStackable(component.items[x_0, y_0].id) && component.items[x_0, y_0].id == crateItems[x_1, y_1].id)
						{
							if (component.items[x_0, y_0].id == crateItems[x_1, y_1].id)
							{
								if (!all)
								{
									crateItems[x_1, y_1].id = component.items[x_0, y_0].id;
									crateItems[x_1, y_1].amount = crateItems[x_1, y_1].amount + 1;
									crateItems[x_1, y_1].state = component.items[x_0, y_0].state;
									component.useItem(x_0, y_0);
								}
								else
								{
									crateItems[x_1, y_1].id = component.items[x_0, y_0].id;
									crateItems[x_1, y_1].amount = crateItems[x_1, y_1].amount + component.items[x_0, y_0].amount;
									crateItems[x_1, y_1].state = component.items[x_0, y_0].state;
									component.deleteItem(x_0, y_0);
								}
							}
							else if (all || !ItemStackable.getStackable(component.items[x_0, y_0].id))
							{
								crateItems[x_1, y_1].id = component.items[x_0, y_0].id;
								crateItems[x_1, y_1].amount = component.items[x_0, y_0].amount;
								crateItems[x_1, y_1].state = component.items[x_0, y_0].state;
								component.deleteItem(x_0, y_0);
							}
							else
							{
								crateItems[x_1, y_1].id = component.items[x_0, y_0].id;
								crateItems[x_1, y_1].amount = crateItems[x_1, y_1].amount + 1;
								crateItems[x_1, y_1].state = component.items[x_0, y_0].state;
								component.useItem(x_0, y_0);
							}
							component.syncItem(x_0, y_0);
							string str = Sneaky.sneak(InteractionInterface.getCrateSave(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id, crateItems));
							SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = str;
							InteractionInterface.tool.networkView.RPC("tellCrateState", RPCMode.All, new object[] { position, str });
						}
					}
				}
			}
		}
	}

	[RPC]
	public void askCrateRemove(Vector3 position, NetworkPlayer player, int x, int y, bool all)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			GameObject modelFromPlayer = NetworkUserList.getModelFromPlayer(player);
			if (modelFromPlayer != null)
			{
				ClientItem[,] crateItems = InteractionInterface.getCrateItems(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id, Sneaky.expose(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state));
				if (crateItems[x, y].id != -1)
				{
					Inventory component = modelFromPlayer.GetComponent<Inventory>();
					if (!all)
					{
						if (!ItemStackable.getStackable(crateItems[x, y].id))
						{
							component.tryAddItem(crateItems[x, y].id, crateItems[x, y].amount, crateItems[x, y].state);
							crateItems[x, y].amount = 0;
						}
						else
						{
							component.tryAddItem(crateItems[x, y].id, 1);
							crateItems[x, y].amount = crateItems[x, y].amount - 1;
						}
						if (crateItems[x, y].amount == 0)
						{
							crateItems[x, y].id = -1;
							crateItems[x, y].state = string.Empty;
						}
					}
					else if (!ItemStackable.getStackable(crateItems[x, y].id))
					{
						component.tryAddItem(crateItems[x, y].id, crateItems[x, y].amount, crateItems[x, y].state);
						crateItems[x, y].id = -1;
						crateItems[x, y].amount = 0;
						crateItems[x, y].state = string.Empty;
					}
					else if (crateItems[x, y].amount <= Inventory.MAX_STACK)
					{
						component.tryAddItem(crateItems[x, y].id, crateItems[x, y].amount);
						crateItems[x, y].id = -1;
						crateItems[x, y].amount = 0;
						crateItems[x, y].state = string.Empty;
					}
					else
					{
						component.tryAddItem(crateItems[x, y].id, Inventory.MAX_STACK);
						crateItems[x, y].amount = crateItems[x, y].amount - Inventory.MAX_STACK;
					}
					string str = Sneaky.sneak(InteractionInterface.getCrateSave(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id, crateItems));
					SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = str;
					InteractionInterface.tool.networkView.RPC("tellCrateState", RPCMode.All, new object[] { position, str });
				}
			}
		}
	}

	[RPC]
	public void askCrateState(Vector3 position, NetworkPlayer player)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			if (player != Network.player)
			{
				InteractionInterface.tool.networkView.RPC("tellCrateState", player, new object[] { position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state });
			}
			else
			{
				InteractionInterface.tool.tellCrateState(position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state);
			}
		}
	}

	[RPC]
	public void askDoor(Vector3 position, NetworkPlayer player)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			NetworkUser userFromPlayer = NetworkUserList.getUserFromPlayer(player);
			if (userFromPlayer != null)
			{
				string[] strArrays = Packer.unpack(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state, '\u005F');
				if (strArrays[0] == userFromPlayer.id || userFromPlayer.friend != string.Empty && userFromPlayer.friend == strArrays[1])
				{
					NetworkSounds.askSound("Sounds/Barricades/door", position, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
					if (strArrays[2] != "t")
					{
						SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = string.Concat(strArrays[0], "_", strArrays[1], "_t_");
						InteractionInterface.tool.networkView.RPC("tellDoorState", RPCMode.All, new object[] { position, true });
					}
					else
					{
						SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = string.Concat(strArrays[0], "_", strArrays[1], "_f_");
						InteractionInterface.tool.networkView.RPC("tellDoorState", RPCMode.All, new object[] { position, false });
					}
				}
			}
		}
	}

	[RPC]
	public void askDoorState(Vector3 position, NetworkPlayer player)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			string[] strArrays = Packer.unpack(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state, '\u005F');
			if (player != Network.player)
			{
				InteractionInterface.tool.networkView.RPC("tellDoorOwner", player, new object[] { position, strArrays[0], strArrays[1] });
				InteractionInterface.tool.networkView.RPC("tellDoorState", player, new object[] { position, strArrays[2] == "t" });
			}
			else
			{
				InteractionInterface.tool.tellDoorOwner(position, strArrays[0], strArrays[1]);
				InteractionInterface.tool.tellDoorState(position, strArrays[2] == "t");
			}
		}
	}

	public void askExplosive(Vector3 position)
	{
		if (SpawnBarricades.getIndexFromPositionServer(NetworkRegions.getRegion(position), position) != -1)
		{
			SpawnBarricades.@remove(position);
		}
	}

	[RPC]
	public void askExplosiveTrap(Vector3 position)
	{
		if (SpawnBarricades.getIndexFromPositionServer(NetworkRegions.getRegion(position), position) != -1)
		{
			SpawnBarricades.@remove(position);
		}
	}

	[RPC]
	public void askFertilize(Vector3 position)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			ServerBarricade item = SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer];
			int num = int.Parse(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state) - 1000;
			item.state = num.ToString();
			InteractionInterface.tool.networkView.RPC("tellHarvestState", RPCMode.All, new object[] { position, int.Parse(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state) });
		}
	}

	[RPC]
	public void askGenerator(Vector3 position)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			if (SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state != "t")
			{
				SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = "t";
				InteractionInterface.tool.networkView.RPC("tellGeneratorState", RPCMode.All, new object[] { position, true });
			}
			else
			{
				SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = "f";
				InteractionInterface.tool.networkView.RPC("tellGeneratorState", RPCMode.All, new object[] { position, false });
			}
		}
	}

	[RPC]
	public void askGeneratorState(Vector3 position, NetworkPlayer player)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			if (player != Network.player)
			{
				InteractionInterface.tool.networkView.RPC("tellGeneratorState", player, new object[] { position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state == "t" });
			}
			else
			{
				InteractionInterface.tool.tellGeneratorState(position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state == "t");
			}
		}
	}

	[RPC]
	public void askHarvest(Vector3 position)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1 && Epoch.getSeconds() - int.Parse(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state) > HarvestStats.getGrowth(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id))
		{
			NetworkSounds.askSound("Sounds/Barricades/harvest", position, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
			SpawnItems.dropItem(HarvestStats.getCrop(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id), position);
			SpawnBarricades.@remove(position);
		}
	}

	[RPC]
	public void askHarvestState(Vector3 position, NetworkPlayer player)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			if (player != Network.player)
			{
				InteractionInterface.tool.networkView.RPC("tellHarvestState", player, new object[] { position, int.Parse(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state) });
			}
			else
			{
				InteractionInterface.tool.tellHarvestState(position, int.Parse(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state));
			}
		}
	}

	[RPC]
	public void askLamp(Vector3 position)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			if (SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state != "t")
			{
				SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = "t";
				InteractionInterface.tool.networkView.RPC("tellLampState", RPCMode.All, new object[] { position, true });
			}
			else
			{
				SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = "f";
				InteractionInterface.tool.networkView.RPC("tellLampState", RPCMode.All, new object[] { position, false });
			}
		}
	}

	[RPC]
	public void askLampState(Vector3 position, NetworkPlayer player)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			if (player != Network.player)
			{
				InteractionInterface.tool.networkView.RPC("tellLampState", player, new object[] { position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state == "t" });
			}
			else
			{
				InteractionInterface.tool.tellLampState(position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state == "t");
			}
		}
	}

	[RPC]
	public void askLockedCrateAdd(Vector3 position, NetworkPlayer player, int x_0, int y_0, int x_1, int y_1, bool all)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			NetworkUser userFromPlayer = NetworkUserList.getUserFromPlayer(player);
			if (userFromPlayer != null)
			{
				string[] strArrays = Packer.unpack(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state, '\u005F');
				if ((strArrays[0] == userFromPlayer.id || userFromPlayer.friend != string.Empty && userFromPlayer.friend == strArrays[1]) && userFromPlayer.model != null)
				{
					Inventory component = userFromPlayer.model.GetComponent<Inventory>();
					if (component.items[x_0, y_0].id != -1)
					{
						if (ItemState.getMarket(component.items[x_0, y_0].id) || ItemType.getType(component.items[x_0, y_0].id) == 7 && component.items[x_0, y_0].state == string.Empty)
						{
							component.deleteItem(x_0, y_0);
							component.syncItem(x_0, y_0);
						}
						else
						{
							ClientItem[,] crateItems = InteractionInterface.getCrateItems(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id, Sneaky.expose(strArrays[2]));
							if (crateItems[x_1, y_1].id == -1 || ItemStackable.getStackable(component.items[x_0, y_0].id) && component.items[x_0, y_0].id == crateItems[x_1, y_1].id)
							{
								if (component.items[x_0, y_0].id == crateItems[x_1, y_1].id)
								{
									if (!all)
									{
										crateItems[x_1, y_1].id = component.items[x_0, y_0].id;
										crateItems[x_1, y_1].amount = crateItems[x_1, y_1].amount + 1;
										crateItems[x_1, y_1].state = component.items[x_0, y_0].state;
										component.useItem(x_0, y_0);
									}
									else
									{
										crateItems[x_1, y_1].id = component.items[x_0, y_0].id;
										crateItems[x_1, y_1].amount = crateItems[x_1, y_1].amount + component.items[x_0, y_0].amount;
										crateItems[x_1, y_1].state = component.items[x_0, y_0].state;
										component.deleteItem(x_0, y_0);
									}
								}
								else if (all || !ItemStackable.getStackable(component.items[x_0, y_0].id))
								{
									crateItems[x_1, y_1].id = component.items[x_0, y_0].id;
									crateItems[x_1, y_1].amount = component.items[x_0, y_0].amount;
									crateItems[x_1, y_1].state = component.items[x_0, y_0].state;
									component.deleteItem(x_0, y_0);
								}
								else
								{
									crateItems[x_1, y_1].id = component.items[x_0, y_0].id;
									crateItems[x_1, y_1].amount = crateItems[x_1, y_1].amount + 1;
									crateItems[x_1, y_1].state = component.items[x_0, y_0].state;
									component.useItem(x_0, y_0);
								}
								component.syncItem(x_0, y_0);
								string str = Sneaky.sneak(InteractionInterface.getCrateSave(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id, crateItems));
								SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = string.Concat(new string[] { strArrays[0], "_", strArrays[1], "_", str, "_" });
								InteractionInterface.tool.networkView.RPC("tellLockedCrateState", RPCMode.All, new object[] { position, str });
							}
						}
					}
				}
			}
		}
	}

	[RPC]
	public void askLockedCrateRemove(Vector3 position, NetworkPlayer player, int x, int y, bool all)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			NetworkUser userFromPlayer = NetworkUserList.getUserFromPlayer(player);
			if (userFromPlayer != null)
			{
				string[] strArrays = Packer.unpack(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state, '\u005F');
				if ((strArrays[0] == userFromPlayer.id || userFromPlayer.friend != string.Empty && userFromPlayer.friend == strArrays[1]) && userFromPlayer.model != null)
				{
					ClientItem[,] crateItems = InteractionInterface.getCrateItems(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id, Sneaky.expose(strArrays[2]));
					if (crateItems[x, y].id != -1)
					{
						Inventory component = userFromPlayer.model.GetComponent<Inventory>();
						if (!all)
						{
							if (!ItemStackable.getStackable(crateItems[x, y].id))
							{
								component.tryAddItem(crateItems[x, y].id, crateItems[x, y].amount, crateItems[x, y].state);
								crateItems[x, y].amount = 0;
							}
							else
							{
								component.tryAddItem(crateItems[x, y].id, 1);
								crateItems[x, y].amount = crateItems[x, y].amount - 1;
							}
							if (crateItems[x, y].amount == 0)
							{
								crateItems[x, y].id = -1;
								crateItems[x, y].state = string.Empty;
							}
						}
						else if (!ItemStackable.getStackable(crateItems[x, y].id))
						{
							component.tryAddItem(crateItems[x, y].id, crateItems[x, y].amount, crateItems[x, y].state);
							crateItems[x, y].id = -1;
							crateItems[x, y].amount = 0;
							crateItems[x, y].state = string.Empty;
						}
						else if (crateItems[x, y].amount <= Inventory.MAX_STACK)
						{
							component.tryAddItem(crateItems[x, y].id, crateItems[x, y].amount);
							crateItems[x, y].id = -1;
							crateItems[x, y].amount = 0;
							crateItems[x, y].state = string.Empty;
						}
						else
						{
							component.tryAddItem(crateItems[x, y].id, Inventory.MAX_STACK);
							crateItems[x, y].amount = crateItems[x, y].amount - Inventory.MAX_STACK;
						}
						string str = Sneaky.sneak(InteractionInterface.getCrateSave(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id, crateItems));
						SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = string.Concat(new string[] { strArrays[0], "_", strArrays[1], "_", str, "_" });
						InteractionInterface.tool.networkView.RPC("tellLockedCrateState", RPCMode.All, new object[] { position, str });
					}
				}
			}
		}
	}

	[RPC]
	public void askLockedCrateState(Vector3 position, NetworkPlayer player)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			string[] strArrays = Packer.unpack(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state, '\u005F');
			if (player != Network.player)
			{
				InteractionInterface.tool.networkView.RPC("tellLockedCrateOwner", player, new object[] { position, strArrays[0], strArrays[1] });
				InteractionInterface.tool.networkView.RPC("tellLockedCrateState", player, new object[] { position, strArrays[2] });
			}
			else
			{
				InteractionInterface.tool.tellLockedCrateOwner(position, strArrays[0], strArrays[1]);
				InteractionInterface.tool.tellLockedCrateState(position, strArrays[2]);
			}
		}
	}

	[RPC]
	public void askNote(Vector3 position, string state)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			state = Sneaky.sneak(state);
			SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = state;
			InteractionInterface.tool.networkView.RPC("tellNoteState", RPCMode.All, new object[] { position, state });
		}
	}

	[RPC]
	public void askNoteState(Vector3 position, NetworkPlayer player)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			if (player != Network.player)
			{
				InteractionInterface.tool.networkView.RPC("tellNoteState", player, new object[] { position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state });
			}
			else
			{
				InteractionInterface.tool.tellNoteState(position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state);
			}
		}
	}

	[RPC]
	public void askTrap(NetworkPlayer player, Vector3 position)
	{
		if (ServerSettings.pvp)
		{
			Point2 region = NetworkRegions.getRegion(position);
			int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
			if (indexFromPositionServer != -1)
			{
				GameObject modelFromPlayer = NetworkUserList.getModelFromPlayer(player);
				if (modelFromPlayer != null)
				{
					if (SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id == 16006)
					{
						modelFromPlayer.GetComponent<Life>().damage(BarricadeStats.getDamage(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id), "You got your leg stuck in a snare.");
					}
					else if (SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id != 16009)
					{
						modelFromPlayer.GetComponent<Life>().damage(BarricadeStats.getDamage(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id), "You punctured yourself on something sharp.");
					}
					else
					{
						modelFromPlayer.GetComponent<Life>().damage(BarricadeStats.getDamage(SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].id), "You electrocuted yourself.");
					}
					SpawnBarricades.damage(position, 10);
				}
			}
		}
	}

	[RPC]
	public void askUnlockedDoor(Vector3 position, NetworkPlayer player)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1 && NetworkUserList.getUserFromPlayer(player) != null)
		{
			NetworkSounds.askSound("Sounds/Barricades/door", position, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
			if (SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state != "t")
			{
				SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = "t";
				InteractionInterface.tool.networkView.RPC("tellUnlockedDoorState", RPCMode.All, new object[] { position, true });
			}
			else
			{
				SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state = "f";
				InteractionInterface.tool.networkView.RPC("tellUnlockedDoorState", RPCMode.All, new object[] { position, false });
			}
		}
	}

	[RPC]
	public void askUnlockedDoorState(Vector3 position, NetworkPlayer player)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionServer = SpawnBarricades.getIndexFromPositionServer(region, position);
		if (indexFromPositionServer != -1)
		{
			if (player != Network.player)
			{
				InteractionInterface.tool.networkView.RPC("tellUnlockedDoorState", player, new object[] { position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state == "t" });
			}
			else
			{
				InteractionInterface.tool.tellUnlockedDoorState(position, SpawnBarricades.regions[region.x, region.y].barricades[indexFromPositionServer].state == "t");
			}
		}
	}

	public static ClientItem[,] getCrateItems(int id, string state)
	{
		ClientItem[,] clientItem = new ClientItem[2, BarricadeStats.getCapacity(id)];
		if (state != string.Empty)
		{
			string[] strArrays = Packer.unpack(state, ';');
			for (int i = 0; i < 2 * BarricadeStats.getCapacity(id); i++)
			{
				int num = i % 2;
				int num1 = i / 2;
				string[] strArrays1 = Packer.unpack(strArrays[i], ':');
				clientItem[num, num1] = new ClientItem(int.Parse(strArrays1[0]), int.Parse(strArrays1[1]), strArrays1[2]);
			}
		}
		else
		{
			for (int j = 0; j < 2; j++)
			{
				for (int k = 0; k < BarricadeStats.getCapacity(id); k++)
				{
					clientItem[j, k] = new ClientItem(-1, 0, string.Empty);
				}
			}
		}
		return clientItem;
	}

	public static string getCrateSave(int id, ClientItem[,] items)
	{
		string empty = string.Empty;
		for (int i = 0; i < BarricadeStats.getCapacity(id); i++)
		{
			for (int j = 0; j < 2; j++)
			{
				string str = empty;
				empty = string.Concat(new object[] { str, items[j, i].id, ":", items[j, i].amount, ":", items[j, i].state, ":;" });
			}
		}
		return empty;
	}

	public void onReady()
	{
		InteractionInterface.tool = this;
	}

	public static void requestBed(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askBedState", RPCMode.Server, new object[] { position, Network.player });
		}
		else
		{
			InteractionInterface.tool.askBedState(position, Network.player);
		}
	}

	public static void requestCampfire(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askCampfireState", RPCMode.Server, new object[] { position, Network.player });
		}
		else
		{
			InteractionInterface.tool.askCampfireState(position, Network.player);
		}
	}

	public static void requestCrate(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askCrateState", RPCMode.Server, new object[] { position, Network.player });
		}
		else
		{
			InteractionInterface.tool.askCrateState(position, Network.player);
		}
	}

	public static void requestDoor(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askDoorState", RPCMode.Server, new object[] { position, Network.player });
		}
		else
		{
			InteractionInterface.tool.askDoorState(position, Network.player);
		}
	}

	public static void requestGenerator(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askGeneratorState", RPCMode.Server, new object[] { position, Network.player });
		}
		else
		{
			InteractionInterface.tool.askGeneratorState(position, Network.player);
		}
	}

	public static void requestHarvest(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askHarvestState", RPCMode.Server, new object[] { position, Network.player });
		}
		else
		{
			InteractionInterface.tool.askHarvestState(position, Network.player);
		}
	}

	public static void requestLamp(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askLampState", RPCMode.Server, new object[] { position, Network.player });
		}
		else
		{
			InteractionInterface.tool.askLampState(position, Network.player);
		}
	}

	public static void requestLockedCrate(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askLockedCrateState", RPCMode.Server, new object[] { position, Network.player });
		}
		else
		{
			InteractionInterface.tool.askLockedCrateState(position, Network.player);
		}
	}

	public static void requestNote(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askNoteState", RPCMode.Server, new object[] { position, Network.player });
		}
		else
		{
			InteractionInterface.tool.askNoteState(position, Network.player);
		}
	}

	public static void requestUnlockedDoor(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askUnlockedDoorState", RPCMode.Server, new object[] { position, Network.player });
		}
		else
		{
			InteractionInterface.tool.askUnlockedDoorState(position, Network.player);
		}
	}

	public static void sendBed(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askBed", RPCMode.Server, new object[] { position, Network.player });
		}
		else
		{
			InteractionInterface.tool.askBed(position, Network.player);
		}
	}

	public static void sendCampfire(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askCampfire", RPCMode.Server, new object[] { position });
		}
		else
		{
			InteractionInterface.tool.askCampfire(position);
		}
	}

	public static void sendCrateAdd(Vector3 position, int x_0, int y_0, int x_1, int y_1, bool all)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askCrateAdd", RPCMode.Server, new object[] { position, Network.player, x_0, y_0, x_1, y_1, all });
		}
		else
		{
			InteractionInterface.tool.askCrateAdd(position, Network.player, x_0, y_0, x_1, y_1, all);
		}
	}

	public static void sendCrateRemove(Vector3 position, int x, int y, bool all)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askCrateRemove", RPCMode.Server, new object[] { position, Network.player, x, y, all });
		}
		else
		{
			InteractionInterface.tool.askCrateRemove(position, Network.player, x, y, all);
		}
	}

	public static void sendDoor(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askDoor", RPCMode.Server, new object[] { position, Network.player });
		}
		else
		{
			InteractionInterface.tool.askDoor(position, Network.player);
		}
	}

	public static void sendExplosive(Vector3 position)
	{
		if (Network.isServer)
		{
			InteractionInterface.tool.askExplosive(position);
		}
	}

	public static void sendExplosiveTrap(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askExplosiveTrap", RPCMode.Server, new object[] { position });
		}
		else
		{
			InteractionInterface.tool.askExplosiveTrap(position);
		}
	}

	public static void sendFertilize(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askFertilize", RPCMode.Server, new object[] { position });
		}
		else
		{
			InteractionInterface.tool.askFertilize(position);
		}
	}

	public static void sendGenerator(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askGenerator", RPCMode.Server, new object[] { position });
		}
		else
		{
			InteractionInterface.tool.askGenerator(position);
		}
	}

	public static void sendHarvest(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askHarvest", RPCMode.Server, new object[] { position });
		}
		else
		{
			InteractionInterface.tool.askHarvest(position);
		}
	}

	public static void sendLamp(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askLamp", RPCMode.Server, new object[] { position });
		}
		else
		{
			InteractionInterface.tool.askLamp(position);
		}
	}

	public static void sendLockedCrateAdd(Vector3 position, int x_0, int y_0, int x_1, int y_1, bool all)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askLockedCrateAdd", RPCMode.Server, new object[] { position, Network.player, x_0, y_0, x_1, y_1, all });
		}
		else
		{
			InteractionInterface.tool.askLockedCrateAdd(position, Network.player, x_0, y_0, x_1, y_1, all);
		}
	}

	public static void sendLockedCrateRemove(Vector3 position, int x, int y, bool all)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askLockedCrateRemove", RPCMode.Server, new object[] { position, Network.player, x, y, all });
		}
		else
		{
			InteractionInterface.tool.askLockedCrateRemove(position, Network.player, x, y, all);
		}
	}

	public static void sendNote(Vector3 position, string state)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askNote", RPCMode.Server, new object[] { position, state });
		}
		else
		{
			InteractionInterface.tool.askNote(position, state);
		}
	}

	public static void sendTrap(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askTrap", RPCMode.Server, new object[] { Network.player, position });
		}
		else
		{
			InteractionInterface.tool.askTrap(Network.player, position);
		}
	}

	public static void sendUnlockedDoor(Vector3 position)
	{
		if (!Network.isServer)
		{
			InteractionInterface.tool.networkView.RPC("askUnlockedDoor", RPCMode.Server, new object[] { position, Network.player });
		}
		else
		{
			InteractionInterface.tool.askUnlockedDoor(position, Network.player);
		}
	}

	public void Start()
	{
		NetworkEvents.onReady += new NetworkEventDelegate(this.onReady);
	}

	[RPC]
	public void tellBedState(Vector3 position, string state)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionClient = SpawnBarricades.getIndexFromPositionClient(region, position);
		if (indexFromPositionClient != -1)
		{
			SpawnBarricades.regions[region.x, region.y].models[indexFromPositionClient].transform.FindChild("model").GetComponent<Bed>().setState(state);
		}
	}

	[RPC]
	public void tellCampfireState(Vector3 position, bool state)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionClient = SpawnBarricades.getIndexFromPositionClient(region, position);
		if (indexFromPositionClient != -1)
		{
			SpawnBarricades.regions[region.x, region.y].models[indexFromPositionClient].transform.FindChild("model").GetComponent<Campfire>().setState(state);
		}
	}

	[RPC]
	public void tellCrateState(Vector3 position, string state)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionClient = SpawnBarricades.getIndexFromPositionClient(region, position);
		if (indexFromPositionClient != -1)
		{
			SpawnBarricades.regions[region.x, region.y].models[indexFromPositionClient].transform.FindChild("model").GetComponent<Crate>().setState(state);
		}
	}

	[RPC]
	public void tellDoorOwner(Vector3 position, string player, string friend)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionClient = SpawnBarricades.getIndexFromPositionClient(region, position);
		if (indexFromPositionClient != -1)
		{
			SpawnBarricades.regions[region.x, region.y].models[indexFromPositionClient].transform.FindChild("model").GetComponent<Door>().setOwner(player, friend);
		}
	}

	[RPC]
	public void tellDoorState(Vector3 position, bool state)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionClient = SpawnBarricades.getIndexFromPositionClient(region, position);
		if (indexFromPositionClient != -1)
		{
			SpawnBarricades.regions[region.x, region.y].models[indexFromPositionClient].transform.FindChild("model").GetComponent<Door>().setState(state);
		}
	}

	[RPC]
	public void tellGeneratorState(Vector3 position, bool state)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionClient = SpawnBarricades.getIndexFromPositionClient(region, position);
		if (indexFromPositionClient != -1)
		{
			SpawnBarricades.regions[region.x, region.y].models[indexFromPositionClient].transform.FindChild("model").GetComponent<Generator>().setState(state);
		}
	}

	[RPC]
	public void tellHarvestState(Vector3 position, int state)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionClient = SpawnBarricades.getIndexFromPositionClient(region, position);
		if (indexFromPositionClient != -1)
		{
			SpawnBarricades.regions[region.x, region.y].models[indexFromPositionClient].transform.FindChild("model").GetComponent<Harvest>().setState(state);
		}
	}

	[RPC]
	public void tellLampState(Vector3 position, bool state)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionClient = SpawnBarricades.getIndexFromPositionClient(region, position);
		if (indexFromPositionClient != -1)
		{
			SpawnBarricades.regions[region.x, region.y].models[indexFromPositionClient].transform.FindChild("model").GetComponent<Lamp>().setState(state);
		}
	}

	[RPC]
	public void tellLockedCrateOwner(Vector3 position, string player, string friend)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionClient = SpawnBarricades.getIndexFromPositionClient(region, position);
		if (indexFromPositionClient != -1)
		{
			SpawnBarricades.regions[region.x, region.y].models[indexFromPositionClient].transform.FindChild("model").GetComponent<LockedCrate>().setOwner(player, friend);
		}
	}

	[RPC]
	public void tellLockedCrateState(Vector3 position, string state)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionClient = SpawnBarricades.getIndexFromPositionClient(region, position);
		if (indexFromPositionClient != -1)
		{
			SpawnBarricades.regions[region.x, region.y].models[indexFromPositionClient].transform.FindChild("model").GetComponent<LockedCrate>().setState(state);
		}
	}

	[RPC]
	public void tellNoteState(Vector3 position, string state)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionClient = SpawnBarricades.getIndexFromPositionClient(region, position);
		if (indexFromPositionClient != -1)
		{
			SpawnBarricades.regions[region.x, region.y].models[indexFromPositionClient].transform.FindChild("model").GetComponent<Note>().setState(state);
		}
	}

	[RPC]
	public void tellUnlockedDoorState(Vector3 position, bool state)
	{
		Point2 region = NetworkRegions.getRegion(position);
		int indexFromPositionClient = SpawnBarricades.getIndexFromPositionClient(region, position);
		if (indexFromPositionClient != -1)
		{
			SpawnBarricades.regions[region.x, region.y].models[indexFromPositionClient].transform.FindChild("model").GetComponent<UnlockedDoor>().setState(state);
		}
	}
}