using System;
using UnityEngine;

public class FancyAnimifier : MonoBehaviour
{
	public readonly static float FADE;

	private string playID = string.Empty;

	private float startedPlay;

	private string stanceID = string.Empty;

	private Animation anim;

	static FancyAnimifier()
	{
		FancyAnimifier.FADE = 0.25f;
	}

	public FancyAnimifier()
	{
	}

	public void play(string id)
	{
		if (id != this.playID)
		{
			this.playID = id;
			this.startedPlay = Time.realtimeSinceStartup;
		}
	}

	public void stance(string id)
	{
		this.stanceID = id;
	}

	public void Start()
	{
		this.anim = base.transform.FindChild("character").animation;
	}

	public void Update()
	{
		/*
		if (this.anim != null)
		{
			if (this.playID != string.Empty)
			{
				if (Time.realtimeSinceStartup - this.startedPlay >= this.anim[this.playID].length)
				{
					this.playID = string.Empty;
				}
				else
				{
					this.anim.Play(this.playID);
				}
			}
			else if (this.stanceID != string.Empty)
			{
				this.anim.CrossFade(this.stanceID, FancyAnimifier.FADE);
			}
		}
		*/
	}
}