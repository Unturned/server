using System;
using UnityEngine;

public class SleekWindow : SleekFrame
{
	public static int display;

	public static float fps;

	public static int frames;

	public static float rate;

	static SleekWindow()
	{
		SleekWindow.rate = 0.5f;
	}

	public SleekWindow()
	{
	}

	public override void drawFrame()
	{
		base.drawChildren();
		Screen.showCursor = false;
		if (!Screen.lockCursor)
		{
			float single = Input.mousePosition.x;
			float single1 = (float)Screen.height;
			Vector3 vector3 = Input.mousePosition;
			SleekRender.image(new Rect(single, single1 - vector3.y, 16f, 16f), Images.cursor, Color.white);
			if (GUI.tooltip != string.Empty)
			{
				if (Input.mousePosition.x >= (float)(Screen.width - 310))
				{
					GUI.skin.label.alignment = TextAnchor.MiddleRight;
					Vector3 vector31 = Input.mousePosition;
					float single2 = (float)Screen.height;
					Vector3 vector32 = Input.mousePosition;
					SleekRender.label(new Rect(vector31.x - 310f, single2 - vector32.y - 8f, 300f, 32f), GUI.tooltip, string.Empty, Color.white);
				}
				else
				{
					GUI.skin.label.alignment = TextAnchor.MiddleLeft;
					Vector3 vector33 = Input.mousePosition;
					float single3 = (float)Screen.height;
					Vector3 vector34 = Input.mousePosition;
					SleekRender.label(new Rect(vector33.x + 26f, single3 - vector34.y - 8f, 300f, 32f), GUI.tooltip, string.Empty, Color.white);
				}
				GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			}
		}
		if (GameSettings.fps)
		{
			SleekWindow.rate = SleekWindow.rate - Time.deltaTime;
			SleekWindow.fps = SleekWindow.fps + Time.timeScale / Time.deltaTime;
			SleekWindow.frames = SleekWindow.frames + 1;
			if (SleekWindow.rate < 0f)
			{
				SleekWindow.display = (int)(SleekWindow.fps / (float)SleekWindow.frames);
				SleekWindow.rate = 0.5f;
				SleekWindow.fps = 0f;
				SleekWindow.frames = 0;
			}
			SleekRender.label(new Rect((float)(Screen.width - 100), 0f, 100f, 20f), string.Concat("FPS: ", Mathf.FloorToInt((float)SleekWindow.display)), string.Empty, Color.green);
		}
	}
}