using System;
using UnityEngine;

public class Viewmodel : MonoBehaviour
{
	public static GameObject view;

	public static GameObject model;

	private static string playID;

	private static float startedPlay;

	private static float sway_x;

	private static float sway_y;

	public static float swayPitch;

	public static float swayRoll;

	public static float offset_x;

	public static float offset_y;

	public static float offset_z;

	static Viewmodel()
	{
		Viewmodel.playID = string.Empty;
		Viewmodel.startedPlay = Single.MaxValue;
	}

	public Viewmodel()
	{
	}

	public static void play(string id)
	{
		if (id != Viewmodel.playID)
		{
			Viewmodel.playID = id;
			Viewmodel.startedPlay = Time.realtimeSinceStartup;
		}
	}

	public void Start()
	{
		Viewmodel.view = base.transform.parent.gameObject;
		Viewmodel.model = base.gameObject;
		base.transform.localScale = new Vector3(1f, 1f, (float)((!PlayerSettings.arm ? 1 : -1)));
		Viewmodel.playID = string.Empty;
		Viewmodel.startedPlay = Single.MaxValue;
		Viewmodel.sway_x = 0f;
		Viewmodel.sway_y = 0f;
		Viewmodel.swayPitch = 0f;
		Viewmodel.swayRoll = 0f;
		Viewmodel.offset_x = 0f;
		Viewmodel.offset_y = 0f;
		Viewmodel.offset_z = 0f;
	}

	public void Update()
	{
		if (!Network.isServer)
		{
			float height;
			float single;
			
			if (Movement.isDriving) {
				// Driving
				base.animation.CrossFade("drive", Animifier.FADE);
			} else if (Player.life.dead) {
				// Death state
				base.animation.CrossFade("standIdle", Animifier.FADE);
			} else if (Viewmodel.playID != string.Empty) {
				if (!(base.animation[Viewmodel.playID] != null) || Time.realtimeSinceStartup - Viewmodel.startedPlay >= base.animation[Viewmodel.playID].length) {
					Viewmodel.playID = string.Empty;
				} else {
					base.animation.Play(Viewmodel.playID);
				}
			} else if (Equipment.model == null) {
				if (Movement.isMoving) {
					if (Movement.isClimbing) {
						base.animation.CrossFade("ladderMove", Animifier.FADE);
					} else if (Movement.isSwimming) {
						base.animation.CrossFade("swimMove", Animifier.FADE);
					} else if (Stance.state != 2) {
						base.animation.CrossFade("standIdle", Animifier.FADE);
					} else {
						base.animation.CrossFade("proneMove", Animifier.FADE);
					}
				} else if (Movement.isClimbing) {
					base.animation.CrossFade("ladderIdle", Animifier.FADE);
				} else if (Movement.isSwimming) {
					base.animation.CrossFade("swimIdle", Animifier.FADE);
				} else if (Stance.state != 2 || Player.life.dead) {
					base.animation.CrossFade("standIdle", Animifier.FADE);
				} else {
					base.animation.CrossFade("proneIdle", Animifier.FADE);
				}
			}
			
			if (!Movement.isMoving) {
				Viewmodel.sway_x = 0f;
				Viewmodel.sway_y = 0f;
			} else if (Gun.aiming) {
				Viewmodel.sway_x = Mathf.Sin(Time.realtimeSinceStartup * 5f) * 0.001f;
				Viewmodel.sway_y = -Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup * 5f) * 0.002f);
			} else if (Stance.state == 0) {
				if (!Movement.isSprinting) {
					Viewmodel.sway_x = Mathf.Sin(Time.realtimeSinceStartup * 7f) * 0.05f;
					Viewmodel.sway_y = -Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup * 7f) * 0.1f);
				} else {
					Viewmodel.sway_x = Mathf.Sin(Time.realtimeSinceStartup * 9f) * 0.1f;
					Viewmodel.sway_y = -Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup * 9f) * 0.2f);
				}
			} else if (Stance.state != 1) {
				Viewmodel.sway_x = Mathf.Sin(Time.realtimeSinceStartup * 3f) * 0.0125f;
				Viewmodel.sway_y = -Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup * 3f) * 0.025f);
			} else {
				Viewmodel.sway_x = Mathf.Sin(Time.realtimeSinceStartup * 5f) * 0.025f;
				Viewmodel.sway_y = -Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup * 5f) * 0.05f);
			}
			
			if (!Movement.isDriving) {
				float single1 = Viewmodel.swayPitch;
				single = (!Screen.lockCursor ? 0f : Movement.input_y * (!Gun.aiming ? 5f : 0.05f));
				if (!Movement.isGrounded) {
					height = 10;
				} else {
					height = 0;
				}
				
				Viewmodel.swayPitch = Mathf.Lerp(single1, single + (float)height, 4f * Time.deltaTime);
				Viewmodel.swayRoll = Mathf.Lerp(Viewmodel.swayRoll, (!Screen.lockCursor ? 0f : Movement.input_x * (!Gun.aiming ? 5f : 0.05f)), 4f * Time.deltaTime);
				Viewmodel.model.transform.localPosition = Vector3.Lerp(Viewmodel.model.transform.localPosition, new Vector3(Viewmodel.sway_x + Viewmodel.offset_x, Viewmodel.sway_y + Viewmodel.offset_y + (!Movement.isGrounded ? 0.05f : 0f), Viewmodel.offset_z), 16f * Time.deltaTime);
				Viewmodel.model.transform.localRotation = Quaternion.Euler(Viewmodel.swayRoll, 90f, Viewmodel.swayPitch);
			} else {
				Viewmodel.swayRoll = Mathf.Lerp(Viewmodel.swayRoll, Movement.input_x * 15f, 4f * Time.deltaTime);
				Viewmodel.model.transform.localPosition = new Vector3(0f, -0.25f - Mathf.Abs(Look.rear) * 2f, Look.rear);
				Viewmodel.model.transform.localRotation = Quaternion.Euler(Viewmodel.swayRoll, -Look.yaw + 90f, -Look.pitch);
			}
		}
	}

	public static void wear()
	{
		Texture2D texture2D = new Texture2D(64, 64, TextureFormat.RGBA32, false);
		Texture2D texture2D1 = null;
		if (Player.clothes.shirt != -1)
		{
			texture2D1 = (Texture2D)Resources.Load(string.Concat("Textures/Shirts/", Player.clothes.shirt));
		}
		for (int i = 0; i < 64; i++)
		{
			for (int j = 0; j < 64; j++)
			{
				if (!(texture2D1 != null) || !(texture2D1.GetPixel(i, j) != Color.white))
				{
					texture2D.SetPixel(i, j, SkinColor.getColor(Player.clothes.skinColor));
				}
				else
				{
					texture2D.SetPixel(i, j, texture2D1.GetPixel(i, j));
				}
			}
		}
		texture2D.filterMode = FilterMode.Point;
		texture2D.name = "texture";
		texture2D.Apply();
		Viewmodel.model.transform.FindChild("model").renderer.material.SetTexture("_MainTex", texture2D);
	}
}