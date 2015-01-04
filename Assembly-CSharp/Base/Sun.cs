using System;
using UnityEngine;

public class Sun : MonoBehaviour
{
	public readonly static float COURSE;

	public readonly static float WEIGHT;

	public static Sun tool;

	public static float time;

	public static float tick;

	public static float lastTick;

	public static float day;

	public static int vision;

	public float azimuth;

	public Color skyDawn;

	public Color skyMidday;

	public Color skyDusk;

	public Color skyMidnight;

	public Color ambientDawn;

	public Color ambientMidday;

	public Color ambientDusk;

	public Color ambientMidnight;

	public Color colorDawn;

	public Color colorMidday;

	public Color colorDusk;

	public float intensityDawn;

	public float intensityMidday;

	public float intensityDusk;

	public float rangeDawn;

	public float rangeMidday;

	public float rangeDusk;

	public float rangeMidnight;

	public string dayAmbience;

	public string nightAmbience;

	private string ambience = string.Empty;

	private float began = Single.MaxValue;

	private int spook = -1;

	private float lastSpook = Single.MinValue;

	private Material sky;

	private Color starsMidday = new Color(1f, 1f, 1f, 0f);

	private Color starsMidnight = new Color(1f, 1f, 1f, 1f);

	static Sun()
	{
		Sun.COURSE = 2560f;
		Sun.WEIGHT = 0.6f;
		Sun.vision = -1;
	}

	public Sun()
	{
	}

	public void Awake()
	{
		Sun.tool = this;
		this.began = Time.realtimeSinceStartup;
		Sun.vision = -1;
		this.sky = base.transform.parent.FindChild("sky").renderer.material;
		base.InvokeRepeating("cycle", 0f, 0.25f);
		base.light.shadowBias = 0.1f;
	}

