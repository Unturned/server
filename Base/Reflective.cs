using System;
using UnityEngine;

public class Reflective : MonoBehaviour
{
	public readonly static Color REFLECTION;

	public readonly static Color SPECULAR;

	public readonly static float SHINE;

	public static int closest;

	public static float distance;

	public static float offset;

	static Reflective()
	{
		Reflective.REFLECTION = new Color(0.498039216f, 0.498039216f, 0.498039216f);
		Reflective.SPECULAR = new Color(0.294117659f, 0.294117659f, 0.294117659f);
		Reflective.SHINE = 0.5f;
		Reflective.closest = -1;
		Reflective.distance = Single.MaxValue;
		Reflective.offset = 0f;
	}

	public Reflective()
	{
	}

	public void Awake()
	{
		Reflector.build();
		Reflective.closest = -1;
		Reflective.distance = Single.MaxValue;
		for (int i = 0; i < (int)Reflector.cubemaps.Length; i++)
		{
			Vector3 vector3 = Reflector.cubemaps[i].position - base.transform.position;
			Reflective.offset = vector3.magnitude;
			if (Reflective.offset < Reflective.distance)
			{
				Reflective.distance = Reflective.offset;
				Reflective.closest = i;
			}
		}
		if (Reflective.closest != -1)
		{
			if (base.renderer.material.shader.name == "Reflective Fancy/Specular")
			{
				base.renderer.material.SetTexture("_Cube", (Cubemap)Resources.Load(string.Concat(new object[] { "Cubemaps/", Application.loadedLevelName, "/Node_", Reflective.closest, "/reflection" })));
				base.renderer.material.SetColor("_ReflectColor", Reflective.REFLECTION);
				base.renderer.material.SetColor("_SpecColor", Reflective.REFLECTION);
				base.renderer.material.SetFloat("_Shininess", Reflective.SHINE);
			}
			else
			{
				Debug.Log("Missing reflection shader.", base.gameObject);
			}
		}
		UnityEngine.Object.Destroy(this);
	}
}