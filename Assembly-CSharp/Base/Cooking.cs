using System;
using UnityEngine;

public class Cooking
{
	public Cooking()
	{
	}

	public static bool fire(Vector3 position)
	{
		Collider[] colliderArray = Physics.OverlapSphere(position, 8f, 32768);
		for (int i = 0; i < (int)colliderArray.Length; i++)
		{
			if (colliderArray[i].transform.parent.name == "16013" && colliderArray[i].GetComponent<Campfire>().state)
			{
				return true;
			}
		}
		return false;
	}
}