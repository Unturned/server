using System;
using UnityEngine;
using System.Collections.Generic;

using Unturned.Log;

public class Inventory : MonoBehaviour {
    public readonly static int MAX_STACK = 30;

    public ClientItem[,] items;

    public int width;

    public int height;

    public int weight;

    public int capacity;

    public float speed = 1f;

    public bool loaded;

    public void addItem(ServerItem item) {
        if (ItemWeight.getWeight(item.id) != -1000) {
            if (!ItemStackable.getStackable(item.id)) {
                Point2 point2 = this.search(-1);
                if (point2.x != -1) {
                    this.items[point2.x, point2.y] = new ClientItem(item);
                    Inventory weight = this;
                    weight.weight = weight.weight + ItemWeight.getWeight(item.id);
                    this.syncItem(point2.x, point2.y);
                    this.syncWeight();
                }
            } else {
                Point2 point21 = this.search(item.id);
                if (point21.x == -1 || this.items[point21.x, point21.y].amount >= Inventory.MAX_STACK) {
                    point21 = this.search(-1);
                    if (point21.x != -1) {
                        this.items[point21.x, point21.y] = new ClientItem(item);
                        Inventory inventory = this;
                        inventory.weight = inventory.weight + ItemWeight.getWeight(item.id) * item.amount;
                        this.syncItem(point21.x, point21.y);
                        this.syncWeight();
                    }
                }
                else {
                    this.items[point21.x, point21.y].amount = this.items[point21.x, point21.y].amount + item.amount;
                    Inventory weight1 = this;
                    weight1.weight = weight1.weight + ItemWeight.getWeight(item.id) * item.amount;
                    this.syncItem(point21.x, point21.y);
                    this.syncWeight();
                }
            }
        }
    }

