using System;
using System.Collections.Generic;
using UnityEngine;

public class BarricadesRegion
{
	public List<ServerBarricade> barricades;

	public List<GameObject> models;

	public float cooldown;

	public bool edit;

	public BarricadesRegion()
	{
		this.barricades = new List<ServerBarricade>();
		this.models = new List<GameObject>();
	}

	public static bool acceptable(Point2 a, Point2 b)
	{
		return (!BarricadesRegion.acceptable(a.x, b.y) ? false : BarricadesRegion.acceptable(a.y, b.y));
	}

	public static bool acceptable(int a, int b)
	{
		return (a == b || a == b - 1 || a == b - 2 || a == b - 3 || a == b - 4 || a == b + 1 || a == b + 2 || a == b + 3 ? true : a == b + 4);
	}
}