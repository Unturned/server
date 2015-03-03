using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("USpeak/USpeaker")]
public class USpeaker : MonoBehaviour
{
	public static float RemoteGain;

	public static float LocalGain;

	public static bool MuteAll;

	public static List<USpeaker> USpeakerList;

	private static int InputDeviceID;

	private static string InputDeviceName;

	public SpeakerMode SpeakerMode;

	public BandMode BandwidthMode;

	public float SendRate = 16f;

	public SendBehavior SendingMode;

	public bool UseVAD;

	public ThreeDMode _3DMode;

	public bool DebugPlayback;

	public bool AskPermission = true;

	public bool Mute;

	public float SpeakerVolume = 1f;

	public float VolumeThreshold = 0.01f;

	public int Codec;

	private USpeakCodecManager codecMgr;
    
	private int recFreq;

	private int lastReadPos = 0;
    
	//private List<USpeakFrameContainer> sendBuffer = new List<USpeakFrameContainer>();

	private List<byte> tempSendBytes = new List<byte>();

	//private ISpeechDataHandler audioHandler;

	//private IUSpeakTalkController talkController;
    
	//private USpeakSettingsData settings;

	private string currentDeviceName = string.Empty;

	private float talkTimer = 0;

	private float vadHangover = 0.5f;

	private float lastVTime;

	private List<float[]> pendingEncode = new List<float[]>();
    
	private float[] receivedData;

	private BandMode lastBandMode;

	private int lastCodec;

	private ThreeDMode last3DMode;

	private int audioFrequency
	{
		get
		{
			if (this.recFreq == 0)
			{
				switch (this.BandwidthMode)
				{
					case BandMode.Narrow:
					{
						this.recFreq = 8000;
						break;
					}
					case BandMode.Wide:
					{
						this.recFreq = 16000;
						break;
					}
					case BandMode.UltraWide:
					{
						this.recFreq = 32000;
						break;
					}
					default:
					{
						this.recFreq = 8000;
						break;
					}
				}
			}
			return this.recFreq;
		}
	}

	[Obsolete("Use USpeaker._3DMode instead")]
	public bool Is3D
	{
		get
		{
			return this._3DMode == ThreeDMode.SpeakerPan;
		}
		set
		{
			if (!value)
			{
				this._3DMode = ThreeDMode.None;
			}
			else
			{
				this._3DMode = ThreeDMode.SpeakerPan;
			}
		}
	}

	public bool IsTalking
	{
		get
		{
			return this.talkTimer > 0f;
		}
	}

	static USpeaker()
	{
		USpeaker.RemoteGain = 1f;
		USpeaker.LocalGain = 1f;
		USpeaker.MuteAll = false;
		USpeaker.USpeakerList = new List<USpeaker>();
		USpeaker.InputDeviceID = 0;
		USpeaker.InputDeviceName = string.Empty;
	}

	public USpeaker()
	{
	}

	private float amplitude(float[] x)
	{
		float single = 0f;
		for (int i = 0; i < (int)x.Length; i++)
		{
			single = Mathf.Max(single, Mathf.Abs(x[i]));
		}
		return single;
	}

	private void Awake()
	{
		USpeaker.USpeakerList.Add(this);
		if (base.audio == null)
		{
			base.gameObject.AddComponent<AudioSource>();
		}
		base.audio.clip = AudioClip.Create("vc", this.audioFrequency * 10, 1, this.audioFrequency, this._3DMode == ThreeDMode.Full3D, false);
		base.audio.loop = true;
		this.receivedData = new float[this.audioFrequency * 10];
		this.codecMgr = USpeakCodecManager.Instance;
		this.lastBandMode = this.BandwidthMode;
		this.lastCodec = this.Codec;
		this.last3DMode = this._3DMode;
	}

	private int CalculateSamplesRead(int readPos)
	{
		if (readPos >= this.lastReadPos)
		{
			return readPos - this.lastReadPos;
		}
		return this.audioFrequency * 10 - this.lastReadPos + readPos;
	}

	private bool CheckVAD(float[] samples)
	{
		if (Time.realtimeSinceStartup < this.lastVTime + this.vadHangover)
		{
			return true;
		}
		float single = 0f;
		float[] singleArray = samples;
		for (int i = 0; i < (int)singleArray.Length; i++)
		{
			float single1 = (float)singleArray[i];
			single = Mathf.Max(single, Mathf.Abs(single1));
		}
		bool volumeThreshold = single >= this.VolumeThreshold;
		if (volumeThreshold)
		{
			this.lastVTime = Time.realtimeSinceStartup;
		}
		return volumeThreshold;
	}

	public void DrawTalkControllerUI()
	{
		GUILayout.Label("No component available which implements IUSpeakTalkController\nReverting to default behavior - data is always sent", new GUILayoutOption[0]);
	}

	private Component FindInputHandler()
	{
		return null;
	}

	private Component FindSpeechHandler()
	{
		return null;
	}

	public static USpeaker Get(UnityEngine.Object source)
	{
		if (source is GameObject)
		{
			return (source as GameObject).GetComponent<USpeaker>();
		}
		if (source is Transform)
		{
			return (source as Transform).GetComponent<USpeaker>();
		}
		if (!(source is Component))
		{
			return null;
		}
		return (source as Component).GetComponent<USpeaker>();
	}

	public void GetInputHandler()
	{
		//this.talkController = (IUSpeakTalkController)this.FindInputHandler();
	}

	public void InitializeSettings(int data)
	{
		//this.settings = new USpeakSettingsData((byte)data);
		//this.Codec = this.settings.Codec;
	}

	private float[] normalize(float[] samples, float magnitude)
	{
		float[] singleArray = new float[(int)samples.Length];
		for (int i = 0; i < (int)samples.Length; i++)
		{
			singleArray[i] = samples[i] / magnitude;
		}
		return singleArray;
	}

	private void OnAudioAvailable(float[] pcmData)
	{
		if (this.UseVAD && !this.CheckVAD(pcmData))
		{
			return;
		}
		foreach (float[] singleArray in this.SplitArray(pcmData, 1280))
		{
			this.pendingEncode.Add(singleArray);
		}
	}

	private void OnDestroy()
	{
		USpeaker.USpeakerList.Remove(this);
	}

	private void ProcessPendingEncode(float[] pcm)
	{
		
	}

	private void ProcessPendingEncodeBuffer()
	{
		float single = (float)100 / 1000f;
		float single1 = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup <= single1 + single && this.pendingEncode.Count > 0)
		{
			float[] item = this.pendingEncode[0];
			this.pendingEncode.RemoveAt(0);
			this.ProcessPendingEncode(item);
		}
	}

	public void ReceiveAudio(byte[] data)
	{
		
	}

	public static void SetInputDevice(int deviceID)
	{
		USpeaker.InputDeviceID = deviceID;
		USpeaker.InputDeviceName = Microphone.devices[USpeaker.InputDeviceID];
	}

	private List<float[]> SplitArray(float[] array, int size)
	{
		// TODO: USpeaker kill HAHA 
		return new List<float[]>();
	}

	[DebuggerHidden]
	private IEnumerator Start()
	{
		//USpeaker.<Start>c__Iterator0 variable = null;
		return null;
	}

	private void StopPlaying()
	{
		base.audio.Stop();
		base.audio.time = 0f;
	}

	private void Update()
	{		
	}

	private void UpdateSettings()
	{
		
	}
}