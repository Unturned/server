using System;
using UnityEngine;

public class SleekRender
{
	public static GUISkin SKIN;

	public readonly static Color OUTLINE;

	private static bool state;

	private static float slide;

	private static string entry;

	private static string pass;

	static SleekRender()
	{
		SleekRender.SKIN = (GUISkin)Resources.Load("Styles/Sleek/sleekFree");
		SleekRender.OUTLINE = new Color(0f, 0f, 0f, 0.5f);
		SleekRender.state = false;
		SleekRender.slide = 0f;
		SleekRender.entry = string.Empty;
		SleekRender.pass = string.Empty;
	}

	public SleekRender()
	{
	}

	public static void box(Rect area, string text, string tooltip, Color color, Color paint)
	{
		GUI.skin = SleekRender.SKIN;
		GUI.color = color;
		GUI.Box(area, string.Empty);
		SleekRender.label(area, text, tooltip, paint);
		GUI.color = Color.white;
	}

	public static bool button(Rect area, string text, string tooltip, Color color, Color paint)
	{
		GUI.skin = SleekRender.SKIN;
		GUI.color = color;
		SleekRender.state = GUI.Button(area, string.Empty);
		SleekRender.label(area, text, tooltip, paint);
		if (SleekRender.state)
		{
			Camera.main.audio.PlayOneShot((AudioClip)Resources.Load("Sounds/Sleek/button"), 0.05f);
		}
		GUI.color = Color.white;
		return SleekRender.state;
	}

	public static string doc(Rect area, string text, string tooltip, Color color, Color paint)
	{
		GUI.skin = SleekRender.SKIN;
		GUI.color = color;
		SleekRender.entry = GUI.TextArea(area, text);
		SleekRender.label(area, text, tooltip, paint);
		GUI.color = Color.white;
		return SleekRender.entry;
	}

	public static string field(Rect area, string text, string tooltip, Color color, Color paint)
	{
		GUI.skin = SleekRender.SKIN;
		GUI.color = color;
		GUI.SetNextControlName("field");
		SleekRender.entry = GUI.TextField(area, text);
		SleekRender.label(area, text, tooltip, paint);
		GUI.color = Color.white;
		return SleekRender.entry;
	}

	public static string field(Rect area, string text, string tooltip, char replace, Color color, Color paint)
	{
		GUI.skin = SleekRender.SKIN;
		SleekRender.pass = string.Empty;
		for (int i = 0; i < text.Length; i++)
		{
			SleekRender.pass = string.Concat(SleekRender.pass, replace);
		}
		GUI.color = color;
		GUI.SetNextControlName("field");
		SleekRender.entry = GUI.PasswordField(area, text, replace);
		SleekRender.label(area, SleekRender.pass, tooltip, paint);
		GUI.color = Color.white;
		return SleekRender.entry;
	}

	public static float horizontalBar(Rect area, float state, float size, Color color)
	{
		GUI.skin = SleekRender.SKIN;
		GUI.color = color;
		SleekRender.slide = GUI.HorizontalScrollbar(area, state, size, 0f, 1f + size);
		GUI.color = Color.white;
		return SleekRender.slide;
	}

	public static void image(Rect area, Texture texture, Color color)
	{
		GUI.skin = SleekRender.SKIN;
		if (texture != null)
		{
			GUI.color = color;
			GUI.DrawTexture(area, texture, ScaleMode.StretchToFill);
			GUI.color = Color.white;
		}
	}

	public static void label(Rect area, string text, string tooltip, Color color)
	{
		GUI.skin = SleekRender.SKIN;
		GUI.color = SleekRender.OUTLINE;
		GUI.Label(new Rect(area.x - 1f, area.y, area.width, area.height), text);
		GUI.Label(new Rect(area.x + 1f, area.y, area.width, area.height), text);
		GUI.Label(new Rect(area.x, area.y - 1f, area.width, area.height), text);
		GUI.Label(new Rect(area.x, area.y + 1f, area.width, area.height), text);
		GUI.color = color;
		GUI.Label(area, new GUIContent(text, tooltip));
		GUI.color = Color.white;
	}

	public static bool radio(Rect area, bool state, string tooltip, Color color)
	{
		GUI.skin = SleekRender.SKIN;
		GUI.color = color;
		state = GUI.Toggle(area, state, new GUIContent(string.Empty, tooltip));
		GUI.color = Color.white;
		return state;
	}

	public static void swapPaid()
	{
		SleekRender.SKIN = (GUISkin)Resources.Load("Styles/Sleek/sleek");
	}

	public static float verticalBar(Rect area, float state, float size, Color color)
	{
		GUI.skin = SleekRender.SKIN;
		GUI.color = color;
		SleekRender.slide = GUI.VerticalScrollbar(area, state, size, 0f, 1f + size);
		GUI.color = Color.white;
		return SleekRender.slide;
	}
}