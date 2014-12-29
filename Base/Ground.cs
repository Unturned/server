using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
	private static Terrain terrain;

	private static TerrainData data;

	public Ground()
	{
	}

	public void Awake()
	{
		Ground.terrain = base.GetComponent<Terrain>();
		Ground.data = (TerrainData)UnityEngine.Object.Instantiate(Ground.terrain.terrainData);
		Ground.terrain.terrainData = Ground.data;
	}

	public static void clear(Vector3 position, int size)
	{
		float single = position.x - Ground.terrain.transform.position.x;
		Vector3 vector3 = Ground.data.size;
		int num = (int)(single / vector3.x * (float)Ground.data.detailWidth);
		float single1 = position.z - Ground.terrain.transform.position.z;
		Vector3 vector31 = Ground.data.size;
		int num1 = (int)(single1 / vector31.z * (float)Ground.data.detailHeight);
		int[,] numArray = new int[size, size];
		for (int i = 0; i < (int)Ground.data.detailPrototypes.Length; i++)
		{
			Ground.data.SetDetailLayer(num - size / 2, num1 - size / 2, i, numArray);
		}
	}

	public static float height(Vector3 position)
	{
		if (Ground.terrain == null)
		{
			return -1f;
		}
		return Ground.terrain.SampleHeight(position);
	}

	public static int material(Vector3 position)
	{
		TerrainData terrainDatum = Ground.terrain.terrainData;
		float single = position.x - Ground.terrain.transform.position.x;
		Vector3 vector3 = Ground.terrain.terrainData.size;
		int num = (int)(single / vector3.x * (float)Ground.terrain.terrainData.alphamapWidth);
		float single1 = position.z - Ground.terrain.transform.position.z;
		Vector3 vector31 = Ground.terrain.terrainData.size;
		float[,,] alphamaps = terrainDatum.GetAlphamaps(num, (int)(single1 / vector31.z * (float)Ground.terrain.terrainData.alphamapHeight), 1, 1);
		float[] singleArray = new float[alphamaps.GetUpperBound(2) + 1];
		for (int i = 0; i < (int)singleArray.Length; i++)
		{
			singleArray[i] = alphamaps[0, 0, i];
		}
		float single2 = 0f;
		int num1 = 0;
		for (int j = 0; j < (int)singleArray.Length; j++)
		{
			if (singleArray[j] > single2)
			{
				num1 = j;
				single2 = singleArray[j];
			}
		}
		return num1;
	}
}