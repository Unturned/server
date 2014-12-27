using System;
using UnityEngine;

public class SleekImage : SleekFrame
{
	private Texture texture;

	private string image;

	public SleekImage()
	{
	}

	public override void drawFrame()
	{
		if (this.texture != null)
		{
			base.update();
			SleekRender.image(new Rect((float)base.getPosition_x(), (float)base.getPosition_y(), (float)base.getSize_x(), (float)base.getSize_y()), this.texture, this.color);
			base.drawChildren();
		}
	}

	public void setImage(string path)
	{
		if (path != this.image)
		{
			this.image = path;
			if (path == string.Empty)
			{
				this.texture = null;
			}
			else
			{
				this.texture = (Texture)Resources.Load(path);
			}
		}
	}

	public void setImage(RenderTexture tex)
	{
		this.texture = tex;
	}

	public void setImage(Texture2D tex)
	{
		this.texture = tex;
	}
}