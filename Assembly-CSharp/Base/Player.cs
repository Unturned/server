using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

using Unturned.Log;
using Unturned.Entity;

public class Player : MonoBehaviour
{
	public bool canBuild = false;

	public static GameObject model;

	public static Inventory inventory;

	public static Equipment equipment;

	public static Clothes clothes;

	public static Life life;

	public static Skills skills;

	public static float spawned;

	public NetworkUser owner;

	public Vehicle vehicle;

	public Vector3 prediction;

	public bool arm;

	public int stance;

	private int lastStance;

	public int action;

    // Credit system
    public int credit;

	private int lastAction;

	public int lean;

	private int lastLean;

	public int angle;

	private int lastAngle;

	public bool moving;

	private bool lastMoving;

	private Vector3 lastPosition;

	private float lastPacket = Time.realtimeSinceStartup;

	private float lastVerify = Single.MaxValue;

	private Vector3 lastPrediction;

	private Vector3 predictedPrediction;

	private Vector3 lastPredictedPrediction;

	private FancyAnimifier anim;

	private Transform spine;

	private bool shouldSave;

    static Player()
	{
		Player.spawned = Single.MinValue;
	}

	public Player()
	{
	}

	public void antinoclip()
	{
		if (base.transform.position.y < Ground.height(base.transform.position) - 1f)
		{
			Transform vector3 = base.transform;
			float single = base.transform.position.x;
			Vector3 vector31 = base.transform.position;
			vector3.position = new Vector3(single, Ground.height(base.transform.position) + 5f, vector31.z);
			NetworkTools.kick(base.networkView.owner, string.Concat("Kicking ", base.name, " for noclip."));
		}
		else if (base.transform.position.y < Ocean.level - 3f)
		{
			Transform transforms = base.transform;
			float single1 = base.transform.position.x;
			Vector3 vector32 = base.transform.position;
			transforms.position = new Vector3(single1, Ocean.level + 5f, vector32.z);
			NetworkTools.kick(base.networkView.owner, string.Concat("Kicking ", base.name, " for noclip."));
		}
	}

	[RPC]
	public void askAllPlayer(NetworkPlayer player)
	{
        if ( !DedicatedServer.CheckPlayer(player, "Player.cs @askAllPlayer") ){
            return;
        }

        StartCoroutine( "introduceSelf", player );
	}

    IEnumerator introduceSelf(NetworkPlayer player) {
        yield return new WaitForSeconds(0.5f);
        base.networkView.RPC("tellAllPlayer", player, new object[] { this.stance, this.action, this.lean, this.angle, this.moving, this.owner.id });
        yield return new WaitForSeconds(1.0f);
        base.networkView.RPC("tellAllPlayer", player, new object[] { this.stance, this.action, this.lean, this.angle, this.moving, this.owner.id });
        yield return new WaitForSeconds(2.0f);
        base.networkView.RPC("tellAllPlayer", player, new object[] { this.stance, this.action, this.lean, this.angle, this.moving, this.owner.id });
        yield return new WaitForSeconds(3.0f);
        base.networkView.RPC("tellAllPlayer", player, new object[] { this.stance, this.action, this.lean, this.angle, this.moving, this.owner.id });
    }

	public void Awake()
	{
		if (base.networkView.isMine || Network.isServer)
		{
			this.shouldSave = true;
		}
		if (!base.networkView.isMine)
		{
			UnityEngine.Object.Destroy(base.transform.FindChild("firstPerson").gameObject);
			base.transform.FindChild("thirdPerson").gameObject.SetActive(true);
			base.tag = "Enemy";
			base.gameObject.layer = 9;
			this.anim = base.transform.FindChild("thirdPerson").GetComponent<FancyAnimifier>();
			this.spine = base.transform.FindChild("thirdPerson").FindChild("character").FindChild("skeleton").FindChild("spine");
		}
		else
		{
			UnityEngine.Object.Destroy(base.transform.FindChild("thirdPerson").gameObject);
			base.transform.FindChild("firstPerson").gameObject.SetActive(true);

            bool found = false;

            for (int i = 0; i < NetworkUserList.users.Count; i++)
			{
				NetworkUser item = NetworkUserList.users[i];
				if (item.player == base.networkView.owner)
				{

					this.owner = item;
					item.model = base.gameObject;
                    base.name = this.owner.name;
                    found = true;
				}
			}

            if (!found) {
                Network.CloseConnection(base.networkView.owner, true);
				Logger.LogSecurity("", "", "Not found player in instantiate cycle!");
            }
		}
	}

