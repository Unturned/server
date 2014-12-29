using System;
using UnityEngine;

public class Images
{
	public readonly static Texture2D pixel;

	public readonly static Texture2D binoculars;

	public static Texture2D cursor;

	static Images()
	{
		Images.pixel = (Texture2D)Resources.Load("Textures/Sleek/pixel");
		Images.binoculars = (Texture2D)Resources.Load("Textures/Sleek/binoculars");
		Images.cursor = (Texture2D)Resources.Load("Textures/Sleek/cursorFree");
	}

	public Images()
	{
	}

	public static void swapPaid()
	{
		Images.cursor = (Texture2D)Resources.Load("Textures/Sleek/cursor");
	}
}