using System;
using UnityEngine;

public class Jack : Useable
{
	private float startedUse = Single.MaxValue;

	private GameObject target;

	private bool done;

	private static RaycastHit hit;

	public Jack()
	{
	}

	public override void equip()
	{
		Viewmodel.play("equip");
	}

	public override void startPrimary()
	{
		Physics.Raycast(Camera.main.transform.position + (Camera.main.transform.forward * -0.5f), Camera.main.transform.forward, out Jack.hit, 5f, -29365511);
		if (Jack.hit.collider != null && Jack.hit.collider.tag == "Vehicle" && Jack.hit.collider.GetComponent<Vehicle>().lastSpeed == 0)
		{
			this.target = Jack.hit.collider.gameObject;
			NetworkSounds.askSound(string.Concat("Sounds/Items/", ItemSounds.getSource(Equipment.id), "/use"), Camera.main.transform.position + (Camera.main.transform.forward * 0.5f), 0.5f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
			Equipment.busy = true;
			this.startedUse = Time.realtimeSinceStartup;
			Viewmodel.play("use");
		}
	}

	public override void tick()
	{
		if (Time.realtimeSinceStartup - this.startedUse > Viewmodel.model.animation["use"].length && !this.done)
		{
			this.done = true;
			if (this.target != null)
			{
				if (!Network.isServer)
				{
					base.networkView.RPC("useVehicle", RPCMode.Server, new object[] { this.target.networkView.viewID });
				}
				else
				{
					this.useVehicle(this.target.networkView.viewID);
				}
			}
			Equipment.dequip();
		}
	}

	[RPC]
	public void useVehicle(NetworkViewID id)
	{
		if (!base.GetComponent<Life>().dead)
		{
			GameObject gameObject = NetworkView.Find(id).gameObject;
			if (gameObject != null)
			{
				bool flag = true;
				for (int i = 0; i < (int)gameObject.GetComponent<Vehicle>().passengers.Length; i++)
				{
					if (gameObject.GetComponent<Vehicle>().passengers[i] != null)
					{
						flag = false;
					}
				}
				if (flag && gameObject.GetComponent<Vehicle>().lastSpeed == 0)
				{
					gameObject.rigidbody.AddForce(new Vector3(0f, 11f, 0f), ForceMode.Impulse);
					gameObject.rigidbody.AddTorque(new Vector3((float)UnityEngine.Random.Range(-11, 11), 0f, 0f), ForceMode.Impulse);
				}
			}
		}
	}
}