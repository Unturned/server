using System;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public readonly static float COURSE = 2560f;
    public readonly static float WEIGHT = 0.6f;

	public static Sun tool;
	public static float time;
	public static float tick;
	public static float lastTick;
	public static float day;

	public Sun()
	{
	}

	public void Awake()
	{
		Sun.tool = this;
		base.InvokeRepeating("cycle", 0f, 0.25f);
		base.light.shadowBias = 0.1f;
	}

	public void cycle() {
        ServerSettings.offset = ServerSettings.offset + (Time.realtimeSinceStartup - Sun.lastTick);
		Sun.tick = Sun.tick + (Time.realtimeSinceStartup - Sun.lastTick);
		Sun.lastTick = Time.realtimeSinceStartup;
		Sun.time = (float)ServerSettings.time + Sun.tick;
		Sun.day = Sun.time % Sun.COURSE / Sun.COURSE;
	}

	public static string getTime()
	{
		float single = Sun.day + 0.25f;
		if (single > 1f)
		{
			single = single - 1f;
		}
		float single1 = single * 86400f;
		float single2 = single1 / 60f % 60f;
		float single3 = single1 / 3600f % 12f + 1f;
		if (single2 < 10f)
		{
			return string.Concat((int)single3, ":0", (int)single2);
		}
		return string.Concat((int)single3, ":", (int)single2);
	}

}