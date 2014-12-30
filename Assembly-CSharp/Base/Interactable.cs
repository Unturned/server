using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	public Interactable()
	{
	}

	public virtual string hint()
	{
		return string.Empty;
	}

	public virtual string icon()
	{
		return string.Empty;
	}

	public virtual void trigger()
	{
	}
}