using System;
using UnityEngine;

public class Generator : Interactable
{
	public bool state;

	public Generator()
	{
	}

	public override string hint()
	{
		if (this.state)
		{
			return "Turn Off";
		}
		return "Turn On";
	}

	public void OnDestroy()
	{
		this.state = false;
		Electricity.applyPower(base.transform.position, 8f);
	}

	public void setState(bool setState)
	{
		if (setState != this.state)
		{
			this.state = setState;
			if (!this.state)
			{
				base.audio.Stop();
			}
			else
			{
				base.audio.Play();
			}
			Electricity.applyPower(base.transform.position, 8f);
		}
	}

	public void Start()
	{
		InteractionInterface.requestGenerator(base.transform.parent.position);
	}

	public override void trigger()
	{
		InteractionInterface.sendGenerator(base.transform.parent.position);
		NetworkSounds.askSound("Sounds/General/firemode", base.transform.position, 0.1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
	}
}