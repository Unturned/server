using System;
using UnityEngine;

public class Electricity
{
	public Electricity()
	{
	}

	public static void applyPower(Vector3 position, float range)
	{
		Collider[] colliderArray = Physics.OverlapSphere(position, range, 32768);
		for (int i = 0; i < (int)colliderArray.Length; i++)
		{
			if (colliderArray[i].transform.parent.name == "16002")
			{
				colliderArray[i].GetComponent<Lamp>().setPowered(Electricity.checkPower(colliderArray[i].transform.position, 8f));
			}
			else if (colliderArray[i].transform.parent.name == "16009" || colliderArray[i].transform.parent.name == "16021")
			{
				colliderArray[i].GetComponent<ElectricTrap>().setPowered(Electricity.checkPower(colliderArray[i].transform.position, 8f));
			}
		}
	}

	public static bool checkPower(Vector3 position, float range)
	{
		Collider[] colliderArray = Physics.OverlapSphere(position, range, 32768);
		for (int i = 0; i < (int)colliderArray.Length; i++)
		{
			if (colliderArray[i].transform.parent.name == "16007" && colliderArray[i].GetComponent<Generator>().state)
			{
				return true;
			}
		}
		return false;
	}
}