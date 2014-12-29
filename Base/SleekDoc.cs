using System;
using UnityEngine;

public class SleekDoc : SleekLabel
{
	private string lastText = string.Empty;
	
	public event SleekDelegate onUsed;

	public SleekDoc()
	{
	}

	public override void drawFrame()
	{
		base.update();
		GUI.skin.label.fontSize = this.fontSize;
		GUI.skin.textArea.fontSize = this.fontSize;
		GUI.skin.label.alignment = this.alignment;
		GUI.skin.textArea.alignment = this.alignment;
		this.text = SleekRender.doc(new Rect((float)base.getPosition_x(), (float)base.getPosition_y(), (float)base.getSize_x(), (float)base.getSize_y()), this.text, this.tooltip, this.color, this.paint);
		if (this.text != this.lastText)
		{
			this.lastText = this.text;
			this.used();
		}
		base.drawChildren();
	}

	public void setText(string value)
	{
		this.text = value;
		this.lastText = value;
	}

	private void used()
	{
		if (this.onUsed != null)
		{
			this.onUsed(this);
		}
	}
}