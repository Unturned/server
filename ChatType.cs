using System;

public class ChatType
{
	public ChatType()
	{
	}

	public static string getIcon(int type)
	{
		switch (type)
		{
			case 0:
			{
				return "Textures/Icons/global";
			}
			case 1:
			{
				return "Textures/Icons/local";
			}
			case 2:
			{
				return "Textures/Icons/clan";
			}
			case 3:
			{
				return "Textures/Icons/alert";
			}
		}
		return string.Empty;
	}
}