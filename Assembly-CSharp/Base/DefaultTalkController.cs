using System;
using UnityEngine;

[AddComponentMenu("USpeak/Default Talk Controller")]
public class DefaultTalkController : MonoBehaviour
{
	[HideInInspector]
	[SerializeField]
	public KeyCode TriggerKey;

	[HideInInspector]
	[SerializeField]
	public int ToggleMode;

	private bool val;

	public DefaultTalkController()
	{
	}

	public void OnInspectorGUI()
	{
	}

	public bool ShouldSend()
	{
		if (this.ToggleMode == 0)
		{
			this.val = Input.GetKey(this.TriggerKey);
		}
		else if (Input.GetKeyDown(this.TriggerKey))
		{
			this.val = !this.val;
		}
		return this.val;
	}
}