using System;
using UnityEngine;

public class SleekSlider : SleekFrame
{
	public float state;

	public float scale = 0.5f;

	private float lastSlide;

	private float lastTick;

	public Orient2 orientation;
	
	public event SleekDelegate onUsed;

	public SleekSlider()
	{
	}

	public override void drawFrame()
	{
		base.update();
		if (this.orientation != Orient2.HORIZONTAL)
		{
			this.state = SleekRender.verticalBar(new Rect((float)base.getPosition_x(), (float)base.getPosition_y(), (float)base.getSize_x(), (float)base.getSize_y()), this.state, this.scale, this.color);
		}
		else
		{
			this.state = SleekRender.horizontalBar(new Rect((float)base.getPosition_x(), (float)base.getPosition_y(), (float)base.getSize_x(), (float)base.getSize_y()), this.state, this.scale, this.color);
		}
		if (Input.GetAxis("Mouse ScrollWheel") != 0f)
		{
			Rect rect = new Rect((float)(base.getPosition_x() - 64), (float)(base.getPosition_y() - 64), (float)(base.getSize_x() + 128), (float)(base.getSize_y() + 128));
			if (rect.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)))
			{
				SleekSlider axis = this;
				axis.state = axis.state - 0.16f * Input.GetAxis("Mouse ScrollWheel");
			}
		}
		if (this.state < 0f)
		{
			this.state = 0f;
		}
		else if (this.state > 1f)
		{
			this.state = 1f;
		}
		if (this.state != this.lastSlide)
		{
			this.lastSlide = this.state;
			this.used();
			if ((double)(Time.realtimeSinceStartup - this.lastTick) > 0.05)
			{
				this.lastTick = Time.realtimeSinceStartup;
				Camera.main.audio.PlayOneShot((AudioClip)Resources.Load("Sounds/Sleek/slider"), 0.05f);
			}
		}
		base.drawChildren();
	}

	public void setState(float value)
	{
		this.state = value;
		this.lastSlide = value;
	}

	private void used()
	{
		if (this.onUsed != null)
		{
			this.onUsed(this);
		}
	}
}