	[RPC]
	public void collectedResource()
	{
	}

	[RPC]
	public void killedAnimal()
	{
	}

	[RPC]
    public void killedPlayer()
	{
	}

	[RPC]
	public void killedZombie()
	{
	}

	public void LateUpdate()
	{
		// This is server..
		/*
		if (!base.networkView.isMine && this.spine != null && this.spine.localRotation != this.desiredSpineRot)
		{
			this.realLean = Mathf.Lerp(this.realLean, (float)(this.lean * -1), 8f * Time.deltaTime);
			this.realPivot = Mathf.Lerp(this.realPivot, (float)(this.angle * 5), 8f * Time.deltaTime);
			if (!this.arm)
			{
				this.desiredSpineRot = this.spine.localRotation * Quaternion.Euler(0f, this.realLean, this.realPivot);
			}
			else
			{
				this.desiredSpineRot = this.spine.localRotation * Quaternion.Euler(0f, -this.realLean, this.realPivot);
			}
			this.spine.localRotation = this.desiredSpineRot;
		}
		*/
	}

	public void OnDestroy()
	{
		if (this.shouldSave)
		{
			this.save();
		}
	}

	public static void play(string id)
	{
		Player.model.networkView.RPC("tellPlay", RPCMode.Others, new object[] { id });
	}

	public void predict()
	{
		if (this.lastPrediction != base.transform.position)
		{
			Vector3 vector3 = base.transform.position - this.lastPrediction;
			this.predictedPrediction = vector3.normalized * 1.5f;
		}
		else
		{
			this.predictedPrediction = Vector3.zero;
		}
		this.lastPrediction = base.transform.position;
		if (this.predictedPrediction == this.lastPredictedPrediction)
		{
			this.prediction = this.predictedPrediction;
		}
		this.lastPredictedPrediction = this.predictedPrediction;
	}

	public void save()
	{
		if ( false ) { // TODO: implement config manager
			this.saveAllOrientation();
			base.GetComponent<Inventory>().saveAllItems();
			base.GetComponent<Clothes>().saveAllClothing();
			base.GetComponent<Life>().saveAllVitality();
			base.GetComponent<Skills>().saveAllKnowledge();
	        PlayerPrefs.Save();
		} 
		else
		{	// Remote database woooh :P
#if DEBUG
			Console.WriteLine("Saving player: " + this.name);
#endif
			var plr = new Unturned.Entity.Player();
			plr.Reputation = owner.reputation;
			plr.SteamID = owner.id;
			plr.Name = name;
			plr.PositionX = transform.position.x;
			plr.PositionY = transform.position.y;
			plr.PositionZ = transform.position.z;
			plr.ViewDirection = transform.rotation.eulerAngles.y;

#if DEBUG
			Console.WriteLine("Serializing inventory");
#endif

			// Inventory
			Inventory inv = GetComponent<Inventory>();
			plr.Inventory = new PlayerInventory(inv.height, inv.width, inv.capacity);
			plr.Inventory.Items = new List<InventoryItem>();

			for ( int i = 0; i < inv.height; i++ )
			{
				for (int j = 0; j < inv.width; j++) 
				{
					ClientItem item = inv.items[j, i];
					if (item.id != -1)
						plr.Inventory.Items.Add( new InventoryItem(item.id, item.amount, item.state, j, i) );
				}
			}

#if DEBUG
			Console.WriteLine("Serializing Clothes");
#endif

			// Clothes
			Clothes cloth = GetComponent<Clothes>();
			plr.Clothes = new PlayerClothes(
				cloth.shirt,
				cloth.pants,
				cloth.hat,
				cloth.backpack,
				cloth.vest);
#if DEBUG
			Console.WriteLine("Serializing Life");
#endif

			// Life
			Life life = GetComponent<Life>();
			plr.Life = new PlayerLife(
				life.health,
				life.food,
				life.water,
				life.sickness,
				life.bleeding,
				life.bones);

#if DEBUG
			Console.WriteLine("Serializing Skills");
#endif

			// Skills
			Skills skills = GetComponent<Skills>();
			plr.Skills = new PlayerSkills();
			plr.Skills.Experience = skills.experience;

			String skillLine = "";
			for (int j = 0; j < (int)skills.skills.Length; j++)
			{
				skillLine = string.Concat(skillLine, skills.skills[j].level, ";");
			}
			plr.Skills.Skills = skillLine;

#if DEBUG
			Console.WriteLine("Writing player to file");
#endif
			Database.provider.SavePlayer(plr);
			plr = null;
		}
	}

