using System;
using UnityEngine;

public class Smoke : MonoBehaviour
{
	public Smoke()
	{
	}

	public void engage()
	{
		base.transform.FindChild("smokebomb").GetComponent<ParticleSystem>().Play();
		base.transform.FindChild("smokebomb").audio.Play();
	}

	public void Start()
	{
		base.Invoke("engage", 2f);
		UnityEngine.Object.Destroy(base.gameObject, 60f);
	}
}