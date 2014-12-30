using System;

public class RudeFilter
{
	private readonly static string[] blacklist;

	private readonly static string[] whitelist;

	static RudeFilter()
	{
		RudeFilter.blacklist = new string[] { "shit", "fuck", "damn", "ass", "fag", "nigger", "bitch", "cock", "sex", "cunt", "whore", "testicle", "piss", "vagina", "penis", "prostitute", "stripper" };
		RudeFilter.whitelist = new string[] { "poop", "fruitcake", "gosh darnit", "donkeybumb", "stick", "person", "doge", "chicken", "reproduction", "private", "person", "private", "urine", "private", "private", "person", "person" };
	}

	public RudeFilter()
	{
	}

	public static string filter(string text)
	{
		string lower = text.ToLower();
		for (int i = 0; i < (int)RudeFilter.blacklist.Length; i++)
		{
			while (lower.Contains(RudeFilter.blacklist[i]))
			{
				int num = lower.IndexOf(RudeFilter.blacklist[i]);
				if (num != -1)
				{
					lower = string.Concat(lower.Substring(0, num), RudeFilter.whitelist[i], lower.Substring(num + RudeFilter.blacklist[i].Length, lower.Length - num - RudeFilter.blacklist[i].Length));
					text = string.Concat(text.Substring(0, num), RudeFilter.whitelist[i], text.Substring(num + RudeFilter.blacklist[i].Length, text.Length - num - RudeFilter.blacklist[i].Length));
				}
				else
				{
					break;
				}
			}
		}
		return text;
	}
}