	public void saveAllOrientation()
	{
		string positionString = string.Empty;
		if (!base.GetComponent<Life>().dead)
		{
			this.lastPosition = base.transform.position;
			if (this.vehicle != null)
			{
				this.lastPosition = this.vehicle.getPosition();
			}

			positionString = string.Concat(positionString, Mathf.Floor(this.lastPosition.x * 100f) / 100f, ";");
			positionString = string.Concat(positionString, Mathf.Floor(this.lastPosition.y * 100f) / 100f, ";");
			positionString = string.Concat(positionString, Mathf.Floor(this.lastPosition.z * 100f) / 100f, ";");
			Vector3 vector3 = base.transform.rotation.eulerAngles;
			positionString = string.Concat(positionString, (int)vector3.y, ";");
		}
		Savedata.savePosition(this.owner.id, positionString);
	}

	[RPC]
	public void speedPacket(NetworkMessageInfo info)
	{
		if (info.sender != this.owner.player) {
			NetworkTools.kick(info.sender, "VAC: Kick hack attempt detected. Incident reported.");
			NetworkUser user = NetworkUserList.getUserFromPlayer(info.sender);
			Logger.LogSecurity(user.id, user.name, "Tried to use speedhack kick.");
			return;
		}

        // what a hell is this shit?
		/*Player player = this;
		player.elapsed = player.elapsed + ((float)info.timestamp - this.lastPacket);
		Player player1 = this;
		player1.packets = player1.packets + 1;
		if (this.packets > 3)
		{
			if (this.elapsed / (float)this.packets < 4f)
			{
				NetworkTools.kick(this.owner.player, string.Concat("Kicking ", base.name, " for speedhacks."));
			}
			this.elapsed = 0f;
			this.packets = 0;
		}
		this.lastPacket = (float)info.timestamp;*/
	}

