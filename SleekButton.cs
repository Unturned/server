using System;
using UnityEngine;

public class SleekButton : SleekLabel
{
	public SleekDelegate onUsed;
	
	public SleekButton()
	{
	}

	public override void drawFrame()
	{
		base.update();
		GUI.skin.label.fontSize = this.fontSize;
		GUI.skin.label.alignment = this.alignment;
		if (MenuKeys.binding != -1)
		{
			SleekRender.box(new Rect((float)base.getPosition_x(), (float)base.getPosition_y(), (float)base.getSize_x(), (float)base.getSize_y()), this.text, this.tooltip, this.color, this.paint);
		}
		else if (SleekRender.button(new Rect((float)base.getPosition_x(), (float)base.getPosition_y(), (float)base.getSize_x(), (float)base.getSize_y()), this.text, this.tooltip, this.color, this.paint))
		{
			this.used();
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