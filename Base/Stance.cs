using System;
using UnityEngine;

public class Stance : MonoBehaviour
{
	public static int state;

	public static int range;

	public static float aim;

	public static float cross;

	static Stance()
	{
	}

	public Stance()
	{
	}

	public static void change(int setState)
	{
		if (setState != Stance.state)
		{
			Stance.state = setState;
			if (Stance.state == 0)
			{
				Movement.control.height = 1.95f;
				Movement.control.center = new Vector3(0f, 0.975f, 0f);
			}
			else if (Stance.state != 1)
			{
				Movement.control.height = 0.65f;
				Movement.control.center = new Vector3(0f, 0.325f, 0f);
			}
			else
			{
				Movement.control.height = 1.1f;
				Movement.control.center = new Vector3(0f, 0.55f, 0f);
			}
			Transform vector3 = Player.model.transform;
			vector3.position = vector3.position + new Vector3(0f, 0.01f, 0f);
		}
	}

	public void Start()
	{
		Stance.change(0);
	}

	public void Update()
	{
		if (Movement.isDriving)
		{
			Stance.range = 48;
			Stance.aim = 10f;
		}
		else if (Movement.isSprinting)
		{
			Stance.range = (int)(32f * (1f - Player.skills.sneakybeaky() / 2f));
			Stance.aim = 2f;
		}
		else if (Movement.isMoving)
		{
			if (Stance.state == 0)
			{
				Stance.range = (int)(24f * (1f - Player.skills.sneakybeaky() / 2f));
				Stance.aim = 1.5f;
			}
			else if (Stance.state != 1)
			{
				Stance.range = (int)(8f * (1f - Player.skills.sneakybeaky() / 2f));
				Stance.aim = 1f;
			}
			else
			{
				Stance.range = (int)(16f * (1f - Player.skills.sneakybeaky() / 2f));
				Stance.aim = 1.25f;
			}
		}
		else if (Stance.state == 0)
		{
			Stance.range = (int)(16f * (1f - Player.skills.sneakybeaky() / 2f));
			Stance.aim = 1f;
		}
		else if (Stance.state != 1)
		{
			Stance.range = (int)(4f * (1f - Player.skills.sneakybeaky() / 2f));
			Stance.aim = 0.5f;
		}
		else
		{
			Stance.range = (int)(8f * (1f - Player.skills.sneakybeaky() / 2f));
			Stance.aim = 0.75f;
		}
		Stance.cross = Mathf.Lerp(Stance.cross, Stance.aim, 4f * Time.deltaTime);
		if (base.transform.position.y >= Ocean.level && !Movement.isClimbing)
		{
			if (Movement.isGrounded && !Movement.isDriving && Screen.lockCursor)
			{
				if (!Input.GetKeyDown(InputSettings.proneKey))
				{
					if (!Input.GetKey(InputSettings.proneKey) && !InputSettings.proneToggle && Stance.state == 2 && (int)Physics.OverlapSphere(Player.model.transform.position + new Vector3(0f, 1.7f, 0f), 0.3f, -225580823).Length == 0)
					{
						Stance.change(0);
					}
				}
				else if (!InputSettings.proneToggle || Stance.state != 2)
				{
					Stance.change(2);
				}
				else if ((int)Physics.OverlapSphere(Player.model.transform.position + new Vector3(0f, 1.7f, 0f), 0.3f, -225580823).Length == 0)
				{
					Stance.change(0);
				}
				if (!Input.GetKeyDown(InputSettings.crouchKey))
				{
					if (!Input.GetKey(InputSettings.crouchKey) && !InputSettings.crouchToggle && Stance.state == 1 && (int)Physics.OverlapSphere(Player.model.transform.position + new Vector3(0f, 1.7f, 0f), 0.3f, -225580823).Length == 0)
					{
						Stance.change(0);
					}
				}
				else if (Stance.state == 1 && InputSettings.crouchToggle)
				{
					if ((int)Physics.OverlapSphere(Player.model.transform.position + new Vector3(0f, 1.7f, 0f), 0.3f, -225580823).Length == 0)
					{
						Stance.change(0);
					}
				}
				else if (Stance.state != 2)
				{
					Stance.change(1);
				}
				else if ((int)Physics.OverlapSphere(Player.model.transform.position + new Vector3(0f, 0.7f, 0f), 0.3f, -225580823).Length == 0)
				{
					Stance.change(1);
				}
			}
		}
		else if (Stance.state != 0)
		{
			Stance.change(0);
		}
	}
}