	public void Start()
	{
        NetworkPlayer networkPlayer = base.networkView.owner;
        NetworkUser usr = NetworkUserList.getUserFromPlayer(networkPlayer);

        foreach (Player plr in GameObject.FindObjectsOfType<Player>())
        {
            if ( plr.owner == usr )
            {
                Logger.Instantiate("We have found a haxor, who wants to crash the server. Disconnecting!");

                Network.Destroy(plr.gameObject);
                GameObject.Destroy(plr.gameObject);
                Network.DestroyPlayerObjects(plr.networkView.owner);

                Network.Destroy(plr.gameObject);
                GameObject.Destroy(base.gameObject);
                Network.DestroyPlayerObjects(base.networkView.owner);

                Network.CloseConnection(networkPlayer, false);
            }
        }

		this.lastPrediction = base.transform.position;
		this.predictedPrediction = Vector3.zero;
		this.lastPredictedPrediction = Vector3.zero;
		base.transform.parent = SpawnPlayers.model.transform.FindChild("models");

		if (Network.isServer)
		{
			base.InvokeRepeating("antinoclip", 4f, 4f);
			base.InvokeRepeating("predict", 0f, 0.1f);
			base.transform.FindChild("nav").gameObject.SetActive(true);
			if (ServerSettings.dedicated)
			{
				base.transform.FindChild("thirdPerson").FindChild("character").FindChild("model").renderer.enabled = false;
			}
		}

		if (Network.isServer || base.networkView.isMine)
		{
			for (int i = 0; i < NetworkUserList.users.Count; i++)
			{
				NetworkUser user = NetworkUserList.users[i];
				if (user.player == base.networkView.owner)
				{
					this.owner = user;
					user.model = base.gameObject;
					base.name = this.owner.name;

                    StartCoroutine("LoadCredits", owner.id);
				}
			}
		}

		if ( Network.isServer )
			StartCoroutine("LoadPlayerFromDatabase");

		if (base.networkView.isMine)
		{
			Player.inventory = base.GetComponent<Inventory>();
			Player.equipment = base.GetComponent<Equipment>();
			Player.clothes = base.GetComponent<Clothes>();
			Player.life = base.GetComponent<Life>();
			Player.skills = base.GetComponent<Skills>();
			this.lastVerify = Time.realtimeSinceStartup + 1f;
			Player.spawned = Time.realtimeSinceStartup;
		}
		else if (!Network.isServer)
		{
			base.networkView.RPC("askAllPlayer", RPCMode.Server, new object[] { Network.player });
		}
	}

	private IEnumerator LoadPlayerFromDatabase()
	{
#if DEBUG
		Console.WriteLine("Requesting player from db: " + owner.id );
		long startTime = DateTime.Now.Millisecond;
#endif
		Unturned.Entity.Player plr = Database.provider.LoadPlayer(owner.id);
		yield return plr;

#if DEBUG
		Console.WriteLine("Loaded player " + owner.id + " in " + (DateTime.Now.Millisecond - startTime) + "ms");
#endif

		TellLoaded(plr);
	}

	private void TellLoaded(Unturned.Entity.Player plr)
	{
		#if DEBUG
		Console.WriteLine("Loading inventory...");
		#endif

		// Clothes
		Clothes clothes = GetComponent<Clothes>();
		clothes.item = -1;
		clothes.state = string.Empty;
		clothes.networkView.RPC("tellAllClothes", RPCMode.All, new object[] { 
			clothes.face, 
			plr.Clothes.Shirt, 
			plr.Clothes.Pants, 
			plr.Clothes.Hat, 
			clothes.hair, 
			plr.Clothes.Backpack, 
			plr.Clothes.Vest, 
			clothes.item,
			clothes.state,
			clothes.skinColor, 
			clothes.hairColor, 
			clothes.arm
		});
		clothes.loaded = true;
		if (!clothes.networkView.isMine)
		{
			clothes.networkView.RPC("tellLoadedClothes", base.networkView.owner, new object[] { true });
		}

		#if DEBUG
		Console.WriteLine("Loading inventory...");
		#endif
		// Inventory
		Inventory inventory = GetComponent<Inventory>();
		inventory.resize( plr.Inventory.Width, plr.Inventory.Height, plr.Inventory.Capacity );

		foreach(InventoryItem item in plr.Inventory.Items )
		{
			inventory.items[item.X, item.Y] = new ClientItem(
				item.ItemID,
				item.Amount,
				item.State);
		}

		inventory.UpdateItems();
		inventory.syncWeight();
		inventory.loaded = true;
		if (!inventory.networkView.isMine) {
			inventory.networkView.RPC("tellLoadedInventory", base.networkView.owner, new object[] { true });
		}

		#if DEBUG
		Console.WriteLine("Loading skills...");
		#endif
		// Skills
		Skills skills = GetComponent<Skills>();
		skills.loadAllKnowledgeFromSerial( plr.Skills.Experience + ";" + plr.Skills.Skills );

		#if DEBUG
		Console.WriteLine("Loading life...");
		#endif
		// Life
		Life life = GetComponent<Life>();
		life.bleeding = plr.Life.Bleeding;
		life.bones = plr.Life.BrokenBones;
		
		life.health = plr.Life.Health;
		life.water = plr.Life.Water;
		life.food = plr.Life.Food;
		life.sickness = plr.Life.Sickness;
		
		life.FixStats();
		life.FinalizeLoad();
	}