	public void cycle() {
        // TODO: write cycle stop to config
        return;

		ServerSettings.offset = ServerSettings.offset + (Time.realtimeSinceStartup - Sun.lastTick);
		Sun.tick = Sun.tick + (Time.realtimeSinceStartup - Sun.lastTick);
		Sun.lastTick = Time.realtimeSinceStartup;
		Sun.time = (float)ServerSettings.time + Sun.tick;
		Sun.day = Sun.time % Sun.COURSE / Sun.COURSE;
		if (Sun.vision == 0)
		{
			RenderSettings.ambientLight = Colors.NVG_0;
		}
		else if (Sun.vision == 1)
		{
			RenderSettings.ambientLight = Colors.NVG_1;
		}
		else if ((double)Sun.day < 0.05)
		{
			RenderSettings.ambientLight = Color.Lerp(this.ambientDawn, this.ambientMidday, Sun.day / 0.05f);
		}
		else if (Sun.day < Sun.WEIGHT - 0.05f)
		{
			RenderSettings.ambientLight = this.ambientMidday;
		}
		else if (Sun.day < Sun.WEIGHT)
		{
			RenderSettings.ambientLight = Color.Lerp(this.ambientMidday, this.ambientDusk, (Sun.day - Sun.WEIGHT + 0.05f) / 0.05f);
		}
		else if (Sun.day < Sun.WEIGHT + 0.05f)
		{
			RenderSettings.ambientLight = Color.Lerp(this.ambientDusk, this.ambientMidnight, (Sun.day - Sun.WEIGHT) / 0.05f);
		}
		else if ((double)Sun.day >= 0.95)
		{
			RenderSettings.ambientLight = Color.Lerp(this.ambientMidnight, this.ambientDawn, (Sun.day - 0.95f) / 0.05f);
		}
		else
		{
			RenderSettings.ambientLight = this.ambientMidnight;
		}
		if (Sun.vision == 1)
		{
			base.light.color = Color.black;
			base.light.intensity = 0f;
		}
		else if ((double)Sun.day < 0.05)
		{
			base.light.color = Color.Lerp(this.colorDawn, this.colorMidday, Sun.day / 0.05f);
			base.light.intensity = Mathf.Lerp(this.intensityDawn, this.intensityMidday, Sun.day / 0.05f);
		}
		else if (Sun.day < Sun.WEIGHT - 0.45f)
		{
			base.light.color = this.colorMidday;
			base.light.intensity = this.intensityMidday;
		}
		else if (Sun.day < Sun.WEIGHT)
		{
			base.light.color = Color.Lerp(this.colorMidday, this.colorDusk, (Sun.day - Sun.WEIGHT + 0.05f) / 0.05f);
			base.light.intensity = Mathf.Lerp(this.intensityMidday, this.intensityDusk, (Sun.day - Sun.WEIGHT + 0.05f) / 0.05f);
		}
		else if (Sun.day < Sun.WEIGHT + 0.05f)
		{
			base.light.color = Color.Lerp(this.colorDusk, Color.black, (Sun.day - Sun.WEIGHT) / 0.05f);
			base.light.intensity = Mathf.Lerp(this.intensityDusk, 0f, (Sun.day - Sun.WEIGHT) / 0.05f);
		}
		else if ((double)Sun.day >= 0.95)
		{
			base.light.color = Color.Lerp(Color.black, this.colorDawn, (Sun.day - 0.95f) / 0.05f);
			base.light.intensity = Mathf.Lerp(0f, this.intensityDawn, (Sun.day - 0.95f) / 0.05f);
		}
		else
		{
			base.light.color = Color.black;
			base.light.intensity = 0f;
		}
		if (Sun.vision == 0)
		{
			Light light = base.light;
			light.intensity = light.intensity * 4f;
		}
		if (Camera.main != null && (!Network.isServer || !ServerSettings.dedicated))
		{
			if (Sun.vision == 0)
			{
				Camera.main.backgroundColor = Colors.NVG_0;
			}
			else if (Sun.vision == 1)
			{
				Camera.main.backgroundColor = Colors.NVG_1;
			}
			else if ((double)Sun.day < 0.05)
			{
				Camera.main.backgroundColor = Color.Lerp(this.skyDawn, this.skyMidday, Sun.day / 0.05f);
			}
			else if (Sun.day < Sun.WEIGHT - 0.05f)
			{
				Camera.main.backgroundColor = this.skyMidday;
			}
			else if (Sun.day < Sun.WEIGHT)
			{
				Camera.main.backgroundColor = Color.Lerp(this.skyMidday, this.skyDusk, (Sun.day - Sun.WEIGHT + 0.05f) / 0.05f);
			}
			else if (Sun.day < Sun.WEIGHT + 0.05f)
			{
				Camera.main.backgroundColor = Color.Lerp(this.skyDusk, this.skyMidnight, (Sun.day - Sun.WEIGHT) / 0.05f);
			}
			else if ((double)Sun.day >= 0.95)
			{
				Camera.main.backgroundColor = Color.Lerp(this.skyMidnight, this.skyDawn, (Sun.day - 0.95f) / 0.05f);
			}
			else
			{
				Camera.main.backgroundColor = this.skyMidnight;
			}
			RenderSettings.fogColor = Camera.main.backgroundColor;
			if (GraphicsSettings.streaks)
			{
				Camera.main.GetComponent<SunShafts>().\u08c8 = base.light.color;
			}
			if (Look.zoom != null)
			{
				Look.zoom.backgroundColor = Camera.main.backgroundColor;
			}
		}
		RenderSettings.fogStartDistance = 0f;
		if (Sun.vision == 0)
		{
			RenderSettings.fogEndDistance = 200f;
		}
		else if (Sun.vision == 1)
		{
			RenderSettings.fogEndDistance = 400f;
		}
		else if ((double)Sun.day < 0.05)
		{
			RenderSettings.fogEndDistance = Mathf.Lerp(this.rangeDawn, this.rangeMidday, Sun.day / 0.05f);
		}
		else if (Sun.day < Sun.WEIGHT - 0.05f)
		{
			RenderSettings.fogEndDistance = this.rangeMidday;
		}
		else if (Sun.day < Sun.WEIGHT)
		{
			RenderSettings.fogEndDistance = Mathf.Lerp(this.rangeMidday, this.rangeDusk, (Sun.day - Sun.WEIGHT + 0.05f) / 0.05f);
		}
		else if (Sun.day < Sun.WEIGHT + 0.05f)
		{
			RenderSettings.fogEndDistance = Mathf.Lerp(this.rangeDusk, this.rangeMidnight, (Sun.day - Sun.WEIGHT) / 0.05f);
		}
		else if ((double)Sun.day >= 0.95)
		{
			RenderSettings.fogEndDistance = Mathf.Lerp(this.rangeMidnight, this.rangeDawn, (Sun.day - 0.95f) / 0.05f);
		}
		else
		{
			RenderSettings.fogEndDistance = this.rangeMidnight;
		}
		if ((double)Sun.day < 0.5)
		{
			this.sky.color = this.starsMidday;
		}
		else if (Sun.day < Sun.WEIGHT + 0.05f)
		{
			this.sky.color = Color.Lerp(this.starsMidday, this.starsMidnight, (Sun.day - Sun.WEIGHT) / 0.05f);
		}
		else if ((double)Sun.day >= 0.95)
		{
			this.sky.color = Color.Lerp(this.starsMidnight, this.starsMidday, (Sun.day - 0.95f) / 0.05f);
		}
		else
		{
			this.sky.color = this.starsMidnight;
		}
		if (Sun.day >= Sun.WEIGHT)
		{
			base.transform.rotation = Quaternion.Euler(180f + (Sun.day - Sun.WEIGHT) / (1f - Sun.WEIGHT) * 180f, this.azimuth, 0f);
		}
		else
		{
			base.transform.rotation = Quaternion.Euler(Sun.day / Sun.WEIGHT * 180f, this.azimuth, 0f);
		}
		if (Time.realtimeSinceStartup - this.began > 1f)
		{
			if (Sun.day < Sun.WEIGHT)
			{
				if (this.ambience != this.dayAmbience)
				{
					this.ambience = this.dayAmbience;
					base.audio.clip = (AudioClip)Resources.Load(string.Concat("Sounds/Ambience/", this.dayAmbience));
					base.audio.Play();
				}
			}
			else if (this.ambience != this.nightAmbience)
			{
				this.ambience = this.nightAmbience;
				base.audio.clip = (AudioClip)Resources.Load(string.Concat("Sounds/Ambience/", this.nightAmbience));
				base.audio.Play();
			}
		}
		if (Player.model == null)
		{
			this.lastSpook = Time.realtimeSinceStartup;
		}
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

	public static void setVision(int id, bool on)
	{
		if (id != 19 || !on)
		{
			Camera.main.transform.FindChild("light_0").light.enabled = false;
			Camera.main.transform.FindChild("light_1").light.enabled = false;
		}
		else
		{
			Camera.main.transform.FindChild("light_0").light.enabled = true;
			Camera.main.transform.FindChild("light_1").light.enabled = true;
		}
		if (id != 1 && id != 17 && Sun.vision != -1 || !on)
		{
			Sun.vision = -1;
			Camera.main.GetComponent<Vignetting>().enabled = false;
			Camera.main.GetComponent<GrayscaleEffect>().enabled = false;
			Look.view.GetComponent<GrayscaleEffect>().enabled = false;
			Look.zoom.GetComponent<GrayscaleEffect>().enabled = false;
			Sun.tool.cycle();
		}
		else if (id == 1 && Sun.vision != 0)
		{
			Sun.vision = 0;
			Camera.main.GetComponent<Vignetting>().enabled = true;
			Camera.main.GetComponent<GrayscaleEffect>().enabled = false;
			Look.view.GetComponent<GrayscaleEffect>().enabled = false;
			Look.zoom.GetComponent<GrayscaleEffect>().enabled = false;
			Sun.tool.cycle();
		}
		else if (id == 17 && Sun.vision != 1)
		{
			Sun.vision = 1;
			Camera.main.GetComponent<Vignetting>().enabled = true;
			Camera.main.GetComponent<GrayscaleEffect>().enabled = true;
			Look.view.GetComponent<GrayscaleEffect>().enabled = true;
			Look.zoom.GetComponent<GrayscaleEffect>().enabled = true;
			Sun.tool.cycle();
		}
	}
}