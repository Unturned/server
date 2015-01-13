using System;
using UnityEngine;

public class Ocean : MonoBehaviour
{
	public static float level;

    public Color colorMidday;

	public Color refractionMidday;

	public Color reflectionMidday;

	public Color colorMidnight;

	public Color refractionMidnight;

	public Color reflectionMidnight;

	public Ocean()
	{
	}

	public void Awake()
	{
		// TODO: sorry...
		Ocean.level = base.transform.position.y;
	}
}