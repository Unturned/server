using System;
using UnityEngine;

public class Explosive : Interactable
{
	public Explosive()
	{
	}

	public void explode()
	{
		InteractionInterface.sendExplosive(base.transform.parent.position);
	}

	public void Start()
	{
		if (Network.isServer)
		{
			base.Invoke("explode", 5f);
		}
		base.audio.Play();
	}
}