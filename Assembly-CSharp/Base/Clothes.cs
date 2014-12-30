using System;
using UnityEngine;

public class Clothes : MonoBehaviour
{
	public int face = 6001;

	public int shirt = -1;

	public int pants = -1;

	public int hat = -1;

	public int hair = -1;

	public int backpack = -1;

	public int vest = -1;

	public int item = -1;

	public string state = string.Empty;

	public int skinColor;

	public int hairColor;

	public bool arm;

	private Character character;

	private bool loaded;

	public Clothes()
	{
	}

	[RPC]
	public void askAllClothes(NetworkPlayer player)
	{
		if (player != Network.player)
		{
			base.networkView.RPC("tellAllClothes", player, new object[] { this.face, this.shirt, this.pants, this.hat, this.hair, this.backpack, this.vest, this.item, this.state, this.skinColor, this.hairColor, this.arm });
		}
		else
		{
			this.tellAllClothes(this.face, this.shirt, this.pants, this.hat, this.hair, this.backpack, this.vest, this.item, this.state, this.skinColor, this.hairColor, this.arm);
		}
	}

	public void Awake()
	{
		if (!base.networkView.isMine)
		{
			this.character = base.transform.FindChild("thirdPerson").FindChild("character").GetComponent<Character>();
		}
	}

