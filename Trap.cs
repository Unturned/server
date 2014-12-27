using System;
using UnityEngine;

public class Trap : MonoBehaviour
{
	public Trap()
	{
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			InteractionInterface.sendTrap(base.transform.parent.position);
			NetworkSounds.askSound("Sounds/Barricades/spike", other.transform.position + Vector3.up, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
			NetworkEffects.askEffect("Effects/flesh", other.transform.position + Vector3.up, Quaternion.identity, -1f);
		}
	}
}