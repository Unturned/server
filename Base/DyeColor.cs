using System;
using UnityEngine;

public class DyeColor
{
	public readonly static Color[] COLORS;

	static DyeColor()
	{
		DyeColor.COLORS = new Color[] { new Color(1f, 1f, 1f), new Color(1f, 0f, 0f) };
	}

	public DyeColor()
	{
	}

	public static Color getColor(int id)
	{
		return DyeColor.COLORS[id];
	}
}