    [RPC]
    public void askCraft(int x_0, int y_0, int x_1, int y_1, int xTool, int yTool, int mode, bool all) {
        if (!base.GetComponent<Life>().dead && (x_0 == -1 || this.items[x_0, y_0].id != -1) && (x_1 == -1 || this.items[x_1, y_1].id != -1) && (xTool == -1 || this.items[xTool, yTool].id != -1)) {
            if (x_0 == -1 || x_1 == -1 || ItemType.getType(this.items[x_0, y_0].id) != 10 || ItemType.getType(this.items[x_1, y_1].id) != 10 || !AmmoStats.getCaliberCompatible(this.items[x_0, y_0].id, this.items[x_1, y_1].id) || this.items[x_0, y_0].amount <= 1 || this.items[x_1, y_1].amount <= 1) {
                bool flag = Cooking.fire(base.transform.position);
                bool flag1 = false;
                int num = 0;
                while (num < (int)Blueprints.prints.Length) {
                    if (!flag1) {
                        Blueprint blueprint = Blueprints.prints[num];
                        if ((mode == 0 && !blueprint.fire || mode == 1 && blueprint.fire && flag) && (blueprint.knowledge == 0f || base.GetComponent<Skills>().crafting() >= blueprint.knowledge) && ((blueprint.id_0 == -1 && x_0 == -1 || x_0 != -1 && blueprint.id_0 == this.items[x_0, y_0].id) && (blueprint.id_1 == -1 && x_1 == -1 || x_1 != -1 && blueprint.id_1 == this.items[x_1, y_1].id) || (blueprint.id_0 == -1 && x_1 == -1 || x_1 != -1 && blueprint.id_0 == this.items[x_1, y_1].id) && (blueprint.id_1 == -1 && x_0 == -1 || x_0 != -1 && blueprint.id_1 == this.items[x_0, y_0].id)) && (blueprint.idTool == -1 && xTool == -1 || xTool != -1 && blueprint.idTool == this.items[xTool, yTool].id)) {
                            int num1 = 0;
                            while (true) {
                                if ((blueprint.id_0 == -1 && x_0 == -1 || x_0 != -1 && blueprint.id_0 == this.items[x_0, y_0].id && this.items[x_0, y_0].amount >= blueprint.amount_0) && (blueprint.id_1 == -1 && x_1 == -1 || x_1 != -1 && blueprint.id_1 == this.items[x_1, y_1].id && this.items[x_1, y_1].amount >= blueprint.amount_1) && (blueprint.idTool == -1 && xTool == -1 || xTool != -1 && blueprint.idTool == this.items[xTool, yTool].id)) {
                                    if (x_0 != -1) {
                                        this.useItem(x_0, y_0, blueprint.amount_0);
                                    }
                                    if (x_1 != -1) {
                                        this.useItem(x_1, y_1, blueprint.amount_1);
                                    }
                                    num1++;
                                }
                                else if ((blueprint.id_0 != -1 || x_1 != -1) && (x_1 == -1 || blueprint.id_0 != this.items[x_1, y_1].id || this.items[x_1, y_1].amount < blueprint.amount_0) || (blueprint.id_1 != -1 || x_0 != -1) && (x_0 == -1 || blueprint.id_1 != this.items[x_0, y_0].id || this.items[x_0, y_0].amount < blueprint.amount_1) || (blueprint.idTool != -1 || xTool != -1) && (xTool == -1 || blueprint.idTool != this.items[xTool, yTool].id)) {
                                    break;
                                }
                                else {
                                    if (x_1 != -1) {
                                        this.useItem(x_1, y_1, blueprint.amount_0);
                                    }
                                    if (x_0 != -1) {
                                        this.useItem(x_0, y_0, blueprint.amount_1);
                                    }
                                    num1++;
                                }
                                if (!all || ItemType.getType(blueprint.id_0) == 10 || ItemType.getType(blueprint.id_1) == 10) {
                                    break;
                                }
                            }
                            for (int i = 0; i < num1; i++) {
                                this.tryAddItem(blueprint.idReward, blueprint.amountReward);
                            }
                            flag1 = true;
                        }
                        num++;
                    }
                    else {
                        if (x_0 != -1) {
                            this.syncItem(x_0, y_0);
                        }
                        if (x_1 != -1) {
                            this.syncItem(x_1, y_1);
                        }
                        NetworkSounds.askSound("Sounds/General/craft", base.transform.position, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
                        break;
                    }
                }
            }
            else {
                int num2 = this.items[x_0, y_0].id;
                int num3 = this.items[x_1, y_1].id;
                int amount = this.items[x_0, y_0].amount;
                int amount1 = this.items[x_1, y_1].amount;
                amount1 = amount1 + (this.items[x_0, y_0].amount - 1);
                if (amount1 <= ItemAmount.getAmount(this.items[x_1, y_1].id)) {
                    num2 = -1;
                }
                else {
                    amount = amount1 - ItemAmount.getAmount(this.items[x_1, y_1].id) + 1;
                    amount1 = ItemAmount.getAmount(this.items[x_1, y_1].id);
                }
                this.deleteItem(x_0, y_0);
                this.deleteItem(x_1, y_1);
                this.syncItem(x_0, y_0);
                this.syncItem(x_1, y_1);
                if (num2 != -1) {
                    this.tryAddItem(num2, amount, string.Empty);
                }
                if (num3 != -1) {
                    this.tryAddItem(num3, amount1, string.Empty);
                }
                NetworkSounds.askSound("Sounds/General/craft", base.transform.position, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
            }
        }
    }

    [RPC]
    public void askDeleteItem(int x, int y, NetworkMessageInfo info) { // Wrong position, sorry...
        // Hack detect
        if (info.sender != base.networkView.owner) {
            NetworkTools.kick(info.sender, "Inventory clear hack detected. Incident reported.");
			NetworkUser user = NetworkUserList.getUserFromPlayer(info.sender);
            Logger.LogSecurity(user.id, user.name, "Inventory clear hack.");
            return;
        }

        this.deleteItem(x, y);
        this.syncItem(x, y);
    }

    [RPC]
    public void askDrag(int x_0, int y_0, int x_1, int y_1, bool all) {
        if (x_0 != x_1 || y_0 != y_1) {
            ClientItem clientItem = this.items[x_0, y_0];
            ClientItem clientItem1 = this.items[x_1, y_1];
            if (clientItem.id == clientItem1.id && ItemStackable.getStackable(clientItem.id) && (x_0 != x_1 || y_0 != y_1)) {
                if (this.items[x_1, y_1].amount <= Inventory.MAX_STACK) {
                    this.items[x_1, y_1].amount = this.items[x_1, y_1].amount + clientItem.amount;
                    if (this.items[x_1, y_1].amount <= Inventory.MAX_STACK) {
                        this.items[x_0, y_0].id = -1;
                        this.items[x_0, y_0].amount = 0;
                        this.items[x_0, y_0].state = string.Empty;
                    }
                    else {
                        this.items[x_0, y_0].amount = this.items[x_1, y_1].amount - Inventory.MAX_STACK;
                        this.items[x_1, y_1].amount = Inventory.MAX_STACK;
                    }
                }
                else {
                    this.items[x_0, y_0] = clientItem1;
                    this.items[x_1, y_1] = clientItem;
                }
            }
            else if (all) {
                this.items[x_0, y_0] = clientItem1;
                this.items[x_1, y_1] = clientItem;
            }
            else if (clientItem1.id != -1 || !ItemStackable.getStackable(clientItem.id)) {
                this.items[x_0, y_0] = clientItem1;
                this.items[x_1, y_1] = clientItem;
            }
            else {
                this.items[x_1, y_1] = clientItem;
                if (clientItem.amount <= 1) {
                    this.items[x_0, y_0] = clientItem1;
                }
                else {
                    this.items[x_1, y_1].amount = Mathf.FloorToInt((float)clientItem.amount / 2f);
                    this.items[x_0, y_0].amount = Mathf.CeilToInt((float)clientItem.amount / 2f);
                }
            }
            this.syncItem(x_0, y_0);
            this.syncItem(x_1, y_1);
        }
    }

    [RPC]
    public void askUseItem(int x, int y) {
        this.useItem(x, y);
        this.syncItem(x, y);
    }

    public void deleteItem(int x, int y) {
        if (!ItemStackable.getStackable(this.items[x, y].id)) {
            Inventory weight = this;
            weight.weight = weight.weight - ItemWeight.getWeight(this.items[x, y].id);
        }
        else {
            Inventory inventory = this;
            inventory.weight = inventory.weight - ItemWeight.getWeight(this.items[x, y].id) * this.items[x, y].amount;
        }
        this.syncWeight();
        this.items[x, y].id = -1;
        this.items[x, y].amount = 0;
        this.items[x, y].state = string.Empty;
    }

    public void drop() {
        //this.resize(0, 0, 0);
        Vector3 position = base.transform.position;

        int maxItems = height * width;
        int droppableItems = (int)(maxItems * 0.60);

        List<int> list = new List<int>();

        int i = 0;
        do {
            int rand = UnityEngine.Random.Range(0, maxItems - 1);
            if (!list.Contains(rand)) 
            {
                list.Add(rand);
                i++;
            }

        } while (i < droppableItems);

        foreach( int item in list )
        {
            int itemX = item / height;
            int itemY = item % height;

            try {
                if (!ItemStackable.getStackable(this.items[itemX, itemY].id)) 
                {
					SpawnItems.drop(this.items[itemX, itemY].id, this.items[itemX, itemY].amount, this.items[itemX, itemY].state, position + new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0f, UnityEngine.Random.Range(-1.5f, 1.5f)));
                }
                else 
                {
                    for (int n = 0; n < this.items[itemX, itemY].amount; n++) {
						SpawnItems.drop(
                            this.items[itemX, itemY].id, 
                            1, 
                            this.items[itemX, itemY].state, 
                            position + new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0f, UnityEngine.Random.Range(-1.5f, 1.5f))
                        );
                    }
                }

                this.items[itemX, itemY].amount = 0;
                this.items[itemX, itemY].id = -1;
                this.items[itemX, itemY].state = String.Empty;
            } 
            catch (Exception e)
            {
                Console.WriteLine("Error while dropping item: " + e.Data + " X: " + itemX + " Y:" + itemY);
            }
        }

		weight = 0;
        for (int itemX = 0; itemX < this.width; itemX++) {
            for (int itemY = 0; itemY < this.height; itemY++) {
                this.syncItem(itemX, itemY);
				this.weight += ItemWeight.getWeight(this.items[itemX, itemY].id) * this.items[itemX, itemY].amount;
            }
        }

		this.syncWeight();
    }

