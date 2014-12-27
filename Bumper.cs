using System;
using UnityEngine;

public class Bumper : MonoBehaviour
{
	public Bumper()
	{
	}

	public void Awake()
	{
		if (!Network.isServer)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (base.transform.parent.GetComponent<Vehicle>().lastSpeed > 5 && base.transform.parent.GetComponent<Vehicle>().passengers[0] != null)
		{
			if ((other.tag == "Enemy" || other.tag == "Player") && ServerSettings.pvp)
			{
				GameObject gameObject = null;
				gameObject = (other.tag != "Enemy" ? other.gameObject : OwnerFinder.getOwner(other.gameObject));
				NetworkUser userFromPlayer = NetworkUserList.getUserFromPlayer(base.transform.parent.GetComponent<Vehicle>().passengers[0].player);
				if (userFromPlayer != null && !gameObject.GetComponent<Life>().dead && (userFromPlayer.friend == string.Empty || userFromPlayer.friend != gameObject.GetComponent<Player>().owner.friend))
				{
					if (gameObject.GetComponent<Player>().owner.reputation >= 0)
					{
						NetworkHandler.offsetReputation(userFromPlayer.player, -1);
					}
					else
					{
						NetworkHandler.offsetReputation(userFromPlayer.player, 1);
					}
					gameObject.GetComponent<Life>().damage(1000, string.Concat("You were run over by ", userFromPlayer.name, "!"));
					NetworkSounds.askSound("Sounds/Impacts/flesh", gameObject.transform.position + Vector3.up, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
					NetworkEffects.askEffect("Effects/flesh", gameObject.transform.position + Vector3.up, Quaternion.identity, -1f);
				}
			}
			else if (other.tag == "Animal")
			{
				GameObject owner = OwnerFinder.getOwner(other.gameObject);
				owner.GetComponent<AI>().damage(1000);
				NetworkSounds.askSound("Sounds/Impacts/flesh", owner.transform.position + Vector3.up, 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 0.25f);
				NetworkEffects.askEffect("Effects/flesh", owner.transform.position + Vector3.up, Quaternion.identity, -1f);
			}
		}
	}
}