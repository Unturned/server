using System;
using UnityEngine;

public class SleekField : SleekLabel
{
	public char replace = 'a';

	private string lastText = string.Empty;
	
	public event SleekDelegate onUsed;
	
	public SleekField()
	{
	}

	public override void drawFrame()
	{
		base.update();
		GUI.skin.label.fontSize = this.fontSize;
		GUI.skin.textField.fontSize = this.fontSize;
		GUI.skin.label.alignment = this.alignment;
		GUI.skin.textField.alignment = this.alignment;
		if (this.replace == 'a')
		{
			this.text = SleekRender.field(new Rect((float)base.getPosition_x(), (float)base.getPosition_y(), (float)base.getSize_x(), (float)base.getSize_y()), this.text, this.tooltip, this.color, this.paint);
		}
		else
		{
			this.text = SleekRender.field(new Rect((float)base.getPosition_x(), (float)base.getPosition_y(), (float)base.getSize_x(), (float)base.getSize_y()), this.text, this.tooltip, this.replace, this.color, this.paint);
		}
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