using System;
using UnityEngine;

public class Harvest : Interactable
{
	private long state = (long)2147483647;

	private bool ready;

	public Harvest()
	{
	}

	public void grow()
	{
		if (!this.ready && (long)Epoch.getSeconds() - this.state > (long)HarvestStats.getGrowth(int.Parse(base.transform.parent.name)))
		{
			this.ready = true;
			base.renderer.material = (Material)Resources.Load(string.Concat("Models/Barricades/", base.transform.parent.name, "/matPlant"));
		}
	}

	public override string hint()
	{
		if (this.ready)
		{
			return "Harvest";
		}
		return string.Empty;
	}

	public void setState(int setState)
	{
		if (this.state != (long)setState)
		{
			this.state = (long)setState;
			this.grow();
		}
	}

	public void Start()
	{
		InteractionInterface.requestHarvest(base.transform.parent.position);
		base.InvokeRepeating("grow", UnityEngine.Random.Range(1f, 2f), 3f);
	}

	public override void trigger()
	{
		InteractionInterface.sendHarvest(base.transform.parent.position);
	}
}