	public void changeBackpack(int setBackpack)
	{
		if (setBackpack != -1 || this.backpack != -1)
		{
			if (Equipment.equipped.x != -1 && (Equipment.equipped.x >= BagSize.getWidth(setBackpack) || Equipment.equipped.y >= BagSize.getHeight(setBackpack)) && Equipment.id != setBackpack)
			{
				Equipment.dequip();
			}
			base.networkView.RPC("tellBackpack", RPCMode.All, new object[] { setBackpack });
			PlayerPrefs.SetInt(string.Concat("lastBackpack_", PlayerSettings.id), Sneaky.sneak(setBackpack));
			HUDCharacter.updateItems();
			NetworkSounds.askSound("Sounds/Clothes/zipper", base.transform.position, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
		}
	}

	public void changeHat(int setHat)
	{
		if (setHat != -1 || this.hat != -1)
		{
			Sun.setVision(setHat, HUDGame.nvg);
			base.networkView.RPC("tellHat", RPCMode.All, new object[] { setHat });
			PlayerPrefs.SetInt(string.Concat("lastHat_", PlayerSettings.id), Sneaky.sneak(setHat));
			HUDCharacter.updateItems();
			NetworkSounds.askSound("Sounds/Clothes/sleeve", base.transform.position, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
		}
	}

	public void changeItem(int setItem, string setState)
	{
		base.networkView.RPC("tellItem", RPCMode.All, new object[] { setItem, setState });
		PlayerPrefs.SetInt(string.Concat("lastItem_", PlayerSettings.id), Sneaky.sneak(setItem));
		PlayerPrefs.SetString(string.Concat("lastState_", PlayerSettings.id), Sneaky.sneak(setState));
		HUDCharacter.updateItems();
	}

	public void changePants(int setPants)
	{
		if (setPants != -1 || this.pants != -1)
		{
			base.networkView.RPC("tellPants", RPCMode.All, new object[] { setPants });
			PlayerPrefs.SetInt(string.Concat("lastPants_", PlayerSettings.id), Sneaky.sneak(setPants));
			HUDCharacter.updateItems();
			NetworkSounds.askSound("Sounds/Clothes/sleeve", base.transform.position, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
		}
	}

	public void changeShirt(int setShirt)
	{
		if (setShirt != -1 || this.shirt != -1)
		{
			base.networkView.RPC("tellShirt", RPCMode.All, new object[] { setShirt });
			PlayerPrefs.SetInt(string.Concat("lastShirt_", PlayerSettings.id), Sneaky.sneak(setShirt));
			HUDCharacter.updateItems();
			NetworkSounds.askSound("Sounds/Clothes/sleeve", base.transform.position, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
		}
	}

	public void changeVest(int setVest)
	{
		if (setVest != -1 || this.vest != -1)
		{
			base.networkView.RPC("tellVest", RPCMode.All, new object[] { setVest });
			PlayerPrefs.SetInt(string.Concat("lastVest_", PlayerSettings.id), Sneaky.sneak(setVest));
			HUDCharacter.updateItems();
			NetworkSounds.askSound("Sounds/Clothes/zipper", base.transform.position, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
		}
	}

	public void drop()
	{
		Vector3 position = base.transform.position;
		if (base.GetComponent<Player>().vehicle != null)
		{
			position = base.GetComponent<Player>().vehicle.getPosition();
		}
		if (this.shirt != -1)
		{
			SpawnItems.dropItem(this.shirt, position);
		}
		if (this.pants != -1)
		{
			SpawnItems.dropItem(this.pants, position);
		}
		if (this.hat != -1)
		{
			SpawnItems.dropItem(this.hat, position);
		}
		if (this.backpack != -1)
		{
			SpawnItems.dropItem(this.backpack, position);
		}
		if (this.vest != -1)
		{
			SpawnItems.dropItem(this.vest, position);
		}
		this.shirt = -1;
		this.pants = -1;
		this.hat = -1;
		this.backpack = -1;
		this.vest = -1;
		this.item = -1;
		this.state = string.Empty;
		base.networkView.RPC("tellAllClothes", RPCMode.All, new object[] { this.face, this.shirt, this.pants, this.hat, this.hair, this.backpack, this.vest, this.item, this.state, this.skinColor, this.hairColor, this.arm });
	}

	public void load()
	{
		if (Network.isServer)
		{
			if (!ServerSettings.save)
			{
				this.loadAllClothing();
			}
			else
			{
				this.loadAllClothingFromSerial(Savedata.loadClothes(base.GetComponent<Player>().owner.id));
			}
		}
		else if (!ServerSettings.save)
		{
			base.networkView.RPC("loadAllClothing", RPCMode.Server, new object[0]);
		}
		else
		{
			base.networkView.RPC("loadAllClothingFromSerial", RPCMode.Server, new object[] { Savedata.loadClothes(PlayerSettings.id) });
		}
	}

	[RPC]
	public void loadAllClothing()
	{
		NetworkUser userFromPlayer = NetworkUserList.getUserFromPlayer(base.networkView.owner);
		string empty = string.Empty;
		if (userFromPlayer != null)
		{
			empty = Savedata.loadClothes(userFromPlayer.id);
		}
		this.loadAllClothingFromSerial(empty);
	}

	[RPC]
	public void loadAllClothingFromSerial(string serial)
	{
		if (serial != string.Empty)
		{
			string[] strArrays = Packer.unpack(serial, ';');
			this.shirt = int.Parse(strArrays[0]);
			this.pants = int.Parse(strArrays[1]);
			this.hat = int.Parse(strArrays[2]);
			this.backpack = int.Parse(strArrays[3]);
			this.vest = int.Parse(strArrays[4]);
		}
		else
		{
			if (ServerSettings.mode != 3)
			{
				this.shirt = -1;
				this.pants = -1;
				this.hat = -1;
			}
			else
			{
				this.shirt = 4000;
				this.pants = 5000;
				this.hat = 0;
			}
			this.backpack = -1;
			this.vest = -1;
		}
		this.item = -1;
		this.state = string.Empty;
		base.networkView.RPC("tellAllClothes", RPCMode.All, new object[] { this.face, this.shirt, this.pants, this.hat, this.hair, this.backpack, this.vest, this.item, this.state, this.skinColor, this.hairColor, this.arm });
		this.loaded = true;
		if (!base.networkView.isMine)
		{
			base.networkView.RPC("tellLoadedClothes", base.networkView.owner, new object[] { true });
		}
	}

	public void saveAllClothing()
	{
		if (this.loaded)
		{
			string empty = string.Empty;
			if (!base.GetComponent<Life>().dead)
			{
				string str = empty;
				empty = string.Concat(new object[] { str, this.shirt, ";", this.pants, ";", this.hat, ";", this.backpack, ";", this.vest, ";" });
			}
			Savedata.saveClothes(base.GetComponent<Player>().owner.id, empty);
		}
	}

	public void Start()
	{
		if (base.networkView.isMine)
		{
			if (!Network.isServer)
			{
				base.networkView.RPC("tellAllCustom", RPCMode.Server, new object[] { PlayerSettings.face, PlayerSettings.hair, PlayerSettings.skinColor, PlayerSettings.hairColor, PlayerSettings.arm });
			}
			else
			{
				this.tellAllCustom(PlayerSettings.face, PlayerSettings.hair, PlayerSettings.skinColor, PlayerSettings.hairColor, PlayerSettings.arm);
			}
			this.load();
		}
		else if (!Network.isServer)
		{
			base.networkView.RPC("askAllClothes", RPCMode.Server, new object[] { Network.player });
		}
	}

	[RPC]
	public void tellAllClothes(int setFace, int setShirt, int setPants, int setHat, int setHair, int setBackpack, int setVest, int setItem, string setState, int setSkinColor, int setHairColor, bool setArm)
	{
		this.face = setFace;
		this.shirt = setShirt;
		this.pants = setPants;
		this.hat = setHat;
		this.hair = setHair;
		this.backpack = setBackpack;
		this.vest = setVest;
		this.item = setItem;
		this.state = setState;
		this.skinColor = setSkinColor;
		this.hairColor = setHairColor;
		this.arm = setArm;
		base.GetComponent<Player>().arm = this.arm;
		if (this.character != null)
		{
			this.character.face = this.face;
			this.character.shirt = this.shirt;
			this.character.pants = this.pants;
			this.character.hat = this.hat;
			this.character.hair = this.hair;
			this.character.backpack = this.backpack;
			this.character.vest = this.vest;
			this.character.item = this.item;
			this.character.state = this.state;
			this.character.skinColor = this.skinColor;
			this.character.hairColor = this.hairColor;
			this.character.arm = this.arm;
			this.character.wear();
		}
		if (base.networkView.isMine)
		{
			Sun.setVision(this.hat, HUDGame.nvg);
			Viewmodel.wear();
			PlayerPrefs.SetInt(string.Concat("lastShirt_", PlayerSettings.id), Sneaky.sneak(this.shirt));
			PlayerPrefs.SetInt(string.Concat("lastPants_", PlayerSettings.id), Sneaky.sneak(this.pants));
			PlayerPrefs.SetInt(string.Concat("lastHat_", PlayerSettings.id), Sneaky.sneak(this.hat));
			PlayerPrefs.SetInt(string.Concat("lastBackpack_", PlayerSettings.id), Sneaky.sneak(this.backpack));
			PlayerPrefs.SetInt(string.Concat("lastVest_", PlayerSettings.id), Sneaky.sneak(this.vest));
			PlayerPrefs.SetInt(string.Concat("lastItem_", PlayerSettings.id), Sneaky.sneak(-1));
			PlayerPrefs.SetString(string.Concat("lastState_", PlayerSettings.id), Sneaky.sneak(string.Empty));
			HUDCharacter.updateItems();
		}
	}

	[RPC]
	public void tellAllCustom(int setFace, int setHair, int setSkinColor, int setHairColor, bool setArm)
	{
		this.face = setFace;
		this.hair = setHair;
		this.skinColor = setSkinColor;
		this.hairColor = setHairColor;
		this.arm = setArm;
		base.GetComponent<Player>().arm = this.arm;
		if (this.character != null)
		{
			this.character.face = this.face;
			this.character.hair = this.hair;
			this.character.skinColor = this.skinColor;
			this.character.hairColor = this.hairColor;
			this.character.arm = this.arm;
			this.character.wear();
		}
		if (base.networkView.isMine)
		{
			Viewmodel.wear();
		}
	}

	[RPC]
	public void tellBackpack(int setBackpack)
	{
		if (Network.isServer && this.backpack != -1 && ItemType.getType(this.backpack) == 2)
		{
			SpawnItems.dropItem(this.backpack, base.transform.position);
		}
		this.backpack = setBackpack;
		if (this.character != null)
		{
			this.character.backpack = this.backpack;
			this.character.wear();
		}
		if (Network.isServer)
		{
			base.GetComponent<Inventory>().resize(BagSize.getWidth(this.backpack), BagSize.getHeight(this.backpack), BagSize.getCapacity(this.backpack));
		}
	}

	[RPC]
	public void tellHat(int setHat)
	{
		if (Network.isServer && this.hat != -1 && ItemType.getType(this.hat) == 0)
		{
			base.GetComponent<Inventory>().tryAddItem(this.hat, 1);
		}
		this.hat = setHat;
		if (this.character != null)
		{
			this.character.hat = this.hat;
			this.character.wear();
		}
	}

	[RPC]
	public void tellItem(int setItem, string setState)
	{
		this.item = setItem;
		this.state = setState;
		if (this.character != null)
		{
			this.character.item = this.item;
			this.character.state = this.state;
			this.character.wear();
		}
	}

	[RPC]
	public void tellLoadedClothes(bool setLoaded)
	{
		this.loaded = setLoaded;
	}

	[RPC]
	public void tellPants(int setPants)
	{
		if (Network.isServer && this.pants != -1 && ItemType.getType(this.pants) == 5)
		{
			base.GetComponent<Inventory>().tryAddItem(this.pants, 1);
		}
		this.pants = setPants;
		if (this.character != null)
		{
			this.character.pants = this.pants;
			this.character.wear();
		}
	}

	[RPC]
	public void tellShirt(int setShirt)
	{
		if (Network.isServer && this.shirt != -1 && ItemType.getType(this.shirt) == 4)
		{
			base.GetComponent<Inventory>().tryAddItem(this.shirt, 1);
		}
		this.shirt = setShirt;
		if (this.character != null)
		{
			this.character.shirt = this.shirt;
			this.character.wear();
		}
		if (base.networkView.isMine)
		{
			Viewmodel.wear();
		}
	}

	[RPC]
	public void tellVest(int setVest)
	{
		if (Network.isServer && this.vest != -1 && ItemType.getType(this.vest) == 3)
		{
			base.GetComponent<Inventory>().tryAddItem(this.vest, 1);
		}
		this.vest = setVest;
		if (this.character != null)
		{
			this.character.vest = this.vest;
			this.character.wear();
		}
	}
}