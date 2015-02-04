using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public static int STAND_SPEED;

	public static float SPRINT_MULTIPLIER;

	public static float CROUCH_MULTIPLIER;

	public static float PRONE_MULTIPLIER;

	public static float SWIM_MULTIPLIER;

	public static float AIM_MULTIPLIER;

	public static float BONES_MULTIPLIER;

	public static float JUMP_SPEED;

	public static bool isGrounded;

	public static bool isMoving;

	public static bool isSprinting;

	public static bool isSwimming;

	public static bool isClimbing;

	public static bool isDriving;

	public static float speed;

	public static CharacterController control;

	public static Vehicle vehicle;

	public static Transform seat;

	private static float move_x;

	private static float move_y;

	private static float move_z;

	public static float input_x;

	public static float input_y;

	private static float lastPant;

	private static float lastHeight;

	private static bool wasGrounded;

	static Movement()
	{
		Movement.STAND_SPEED = 4;
		Movement.SPRINT_MULTIPLIER = 1.4f;
		Movement.CROUCH_MULTIPLIER = 0.5f;
		Movement.PRONE_MULTIPLIER = 0.25f;
		Movement.SWIM_MULTIPLIER = 0.6f;
		Movement.AIM_MULTIPLIER = 0.5f;
		Movement.BONES_MULTIPLIER = 0.9f;
		Movement.JUMP_SPEED = 6f;
	}

	public Movement()
	{
	}

	public void Start()
	{
		Gun.aiming = false;
		Gun.dualrender = false;
		Movement.isGrounded = false;
		Movement.isMoving = false;
		Movement.isSprinting = false;
		Movement.isSwimming = false;
		Movement.isClimbing = false;
		Movement.isDriving = false;
		Movement.speed = 0f;
		Movement.control = base.transform.parent.gameObject.AddComponent<CharacterController>();
		Movement.control.height = 2f;
		Movement.control.center = Vector3.up;
		Movement.control.radius = 0.3f;
		Movement.control.slopeLimit = 70f;
		Movement.control.stepOffset = 0.4f;
		Movement.vehicle = null;
		Movement.seat = null;
		Movement.move_x = 0f;
		Movement.move_y = 0f;
		Movement.move_z = 0f;
		Movement.input_x = 0f;
		Movement.input_y = 0f;
		Movement.lastHeight = base.transform.position.y;
	}

	public void Update()
	{
		Console.WriteLine("Movement Update Disabled!!");
		/*RaycastHit raycastHit;
		Movement.input_x = Mathf.Lerp(Movement.input_x, InputSettings.getX(), 8f * Time.deltaTime);
		Movement.input_y = Mathf.Lerp(Movement.input_y, InputSettings.getY(), 8f * Time.deltaTime);
		if (InputSettings.getX() == 0f && ((double)Movement.input_x > -0.01 || (double)Movement.input_x < 0.01))
		{
			Movement.input_x = 0f;
		}
		if (InputSettings.getY() == 0f && ((double)Movement.input_y > -0.01 || (double)Movement.input_y < 0.01))
		{
			Movement.input_y = 0f;
		}
		if ((double)(Time.realtimeSinceStartup - Player.spawned) > 0.5)
		{
			if (Movement.vehicle == null)
			{
				Vector3 vector3 = base.transform.position;
				Movement.isSwimming = vector3.y < Ocean.level - 1.5f;
				if (Stance.state != 0)
				{
					Movement.isClimbing = false;
				}
				else
				{
					Physics.Raycast(base.transform.position + new Vector3(0f, 0.5f, 0f), base.transform.forward, out raycastHit, 0.5f, 1048576);
					if (raycastHit.collider == null)
					{
						Movement.isClimbing = false;
					}
					else
					{
						Movement.isClimbing = true;
					}
				}
				Movement.move_x = Movement.input_x;
				Movement.isGrounded = Movement.control.isGrounded;
				float moveY = Movement.move_y;
				Vector3 vector31 = Physics.gravity;
				Movement.move_y = moveY + vector31.y * Time.deltaTime * 2f;
				if (Movement.move_y < Physics.gravity.y)
				{
					Movement.move_y = Physics.gravity.y;
				}
				if (Movement.isGrounded || Movement.isClimbing)
				{
					if (!Movement.wasGrounded)
					{
						if (base.transform.position.y < Ocean.level)
						{
							NetworkSounds.askSound("Sounds/Footsteps/submerge", base.transform.position, 1f, UnityEngine.Random.Range(0.9f, 1.1f), 1f);
						}
						else if (base.transform.position.y - Movement.lastHeight < -6f)
						{
							Player.life.sendBones();
						}
					}
					Movement.wasGrounded = true;
					Movement.lastHeight = base.transform.position.y;
				}
				else
				{
					Movement.wasGrounded = false;
				}
				if (Movement.isGrounded && !Player.life.bones && Footsteps.ground != null && !Movement.isSwimming && Input.GetKeyDown(InputSettings.jumpKey) && !Player.life.dead && Stance.state == 0 && Screen.lockCursor)
				{
					if (Player.life.stamina < 10 - (int)(Player.skills.endurance() * 10f))
					{
						//HUDGame.openError("Sorry: Not Enough Energy", "Textures/Icons/error");
					}
					else
					{
						Player.life.exhaust(10 - (int)(Player.skills.endurance() * 10f));
						Movement.move_y = Movement.JUMP_SPEED * Player.inventory.speed * Footsteps.hit.normal.y;
					}
				}
				if (Footsteps.ground != null)
				{
					float moveX = Movement.move_x;
					Vector3 vector32 = Footsteps.hit.normal;
					Movement.move_x = moveX * (0.25f + vector32.y * 0.75f);
					float moveZ = Movement.move_z;
					Vector3 vector33 = Footsteps.hit.normal;
					Movement.move_z = moveZ * (0.25f + vector33.y * 0.75f);
					Vector3 vector34 = Footsteps.hit.normal;
					Movement.speed = 0.25f + vector34.y * 0.75f;
				}
				Movement.move_z = Movement.input_y;
				Movement.isMoving = ((Movement.move_x != 0f || Movement.move_z != 0f) && !Player.life.dead ? Screen.lockCursor : false);
				Movement.move_x = Movement.move_x * (float)Movement.STAND_SPEED;
				Movement.move_z = Movement.move_z * (float)Movement.STAND_SPEED;
				Movement.speed = (float)Movement.STAND_SPEED;
				Movement.move_x = Movement.move_x * Player.inventory.speed;
				Movement.move_z = Movement.move_z * Player.inventory.speed;
				Movement.speed = Movement.speed * Player.inventory.speed;
				if (Player.life.bones)
				{
					Movement.move_x = Movement.move_x * Movement.BONES_MULTIPLIER;
					Movement.move_z = Movement.move_z * Movement.BONES_MULTIPLIER;
					Movement.speed = Movement.speed * Movement.BONES_MULTIPLIER;
				}
				if (Gun.aiming)
				{
					Movement.move_x = Movement.move_x * Movement.AIM_MULTIPLIER;
					Movement.move_z = Movement.move_z * Movement.AIM_MULTIPLIER;
					Movement.speed = Movement.speed * Movement.AIM_MULTIPLIER;
				}
				if (Footsteps.ground != null && Footsteps.ground.name == "ground")
				{
					Movement.move_x = Movement.move_x * Footsteps.hit.normal.y;
					Movement.move_z = Movement.move_z * Footsteps.hit.normal.y;
					Movement.speed = Movement.speed * Footsteps.hit.normal.y;
				}
				if (!Screen.lockCursor)
				{
					Movement.move_x = 0f;
					Movement.move_z = 0f;
				}
				Movement.isSprinting = (!Movement.isMoving || Stance.state != 0 || !Input.GetKey(InputSettings.sprintKey) || Player.life.stamina <= 0 ? false : !Player.life.bones);
				if (Movement.isSprinting && (double)(Time.realtimeSinceStartup - Movement.lastPant) > 0.1 && (double)(Time.realtimeSinceStartup - Movement.lastPant) > 0.1 + (double)(Player.skills.endurance() * 0.4f))
				{
					Movement.lastPant = Time.realtimeSinceStartup;
					Player.life.exhaust(1);
				}
				else if (Movement.isSprinting)
				{
					Movement.move_x = Movement.move_x * Movement.SPRINT_MULTIPLIER;
					Movement.move_z = Movement.move_z * Movement.SPRINT_MULTIPLIER;
					Movement.speed = Movement.speed * Movement.SPRINT_MULTIPLIER;
				}
				if (Movement.isClimbing)
				{
					Movement.move_x = 0f;
					Movement.move_y = Movement.move_z;
					Movement.move_z = 0f;
					Movement.speed = Mathf.Abs(Movement.move_y);
				}
				else if (Movement.isSwimming)
				{
					Movement.move_x = Movement.move_x * Movement.SWIM_MULTIPLIER;
					Movement.move_z = Movement.move_z * Movement.SWIM_MULTIPLIER;
					Movement.speed = Movement.speed * Movement.SWIM_MULTIPLIER;
				}
				else if (Stance.state == 1)
				{
					Movement.move_x = Movement.move_x * Movement.CROUCH_MULTIPLIER;
					Movement.move_z = Movement.move_z * Movement.CROUCH_MULTIPLIER;
					Movement.speed = Movement.speed * Movement.CROUCH_MULTIPLIER;
				}
				else if (Stance.state == 2)
				{
					Movement.move_x = Movement.move_x * Movement.PRONE_MULTIPLIER;
					Movement.move_z = Movement.move_z * Movement.PRONE_MULTIPLIER;
					Movement.speed = Movement.speed * Movement.PRONE_MULTIPLIER;
				}
				Movement.control.Move((base.transform.parent.rotation * new Vector3(Movement.move_x, Movement.move_y, Movement.move_z)) * Time.deltaTime);
			}
			else
			{
				Movement.isGrounded = true;
				Movement.isMoving = false;
				Movement.isSprinting = false;
				Movement.isSwimming = false;
				Movement.isClimbing = false;
				if (Movement.isDriving)
				{
					Movement.move_x = Movement.input_x;
					Movement.move_z = Movement.input_y;
					Movement.isMoving = (Movement.move_x != 0f ? true : Movement.move_z != 0f);
					if (!Input.GetKey(InputSettings.jumpKey))
					{
						Movement.vehicle.drive(Movement.move_x, Movement.move_z);
					}
					else
					{
						Movement.vehicle.drive(Movement.move_x, -1000f);
					}
				}
				Movement.wasGrounded = true;
				Movement.lastHeight = base.transform.position.y;
				Movement.vehicle.control();
			}
		}
		*/
	}
}