    public int hasSpace(ServerItem item) {
        if (ItemWeight.getWeight(item.id) == -1000) {
            return 1;
        }
        if (this.weight + ItemWeight.getWeight(item.id) > this.capacity) {
            return 2;
        }
        if (!ItemStackable.getStackable(item.id)) {
            if (this.search(-1).x != -1) {
                return 0;
            }
            return 1;
        }
        Point2 point2 = this.search(item.id);
        if (point2.x != -1 && this.items[point2.x, point2.y].amount < Inventory.MAX_STACK) {
            return 0;
        }
        point2 = this.search(-1);
        if (point2.x != -1) {
            return 0;
        }
        return 1;
    }

	/// <summary>
	/// Loads the inventory
	/// </summary>
    public void load() {
		// FIXME: config manager implementation
		return;
        if (Network.isServer) {
            this.loadAllItems();
        }
        else
        {
            base.networkView.RPC("loadAllItems", RPCMode.Server, new object[0]);
        }
    }

    [RPC]
    public void loadAllItems() {
        NetworkUser userFromPlayer = NetworkUserList.getUserFromPlayer(base.networkView.owner);
        string empty = string.Empty;
        if (userFromPlayer != null) {
            empty = Savedata.loadInventory(userFromPlayer.id);
        }
        this.loadAllItemsFromSerial(empty);
    }

