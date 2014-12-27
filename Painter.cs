using System;
using UnityEngine;

public class Painter : MonoBehaviour
{
	public Color color;

	private Texture2D source;

	public Painter()
	{
	}

	public void paint(Color setColor)
	{
		this.color = setColor;
		Texture2D texture2D = new Texture2D(32, 32, TextureFormat.RGBA32, true, true)
		{
			filterMode = FilterMode.Point
		};
		for (int i = 0; i < 32; i++)
		{
			for (int j = 0; j < 32; j++)
			{
				if (this.source.GetPixel(i, j) != Color.white)
				{
					texture2D.SetPixel(i, j, this.source.GetPixel(i, j));
				}
				else
				{
					texture2D.SetPixel(i, j, this.color);
				}
			}
		}
		texture2D.Apply();
		base.renderer.material.mainTexture = texture2D;
	}

	public void Start()
	{
		this.source = (Texture2D)base.renderer.material.mainTexture;
		this.paint(this.color);
	}
}