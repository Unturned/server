using System;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
	private static SpawnProjectiles tool;

	public static GameObject model;

	public SpawnProjectiles()
	{
	}

	[RPC]
	public void createProjectile(int id, Vector3 position, Vector3 force)
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(string.Concat("Prefabs/Projectiles/", id)), position, Quaternion.LookRotation(force) * Quaternion.Euler(0f, 0f, -90f));
		gameObject.name = "projectile";
		gameObject.transform.parent = SpawnProjectiles.model.transform.FindChild("models");
		gameObject.rigidbody.AddForce(force);
	}

	public void onReady()
	{
		SpawnProjectiles.tool = this;
		SpawnProjectiles.model = GameObject.Find(Application.loadedLevelName).transform.FindChild("projectiles").gameObject;
	}

	public void Start()
	{
		NetworkEvents.onReady += new NetworkEventDelegate(this.onReady);
	}

	public static void throwProjectile(int id, Vector3 position, Vector3 force)
	{
		SpawnProjectiles.tool.networkView.RPC("createProjectile", RPCMode.All, new object[] { id, position, force });
	}
}