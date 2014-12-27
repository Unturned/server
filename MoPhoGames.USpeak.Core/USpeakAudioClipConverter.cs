using MoPhoGames.USpeak.Core.Utils;
using System;
using UnityEngine;

namespace MoPhoGames.USpeak.Core
{
	public class USpeakAudioClipConverter
	{
		public USpeakAudioClipConverter()
		{
		}

		public static short[] AudioDataToShorts(float[] samples, int channels, float gain = 1f)
		{
			short[] num = USpeakPoolUtils.GetShort((int)samples.Length * channels);
			for (int i = 0; i < (int)samples.Length; i++)
			{
				float single = samples[i] * gain;
				if (Mathf.Abs(single) > 1f)
				{
					single = (single <= 0f ? -1f : 1f);
				}
				num[i] = (short)(single * 3267f);
			}
			return num;
		}

		public static float[] ShortsToAudioData(short[] data, int channels, int frequency, bool threedimensional, float gain)
		{
			float[] num = USpeakPoolUtils.GetFloat((int)data.Length);
			for (int i = 0; i < (int)num.Length; i++)
			{
				int num1 = data[i];
				num[i] = (float)num1 / 3267f * gain;
			}
			return num;
		}
	}
}