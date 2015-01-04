using System;
using UnityEngine;

public class Ocean : MonoBehaviour
{
	public static float level;

	private Material water;

	public Color colorMidday;

	public Color refractionMidday;

	public Color reflectionMidday;

	public Color colorMidnight;

	public Color refractionMidnight;

	public Color reflectionMidnight;

	public Ocean()
	{
	}

	public void apply()
	{
		if (GraphicsSettings.water == 2)
		{
			this.water.shader.maximumLOD = 501;
			for (int i = 0; i < base.transform.childCount; i++)
			{
				base.transform.GetChild(i).gameObject.SetActive(true);
			}
			base.transform.FindChild("lowq1").gameObject.SetActive(false);
			base.transform.FindChild("lowq2").gameObject.SetActive(false);
			base.transform.FindChild("lowq3").gameObject.SetActive(false);
			base.transform.FindChild("lowq4").gameObject.SetActive(false);
			base.transform.FindChild("sand").gameObject.SetActive(true);
		}
		else if (GraphicsSettings.water != 1)
		{
			this.water.shader.maximumLOD = 201;
			for (int j = 0; j < base.transform.childCount; j++)
			{
				base.transform.GetChild(j).gameObject.SetActive(false);
			}
			base.transform.FindChild("lowq1").gameObject.SetActive(true);
			base.transform.FindChild("lowq2").gameObject.SetActive(true);
			base.transform.FindChild("lowq3").gameObject.SetActive(true);
			base.transform.FindChild("lowq4").gameObject.SetActive(true);
			base.transform.FindChild("sand").gameObject.SetActive(true);
		}
		else
		{
			this.water.shader.maximumLOD = 301;
			for (int k = 0; k < base.transform.childCount; k++)
			{
				base.transform.GetChild(k).gameObject.SetActive(true);
			}
			base.transform.FindChild("lowq1").gameObject.SetActive(false);
			base.transform.FindChild("lowq2").gameObject.SetActive(false);
			base.transform.FindChild("lowq3").gameObject.SetActive(false);
			base.transform.FindChild("lowq4").gameObject.SetActive(false);
			base.transform.FindChild("sand").gameObject.SetActive(true);
		}
	}

	public void Awake()
	{
		// TODO: sorry...
		Ocean.level = base.transform.position.y;
		this.apply();
		base.InvokeRepeating("cycle", 0f, 1f);
	}

	public void cycle()
	{		
		if ((double)Sun.day < 0.05)
		{
			this.water.SetColor("_BaseColor", Color.Lerp(this.colorMidnight, this.colorMidday, 0.5f + Sun.day / 0.1f));
			this.water.SetColor("_RefractionColor", Color.Lerp(this.refractionMidnight, this.refractionMidday, 0.5f + Sun.day / 0.1f));
			this.water.SetColor("_ReflectionColor", Color.Lerp(this.reflectionMidnight, this.reflectionMidday, 0.5f + Sun.day / 0.1f));
			this.water.SetVector("_Foam", new Vector4(0.33f, Mathf.Lerp(0f, 0.25f, 0.5f + Sun.day / 0.1f), Mathf.Lerp(0f, 0.25f, 0.5f + Sun.day / 0.1f), 0.4f));
		}
		else if (Sun.day < Sun.WEIGHT - 0.05f)
		{
			this.water.SetColor("_BaseColor", this.colorMidday);
			this.water.SetColor("_RefractionColor", this.refractionMidday);
			this.water.SetColor("_ReflectionColor", this.reflectionMidday);
			this.water.SetVector("_Foam", new Vector4(0.33f, 0.25f, 0.25f, 0.4f));
		}
		else if (Sun.day < Sun.WEIGHT + 0.05f)
		{
			this.water.SetColor("_BaseColor", Color.Lerp(this.colorMidday, this.colorMidnight, (Sun.day - Sun.WEIGHT + 0.05f) / 0.1f));
			this.water.SetColor("_RefractionColor", Color.Lerp(this.refractionMidday, this.refractionMidnight, (Sun.day - Sun.WEIGHT + 0.05f) / 0.1f));
			this.water.SetColor("_ReflectionColor", Color.Lerp(this.reflectionMidday, this.reflectionMidnight, (Sun.day - Sun.WEIGHT + 0.05f) / 0.1f));
			this.water.SetVector("_Foam", new Vector4(0.33f, Mathf.Lerp(0.25f, 0f, (Sun.day - Sun.WEIGHT + 0.05f) / 0.1f), Mathf.Lerp(0.25f, 0f, (Sun.day - Sun.WEIGHT + 0.05f) / 0.1f), 0.4f));
		}
		else if ((double)Sun.day >= 0.95)
		{
			this.water.SetColor("_BaseColor", Color.Lerp(this.colorMidnight, this.colorMidday, (Sun.day - 0.95f) / 0.1f));
			this.water.SetColor("_RefractionColor", Color.Lerp(this.refractionMidnight, this.refractionMidday, (Sun.day - 0.95f) / 0.1f));
			this.water.SetColor("_ReflectionColor", Color.Lerp(this.reflectionMidnight, this.reflectionMidday, (Sun.day - 0.95f) / 0.1f));
			this.water.SetVector("_Foam", new Vector4(0.33f, Mathf.Lerp(0f, 0.25f, (Sun.day - 0.95f) / 0.1f), Mathf.Lerp(0f, 0.25f, (Sun.day - 0.95f) / 0.1f), 0.4f));
		}
		else
		{
			this.water.SetColor("_BaseColor", this.colorMidnight);
			this.water.SetColor("_RefractionColor", this.refractionMidnight);
			this.water.SetColor("_ReflectionColor", this.reflectionMidnight);
			this.water.SetVector("_Foam", new Vector4(0.33f, 0f, 0f, 0.4f));
		}
	}
}