using System;
using UnityEngine;

public class Lamp : Interactable
{
	private bool state;

	private bool powered;

	private bool lights;

	public Lamp()
	{
	}

	public override string hint()
	{
		if (!this.powered)
		{
			return string.Empty;
		}
		if (this.state)
		{
			return "Turn Off";
		}
		return "Turn On";
	}

	public void setLights(bool setLights)
	{
		if (setLights != this.lights)
		{
			this.lights = setLights;
			base.transform.FindChild("light_0").light.enabled = setLights;
			base.transform.FindChild("light_1").light.enabled = setLights;
		}
	}

	public void setPowered(bool setPowered)
	{
		if (this.powered != setPowered)
		{
			this.powered = setPowered;
			if (!this.powered || !this.state)
			{
				this.setLights(false);
			}
			else
			{
				this.setLights(true);
			}
		}
	}

	public void setState(bool setState)
	{
		this.state = setState;
		this.setPowered(Electricity.checkPower(base.transform.position, 8f));
		if (!this.powered || !this.state)
		{
			this.setLights(false);
		}
		else
		{
			this.setLights(true);
		}
	}

	public void Start()
	{
		InteractionInterface.requestLamp(base.transform.parent.position);
	}

	public override void trigger()
	{
		InteractionInterface.sendLamp(base.transform.parent.position);
		NetworkSounds.askSound("Sounds/General/firemode", base.transform.position, 0.1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
	}
}