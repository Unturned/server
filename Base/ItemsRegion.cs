using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemsRegion
{
	public List<ServerItem> items;

	public List<GameObject> models;

	public float cooldown;

	public ItemsRegion()
	{
		this.items = new List<ServerItem>();
		this.models = new List<GameObject>();
	}

	public static bool acceptable(Point2 a, Point2 b)
	{
		return (!ItemsRegion.acceptable(a.x, b.y) ? false : ItemsRegion.acceptable(a.y, b.y));
	}

	public static bool acceptable(int a, int b)
	{
		return (a == b || a == b - 1 ? true : a == b + 1);
	}
}