using System;
using UnityEngine;

public class ExplosionTool : MonoBehaviour
{
	public ExplosionTool()
	{
	}

	public static void explode(Vector3 position, float range, int damage)
	{
		Collider[] colliderArray = Physics.OverlapSphere(position, range, RayMasks.ATTACKABLE);
		for (int i = 0; i < (int)colliderArray.Length; i++)
		{
			if (RayMasks.isVisible(position, colliderArray[i].transform.position + Vector3.up))
			{
				if (colliderArray[i].tag == "Player")
				{
					if (ServerSettings.pvp)
					{
						colliderArray[i].GetComponent<Life>().damage(damage, "You were blown up by an explosion.");
						NetworkEffects.askEffect("Effects/flesh", colliderArray[i].transform.position + Vector3.up, Quaternion.identity, -1f);
					}
				}
				else if (colliderArray[i].tag == "Enemy")
				{
					if (ServerSettings.pvp)
					{
						GameObject owner = OwnerFinder.getOwner(colliderArray[i].gameObject);
						owner.GetComponent<Life>().damage(damage, "You were blown up by an explosion.");
						NetworkEffects.askEffect("Effects/flesh", colliderArray[i].transform.position + Vector3.up, Quaternion.identity, -1f);
					}
				}
				else if (colliderArray[i].tag == "Animal")
				{
					GameObject gameObject = OwnerFinder.getOwner(colliderArray[i].gameObject);
					gameObject.GetComponent<AI>().damage(damage);
					NetworkEffects.askEffect("Effects/flesh", colliderArray[i].transform.position + Vector3.up, Quaternion.identity, -1f);
				}
				else if (colliderArray[i].tag == "Barricade")
				{
					if (ServerSettings.pvp)
					{
						SpawnBarricades.damage(colliderArray[i].transform.parent.position, damage);
					}
				}
				else if (colliderArray[i].tag == "Structure")
				{
					if (ServerSettings.pvp)
					{
						SpawnStructures.damage(colliderArray[i].transform.parent.position, damage);
					}
				}
				else if (colliderArray[i].tag == "Vehicle" && ServerSettings.pvp)
				{
					colliderArray[i].GetComponent<Vehicle>().damage(damage);
				}
			}
		}
		SpawnAnimals.attract(position + Vector3.up, 64f);
	}
}