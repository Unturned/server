using System;
using UnityEngine;

public class AI : MonoBehaviour
{
	public bool moving;

	public int minDrops;

	public int maxDrops;

	public float chanceDrop;

	public string loot;

	public bool dead;

	public Animifier anim;

	public NavMeshAgent agent;

	public int health;

	public int maxHealth;

	public int speed;

	public int xp;

	private float traversed;

	public AI()
	{
	}

	public virtual void alert(bool angry)
	{
	}

	public virtual void attract(Vector3 point)
	{
	}

	public void Awake()
	{
		base.transform.parent = SpawnAnimals.model.transform.FindChild("models");
		this.anim = base.transform.FindChild("model").GetComponent<Animifier>();
	}

	public void damage(int amount)
	{
		if (!this.dead)
		{
			AI aI = this;
			aI.health = aI.health - amount;
			if (this.health <= 0)
			{
				base.networkView.RPC("tellDead", RPCMode.All, new object[0]);
				if (UnityEngine.Random.@value > this.chanceDrop)
				{
					for (int i = 0; i < UnityEngine.Random.Range(this.minDrops, this.maxDrops + 1); i++)
					{
						SpawnItems.dropItem(Loot.getLoot(this.loot), base.transform.position);
					}
				}
				base.transform.rotation = Quaternion.Euler(0f, (float)UnityEngine.Random.Range(0, 360), 0f);
				base.Invoke("respawn", (float)UnityEngine.Random.Range(100, 140));
			}
		}
	}

	public virtual void die()
	{
	}

	public void respawn()
	{
		SpawnAnimals.respawn(base.gameObject);
	}

	public virtual void setupClient()
	{
	}

	public virtual void setupServer()
	{
	}

	public void Start()
	{
		if (!Network.isServer)
		{
			base.networkView.RPC("askAllAI", RPCMode.Server, new object[] { Network.player });
		}
		else
		{
			base.transform.rotation = Quaternion.Euler(0f, (float)UnityEngine.Random.Range(0, 360), 0f);
			this.agent = base.gameObject.AddComponent<NavMeshAgent>();
			this.agent.speed = (float)this.speed;
			this.agent.acceleration = 64f;
			this.agent.angularSpeed = 720f;
			this.agent.stoppingDistance = 1f;
			this.agent.radius = 0.5f;
			this.agent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
			this.agent.autoRepath = false;
			this.agent.autoTraverseOffMeshLink = false;
			this.agent.avoidancePriority = 99;
			this.agent.enabled = false;
			this.health = this.maxHealth;
			this.setupServer();
			if (ServerSettings.dedicated)
			{
				base.transform.FindChild("model").FindChild("character").FindChild("model").renderer.enabled = false;
			}
		}
		this.setupClient();
		if (base.networkView.owner.ToString() != "0" && base.networkView.owner.ToString() != "-1")
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	[RPC]
	public void tellDead()
	{
		this.die();
	}

	[RPC]
	public void tellMoving(bool setMoving)
	{
		this.moving = setMoving;
	}

	public void traverse()
	{
		if (this.traversed == 0f)
		{
			this.traversed = Time.realtimeSinceStartup;
		}
		if ((double)(Time.realtimeSinceStartup - this.traversed) >= 0.25)
		{
			this.traversed = 0f;
			base.transform.position = this.agent.currentOffMeshLinkData.endPos;
			this.agent.CompleteOffMeshLink();
			this.agent.Resume();
		}
		else
		{
			Transform transforms = base.transform;
			Vector3 vector3 = this.agent.currentOffMeshLinkData.startPos;
			OffMeshLinkData offMeshLinkDatum = this.agent.currentOffMeshLinkData;
			transforms.position = Vector3.Lerp(vector3, offMeshLinkDatum.endPos, (Time.realtimeSinceStartup - this.traversed) * 4f);
			Transform transforms1 = base.transform;
			OffMeshLinkData offMeshLinkDatum1 = this.agent.currentOffMeshLinkData;
			float single = offMeshLinkDatum1.endPos.z - this.agent.currentOffMeshLinkData.startPos.z;
			float single1 = this.agent.currentOffMeshLinkData.endPos.x;
			Vector3 vector31 = this.agent.currentOffMeshLinkData.startPos;
			transforms1.rotation = Quaternion.Euler(0f, 90f - 57.29578f * Mathf.Atan2(single, single1 - vector31.x), 0f);
		}
	}
}