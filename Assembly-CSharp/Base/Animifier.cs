using System;
using UnityEngine;

public class Animifier : MonoBehaviour
{
	public readonly static float FADE;

	private string playID = string.Empty;

	private float startedPlay;

	private string stanceID = string.Empty;

	private Animation anim;

	static Animifier()
	{
		Animifier.FADE = 0.15f;
	}

	public Animifier()
	{
	}

	public void play(string id)
	{
		if (id != this.playID)
		{
			this.playID = id;
			this.startedPlay = Time.realtimeSinceStartup;
			this.tick();
		}
	}

	public void stance(string id)
	{
		this.stanceID = id;
		this.tick();
	}

	public void Start()
	{
		this.anim = base.transform.FindChild("character").animation;
		this.tick();
	}

	public void tick()
	{
		if (this.anim != null)
		{
			if (this.playID != string.Empty)
			{
				if (Time.realtimeSinceStartup - this.startedPlay >= this.anim[this.playID].length)
				{
					this.playID = string.Empty;
					this.tick();
				}
				else
				{
					this.anim.Play(this.playID);
				}
			}
			else if (this.stanceID != string.Empty)
			{
				this.anim.Play(this.stanceID);
			}
		}
	}
}