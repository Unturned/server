using System;
using System.Collections.Generic;
using UnityEngine;

public class SleekFrame
{
	public SleekFrame parent;

	public List<SleekFrame> children = new List<SleekFrame>();

	public Coord2 position = Coord2.ZERO;

	public Coord2 size = Coord2.ZERO;

	public bool visible = true;

	public string tooltip = string.Empty;

	public Color color = Color.white;

	public Color paint = Color.white;

	private Coord2 oldPosition = Coord2.ZERO;

	private Coord2 oldSize = Coord2.ZERO;

	private Coord2 dirPosition = Coord2.ZERO;

	private Coord2 dirSize = Coord2.ZERO;

	private float lerpSpeed = 1f;

	private float step = 1f;

	private bool lerpVisible;

	public SleekFrame()
	{
	}

	public void addFrame(SleekFrame frame)
	{
		this.children.Add(frame);
		frame.parent = this;
	}

	public void clearFrames()
	{
		this.children = new List<SleekFrame>();
	}

	public void drawChildren()
	{
		for (int i = 0; i < this.children.Count; i++)
		{
			if (this.children[i].visible)
			{
				this.children[i].drawFrame();
			}
		}
	}

	public virtual void drawFrame()
	{
		this.update();
	}

	public int getPosition_x()
	{
		if (this.parent == null)
		{
			return this.position.offset_x;
		}
		return this.position.offset_x + (int)((float)this.parent.getSize_x() * this.position.scale_x) + this.parent.getPosition_x();
	}

	public int getPosition_y()
	{
		if (this.parent == null)
		{
			return this.position.offset_y;
		}
		return this.position.offset_y + (int)((float)this.parent.getSize_y() * this.position.scale_y) + this.parent.getPosition_y();
	}

	public int getSize_x()
	{
		if (this.parent == null)
		{
			return Screen.width;
		}
		return this.size.offset_x + (int)((float)this.parent.getSize_x() * this.size.scale_x);
	}

	public int getSize_y()
	{
		if (this.parent == null)
		{
			return Screen.height;
		}
		return this.size.offset_y + (int)((float)this.parent.getSize_y() * this.size.scale_y);
	}

	public void lerp(Coord2 newTargetPosition, Coord2 newTargetSize, float speed)
	{
		this.lerpVisible = false;
		this.step = 0f;
		this.lerpSpeed = speed;
		this.oldPosition = this.position;
		this.oldSize = this.size;
		this.dirPosition = new Coord2(newTargetPosition.offset_x - this.position.offset_x, newTargetPosition.offset_y - this.position.offset_y, newTargetPosition.scale_x - this.position.scale_x, newTargetPosition.scale_y - this.position.scale_y);
		this.dirSize = new Coord2(newTargetSize.offset_x - this.size.offset_x, newTargetSize.offset_y - this.size.offset_y, newTargetSize.scale_x - this.size.scale_x, newTargetSize.scale_y - this.size.scale_y);
	}

	public void lerp(Coord2 newTargetPosition, Coord2 newTargetSize, float speed, bool setLerpVisible)
	{
		this.lerpVisible = setLerpVisible;
		this.step = 0f;
		this.lerpSpeed = speed;
		this.oldPosition = this.position;
		this.oldSize = this.size;
		this.dirPosition = new Coord2(newTargetPosition.offset_x - this.position.offset_x, newTargetPosition.offset_y - this.position.offset_y, newTargetPosition.scale_x - this.position.scale_x, newTargetPosition.scale_y - this.position.scale_y);
		this.dirSize = new Coord2(newTargetSize.offset_x - this.size.offset_x, newTargetSize.offset_y - this.size.offset_y, newTargetSize.scale_x - this.size.scale_x, newTargetSize.scale_y - this.size.scale_y);
	}

	public void @remove()
	{
		this.parent.children.Remove(this);
		this.parent = null;
	}

	public void removeFrame(SleekFrame frame)
	{
		frame.parent = null;
		this.children.Remove(frame);
	}

	public void update()
	{
		if (this.step < 1f)
		{
			SleekFrame sleekFrame = this;
			sleekFrame.step = sleekFrame.step + Time.deltaTime * this.lerpSpeed;
			if (this.step > 1f)
			{
				this.step = 1f;
				if (this.lerpVisible)
				{
					this.visible = false;
				}
			}
			this.position = new Coord2(this.oldPosition.offset_x + (int)((float)this.dirPosition.offset_x * this.step), this.oldPosition.offset_y + (int)((float)this.dirPosition.offset_y * this.step), this.oldPosition.scale_x + this.dirPosition.scale_x * this.step, this.oldPosition.scale_y + this.dirPosition.scale_y * this.step);
			this.size = new Coord2(this.oldSize.offset_x + (int)((float)this.dirSize.offset_x * this.step), this.oldSize.offset_y + (int)((float)this.dirSize.offset_y * this.step), this.oldSize.scale_x + this.dirSize.scale_x * this.step, this.oldSize.scale_y + this.dirSize.scale_y * this.step);
		}
	}
}