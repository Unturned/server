using System;
using UnityEngine;

public class Chart : Useable
{
	private GameObject marker;

	public Chart()
	{
	}

	public override void equip()
	{
		Viewmodel.play("equip");
		this.marker = Equipment.model.transform.FindChild("model").FindChild("player").gameObject;
		if (PlayerSettings.arm)
		{
			this.marker.transform.parent.localScale = new Vector3(1f, -1f, 1f);
		}
	}

	public void Update()
	{
		if (Player.model != null)
		{
			Transform vector3 = this.marker.transform;
			Vector3 vector31 = Player.model.transform.position;
			Vector3 vector32 = Player.model.transform.position;
			vector3.localPosition = new Vector3(vector31.z / 1024f * 0.3125f, vector32.x / 1024f * 0.3125f, 0.01f);
		}
	}
}