using System;
using UnityEngine;

public class Grenade : MonoBehaviour
{
	public Grenade()
	{
	}

	public void OnDestroy()
	{
		if (Network.isServer)
		{
			ExplosionTool.explode(base.transform.position, (float)ExplosiveStats.getRange(23007), ExplosiveStats.getDamage(23007));
			NetworkEffects.askEffect("Effects/grenade", base.transform.position + Vector3.up, Quaternion.Euler(-90f, 0f, 0f), -1f);
			NetworkEffects.askEffect("Effects/rubble", base.transform.position, Quaternion.Euler(-90f, 0f, 0f), -1f);
			NetworkSounds.askSound("Sounds/Projectiles/grenade", base.transform.position, 1f, UnityEngine.Random.Range(0.95f, 1.05f), 2f);
		}
	}

	public void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject, 1f);
	}
}