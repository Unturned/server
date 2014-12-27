using System;
using UnityEngine;

public class Snare : MonoBehaviour
{
	public Snare()
	{
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			InteractionInterface.sendTrap(base.transform.parent.position);
			if (ServerSettings.pvp)
			{
				Player.life.sendBones();
			}
			NetworkSounds.askSound("Sounds/Barricades/snare", other.transform.position + Vector3.up, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
			NetworkEffects.askEffect("Effects/flesh", other.transform.position + Vector3.up, Quaternion.identity, -1f);
		}
	}
}