    private IEnumerator LoadCredits(String steamId)
    {
        this.credit = Database.provider.GetCredits(owner.id);
        yield return this.credit;
    }

	[RPC]
	public void tellAction(int setAction)
	{
		this.action = setAction;
	}

	[RPC]
	public void tellAllPlayer(int setStance, int setAction, int setLean, int setAngle, bool setMoving, string setID)
	{
		this.stance = setStance;
		this.action = setAction;
		this.lean = setLean;
		this.angle = setAngle;
		this.moving = setMoving;
		for (int i = 0; i < NetworkUserList.users.Count; i++)
		{
			NetworkUser item = NetworkUserList.users[i];
			if (item.id == setID)
			{
				this.owner = item;
				item.model = base.gameObject;
				base.name = this.owner.name;
			}
		}
	}

	[RPC]
	public void tellAngle(int setAngle)
	{
		this.angle = setAngle;
	}

	[RPC]
	public void tellLean(int setLean)
	{
		this.lean = setLean;
	}

	[RPC]
	public void tellMoving(bool setMoving)
	{
		this.moving = setMoving;
	}

	[RPC]
	public void tellPlay(string id)
	{
		this.anim.play(id);
	}

	[RPC]
	public void tellPrediction(Vector3 setPrediction)
	{
		this.prediction = setPrediction;
	}

	[RPC]
	public void tellStance(int setStance)
	{
		this.stance = setStance;
	}

	[RPC]
	public void tookItem()
	{
	}

