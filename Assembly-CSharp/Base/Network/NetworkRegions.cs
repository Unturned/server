using System;
using UnityEngine;

public class NetworkRegions : MonoBehaviour
{
	public readonly static int REGION_SIZE;

	public readonly static int REGION_X;

	public readonly static int REGION_Y;

	public static Point2 region;

	public static Point2 lastRegion;

	private static float loaded;

	static NetworkRegions()
	{
		NetworkRegions.REGION_SIZE = 64;
		NetworkRegions.REGION_X = 32;
		NetworkRegions.REGION_Y = 32;
		NetworkRegions.region = Point2.ZERO;
		NetworkRegions.lastRegion = Point2.NONE;
		NetworkRegions.loaded = 0f;
	}

	public NetworkRegions()
	{
	}

	public static Point2 getRegion(Vector3 point)
	{
		int rEGIONX = (int)((point.x + (float)(NetworkRegions.REGION_X / 2 * NetworkRegions.REGION_SIZE)) / (float)NetworkRegions.REGION_SIZE);
		int rEGIONY = (int)((point.z + (float)(NetworkRegions.REGION_Y / 2 * NetworkRegions.REGION_SIZE)) / (float)NetworkRegions.REGION_SIZE);
		if (rEGIONX < 0)
		{
			rEGIONX = 0;
		}
		else if (rEGIONX >= NetworkRegions.REGION_X)
		{
			rEGIONX = NetworkRegions.REGION_X - 1;
		}
		if (rEGIONY < 0)
		{
			rEGIONY = 0;
		}
		else if (rEGIONY >= NetworkRegions.REGION_Y)
		{
			rEGIONY = NetworkRegions.REGION_Y - 1;
		}
		return new Point2(rEGIONX, rEGIONY);
	}

	public void OnDrawGizmosSelected()
	{
		for (int i = -NetworkRegions.REGION_X / 2; i < NetworkRegions.REGION_X / 2; i++)
		{
			for (int j = -NetworkRegions.REGION_Y / 2; j < NetworkRegions.REGION_Y / 2; j++)
			{
				Gizmos.color = Color.white;
				Gizmos.DrawWireCube(new Vector3((float)(i * NetworkRegions.REGION_SIZE + NetworkRegions.REGION_SIZE / 2), 0f, (float)(j * NetworkRegions.REGION_SIZE + NetworkRegions.REGION_SIZE / 2)), new Vector3((float)NetworkRegions.REGION_SIZE, (float)NetworkRegions.REGION_SIZE, (float)NetworkRegions.REGION_SIZE));
			}
		}
	}

	public void Start()
	{
		NetworkRegions.region = Point2.ZERO;
		NetworkRegions.lastRegion = Point2.NONE;
	}

	public void Update()
	{
		if (Player.model == null)
		{
			NetworkRegions.loaded = Time.realtimeSinceStartup;
		}
		else if (Time.realtimeSinceStartup - NetworkRegions.loaded > 1f)
		{
			NetworkRegions.region = NetworkRegions.getRegion(Player.model.transform.position);
			if (NetworkRegions.region.x != NetworkRegions.lastRegion.x || NetworkRegions.region.y != NetworkRegions.lastRegion.y)
			{
				NetworkEvents.triggerOnRegionUpdate();
				NetworkRegions.lastRegion = NetworkRegions.region;
			}
		}
	}
}