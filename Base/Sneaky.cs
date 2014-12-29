using System;

public class Sneaky
{
	private static int SNEAKY_CHAR_CONST = 8192;
	
	private static string buffer;

	private static char[] letters;

	public Sneaky()
	{
	}

	public static int expose(int sneaked)
	{
		return (sneaked - 10) / -4;
	}

	public static float expose(float sneaked)
	{
		return (sneaked - 5f) / -2f;
	}

	public static string expose(string sneaked)
	{
		Sneaky.letters = sneaked.ToCharArray();
		Sneaky.buffer = string.Empty;
		for (int i = (int)Sneaky.letters.Length - 1; i >= 0; i--)
		{
			Sneaky.buffer = string.Concat(Sneaky.buffer, (char)(Sneaky.letters[i] - SNEAKY_CHAR_CONST));
		}
		return Sneaky.buffer;
	}

	public static int sneak(int exposed)
	{
		return exposed * -4 + 10;
	}

	public static float sneak(float exposed)
	{
		return exposed * -2f + 5f;
	}

	public static string sneak(string exposed)
	{
		Sneaky.letters = exposed.ToCharArray();
		Sneaky.buffer = string.Empty;
		for (int i = (int)Sneaky.letters.Length - 1; i >= 0; i--)
		{
			Sneaky.buffer = string.Concat(Sneaky.buffer, (char)(Sneaky.letters[i] + SNEAKY_CHAR_CONST));
		}
		return Sneaky.buffer;
	}
}