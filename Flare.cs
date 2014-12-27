using System;
using UnityEngine;

public class Flare : MonoBehaviour
{
	public Flare()
	{
	}

	public void attract()
	{
		SpawnAnimals.attract(base.transform.position + Vector3.up, 30f);
	}

	public void Start()
	{
		if (Network.isServer)
		{
			base.Invoke("attract", 2f);
		}
		UnityEngine.Object.Destroy(base.gameObject, 180f);
	}
}