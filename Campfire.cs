using System;
using UnityEngine;

public class Campfire : Interactable
{
	public bool state;

	public Campfire()
	{
	}

	public override string hint()
	{
		if (this.state)
		{
			return "Extinguish";
		}
		return "Light";
	}

	public void setState(bool setState)
	{
		if (setState != this.state)
		{
			this.state = setState;
			if (!this.state)
			{
				base.audio.Stop();
				base.transform.FindChild("fire").light.enabled = false;
				base.transform.FindChild("fire").GetComponent<ParticleSystem>().Stop();
				if (base.transform.FindChild("smoke") != null)
				{
					base.transform.FindChild("smoke").GetComponent<ParticleSystem>().Stop();
				}
			}
			else
			{
				base.audio.Play();
				base.transform.FindChild("fire").light.enabled = true;
				base.transform.FindChild("fire").GetComponent<ParticleSystem>().Play();
				if (base.transform.FindChild("smoke") != null)
				{
					base.transform.FindChild("smoke").GetComponent<ParticleSystem>().Play();
				}
			}
		}
	}

	public void Start()
	{
		InteractionInterface.requestCampfire(base.transform.parent.position);
	}

	public override void trigger()
	{
		InteractionInterface.sendCampfire(base.transform.parent.position);
	}
}