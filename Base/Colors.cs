using System;
using UnityEngine;

public class Colors
{
	public readonly static Color FREE;

	public readonly static Color PAID;

	public readonly static Color GOLD;

	public readonly static Color NVG_0;

	public readonly static Color NVG_1;

	static Colors()
	{
		Colors.FREE = new Color(0.95f, 0.95f, 0.95f);
		Colors.PAID = new Color(0.5f, 0.5f, 0.5f);
		Colors.GOLD = new Color(0.8509804f, 0.68235296f, 0.101960786f);
		Colors.NVG_0 = new Color(0f, 0.75f, 0f);
		Colors.NVG_1 = new Color(0.5f, 0.5f, 0.5f);
	}

	public Colors()
	{
	}
}