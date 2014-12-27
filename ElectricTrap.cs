using System;
using UnityEngine;

public class ElectricTrap : MonoBehaviour
{
	public bool powered;

	private bool sparks;

	public ElectricTrap()
	{
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && this.powered)
		{
			InteractionInterface.sendTrap(base.transform.parent.position);
			NetworkSounds.askSound("Sounds/Barricades/electrocute", other.transform.position + Vector3.up, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
			NetworkEffects.askEffect("Effects/flesh", other.transform.position + Vector3.up, Quaternion.identity, -1f);
		}
	}

	public void setPowered(bool setPowered)
	{
		if (setPowered != this.powered)
		{
			this.powered = setPowered;
			if (!this.powered)
			{
				this.setSparks(false);
			}
			else
			{
				this.setSparks(true);
			}
		}
	}

	public void setSparks(bool setSparks)
	{
		if (setSparks != this.sparks)
		{
			this.sparks = setSparks;
			if (!this.sparks)
			{
				base.transform.parent.FindChild("spark").GetComponent<ParticleSystem>().Stop();
			}
			else
			{
				base.transform.parent.FindChild("spark").GetComponent<ParticleSystem>().Play();
			}
		}
	}

	public void Start()
	{
		this.setPowered(Electricity.checkPower(base.transform.position, 8f));
	}
}