using System;
using UnityEngine;

public class SleekLabel : SleekFrame
{
	public string text = string.Empty;

	public int fontSize = 16;

	public TextAnchor alignment = TextAnchor.MiddleCenter;

	public SleekLabel()
	{
	}

	public override void drawFrame()
	{
		base.update();
		GUI.skin.label.fontSize = this.fontSize;
		GUI.skin.label.alignment = this.alignment;
		SleekRender.label(new Rect((float)base.getPosition_x(), (float)base.getPosition_y(), (float)base.getSize_x(), (float)base.getSize_y()), this.text, this.tooltip, this.paint);
		base.drawChildren();
	}
}