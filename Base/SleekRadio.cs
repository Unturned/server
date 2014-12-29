using System;
using UnityEngine;

public class SleekRadio : SleekFrame
{
	public bool state;

	private bool lastState;
	
	public event SleekDelegate onUsed;
	
	public SleekRadio()
	{
	}

	public override void drawFrame()
	{
		base.update();
		this.state = SleekRender.radio(new Rect((float)base.getPosition_x(), (float)base.getPosition_y(), (float)base.getSize_x(), (float)base.getSize_y()), this.state, this.tooltip, this.color);
		if (this.state != this.lastState)
		{
			this.lastState = this.state;
			this.used();
			Camera.main.audio.PlayOneShot((AudioClip)Resources.Load("Sounds/Sleek/button"), 0.05f);
		}
		base.drawChildren();
	}

	private void used()
	{
		if (this.onUsed != null)
		{
			this.onUsed(this);
		}
	}
}