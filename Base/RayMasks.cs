using System;
using UnityEngine;

public class RayMasks
{
	public readonly static int DAMAGE;

	public readonly static int PLACEABLE;

	public readonly static int ERROR;

	public readonly static int ERRORSTRUCT;

	public readonly static int RAYBLOCK;

	public readonly static int BUILT;

	public readonly static int ATTACKABLE;

	public readonly static int INTERACTABLE;

	public readonly static int STATIC;

	static RayMasks()
	{
		RayMasks.DAMAGE = -1039144215;
		RayMasks.PLACEABLE = -1039324951;
		RayMasks.ERROR = -1039292183;
		RayMasks.ERRORSTRUCT = 65536;
		RayMasks.RAYBLOCK = -1039390487;
		RayMasks.BUILT = 98304;
		RayMasks.ATTACKABLE = 246528;
		RayMasks.INTERACTABLE = -1039140119;
		RayMasks.STATIC = -1039333143;
	}

	public RayMasks()
	{
	}

	public static bool isVisible(Vector3 start, Vector3 end)
	{
		return !Physics.Linecast(start, end, RayMasks.RAYBLOCK);
	}
}