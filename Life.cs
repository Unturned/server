using System;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
	private int realHealth;

	private int realFood;

	private int realWater;

	private int realStamina;

	private int realSickness;

	public bool bleeding;

	public bool bones;

	public bool dead;

	public string death = string.Empty;

	private float lastSickDamage = Single.MaxValue;

	private float lastStarve = Single.MaxValue;

	private float lastFoodDamage = Single.MaxValue;

	private float lastDehydrate = Single.MaxValue;

	private float lastWaterDamage = Single.MaxValue;

	private float lastBleed = Single.MaxValue;

	private float lastStaminaTick;

	private float lastStamina;

	private float lastHealthy = Single.MaxValue;

	private float startedBleeding = Single.MaxValue;

	private float startedBones = Single.MaxValue;

	private float lastTrail;

	private float lastDied;

	private float lastEat;

	private int bufferEat;

	private float lastDrink;

	private int bufferDrink;

	private float lastHealth;

	private int bufferHealth;

	private float lastSick;

	private int bufferSick;

	private float lastEnergy;

	private int bufferEnergy;

	private bool loaded;

	public int food
	{
		get
		{
			return Sneaky.expose(this.realFood);
		}
		set
		{
			this.realFood = Sneaky.sneak(value);
		}
	}

	public int health
	{
		get
		{
			return Sneaky.expose(this.realHealth);
		}
		set
		{
			this.realHealth = Sneaky.sneak(value);
		}
	}

	public int sickness
	{
		get
		{
			return Sneaky.expose(this.realSickness);
		}
		set
		{
			this.realSickness = Sneaky.sneak(value);
		}
	}

	public int stamina
	{
		get
		{
			return Sneaky.expose(this.realStamina);
		}
		set
		{
			this.realStamina = Sneaky.sneak(value);
		}
	}

	public int water
	{
		get
		{
			return Sneaky.expose(this.realWater);
		}
		set
		{
			this.realWater = Sneaky.sneak(value);
		}
	}

	public Life()
	{
	}

	[RPC]
	public void askRespawn(bool bed)
	{
		if (this.dead)
		{
			Vector3 vector3 = base.transform.position;
			Vector3 vector31 = base.transform.rotation.eulerAngles;
			SpawnPlayers.die(vector3, (int)vector31.y, base.GetComponent<Clothes>().face, base.GetComponent<Clothes>().hair, base.GetComponent<Clothes>().skinColor, base.GetComponent<Clothes>().hairColor);
			this.health = 100;
			this.food = 100;
			this.water = 100;
			this.sickness = 100;
			this.bleeding = false;
			this.startedBleeding = Single.MaxValue;
			this.startedBones = Single.MaxValue;
			this.bones = false;
			this.dead = false;
			this.death = string.Empty;
			base.GetComponent<Inventory>().load();
			base.GetComponent<Clothes>().load();
			base.GetComponent<Skills>().load();
			this.load();
			base.GetComponent<Player>().owner.spawned = Time.realtimeSinceStartup;
			if (bed && Time.realtimeSinceStartup - this.lastDied < 20f && NetworkUserList.users.Count > 1 && ServerSettings.pvp)
			{
				bed = false;
			}
			Transform spawnPoint = SpawnPlayers.getSpawnPoint(base.networkView.owner, bed);
			NetworkView networkView = base.networkView;
			object[] objArray = new object[] { spawnPoint.position, null };
			Vector3 vector32 = spawnPoint.rotation.eulerAngles;
			objArray[1] = Quaternion.Euler(0f, vector32.y + 90f, 0f);
			networkView.RPC("tellStatePosition", RPCMode.All, objArray);
		}
	}

	public void damage(int amount, string killer)
	{
		if (amount != 0)
		{
			if (base.networkView.isMine && amount > 3)
			{
				HUDGame.lastFlash = Time.realtimeSinceStartup;
				Look.swayRoll = Look.swayRoll + (float)UnityEngine.Random.Range(-10, 10);
			}
			Life life = this;
			life.health = life.health - amount;
			this.death = killer;
			if (this.health <= 0)
			{
				this.health = 0;
				this.die();
			}
			if (base.networkView.owner != Network.player)
			{
				base.networkView.RPC("tellHealth", base.networkView.owner, new object[] { this.health });
			}
			else
			{
				this.tellHealthPleaseStopKuniiAlsoLetMeKnowIfYouWantToHelpWithAnticheatInVersion3(this.health);
			}
			if (amount > 20)
			{
				this.bleeding = true;
				this.startedBleeding = Time.realtimeSinceStartup;
				if (base.networkView.owner != Network.player)
				{
					base.networkView.RPC("tellBleeding", base.networkView.owner, new object[] { this.bleeding });
				}
				else
				{
					this.tellBleeding(this.bleeding);
				}
			}
			this.lastHealthy = Time.realtimeSinceStartup;
		}
	}

	public void dehydrate(int amount)
	{
		if (amount != 0)
		{
			Life life = this;
			life.water = life.water - amount;
			if (this.water < 0)
			{
				this.water = 0;
			}
			if (base.networkView.owner != Network.player)
			{
				base.networkView.RPC("tellWater", base.networkView.owner, new object[] { this.water });
			}
			else
			{
				this.tellWater(this.water);
			}
		}
	}

	public void die()
	{
		if (!this.dead)
		{
			this.lastDied = Time.realtimeSinceStartup;
			this.dead = true;
			if (base.GetComponent<Player>().vehicle != null)
			{
				base.transform.position = base.GetComponent<Player>().vehicle.getPosition();
				base.GetComponent<Player>().vehicle.eject(base.networkView.owner);
			}
			base.GetComponent<Inventory>().drop();
			base.GetComponent<Clothes>().drop();
			base.GetComponent<Inventory>().saveAllItems();
			base.GetComponent<Clothes>().saveAllClothing();
			base.GetComponent<Skills>().saveAllKnowledge();
			
			this.saveAllVitality();
			if (base.networkView.owner != Network.player)
			{
				base.networkView.RPC("tellDead", base.networkView.owner, new object[] { this.dead, this.death });
			}
			else
			{
				this.tellDead_Pizza(this.dead, this.death);
			}
		}
	}

	public void disinfect(int amount)
	{
		Life life = this;
		life.bufferSick = life.bufferSick + amount;
		this.lastSickDamage = Time.realtimeSinceStartup;
	}

	public void drink(int amount)
	{
		Life life = this;
		life.bufferDrink = life.bufferDrink + amount;
		this.lastDehydrate = Time.realtimeSinceStartup;
		this.lastWaterDamage = Time.realtimeSinceStartup;
	}

	public void drinkBuffer()
	{
		Life life = this;
		life.water = life.water + 1;
		Life life1 = this;
		life1.bufferDrink = life1.bufferDrink - 1;
		if (this.water > 100)
		{
			this.bufferDrink = 0;
			this.water = 100;
		}
		if (base.networkView.owner != Network.player)
		{
			base.networkView.RPC("tellWater", base.networkView.owner, new object[] { this.water });
		}
		else
		{
			this.tellWater(this.water);
		}
	}

	public void eat(int amount)
	{
		Life life = this;
		life.bufferEat = life.bufferEat + amount;
		this.lastStarve = Time.realtimeSinceStartup;
		this.lastFoodDamage = Time.realtimeSinceStartup;
	}

	public void eatBuffer()
	{
		Life life = this;
		life.food = life.food + 1;
		Life life1 = this;
		life1.bufferEat = life1.bufferEat - 1;
		if (this.food > 100)
		{
			this.bufferEat = 0;
			this.food = 100;
		}
		if (base.networkView.owner != Network.player)
		{
			base.networkView.RPC("tellFood", base.networkView.owner, new object[] { this.food });
		}
		else
		{
			this.tellFood(this.food);
		}
	}

	public void energyBuffer()
	{
		Life life = this;
		life.stamina = life.stamina + 1;
		Life life1 = this;
		life1.bufferEnergy = life1.bufferEnergy - 1;
		if (this.stamina > 100)
		{
			this.bufferEnergy = 0;
			this.stamina = 100;
		}
		if (this.stamina > 100)
		{
			this.stamina = 100;
		}
		HUDGame.updateStamina();
	}

	public void exhaust(int amount)
	{
		if (amount != 0)
		{
			this.lastStamina = Time.realtimeSinceStartup;
			Life life = this;
			life.stamina = life.stamina - amount;
			if (this.stamina < 0)
			{
				this.stamina = 0;
			}
			HUDGame.updateStamina();
		}
	}

	public void heal(int amount, bool bleed, bool bone)
	{
		if (amount != 0 || bleed || bone)
		{
			Life life = this;
			life.bufferHealth = life.bufferHealth + amount;
			if (this.bleeding && bleed)
			{
				this.bleeding = false;
				this.startedBleeding = Single.MaxValue;
				if (base.networkView.owner != Network.player)
				{
					base.networkView.RPC("tellBleeding", base.networkView.owner, new object[] { this.bleeding });
				}
				else
				{
					this.tellBleeding(this.bleeding);
				}
			}
			if (this.bones && bone)
			{
				this.bones = false;
				this.startedBones = Single.MaxValue;
				if (base.networkView.owner != Network.player)
				{
					base.networkView.RPC("tellBones", base.networkView.owner, new object[] { this.bones });
				}
				else
				{
					this.tellBones(this.bones);
				}
			}
			this.lastBleed = Time.realtimeSinceStartup;
		}
	}

	public void healBuffer()
	{
		Life life = this;
		life.health = life.health + 1;
		Life life1 = this;
		life1.bufferHealth = life1.bufferHealth - 1;
		if (this.health > 100)
		{
			this.bufferHealth = 0;
			this.health = 100;
		}
		if (base.networkView.owner != Network.player)
		{
			base.networkView.RPC("tellHealth", base.networkView.owner, new object[] { this.health });
		}
		else
		{
			this.tellHealthPleaseStopKuniiAlsoLetMeKnowIfYouWantToHelpWithAnticheatInVersion3(this.health);
		}
	}

	public void infect(int amount)
	{
		if (amount != 0)
		{
			amount = (int)((float)amount * (1f - base.GetComponent<Skills>().immunity() * 0.5f));
			Life life = this;
			life.sickness = life.sickness - amount;
			if (this.sickness < 0)
			{
				this.sickness = 0;
			}
			if (base.networkView.owner != Network.player)
			{
				base.networkView.RPC("tellSickness", base.networkView.owner, new object[] { this.sickness });
			}
			else
			{
				this.tellSickness(this.sickness);
			}
		}
	}

	public void load()
	{
		if (Network.isServer)
		{
			if (!ServerSettings.save)
			{
				this.loadAllVitality();
			}
			else
			{
				this.loadAllVitalityFromSerial(Savedata.loadLife(base.GetComponent<Player>().owner.id));
			}
		}
		else if (!ServerSettings.save)
		{
			base.networkView.RPC("loadAllVitality", RPCMode.Server, new object[0]);
		}
		else
		{
			base.networkView.RPC("loadAllVitalityFromSerial", RPCMode.Server, new object[] { Savedata.loadLife(PlayerSettings.id) });
		}
	}

	[RPC]
	public void loadAllVitality()
	{
		NetworkUser userFromPlayer = NetworkUserList.getUserFromPlayer(base.networkView.owner);
		string empty = string.Empty;
		if (userFromPlayer != null)
		{
			empty = Savedata.loadLife(userFromPlayer.id);
		}
		this.loadAllVitalityFromSerial(empty);
	}

	[RPC]
	public void loadAllVitalityFromSerial(string serial)
	{
		if (serial != string.Empty)
		{
			string[] strArrays = Packer.unpack(serial, ';');
			this.health = int.Parse(strArrays[0]);
			this.food = int.Parse(strArrays[1]);
			this.water = int.Parse(strArrays[2]);
			this.sickness = int.Parse(strArrays[3]);
			this.bleeding = strArrays[4] == "t";
			if (this.bleeding)
			{
				this.startedBleeding = Time.realtimeSinceStartup;
			}
			this.bones = strArrays[5] == "t";
			if (this.bones)
			{
				this.startedBones = Time.realtimeSinceStartup;
			}
			if (this.health > 100)
			{
				this.health = 100;
			}
			else if (this.health < 0)
			{
				this.health = 0;
			}
			if (this.food > 100)
			{
				this.food = 100;
			}
			else if (this.food < 0)
			{
				this.food = 0;
			}
			if (this.water > 100)
			{
				this.water = 100;
			}
			else if (this.water < 0)
			{
				this.water = 0;
			}
			if (this.sickness > 100)
			{
				this.sickness = 100;
			}
			else if (this.sickness < 0)
			{
				this.sickness = 0;
			}
		}
		else
		{
			this.health = 100;
			this.food = 100;
			this.water = 100;
			this.sickness = 100;
			this.bleeding = false;
			this.startedBleeding = Single.MaxValue;
			this.startedBones = Single.MaxValue;
			this.bones = false;
		}
		this.dead = false;
		this.death = string.Empty;
		if (base.networkView.owner != Network.player)
		{
			base.networkView.RPC("tellAllLife", base.networkView.owner, new object[] { this.health, this.food, this.water, this.sickness, this.bleeding, this.bones });
		}
		else
		{
			this.tellAllLife_Pizza(this.health, this.food, this.water, this.sickness, this.bleeding, this.bones);
		}
		this.lastSickDamage = Time.realtimeSinceStartup + 1f;
		this.lastStarve = Time.realtimeSinceStartup + 1f;
		this.lastFoodDamage = Time.realtimeSinceStartup + 1f;
		this.lastDehydrate = Time.realtimeSinceStartup + 1f;
		this.lastWaterDamage = Time.realtimeSinceStartup + 1f;
		this.lastBleed = Time.realtimeSinceStartup + 1f;
		this.loaded = true;
		if (!base.networkView.isMine)
		{
			base.networkView.RPC("tellLoadedLife", base.networkView.owner, new object[] { true });
		}
	}

	public void OnDestroy()
	{
		if (Network.isServer && this.dead)
		{
			Vector3 vector3 = base.transform.position;
			Vector3 vector31 = base.transform.rotation.eulerAngles;
			SpawnPlayers.die(vector3, (int)vector31.y, base.GetComponent<Clothes>().face, base.GetComponent<Clothes>().hair, base.GetComponent<Clothes>().skinColor, base.GetComponent<Clothes>().hairColor);
		}
	}

	public void respawn(bool bed)
	{
		if (!Network.isServer)
		{
			base.networkView.RPC("askRespawn", RPCMode.Server, new object[] { bed });
		}
		else
		{
			this.askRespawn(bed);
		}
	}

	public void rest(int amount)
	{
		Life life = this;
		life.bufferEnergy = life.bufferEnergy + amount;
	}

	public void saveAllVitality()
	{
		if (this.loaded)
		{
			string empty = string.Empty;
			if (!this.dead)
			{
				object[] objArray = new object[] { this.health, ";", this.food, ";", this.water, ";", this.sickness, ";", null, null, null, null };
				objArray[8] = (!this.bleeding ? "f" : "t");
				objArray[9] = ";";
				objArray[10] = (!this.bones ? "f" : "t");
				objArray[11] = ";";
				empty = string.Concat(objArray);
			}
			Savedata.saveLife(base.GetComponent<Player>().owner.id, empty);
		}
	}

	public void sendBones()
	{
		if (!Network.isServer)
		{
			base.networkView.RPC("startBones", RPCMode.Server, new object[0]);
		}
		else
		{
			this.startBones();
		}
	}

	public void sickBuffer()
	{
		Life life = this;
		life.sickness = life.sickness + 1;
		Life life1 = this;
		life1.bufferSick = life1.bufferSick - 1;
		if (this.sickness > 100)
		{
			this.bufferSick = 0;
			this.sickness = 100;
		}
		if (base.networkView.owner != Network.player)
		{
			base.networkView.RPC("tellSickness", base.networkView.owner, new object[] { this.sickness });
		}
		else
		{
			this.tellSickness(this.sickness);
		}
	}

	public void Start()
	{
		if (base.networkView.isMine)
		{
			this.load();
		}
	}

	[RPC]
	public void startBones()
	{
		if (!this.bones)
		{
			this.bones = true;
			this.startedBones = Time.realtimeSinceStartup;
			if (base.networkView.owner != Network.player)
			{
				base.networkView.RPC("tellBones", base.networkView.owner, new object[] { this.bones });
			}
			else
			{
				this.tellBones(this.bones);
			}
			NetworkSounds.askSound("Sounds/Life/bones", base.transform.position, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
		}
	}

	public void starve(int amount)
	{
		if (amount != 0)
		{
			Life life = this;
			life.food = life.food - amount;
			if (this.food < 0)
			{
				this.food = 0;
			}
			if (base.networkView.owner != Network.player)
			{
				base.networkView.RPC("tellFood", base.networkView.owner, new object[] { this.food });
			}
			else
			{
				this.tellFood(this.food);
			}
		}
	}

	[RPC]
	public void tellAllLife(int setHealth, int setFood, int setWater, int setSickness, bool setBleeding, bool setBones, NetworkMessageInfo info)
	{
		if (info.sender.ToString() == "0" || info.sender.ToString() == "-1")
		{
			this.tellAllLife_Pizza(setHealth, setFood, setWater, setSickness, setBleeding, setBones);
		}
	}

	public void tellAllLife_Pizza(int setHealth, int setFood, int setWater, int setSickness, bool setBleeding, bool setBones)
	{
		this.health = setHealth;
		this.food = setFood;
		this.water = setWater;
		this.bleeding = setBleeding;
		this.sickness = setSickness;
		this.bones = setBones;
		this.dead = false;
		this.death = string.Empty;
		this.stamina = 100;
		HUDGame.updateHealth();
		HUDGame.updateFood();
		HUDGame.updateWater();
		HUDGame.updateSickness();
		HUDGame.updateBleeding();
		HUDGame.updateBones();
		HUDGame.updateDead();
		HUDGame.updateStamina();
	}

	[RPC]
	public void tellBleeding(bool setBleeding)
	{
		this.bleeding = setBleeding;
		HUDGame.updateBleeding();
	}

	[RPC]
	public void tellBones(bool setBones)
	{
		this.bones = setBones;
		HUDGame.updateBones();
	}

	[RPC]
	public void tellDead(bool setDead, string setDeath, NetworkMessageInfo info) {
		if ( setDead ) {
			NetworkChat.sendAlert(info.sender.ToString() + " meghalt. " + setDead);
		}
		
		if (info.sender.ToString() == "0" || info.sender.ToString() == "-1")
		{
			this.tellDead_Pizza(setDead, setDeath);
		}
	}

	public void tellDead_Pizza(bool setDead, string setDeath)
	{
		this.dead = setDead;
		this.death = setDeath;
		HUDGame.updateDead();
	}

	[RPC]
	public void tellFood(int setFood)
	{
		this.food = setFood;
		HUDGame.updateFood();
	}

	[RPC]
	public void tellHealth(int setHealth, NetworkMessageInfo info)
	{
		if (info.sender.ToString() == "0" || info.sender.ToString() == "-1")
		{
			this.tellHealthPleaseStopKuniiAlsoLetMeKnowIfYouWantToHelpWithAnticheatInVersion3(setHealth);
		}
	}

	public void tellHealthPleaseStopKuniiAlsoLetMeKnowIfYouWantToHelpWithAnticheatInVersion3(int setHealth)
	{
		if (setHealth < this.health - 5)
		{
			HUDGame.lastFlash = Time.realtimeSinceStartup;
			Look.swayRoll = Look.swayRoll + (float)UnityEngine.Random.Range(-10, 10);
		}
		this.health = setHealth;
		HUDGame.updateHealth();
	}

	[RPC]
	public void tellLoadedLife(bool setLoaded)
	{
		this.loaded = setLoaded;
	}

	[RPC]
	public void tellSickness(int setSickness)
	{
		this.sickness = setSickness;
		HUDGame.updateSickness();
	}

	[RPC]
	public void tellWater(int setWater)
	{
		this.water = setWater;
		HUDGame.updateWater();
	}

	public void Update()
	{
		if (Network.isServer)
		{
			if (this.sickness == 0 && Time.realtimeSinceStartup - this.lastSickDamage > 0.5f)
			{
				this.lastSickDamage = Time.realtimeSinceStartup;
				this.damage(1, "You succumbed to illness.");
			}
			if (Time.realtimeSinceStartup - this.lastStarve > 20f && Time.realtimeSinceStartup - this.lastStarve > 20f + base.GetComponent<Skills>().survivalist() * 10f)
			{
				this.lastStarve = Time.realtimeSinceStartup;
				this.starve(1);
			}
			if (this.food == 0 && Time.realtimeSinceStartup - this.lastFoodDamage > 0.5f && (double)(Time.realtimeSinceStartup - this.lastFoodDamage) > 0.5 + (double)base.GetComponent<Skills>().survivalist())
			{
				this.lastFoodDamage = Time.realtimeSinceStartup;
				this.damage(1, "You starved to death.");
			}
			if (Time.realtimeSinceStartup - this.lastDehydrate > 17f && Time.realtimeSinceStartup - this.lastDehydrate > 17f + base.GetComponent<Skills>().survivalist() * 10f)
			{
				this.lastDehydrate = Time.realtimeSinceStartup;
				this.dehydrate(1);
			}
			if (this.water == 0 && Time.realtimeSinceStartup - this.lastWaterDamage > 0.5f && (double)(Time.realtimeSinceStartup - this.lastWaterDamage) > 0.5 + (double)base.GetComponent<Skills>().survivalist())
			{
				this.lastWaterDamage = Time.realtimeSinceStartup;
				this.damage(1, "You dehydrated to death.");
			}
			if (this.bleeding)
			{
				if (Time.realtimeSinceStartup - this.lastBleed > 1f)
				{
					this.lastBleed = Time.realtimeSinceStartup;
					this.damage(1, "You bled to death.");
				}
				if (Time.realtimeSinceStartup - this.lastTrail > 2f)
				{
					this.lastTrail = Time.realtimeSinceStartup;
					NetworkEffects.askEffect("Effects/bleed", base.transform.position + Vector3.up, Quaternion.identity, 16f);
				}
				if (ServerSettings.mode != 2 && Time.realtimeSinceStartup - this.startedBleeding > 60f)
				{
					this.heal(0, true, false);
				}
			}
			if (this.bones && ServerSettings.mode != 2 && Time.realtimeSinceStartup - this.startedBones > 120f)
			{
				this.heal(0, false, true);
			}
			if (Time.realtimeSinceStartup - this.lastHealthy > 2f && this.health < 100 && this.food > 95 && this.water > 95 && this.sickness > 80 && (float)this.sickness > 95f - base.GetComponent<Skills>().immunity() * 15f && Time.realtimeSinceStartup - this.lastHealthy > 5f - base.GetComponent<Skills>().immunity() * 3f)
			{
				this.lastHealthy = Time.realtimeSinceStartup;
				this.heal(1, false, false);
			}
			if (this.bufferEat > 0 && (double)(Time.realtimeSinceStartup - this.lastEat) > 0.2)
			{
				this.lastEat = Time.realtimeSinceStartup;
				this.eatBuffer();
			}
			if (this.bufferDrink > 0 && (double)(Time.realtimeSinceStartup - this.lastDrink) > 0.2)
			{
				this.lastDrink = Time.realtimeSinceStartup;
				this.drinkBuffer();
			}
			if (this.bufferHealth > 0 && (double)(Time.realtimeSinceStartup - this.lastHealth) > 0.2)
			{
				this.lastHealth = Time.realtimeSinceStartup;
				this.healBuffer();
			}
			if (this.bufferSick > 0 && (double)(Time.realtimeSinceStartup - this.lastSick) > 0.2)
			{
				this.lastSick = Time.realtimeSinceStartup;
				this.sickBuffer();
			}
			if (this.bufferEnergy > 0 && (double)(Time.realtimeSinceStartup - this.lastEnergy) > 0.2)
			{
				this.lastEnergy = Time.realtimeSinceStartup;
				this.energyBuffer();
			}
		}
		if (base.networkView.isMine && Time.realtimeSinceStartup - this.lastStamina > 5f && this.stamina < 100 && (double)(Time.realtimeSinceStartup - this.lastStaminaTick) > 0.2)
		{
			this.lastStaminaTick = Time.realtimeSinceStartup;
			Life life = this;
			life.stamina = life.stamina + 1;
			HUDGame.updateStamina();
		}
	}
}