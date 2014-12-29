using System;
using UnityEngine;

public class ExplosiveTrap : MonoBehaviour
{
	public ExplosiveTrap()
	{
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			InteractionInterface.sendExplosiveTrap(base.transform.parent.position);
		}
	}
}