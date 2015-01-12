using System;
using UnityEngine;

public class Vehicle : Interactable
{
	public Passenger[] passengers;

	public bool wrecked;

	public bool exploded;

	public float lastWrecked = Single.MaxValue;

	public int health;

	public int fuel;

	public bool headlights;

	public bool sirens;

	public int maxHealth;

	public int maxFuel;

	public int maxSpeed;

	public int maxTurn;

	private WheelCollider frontLeft;

	private WheelCollider backLeft;

	private WheelCollider frontRight;

	private WheelCollider backRight;

	private Transform left0;

	private Transform left1;

	private Transform right0;

	private Transform right1;

	private Transform steerer;

	private Vector3 position;

	private Quaternion rotation;

	private Vector3 lastPosition;

	private int lastTurn;

	public int lastSpeed;

	private float spin;

	private float spinSpeed;

	private float lastPump;

	private float lastHorn;

	private float lastTick;

	private SleekBox speedMeter;

	private SleekLabel speedLabel;

	private SleekImage speedIcon;

	private SleekBox fuelMeter;

	private SleekImage fuelBar;

	private SleekLabel fuelLabel;

	private SleekImage fuelIcon;

	private static RaycastHit hit;

	private bool real = true;

	public Vehicle()
	{
	}

	[RPC]
	public void askEnter(NetworkPlayer player)
	{
		if (this.real && !this.wrecked && !this.exploded && Mathf.Abs(this.lastSpeed) < 5)
		{
			GameObject modelFromPlayer = NetworkUserList.getModelFromPlayer(player);
			if (modelFromPlayer != null && !modelFromPlayer.GetComponent<Life>().dead)
			{
				int num = 0;
				while (num < (int)this.passengers.Length)
				{
					if (this.passengers[num] != null)
					{
						num++;
					}
					else
					{
						if (num == 0)
						{
							base.rigidbody.isKinematic = true;
							base.rigidbody.useGravity = false;
							this.stop();
							if (this.fuel > 0)
							{
								NetworkSounds.askSound("Sounds/Vehicles/ignition", base.transform.position + (base.transform.right * 2f), 1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
							}
						}
						this.passengers[num] = new Passenger(player);
						if (player != Network.player)
						{
							base.networkView.RPC("tellEnter", player, new object[] { num });
							base.networkView.RPC("tellFuel", player, new object[] { this.fuel });
							base.networkView.RPC("updateSpeed", player, new object[] { this.lastSpeed });
						}
						else
						{
							this.tellEnter(num);
							this.tellFuel_Pizza(this.fuel);
							this.updateSpeed(this.lastSpeed);
						}
						base.networkView.RPC("tellSitting", RPCMode.All, new object[] { player, num });
						break;
					}
				}
			}
		}
	}

	[RPC]
	public void askExit(NetworkPlayer player)
	{
		if (this.real)
		{
			this.eject(player);
		}
	}

	[RPC]
	public void askState(NetworkPlayer player)
	{
		if (this.real)
		{
			for (int i = 0; i < (int)this.passengers.Length; i++)
			{
				if (this.passengers[i] != null)
				{
					if (player != Network.player)
					{
						base.networkView.RPC("tellSitting", player, new object[] { this.passengers[i].player, i });
					}
					else
					{
						this.tellSitting(this.passengers[i].player, i);
					}
				}
			}
			if (player != Network.player)
			{
				base.networkView.RPC("tellHealth", player, new object[] { this.health });
			}
			else
			{
				this.tellHealth_Pizza(this.health);
			}
			if (player != Network.player)
			{
				base.networkView.RPC("tellHeadlights", player, new object[] { this.headlights });
			}
			else
			{
				this.tellHeadlights(this.headlights);
			}
			if (player != Network.player)
			{
				base.networkView.RPC("tellSirens", player, new object[] { this.sirens });
			}
			else
			{
				this.tellSirens(this.sirens);
			}
			if (player != Network.player)
			{
				base.networkView.RPC("tellWrecked", player, new object[] { this.wrecked });
			}
			else
			{
				this.tellWrecked_Pizza(this.wrecked);
			}
			if (player != Network.player)
			{
				base.networkView.RPC("tellExploded", player, new object[] { this.exploded });
			}
			else
			{
				this.tellExploded_Pizza(this.exploded);
			}
			if (player != Network.player)
			{
				base.networkView.RPC("updatePosition", player, new object[] { base.transform.position, base.transform.rotation });
				if (base.GetComponent<Painter>() != null)
				{
					base.networkView.RPC("tellColor", player, new object[] { base.GetComponent<Painter>().color.r, base.GetComponent<Painter>().color.g, base.GetComponent<Painter>().color.b });
				}
			}
		}
	}

	[RPC]
	public void askSwap(NetworkPlayer player, int seat)
	{
		if (this.real && seat < (int)this.passengers.Length && this.passengers[seat] == null)
		{
			for (int i = 0; i < (int)this.passengers.Length; i++)
			{
				if (this.passengers[i] != null && this.passengers[i].player == player)
				{
					if (i == 0)
					{
						base.rigidbody.isKinematic = false;
						base.rigidbody.useGravity = true;
						base.rigidbody.AddForce(base.transform.right * (float)this.lastSpeed);
						this.stop();
					}
					this.passengers[i] = null;
				}
			}
			if (seat == 0)
			{
				if (this.fuel > 0)
				{
					NetworkSounds.askSound("Sounds/Vehicles/ignition", base.transform.position + (base.transform.right * 2f), 1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
				}
				base.rigidbody.isKinematic = true;
				base.rigidbody.useGravity = false;
				this.stop();
			}
			this.passengers[seat] = new Passenger(player);
			if (player != Network.player)
			{
				base.networkView.RPC("tellEnter", player, new object[] { seat });
				base.networkView.RPC("tellFuel", player, new object[] { this.fuel });
				base.networkView.RPC("updateSpeed", player, new object[] { this.lastSpeed });
			}
			else
			{
				this.tellEnter(seat);
				this.tellFuel_Pizza(this.fuel);
				this.updateSpeed(this.lastSpeed);
			}
			base.networkView.RPC("tellSitting", RPCMode.All, new object[] { player, seat });
		}
	}

	public void Awake()
	{
        if( base.networkView.owner != Network.player ) 
        {
            // Killing vehicle inmediatly
            Logger.LogSecurity(base.networkView.owner, "Spawned a car instance...");
            NetworkTools.kick(base.networkView.owner, "VAC: Vehicle spawn hack detected. Incident reported!");
            Network.RemoveRPCs(base.networkView.viewID);
            Network.Destroy(gameObject);
            return;
        }

        this.real = SpawnVehicles.model.transform.FindChild("models").childCount < Loot.getCars();
		if (this.real)
		{
			this.position = base.transform.position;
			this.rotation = base.transform.rotation;
			this.passengers = new Passenger[base.transform.FindChild("seats").childCount];
			this.frontLeft = base.transform.FindChild("wheels").FindChild("wheelFrontLeft").GetComponent<WheelCollider>();
			this.backLeft = base.transform.FindChild("wheels").FindChild("wheelBackLeft").GetComponent<WheelCollider>();
			this.frontRight = base.transform.FindChild("wheels").FindChild("wheelFrontRight").GetComponent<WheelCollider>();
			this.backRight = base.transform.FindChild("wheels").FindChild("wheelBackRight").GetComponent<WheelCollider>();
			this.left0 = base.transform.FindChild("wheels").FindChild("wheel0Left");
			this.left1 = base.transform.FindChild("wheels").FindChild("wheel1Left");
			this.right0 = base.transform.FindChild("wheels").FindChild("wheel0Right");
			this.right1 = base.transform.FindChild("wheels").FindChild("wheel1Right");
			this.steerer = base.transform.FindChild("steerer");
			if (Network.isServer)
			{
				base.networkView.RPC("tellHealth", RPCMode.All, new object[] { this.health });
				base.networkView.RPC("tellFuel", RPCMode.All, new object[] { this.fuel });
				base.rigidbody.useGravity = true;
				base.rigidbody.isKinematic = false;
				this.stop();
				NetworkEvents.onPlayerDisconnected += new NetworkPlayerDelegate(this.onPlayerDisconnected);
			}
		}
	}

	public void burn(int amount)
	{
		if (this.real)
		{
			Vehicle vehicle = this;
			vehicle.fuel = vehicle.fuel - amount;
			if (this.fuel < 0)
			{
				this.fuel = 0;
			}
			base.networkView.RPC("tellFuel", RPCMode.All, new object[] { this.fuel });
		}
	}

	public void control()
	{
		if (this.real)
		{
			if (Input.GetKeyDown(KeyCode.F1) && (int)this.passengers.Length > 0)
			{
				if (!Network.isServer)
				{
					base.networkView.RPC("askSwap", RPCMode.Server, new object[] { Network.player, 0 });
				}
				else
				{
					this.askSwap(Network.player, 0);
				}
			}
			else if (Input.GetKeyDown(KeyCode.F2) && (int)this.passengers.Length > 1)
			{
				if (!Network.isServer)
				{
					base.networkView.RPC("askSwap", RPCMode.Server, new object[] { Network.player, 1 });
				}
				else
				{
					this.askSwap(Network.player, 1);
				}
			}
			else if (Input.GetKeyDown(KeyCode.F3) && (int)this.passengers.Length > 2)
			{
				if (!Network.isServer)
				{
					base.networkView.RPC("askSwap", RPCMode.Server, new object[] { Network.player, 2 });
				}
				else
				{
					this.askSwap(Network.player, 2);
				}
			}
			else if (Input.GetKeyDown(KeyCode.F4) && (int)this.passengers.Length > 3)
			{
				if (!Network.isServer)
				{
					base.networkView.RPC("askSwap", RPCMode.Server, new object[] { Network.player, 3 });
				}
				else
				{
					this.askSwap(Network.player, 3);
				}
			}
			else if (Input.GetKeyDown(KeyCode.F5) && (int)this.passengers.Length > 4)
			{
				if (!Network.isServer)
				{
					base.networkView.RPC("askSwap", RPCMode.Server, new object[] { Network.player, 4 });
				}
				else
				{
					this.askSwap(Network.player, 4);
				}
			}
			else if (Input.GetKeyDown(KeyCode.F6) && (int)this.passengers.Length > 5)
			{
				if (!Network.isServer)
				{
					base.networkView.RPC("askSwap", RPCMode.Server, new object[] { Network.player, 5 });
				}
				else
				{
					this.askSwap(Network.player, 5);
				}
			}
		}
	}

	public void damage(int amount)
	{
		if (this.real && !this.exploded)
		{
			Vehicle vehicle = this;
			vehicle.health = vehicle.health - amount;
			if (this.health <= 0)
			{
				this.health = 0;
			}
			base.networkView.RPC("tellHealth", RPCMode.All, new object[] { this.health });
		}
	}

	public void drive(float turn, float thrust)
	{
		if (this.real)
		{
			this.frontLeft.steerAngle = Mathf.Lerp(this.frontLeft.steerAngle, turn * (float)this.maxTurn, 4f * Time.deltaTime);
			this.frontRight.steerAngle = Mathf.Lerp(this.frontRight.steerAngle, turn * (float)this.maxTurn, 4f * Time.deltaTime);
			if (thrust != -1000f)
			{
				if (this.fuel <= 0 || this.health < 5)
				{
					this.backLeft.motorTorque = Mathf.Lerp(this.backLeft.motorTorque, 0f, 4f * Time.deltaTime);
					this.backRight.motorTorque = Mathf.Lerp(this.backRight.motorTorque, 0f, 4f * Time.deltaTime);
				}
				else if (thrust > 0f)
				{
					if (base.rigidbody.velocity.magnitude >= (float)this.maxSpeed)
					{
						this.backLeft.motorTorque = Mathf.Lerp(this.backLeft.motorTorque, 0f, 4f * Time.deltaTime);
						this.backRight.motorTorque = Mathf.Lerp(this.backRight.motorTorque, 0f, 4f * Time.deltaTime);
					}
					else
					{
						this.backLeft.motorTorque = Mathf.Lerp(this.backLeft.motorTorque, thrust * (float)this.maxSpeed, 4f * Time.deltaTime);
						this.backRight.motorTorque = Mathf.Lerp(this.backRight.motorTorque, thrust * (float)this.maxSpeed, 4f * Time.deltaTime);
					}
				}
				else if (base.rigidbody.velocity.magnitude >= (float)(this.maxSpeed / 2))
				{
					this.backLeft.motorTorque = Mathf.Lerp(this.backLeft.motorTorque, 0f, 4f * Time.deltaTime);
					this.backRight.motorTorque = Mathf.Lerp(this.backRight.motorTorque, 0f, 4f * Time.deltaTime);
				}
				else
				{
					this.backLeft.motorTorque = Mathf.Lerp(this.backLeft.motorTorque, thrust * (float)this.maxSpeed / 2f, 4f * Time.deltaTime);
					this.backRight.motorTorque = Mathf.Lerp(this.backRight.motorTorque, thrust * (float)this.maxSpeed / 2f, 4f * Time.deltaTime);
				}
				this.backLeft.brakeTorque = Mathf.Lerp(this.backLeft.brakeTorque, 2f, 4f * Time.deltaTime);
				this.backRight.brakeTorque = Mathf.Lerp(this.backRight.brakeTorque, 2f, 4f * Time.deltaTime);
			}
			else
			{
				this.backLeft.motorTorque = Mathf.Lerp(this.backLeft.motorTorque, 0f, 4f * Time.deltaTime);
				this.backRight.motorTorque = Mathf.Lerp(this.backRight.motorTorque, 0f, 4f * Time.deltaTime);
				this.backLeft.brakeTorque = Mathf.Lerp(this.backLeft.brakeTorque, 20f, 4f * Time.deltaTime);
				this.backRight.brakeTorque = Mathf.Lerp(this.backRight.brakeTorque, 20f, 4f * Time.deltaTime);
			}
			if (this.fuel > 0 && this.health >= 5 && Screen.lockCursor)
			{
				if (Input.GetMouseButtonDown(0) && (double)(Time.realtimeSinceStartup - this.lastHorn) > 0.25)
				{
					this.lastHorn = Time.realtimeSinceStartup;
					NetworkSounds.askSound("Sounds/Vehicles/horn", base.transform.position + (base.transform.right * 3f), 1f, 1.25f, 2f);
					SpawnAnimals.attract(base.transform.position, 64f);
				}
				if (Input.GetMouseButtonDown(1))
				{
					base.networkView.RPC("tellHeadlights", RPCMode.All, new object[] { !this.headlights });
					NetworkSounds.askSound("Sounds/General/firemode", base.transform.position, 0.1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
				}
				if (Input.GetKeyDown(InputSettings.otherKey) && base.transform.FindChild("siren") != null)
				{
					base.networkView.RPC("tellSirens", RPCMode.All, new object[] { !this.sirens });
					NetworkSounds.askSound("Sounds/General/firemode", base.transform.position, 0.1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
				}
			}
			if (Mathf.Abs(this.lastSpeed) != (int)base.rigidbody.velocity.magnitude)
			{
				base.networkView.RPC("updateSpeed", RPCMode.All, new object[] { (thrust < 0f ? -(int)base.rigidbody.velocity.magnitude : (int)base.rigidbody.velocity.magnitude) });
			}
			this.steerer.localRotation = Quaternion.Euler(0f, -75f, Viewmodel.swayRoll);
			if (this.lastTurn != (int)Viewmodel.swayRoll)
			{
				base.networkView.RPC("updateTurn", RPCMode.All, new object[] { (int)Viewmodel.swayRoll });
			}
			if ((double)(Time.realtimeSinceStartup - this.lastTick) > 0.2)
			{
				this.lastTick = Time.realtimeSinceStartup;
				if ((base.transform.position - this.lastPosition).sqrMagnitude > 1f)
				{
					this.lastPosition = this.position;
					base.networkView.RPC("updatePosition", RPCMode.All, new object[] { base.transform.position, base.transform.rotation });
				}
			}
		}
	}

	public void eject(NetworkPlayer player)
	{
		if (this.real)
		{
			for (int i = 0; i < (int)this.passengers.Length; i++)
			{
				if (this.passengers[i] != null && this.passengers[i].player == player)
				{
					if (i == 0)
					{
						base.networkView.RPC("updateSpeed", RPCMode.All, new object[] { 0 });
						base.rigidbody.isKinematic = false;
						base.rigidbody.useGravity = true;
						base.rigidbody.AddForce(base.transform.right * (float)this.lastSpeed);
						this.stop();
					}
					this.passengers[i] = null;
				}
			}
			if (player != Network.player)
			{
				base.networkView.RPC("tellEnter", player, new object[] { -1 });
			}
			else
			{
				this.tellEnter(-1);
			}
			base.networkView.RPC("tellSitting", RPCMode.All, new object[] { player, -1 });
		}
	}

	public void ejectAll()
	{
		if (this.real)
		{
			for (int i = 0; i < (int)this.passengers.Length; i++)
			{
				if (this.passengers[i] != null)
				{
					this.eject(this.passengers[i].player);
				}
			}
		}
	}

	public void fill(int amount)
	{
		if (this.real)
		{
			Vehicle vehicle = this;
			vehicle.fuel = vehicle.fuel + amount;
			if (this.fuel > this.maxFuel)
			{
				this.fuel = this.maxFuel;
			}
			base.networkView.RPC("tellFuel", RPCMode.All, new object[] { this.fuel });
		}
	}

	public Vector3 getPosition()
	{
		if (!this.real)
		{
			return Vector3.zero;
		}
		Physics.Raycast(base.transform.position + base.transform.forward, base.transform.up * -1f, out Vehicle.hit, 3f);
		if (Vehicle.hit.collider == null)
		{
			return (base.transform.position + (base.transform.up * -2.5f)) + Vector3.up;
		}
		Physics.Raycast(base.transform.position + base.transform.forward, base.transform.up, out Vehicle.hit, 3f);
		if (Vehicle.hit.collider != null)
		{
			return base.transform.position + new Vector3(0f, 2f, 0f);
		}
		return (base.transform.position + (base.transform.up * 2.5f)) + Vector3.up;
	}

	public void heal(int amount)
	{
		if (this.real && !this.exploded)
		{
			Vehicle vehicle = this;
			vehicle.health = vehicle.health + amount;
			if (this.health > this.maxHealth)
			{
				this.health = this.maxHealth;
			}
			if (this.wrecked && this.health >= 30)
			{
				this.wrecked = false;
				base.networkView.RPC("tellWrecked", RPCMode.All, new object[] { false });
			}
			base.networkView.RPC("tellHealth", RPCMode.All, new object[] { this.health });
		}
	}

	public override string hint()
	{
		if (this.real && !this.wrecked && !this.exploded && Mathf.Abs(this.lastSpeed) < 5)
		{
			return "Enter";
		}
		return string.Empty;
	}

	public override string icon()
	{
		return "Textures/Icons/drive";
	}

	public void onPlayerDisconnected(NetworkPlayer player)
	{
		if (this.real)
		{
			int num = 0;
			while (num < (int)this.passengers.Length)
			{
				if (this.passengers[num] == null || !(this.passengers[num].player == player))
				{
					num++;
				}
				else
				{
					if (num == 0)
					{
						base.networkView.RPC("updateSpeed", RPCMode.All, new object[] { 0 });
						base.rigidbody.isKinematic = false;
						base.rigidbody.useGravity = true;
						base.rigidbody.AddForce(base.transform.right * (float)this.lastSpeed);
						this.stop();
					}
					this.passengers[num] = null;
					break;
				}
			}
		}
	}

	public void Start()
	{
        if (this.real)
		{
			base.transform.parent = SpawnVehicles.model.transform.FindChild("models");
			base.rigidbody.centerOfMass = new Vector3(0f, 0f, -0.5f);
			if (!Network.isServer)
			{
				base.networkView.RPC("askState", RPCMode.Server, new object[] { Network.player });
			}
			else
			{
				this.askState(Network.player);
			}
		}
		if (base.networkView.owner.ToString() != "0" && base.networkView.owner.ToString() != "-1")
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void stop()
	{
		if (this.real)
		{
			this.backLeft.motorTorque = 0f;
			this.backRight.motorTorque = 0f;
			this.backLeft.brakeTorque = 2f;
			this.backRight.brakeTorque = 2f;
			this.frontLeft.steerAngle = 0f;
			this.frontRight.steerAngle = 0f;
		}
	}

	[RPC]
	public void tellColor(float r, float g, float b)
	{
		if (this.real && base.GetComponent<Painter>() != null)
		{
			base.GetComponent<Painter>().paint(new Color(r, g, b));
		}
	}

	[RPC]
	public void tellEnter(int index)
	{
		if (this.real)
		{
			if (this.speedMeter != null)
			{
				this.speedMeter.@remove();
				this.speedMeter = null;
			}
			if (this.fuelMeter != null)
			{
				this.fuelMeter.@remove();
				this.fuelMeter = null;
			}
			if (Movement.isDriving)
			{
				base.networkView.RPC("updateSpeed", RPCMode.All, new object[] { 0 });
			}
			if (index != -1)
			{
				Movement.vehicle = this;
				Movement.seat = base.transform.FindChild("seats").FindChild(index.ToString());
				Movement.isDriving = index == 0;
				Movement.control.enabled = false;
				this.speedMeter = new SleekBox()
				{
					position = new Coord2(-155, -150, 0.5f, 1f),
					size = new Coord2(150, 40, 0f, 0f)
				};
				this.fuelMeter = new SleekBox()
				{
					position = new Coord2(5, -150, 0.5f, 1f),
					size = new Coord2(150, 40, 0f, 0f)
				};
				this.fuelBar = new SleekImage()
				{
					position = new Coord2(50, 10, 0f, 0f),
					size = new Coord2(-60, -20, 1f, 1f)
				};
				this.fuelBar.setImage("Textures/Sleek/pixel");
				this.fuelBar.color = new Color(0.372549027f, 0.274509817f, 0.3529412f);
				this.fuelMeter.addFrame(this.fuelBar);
				this.speedLabel = new SleekLabel()
				{
					position = new Coord2(50, 10, 0f, 0f),
					size = new Coord2(-60, -20, 1f, 1f)
				};
				this.speedMeter.addFrame(this.speedLabel);
				this.fuelLabel = new SleekLabel()
				{
					position = new Coord2(50, 10, 0f, 0f),
					size = new Coord2(-60, -20, 1f, 1f)
				};
				this.fuelMeter.addFrame(this.fuelLabel);
				this.speedIcon = new SleekImage()
				{
					position = new Coord2(4, 4, 0f, 0f),
					size = new Coord2(32, 32, 0f, 0f)
				};
				this.speedIcon.setImage("Textures/Icons/speedometer");
				this.speedMeter.addFrame(this.speedIcon);
				this.fuelIcon = new SleekImage()
				{
					position = new Coord2(4, 4, 0f, 0f),
					size = new Coord2(32, 32, 0f, 0f)
				};
				this.fuelIcon.setImage("Textures/Icons/fuel");
				this.fuelMeter.addFrame(this.fuelIcon);
				if (index == 0)
				{
					base.rigidbody.useGravity = true;
					base.rigidbody.isKinematic = false;
					this.stop();
				}
				else if (!Network.isServer || this.passengers[0] != null)
				{
					base.rigidbody.useGravity = false;
					base.rigidbody.isKinematic = true;
					this.stop();
				}
				else
				{
					base.rigidbody.isKinematic = false;
					base.rigidbody.useGravity = true;
					base.rigidbody.AddForce(base.transform.right * (float)this.lastSpeed);
					this.stop();
				}
			}
			else
			{
				Vector3 position = this.getPosition();
				if (position.y < Ocean.level)
				{
					position = new Vector3(position.x, Ocean.level + 5f, position.z);
				}
				Player.model.transform.position = position;
				Movement.vehicle = null;
				Movement.seat = null;
				Movement.isDriving = false;
				Movement.control.enabled = true;
				if (!Network.isServer)
				{
					base.rigidbody.useGravity = false;
					base.rigidbody.isKinematic = true;
					this.stop();
				}
			}
			Look.resetCameraPosition();
			if (index == -1)
			{
				Quaternion quaternion = base.transform.rotation;
				Look.yaw = quaternion.eulerAngles.y + 90f;
			}
			Stance.change(0);
		}
	}

	[RPC]
	public void tellExploded(bool setExploded, NetworkMessageInfo info)
	{
		if ((info.sender.ToString() == "0" || info.sender.ToString() == "-1") && this.real)
		{
			this.tellExploded_Pizza(setExploded);
		}
	}

	public void tellExploded_Pizza(bool setExploded)
	{
		this.exploded = setExploded;
		if (this.exploded)
		{
			base.renderer.material.color = new Color(UnityEngine.Random.Range(0.2f, 0.3f), UnityEngine.Random.Range(0.2f, 0.3f), UnityEngine.Random.Range(0.2f, 0.3f));
		}
	}

	[RPC]
	public void tellFuel(int setFuel, NetworkMessageInfo info)
	{
		if ((info.sender.ToString() == "0" || info.sender.ToString() == "-1") && this.real)
		{
			this.tellFuel_Pizza(setFuel);
		}
	}

	public void tellFuel_Pizza(int setFuel)
	{
		this.fuel = setFuel;
		if (this.fuelMeter != null)
		{
			this.fuelLabel.text = string.Concat(Mathf.FloorToInt((float)this.fuel / (float)this.maxFuel * 100f), "%");
			this.fuelBar.size = new Coord2((int)((float)this.fuel / (float)this.maxFuel * 90f), 20, 0f, 0f);
		}
	}

	[RPC]
	public void tellHeadlights(bool setHeadlights)
	{
		if (this.real && this.headlights != setHeadlights)
		{
			this.headlights = setHeadlights;
			base.transform.FindChild("light_0").light.enabled = this.headlights;
			base.transform.FindChild("light_1").light.enabled = this.headlights;
			base.transform.FindChild("light_2").light.enabled = this.headlights;
		}
	}

	[RPC]
	public void tellHealth(int setHealth, NetworkMessageInfo info)
	{
		if ((info.sender.ToString() == "0" || info.sender.ToString() == "-1") && this.real)
		{
			this.tellHealth_Pizza(setHealth);
		}
	}

	public void tellHealth_Pizza(int setHealth)
	{
		this.health = setHealth;
		if (this.health >= 60)
		{
			base.transform.FindChild("lightSmoke").GetComponent<ParticleSystem>().Stop();
		}
		else if (!base.transform.FindChild("lightSmoke").GetComponent<ParticleSystem>().isPlaying)
		{
			base.transform.FindChild("lightSmoke").GetComponent<ParticleSystem>().Play();
		}
		if (this.health >= 30)
		{
			base.transform.FindChild("darkSmoke").GetComponent<ParticleSystem>().Stop();
		}
		else if (!base.transform.FindChild("darkSmoke").GetComponent<ParticleSystem>().isPlaying)
		{
			base.transform.FindChild("darkSmoke").GetComponent<ParticleSystem>().Play();
		}
		if (this.health >= 10)
		{
			base.transform.FindChild("fire").GetComponent<ParticleSystem>().Stop();
			base.transform.FindChild("fire").audio.Stop();
			base.transform.FindChild("fire").light.enabled = false;
		}
		else
		{
			if (!base.transform.FindChild("fire").GetComponent<ParticleSystem>().isPlaying)
			{
				base.transform.FindChild("fire").GetComponent<ParticleSystem>().Play();
				base.transform.FindChild("fire").audio.Play();
			}
			base.transform.FindChild("fire").light.enabled = true;
		}
	}

	[RPC]
	public void tellSirens(bool setSirens)
	{
		if (this.real && this.sirens != setSirens && base.transform.FindChild("siren") != null)
		{
			this.sirens = setSirens;
			base.transform.FindChild("siren").light.enabled = this.sirens;
			if (!this.sirens)
			{
				base.transform.FindChild("siren").audio.Stop();
			}
			else
			{
				base.transform.FindChild("siren").audio.Play();
			}
		}
	}

	[RPC]
	public void tellSitting(NetworkPlayer player, int seat)
	{
		if (this.real)
		{
			GameObject modelFromPlayer = NetworkUserList.getModelFromPlayer(player);
			if (modelFromPlayer != null)
			{
				if (modelFromPlayer.transform.parent != null && modelFromPlayer.transform.parent.name == "0")
				{
					base.audio.Stop();
				}
				if (seat != -1)
				{
					if (seat == 0)
					{
						base.audio.Play();
					}
					modelFromPlayer.transform.parent = base.transform.FindChild("seats").FindChild(seat.ToString());
					modelFromPlayer.transform.localPosition = new Vector3(0f, 0f, 0.25f);
					modelFromPlayer.transform.localRotation = Quaternion.Euler(0f, 90f, 90f);
					modelFromPlayer.GetComponent<Player>().vehicle = this;
				}
				else
				{
					modelFromPlayer.transform.parent = SpawnPlayers.model.transform.FindChild("models");
					modelFromPlayer.GetComponent<Player>().vehicle = null;
				}
			}
		}
	}

	[RPC]
	public void tellWrecked(bool setWrecked, NetworkMessageInfo info)
	{
		if (info.sender.ToString() == "0" || info.sender.ToString() == "-1")
		{
			this.tellWrecked_Pizza(setWrecked);
		}
	}

	public void tellWrecked_Pizza(bool setWrecked)
	{
		if (this.real)
		{
			this.wrecked = setWrecked;
		}
	}

	public override void trigger()
	{
		if (this.real)
		{
			if (Movement.vehicle != null)
			{
				if (!Network.isServer)
				{
					base.networkView.RPC("askExit", RPCMode.Server, new object[] { Network.player });
				}
				else
				{
					this.askExit(Network.player);
				}
			}
			else if (!Network.isServer)
			{
				base.networkView.RPC("askEnter", RPCMode.Server, new object[] { Network.player });
			}
			else
			{
				this.askEnter(Network.player);
			}
		}
	}

	public void Update()
	{
		float fuel;
		
		if (this.real)
		{
			if (Network.isServer)
			{
				if (base.transform.position.y < Ocean.level - 1f && this.health != this.maxHealth)
				{
					base.networkView.RPC("tellHealth", RPCMode.All, new object[] { this.maxHealth });
				}
				if ((this.wrecked || this.exploded) && this.headlights)
				{
					base.networkView.RPC("tellHeadlights", RPCMode.All, new object[] { false });
				}
				if ((this.wrecked || this.exploded) && this.sirens)
				{
					base.networkView.RPC("tellSirens", RPCMode.All, new object[] { false });
				}
				if (!this.wrecked)
				{
					if (base.transform.position.y < Ocean.level - 1f)
					{
						this.wrecked = true;
						base.networkView.RPC("tellWrecked", RPCMode.All, new object[] { true });
					}
					else if (this.health < 10)
					{
						this.wrecked = true;
						base.networkView.RPC("tellWrecked", RPCMode.All, new object[] { true });
					}

                    // Checking if NOW wrecked
					if (this.wrecked)
					{
						this.ejectAll();
						this.lastWrecked = Time.realtimeSinceStartup;
					}
					else if (this.lastSpeed != 0 && Time.realtimeSinceStartup - this.lastPump > 3f && this.fuel > 0)
					{
						this.lastPump = Time.realtimeSinceStartup;
						this.burn(1);
						if (this.fuel == 0)
						{
							base.networkView.RPC("tellHeadlights", RPCMode.All, new object[] { false });
							base.networkView.RPC("tellSirens", RPCMode.All, new object[] { false });
						}
					}
				}
				else if (Time.realtimeSinceStartup - this.lastWrecked > 10f && !this.exploded && base.transform.position.y > Ocean.level - 1f)
				{
					this.exploded = true;
					base.networkView.RPC("tellExploded", RPCMode.All, new object[] { true });
					ExplosionTool.explode(base.transform.position + base.transform.forward, 10f, 70);
					NetworkEffects.askEffect("Effects/bomb", this.position, Quaternion.Euler(-90f, 0f, 0f), -1f);
					NetworkSounds.askSoundMax("Sounds/Projectiles/bomb", this.position, 1f, UnityEngine.Random.Range(0.95f, 1.05f), 4f, 64f);
					base.rigidbody.AddForce(new Vector3(0f, 20f, 0f), ForceMode.Impulse);
					base.rigidbody.AddTorque(new Vector3((float)UnityEngine.Random.Range(-20, 20), 0f, 0f), ForceMode.Impulse);
                    base.Invoke("removeFromWorld", 30f);
				}

                if (this.lastSpeed == 0)
				{
					this.lastPump = Time.realtimeSinceStartup + 1f;
				}
				if (this.passengers[0] == null && (double)(Time.realtimeSinceStartup - this.lastTick) > 0.2)
				{
					this.lastTick = Time.realtimeSinceStartup;
					if ((base.transform.position - this.lastPosition).sqrMagnitude > 1f)
					{
						this.lastPosition = this.position;
						base.networkView.RPC("updatePosition", RPCMode.All, new object[] { base.transform.position, base.transform.rotation });
					}
				}
			}
			if ((Movement.vehicle != this || !Movement.isDriving) && (!Network.isServer || this.passengers[0] != null))
			{
				base.rigidbody.MovePosition(Vector3.Lerp(base.transform.position, this.position, (float)NetworkInterpolation.INTERPOLATION_RATE * Time.deltaTime));
				base.rigidbody.MoveRotation(Quaternion.Lerp(base.transform.rotation, this.rotation, (float)NetworkInterpolation.INTERPOLATION_RATE * Time.deltaTime));
			}
			if (this.sirens)
			{
				if (Time.realtimeSinceStartup % 0.717f >= 0.3585f)
				{
					base.transform.FindChild("siren").light.color = Color.blue;
				}
				else
				{
					base.transform.FindChild("siren").light.color = Color.red;
				}
			}
			AudioSource audioSource = base.audio;
			float single = base.audio.volume;
			if (this.fuel <= 0)
			{
				fuel = 0;
			}
			else
			{
				fuel = 1;
			}
			audioSource.volume = Mathf.Lerp(single, (float)fuel, 4f * Time.deltaTime);
			base.audio.pitch = Mathf.Lerp(base.audio.pitch, 0.5f + (float)Mathf.Abs(this.lastSpeed) * 0.08f, 4f * Time.deltaTime);
			this.spinSpeed = Mathf.Lerp(this.spinSpeed, (float)this.lastSpeed, 4f * Time.deltaTime);
			Vehicle vehicle = this;
			vehicle.spin = vehicle.spin + Time.deltaTime * this.spinSpeed * 30f;
			this.frontLeft.transform.FindChild("wheel").localRotation = Quaternion.Euler(0f, (float)(90 + this.lastTurn), this.spin);
			this.frontRight.transform.FindChild("wheel").localRotation = Quaternion.Euler(0f, (float)(90 + this.lastTurn), this.spin);
			this.backLeft.transform.FindChild("wheel").localRotation = Quaternion.Euler(0f, 90f, this.spin);
			this.backRight.transform.FindChild("wheel").localRotation = Quaternion.Euler(0f, 90f, this.spin);
			if (this.left0 != null)
			{
				this.left0.localRotation = Quaternion.Euler(-90f, 90f, this.spin);
			}
			if (this.left1 != null)
			{
				this.left1.localRotation = Quaternion.Euler(-90f, 90f, this.spin);
			}
			if (this.right0 != null)
			{
				this.right0.localRotation = Quaternion.Euler(-90f, 90f, this.spin);
			}
			if (this.right1 != null)
			{
				this.right1.localRotation = Quaternion.Euler(-90f, 90f, this.spin);
			}
		}
	}

    void removeFromWorld() {
        Network.RemoveRPCs(this.networkView.owner);
        Network.Destroy(this.gameObject);
        Destroy(this.gameObject, 1f);
    }

	[RPC]
	public void updatePosition(Vector3 setPosition, Quaternion setRotation)
	{
		if (this.real)
		{
			this.position = setPosition;
			this.rotation = setRotation;
		}
	}

	[RPC]
	public void updateSpeed(int speed)
	{
		if (this.real)
		{
			this.lastSpeed = speed;
		}
	}

	[RPC]
	public void updateTurn(int turn)
	{
		if (this.real)
		{
			this.lastTurn = turn;
		}
	}
}