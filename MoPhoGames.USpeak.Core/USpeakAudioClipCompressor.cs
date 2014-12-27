using MoPhoGames.USpeak.Codec;
using MoPhoGames.USpeak.Core.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoPhoGames.USpeak.Core
{
	public class USpeakAudioClipCompressor : MonoBehaviour
	{
		private static List<byte> data;

		private static List<short> tmp;

		static USpeakAudioClipCompressor()
		{
			USpeakAudioClipCompressor.data = new List<byte>();
			USpeakAudioClipCompressor.tmp = new List<short>();
		}

		public USpeakAudioClipCompressor()
		{
		}

		public static byte[] CompressAudioData(float[] samples, int channels, out int sample_count, BandMode mode, ICodec Codec, float gain = 1f)
		{
			USpeakAudioClipCompressor.data.Clear();
			sample_count = 0;
			short[] shorts = USpeakAudioClipConverter.AudioDataToShorts(samples, channels, gain);
			byte[] numArray = Codec.Encode(shorts, mode);
			USpeakPoolUtils.Return(shorts);
			USpeakAudioClipCompressor.data.AddRange(numArray);
			USpeakPoolUtils.Return(numArray);
			return USpeakAudioClipCompressor.data.ToArray();
		}

		public static float[] DecompressAudio(byte[] data, int samples, int channels, bool threeD, BandMode mode, ICodec Codec, float gain)
		{
			int num = 4000;
			if (mode == BandMode.Narrow)
			{
				num = 8000;
			}
			else if (mode == BandMode.Wide)
			{
				num = 16000;
			}
			short[] numArray = Codec.Decode(data, mode);
			USpeakAudioClipCompressor.tmp.Clear();
			USpeakAudioClipCompressor.tmp.AddRange(numArray);
			USpeakPoolUtils.Return(numArray);
			return USpeakAudioClipConverter.ShortsToAudioData(USpeakAudioClipCompressor.tmp.ToArray(), channels, num, threeD, gain);
		}
	}
}