    [RPC]
    public void loadAllItemsFromSerial(string serial) {
        if (serial != string.Empty) {
            string[] strArrays = Packer.unpack(serial, ';');
            int backpackWidth = int.Parse(strArrays[0]);
            int backpackHeight = int.Parse(strArrays[1]);
            int backpackWeight = int.Parse(strArrays[2]);
            
			if (backpackWidth > 5) {
                backpackWidth = 5;
            }

            if (backpackHeight > 5) {
                backpackHeight = 5;
            }

            if (backpackWeight > 20000) {
                backpackWeight = 20000;
            }

			int backpackItems = (int)strArrays.Length - 3;
            
			this.resize(backpackWidth, backpackHeight, backpackWeight);

			for (int i = 0; i < backpackItems; i++) {
				int bagX = i % this.width;
				int bagY = i / this.width;
				string[] clientItem = Packer.unpack(strArrays[3 + i], ':');
				this.items[bagX, bagY] = new ClientItem(int.Parse(clientItem[0]), int.Parse(clientItem[1]), clientItem[2]);
			}

			UpdateItems();

            this.syncWeight();
        }
        else {
            if (base.GetComponent<Clothes>().backpack == -1) { // Not died, respawn with no sync!
                this.resize(BagSize.getWidth(-1), BagSize.getHeight(-1), BagSize.getCapacity(-1));
                if (ServerSettings.mode == 1) {
                    this.tryAddItem(8008, 1);
                    this.tryAddItem(14021, 1);
                    this.tryAddItem(15002, 1);
                }
                this.syncWeight();
            }
        }
        this.loaded = true;
        if (!base.networkView.isMine) {
            base.networkView.RPC("tellLoadedInventory", base.networkView.owner, new object[] { true });
        }
    }