	public void Update()
	{
		// TODO: in the server we don't wana check this stuff...
		if (!Network.isServer)
			if (base.networkView.isMine)
			{
				if (Movement.seat != null)
				{
					this.action = 7;
				}
				else if (Player.life.dead)
				{
					this.action = 4;
				}
				/*else if (HUDInventory.state)
				{
					this.action = 2;
				}*/
				else if (Movement.isClimbing)
			{
					this.action = 5;
				}
				else if (Movement.isSwimming)
				{
					this.action = 6;
				}
				else if (Equipment.equipped.y == -1)
				{
					this.action = 0;
				}
				else if (Equipment.id == 7004 || Equipment.id == 7014)
				{
					this.action = 9;
				}
				else if (ItemType.getType(Equipment.id) == 7)
				{
					this.action = 3;
				}
				else if (Equipment.id == 8001 || Equipment.id == 8008)
				{
					this.action = 8;
				}
				else
				{
					this.action = 1;
				}
				if (this.action != this.lastAction)
				{
					this.lastAction = this.action;
					base.networkView.RPC("tellAction", RPCMode.Others, new object[] { this.action });
				}
				this.lean = (int)Look.lean;
				if (this.lean != this.lastLean)
				{
					this.lastLean = this.lean;
					base.networkView.RPC("tellLean", RPCMode.Others, new object[] { this.lean });
				}
				this.angle = (int)(Look.pitch / 10f);
				if (this.angle != this.lastAngle)
				{
					this.lastAngle = this.angle;
					base.networkView.RPC("tellAngle", RPCMode.Others, new object[] { this.angle });
				}
				this.moving = Movement.isMoving;
				if (this.moving != this.lastMoving)
				{
					this.lastMoving = this.moving;
					base.networkView.RPC("tellMoving", RPCMode.Others, new object[] { this.moving });
				}
				this.stance = Stance.state;
				if (this.stance != this.lastStance)
				{
					this.lastStance = this.stance;
					base.networkView.RPC("tellStance", RPCMode.Others, new object[] { this.stance });
				}
				if (Time.realtimeSinceStartup - this.lastVerify > 5f)
				{
					this.lastVerify = Time.realtimeSinceStartup;
					if (!Network.isServer)
					{
						base.networkView.RPC("speedPacket", RPCMode.Server, new object[0]);
					}
				}
			}
			else if (this.action != this.lastAction || this.stance != this.lastStance || this.moving != this.lastMoving)
			{
				this.lastAction = this.action;
				this.lastStance = this.stance;
				this.lastMoving = this.moving;
				if (this.action == 7)
				{
					this.anim.stance("sat");
				}
				else if (this.moving)
				{
					if (this.action == 0)
					{
						if (this.stance == 0)
						{
							this.anim.stance("standMoveBasic");
						}
						else if (this.stance != 1)
						{
							this.anim.stance("proneMoveBasic");
						}
						else
						{
							this.anim.stance("crouchMoveBasic");
						}
					}
					else if (this.action == 1)
					{
						if (this.stance == 0)
						{
							this.anim.stance("standMoveItem");
						}
						else if (this.stance != 1)
						{
							this.anim.stance("proneMoveItem");
						}
						else
						{
							this.anim.stance("crouchMoveItem");
						}
					}
					else if (this.action == 3)
					{
						if (this.stance == 0)
						{
							this.anim.stance("standMoveGun");
						}
						else if (this.stance != 1)
						{
							this.anim.stance("proneMoveGun");
						}
						else
						{
							this.anim.stance("crouchMoveGun");
						}
					}
					else if (this.action == 9)
					{
						if (this.stance == 0)
						{
							this.anim.stance("standMoveBow");
						}
						else if (this.stance != 1)
						{
							this.anim.stance("proneMoveBow");
						}
						else
						{
							this.anim.stance("crouchMoveBow");
						}
					}
					else if (this.action == 8)
					{
						if (this.stance == 0)
						{
							this.anim.stance("standMoveFlashlight");
						}
						else if (this.stance != 1)
						{
							this.anim.stance("proneMoveFlashlight");
						}
						else
						{
							this.anim.stance("crouchMoveFlashlight");
						}
					}
					else if (this.action == 5)
					{
						this.anim.stance("ladderMoveBasic");
					}
					else if (this.action == 6)
					{
						this.anim.stance("swimMoveBasic");
					}
				}
				else if (this.action == 0)
				{
					if (this.stance == 0)
					{
						this.anim.stance("standIdleBasic");
					}
					else if (this.stance != 1)
					{
						this.anim.stance("proneIdleBasic");
					}
					else
					{
						this.anim.stance("crouchIdleBasic");
					}
				}
				else if (this.action == 1)
				{
					if (this.stance == 0)
					{
						this.anim.stance("standIdleItem");
					}
					else if (this.stance != 1)
					{
						this.anim.stance("proneIdleItem");
					}
					else
					{
						this.anim.stance("crouchIdleItem");
					}
				}
				else if (this.action == 2)
				{
					this.anim.stance("bag");
				}
				else if (this.action == 3)
				{
					if (this.stance == 0)
					{
						this.anim.stance("standIdleGun");
					}
					else if (this.stance != 1)
					{
						this.anim.stance("proneIdleGun");
					}
					else
					{
						this.anim.stance("crouchIdleGun");
					}
				}
				else if (this.action == 9)
				{
					if (this.stance == 0)
					{
						this.anim.stance("standIdleBow");
					}
					else if (this.stance != 1)
					{
						this.anim.stance("proneIdleBow");
					}
					else
					{
						this.anim.stance("crouchIdleBow");
					}
				}
				else if (this.action == 8)
				{
					if (this.stance == 0)
					{
						this.anim.stance("standIdleFlashlight");
					}
					else if (this.stance != 1)
					{
						this.anim.stance("proneIdleFlashlight");
					}
					else
					{
						this.anim.stance("crouchIdleFlashlight");
					}
				}
				else if (this.action == 4)
				{
					this.anim.play("dead");
					this.anim.stance(string.Empty);
				}
				else if (this.action == 5)
				{
					this.anim.stance("ladderIdleBasic");
				}
				else if (this.action == 6)
				{
					this.anim.stance("swimIdleBasic");
				}
			}
	}
}