	public void UpdateItems()
	{
		for (int i = 0; i < this.height * this.width; i++) 
		{
			int bagX = i % this.width;
			int bagY = i / this.width;
			
			if (ItemStackable.getStackable(this.items[bagX, bagY].id) && this.items[bagX, bagY].amount > Inventory.MAX_STACK) 
			{
				this.items[bagX, bagY].amount = Inventory.MAX_STACK;
			}
			else if (ItemType.getType(this.items[bagX, bagY].id) == 10 && this.items[bagX, bagY].amount > ItemAmount.getAmount(this.items[bagX, bagY].id)) 
			{
				this.items[bagX, bagY].amount = ItemAmount.getAmount(this.items[bagX, bagY].id);
			}
			else if (ItemType.getType(this.items[bagX, bagY].id) == 7) 
			{
				if (this.items[bagX, bagY].state == string.Empty) 
				{
					this.items[bagX, bagY].id = -1;
					this.items[bagX, bagY].amount = 0;
					this.items[bagX, bagY].state = string.Empty;
				}
				else 
				{
					string[] itemStateArray = Packer.unpack(this.items[bagX, bagY].state, '\u005F');
					int bullets = int.Parse(itemStateArray[0]);
					int ammoType = int.Parse(itemStateArray[1]);
				
					if (!AmmoStats.getGunCompatible(this.items[bagX, bagY].id, ammoType)) {
						ammoType = -1;
						bullets = 0;
					}
					else if (bullets > AmmoStats.getCapacity(this.items[bagX, bagY].id, ammoType)) 
					{
						bullets = 0;
					}

					object[] objArray = new object[] { bullets, "_", ammoType, "_", itemStateArray[2], "_", itemStateArray[3], "_", itemStateArray[4], "_", itemStateArray[5], "_", itemStateArray[6], "_" };
					this.items[bagX, bagY].state = string.Concat(objArray);
				}
			}

			if (this.items[bagX, bagY].id != -1) {
				if (!ItemStackable.getStackable(this.items[bagX, bagY].id)) {
					Inventory weight = this;
					weight.weight = weight.weight + ItemWeight.getWeight(this.items[bagX, bagY].id);
				}
				else {
					Inventory inventory = this;
					inventory.weight = inventory.weight + ItemWeight.getWeight(this.items[bagX, bagY].id) * this.items[bagX, bagY].amount;
				}
			}
			this.syncItem(bagX, bagY);
		}
	}

    public void resize(int setWidth, int setHeight, int setCapacity) {
        ClientItem[,] clientItem = new ClientItem[setWidth, setHeight];
        int weight = 0;
        for (int i = 0; i < setWidth; i++) {
            for (int j = 0; j < setHeight; j++) {
                clientItem[i, j] = new ClientItem(-1, 0, string.Empty);
            }
        }
        if (this.items != null) {
            Vector3 position = base.transform.position;

            if (base.GetComponent<Player>().vehicle != null) 
            {
                position = base.GetComponent<Player>().vehicle.getPosition();
            }

            for (int k = 0; k < this.width; k++) {
                for (int l = 0; l < this.height; l++) 
                {
                    if (k < setWidth && l < setHeight) 
                    {
                        if (this.items[k, l].id != -1) 
                        {
                            if (ItemStackable.getStackable(this.items[k, l].id)) 
                            {
                                clientItem[k, l] = this.items[k, l];
                                clientItem[k, l].amount = 0;

                                for (int m = 0; m < this.items[k, l].amount; m++) 
                                {
                                    if (weight + ItemWeight.getWeight(this.items[k, l].id) > setCapacity) 
                                    {
                                        SpawnItems.drop(this.items[k, l].id, 1, this.items[k, l].state, position + new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0f, UnityEngine.Random.Range(-1.5f, 1.5f)));
                                    }
                                    else 
                                    {
                                        clientItem[k, l].amount = clientItem[k, l].amount + 1;
                                        weight = weight + ItemWeight.getWeight(this.items[k, l].id);
                                    }
                                }

                                if (clientItem[k, l].amount == 0) 
                                {
                                    clientItem[k, l].id = -1;
                                    clientItem[k, l].state = string.Empty;
                                }
                            }
                            else if (weight + ItemWeight.getWeight(this.items[k, l].id) > setCapacity) 
                            {
                                SpawnItems.drop(this.items[k, l].id, this.items[k, l].amount, this.items[k, l].state, position + new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0f, UnityEngine.Random.Range(-1.5f, 1.5f)));
                            }
                            else 
                            {
                                clientItem[k, l] = this.items[k, l];
                                weight = weight + ItemWeight.getWeight(this.items[k, l].id);
                            }
                        }
                    }
                    else if (this.items[k, l].id != -1) 
                    {
                        if (!ItemStackable.getStackable(this.items[k, l].id)) 
                        {
                            SpawnItems.drop(this.items[k, l].id, this.items[k, l].amount, this.items[k, l].state, position + new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0f, UnityEngine.Random.Range(-1.5f, 1.5f)));
                        }
                        else 
                        {
                            for (int n = 0; n < this.items[k, l].amount; n++) {
                                SpawnItems.drop(this.items[k, l].id, 1, this.items[k, l].state, position + new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0f, UnityEngine.Random.Range(-1.5f, 1.5f)));
                            }
                        }
                    }
                }
            }
        }

        this.width = setWidth;
        this.height = setHeight;
        this.weight = weight;
        this.capacity = setCapacity;

        if (base.networkView.owner != Network.player) 
        {
            base.networkView.RPC("syncSize", base.networkView.owner, new object[] { this.width, this.height, this.capacity });
        }
        else 
        {
            this.syncSize_Pizza(this.width, this.height, this.capacity);
        }

        this.items = clientItem;
        for (int o = 0; o < this.width; o++) {
            for (int p = 0; p < this.height; p++) {
                this.syncItem(o, p);
            }
        }
    }

    public void saveAllItems() {
        if (this.loaded) {
            string empty = string.Empty;
            
			string str = empty;
            empty = string.Concat(new object[] { str, this.width, ";", this.height, ";", this.capacity, ";" });
            
			for (int i = 0; i < this.height; i++) {
            	for (int j = 0; j < this.width; j++) {
                	str = empty;
					empty = string.Concat(new object[] { str, this.items[j, i].id, ":", this.items[j, i].amount, ":", this.items[j, i].state, ":;" });
				}
			}
        }
    }

    public Point2 search(int id) {
        for (int i = 0; i < this.height; i++) {
            for (int j = 0; j < this.width; j++) {
                if (this.items[j, i].id == id && (!ItemStackable.getStackable(id) || this.items[j, i].amount < Inventory.MAX_STACK)) {
                    return new Point2(j, i);
                }
            }
        }
        return Point2.NONE;
    }

    public void sendCraft(int x_0, int y_0, int x_1, int y_1, int xTool, int yTool, int mode, bool all) {
        if (!Network.isServer) {
            base.networkView.RPC("askCraft", RPCMode.Server, new object[] { x_0, y_0, x_1, y_1, xTool, yTool, mode, all });
        }
        else {
            this.askCraft(x_0, y_0, x_1, y_1, xTool, yTool, mode, all);
        }
    }

    public void sendDeleteItem(int x, int y) {
        if (!Network.isServer) {
            if (!ItemStackable.getStackable(this.items[x, y].id)) {
                Inventory weight = this;
                weight.weight = weight.weight - ItemWeight.getWeight(this.items[x, y].id);
            }
            else {
                Inventory inventory = this;
                inventory.weight = inventory.weight - ItemWeight.getWeight(this.items[x, y].id) * this.items[x, y].amount;
            }
            this.items[x, y].id = -1;
            this.items[x, y].amount = 0;
            this.items[x, y].state = string.Empty;
            base.networkView.RPC("askDeleteItem", RPCMode.Server, new object[] { x, y });
        }
        else {
            this.deleteItem(x, y);
            this.syncItem(x, y);
        }
    }

    public void sendDrag(int x_0, int y_0, int x_1, int y_1, bool all) {
        if (x_0 == x_1 && y_0 == y_1) {
            //HUDInventory.updateItems();
        }
        else if (!Network.isServer) {
            base.networkView.RPC("askDrag", RPCMode.Server, new object[] { x_0, y_0, x_1, y_1, all });
        }
        else {
            this.askDrag(x_0, y_0, x_1, y_1, all);
        }
    }

    public void sendUseItem(int x, int y) {
        if (!Network.isServer) {
            Inventory weight = this;
            weight.weight = weight.weight - ItemWeight.getWeight(this.items[x, y].id);
            if (!ItemStackable.getStackable(this.items[x, y].id)) {
                this.items[x, y].amount = 0;
            }
            else {
                this.items[x, y].amount = this.items[x, y].amount - 1;
            }
            if (this.items[x, y].amount == 0) {
                this.items[x, y].id = -1;
                this.items[x, y].amount = 0;
                this.items[x, y].state = string.Empty;
            }
            base.networkView.RPC("askUseItem", RPCMode.Server, new object[] { x, y });
        }
        else {
            this.askUseItem(x, y);
        }
    }

    public void Start() {
        if (base.networkView.isMine) {
            this.load();
        }
    }

    public void syncItem(int x, int y) {
        if (!base.networkView.isMine) {
            base.networkView.RPC("tellItemSlot", base.networkView.owner, new object[] { x, y, this.items[x, y].id, this.items[x, y].amount, this.items[x, y].state });
        }
        else {
            this.tellItemSlot_Pizza(x, y, this.items[x, y].id, this.items[x, y].amount, this.items[x, y].state);
        }
    }

    [RPC]
    public void syncSize(int setWidth, int setHeight, int setCapacity, NetworkMessageInfo info) {
        if (info.sender.ToString() == "0" || info.sender.ToString() == "-1") {
            this.syncSize_Pizza(setWidth, setHeight, setCapacity);
        }
    }

    public void syncSize_Pizza(int setWidth, int setHeight, int setCapacity) {
        this.width = setWidth;
        this.height = setHeight;
        this.capacity = setCapacity;
        this.items = new ClientItem[this.width, this.height];
        for (int i = 0; i < this.width; i++) {
            for (int j = 0; j < this.height; j++) {
                this.items[i, j] = new ClientItem(-1, 0, string.Empty);
            }
        }
        //HUDInventory.resize(this.width, this.height);
        //HUDInventory.updateWeight();
    }

    public void syncWeight() {
        if (!base.networkView.isMine) {
            base.networkView.RPC("tellWeight", base.networkView.owner, new object[] { this.weight });
        }
        else {
            this.setWeight(this.weight);
        }
    }

    [RPC]
    public void tellItemSlot(int x, int y, int id, int amount, string state, NetworkMessageInfo info) {
        if (info.sender != Network.player)
        {
			NetworkUser user = NetworkUserList.getUserFromPlayer(info.sender);
			Logger.LogSecurity(user.id, user.name, "Added an item to self! ID: " + id + " Amount: " + amount + " name: " + ItemName.getName(id));
            if ( info.sender != null ) 
            {
                NetworkBans.ban(user.name, user.id, "Item add hack (Auto banned)", "SERVER");
                return;
            }
        }

        if (info.sender.ToString() == "0" || info.sender.ToString() == "-1") {
            this.tellItemSlot_Pizza(x, y, id, amount, state);
        }
    }

    public void tellItemSlot_Pizza(int x, int y, int id, int amount, string state) {
        this.items[x, y].id = id;
        this.items[x, y].amount = amount;
        this.items[x, y].state = state;
        //HUDInventory.updateItems();
        //HUDCrafting.updateItems();
    }

    [RPC]
    public void tellLoadedInventory(bool setLoaded) {
        this.loaded = setLoaded;
    }

    [RPC]
    public void tellWeight(int setWeight, NetworkMessageInfo info) {
        if (base.networkView.owner != info.sender) {
            NetworkTools.kick(info.sender, "VAC: Player freeze hack detected. Incident reported.");
			NetworkUser user = NetworkUserList.getUserFromPlayer(info.sender);
			Logger.LogSecurity(user.id, user.name, "Inventory weight set crash");
            return;
        }

        this.setWeight(setWeight);
    }

    private void setWeight(int weight) {
        this.weight = weight;
        if (this.capacity == 0) {
            this.speed = 1f;
        }
        else {
            this.speed = 0.9f + (1f - (float)Player.inventory.weight / (float)Player.inventory.capacity) * 0.1f;
        }
        //HUDInventory.updateWeight();
    }

    public void tryAddItem(int id, int amount) {
        if (ItemWeight.getWeight(id) != -1000) {
            ServerItem serverItem = new ServerItem(id, ItemAmount.getAmount(id), ItemState.getState(id), Vector3.zero);
            Vector3 position = base.transform.position;

            if (base.GetComponent<Player>().vehicle != null) {
                position = base.GetComponent<Player>().vehicle.getPosition();
            }

            for (int i = 0; i < amount; i++) {
                if (this.hasSpace(serverItem) != 0) {
                    SpawnItems.dropItem(id, position);
                }
                else {
                    this.addItem(serverItem);
                }
            }
        }
    }

    public void tryAddItem(int id, int amount, string state) {
        if (ItemWeight.getWeight(id) != -1000) {
            ServerItem serverItem = new ServerItem(id, amount, state, Vector3.zero);
            if (this.hasSpace(serverItem) != 0) {
                Vector3 position = base.transform.position;
                if (base.GetComponent<Player>().vehicle != null) {
                    position = base.GetComponent<Player>().vehicle.getPosition();
                }
                SpawnItems.drop(id, amount, state, position);
            }
            else {
                this.addItem(serverItem);
            }
        }
    }

    public void useItem(int x, int y, int amount) {
        if (!ItemStackable.getStackable(this.items[x, y].id)) {
            Inventory weight = this;
            weight.weight = weight.weight - ItemWeight.getWeight(this.items[x, y].id);
            this.syncWeight();
            this.items[x, y].id = -1;
            this.items[x, y].amount = 0;
            this.items[x, y].state = string.Empty;
        }

        else {
            Inventory inventory = this;
            inventory.weight = inventory.weight - ItemWeight.getWeight(this.items[x, y].id) * amount;
            this.syncWeight();
            this.items[x, y].amount = this.items[x, y].amount - amount;
            if (this.items[x, y].amount == 0) {
                this.items[x, y].id = -1;
                this.items[x, y].amount = 0;
                this.items[x, y].state = string.Empty;
            }
        }
    }

    public void useItem(int x, int y) {
        Inventory weight = this;
        weight.weight = weight.weight - ItemWeight.getWeight(this.items[x, y].id);
        this.syncWeight();
        if (!ItemStackable.getStackable(this.items[x, y].id)) {
            this.items[x, y].amount = 0;
        }
        else {
            this.items[x, y].amount = this.items[x, y].amount - 1;
        }
        if (this.items[x, y].amount == 0) {
            this.items[x, y].id = -1;
            this.items[x, y].amount = 0;
            this.items[x, y].state = string.Empty;
        }
    }
}