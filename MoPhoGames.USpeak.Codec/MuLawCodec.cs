using MoPhoGames.USpeak.Core.Utils;
using System;

namespace MoPhoGames.USpeak.Codec
{
	[Serializable]
	public class MuLawCodec : ICodec
	{
		public MuLawCodec()
		{
		}

		public short[] Decode(byte[] data, BandMode mode)
		{
			return MuLawCodec.MuLawDecoder.MuLawDecode(data);
		}

		public byte[] Encode(short[] data, BandMode mode)
		{
			return MuLawCodec.MuLawEncoder.MuLawEncode(data);
		}

		public int GetSampleSize(int recordingFrequency)
		{
			return 0;
		}

		private class MuLawDecoder
		{
			private readonly static short[] muLawToPcmMap;

			static MuLawDecoder()
			{
				MuLawCodec.MuLawDecoder.muLawToPcmMap = new short[256];
				for (byte i = 0; i < 255; i = (byte)(i + 1))
				{
					MuLawCodec.MuLawDecoder.muLawToPcmMap[i] = MuLawCodec.MuLawDecoder.Decode(i);
				}
			}

			public MuLawDecoder()
			{
			}

			private static short Decode(byte mulaw)
			{
				mulaw = (byte)(~mulaw);
				int num = mulaw & 128;
				int num1 = (mulaw & 112) >> 4;
				int num2 = mulaw & 15;
				num2 = num2 | 16;
				num2 = num2 << 1;
				num2++;
				num2 = num2 << (num1 + 2 & 31);
				num2 = num2 - 132;
				return (short)((num != 0 ? -num2 : num2));
			}

			public static short[] MuLawDecode(byte[] data)
			{
				int length = (int)data.Length;
				short[] num = USpeakPoolUtils.GetShort(length);
				for (int i = 0; i < length; i++)
				{
					num[i] = MuLawCodec.MuLawDecoder.muLawToPcmMap[data[i]];
				}
				return num;
			}
		}

		private class MuLawEncoder
		{
			public const int BIAS = 132;

			public const int MAX = 32635;

			private static byte[] pcmToMuLawMap;

			public static bool ZeroTrap
			{
				get
				{
					return MuLawCodec.MuLawEncoder.pcmToMuLawMap[33000] != 0;
				}
				set
				{
					object obj;
					if (!value)
					{
						obj = null;
					}
					else
					{
						obj = 2;
					}
					byte num = (byte)obj;
					for (int i = 32768; i <= 33924; i++)
					{
						MuLawCodec.MuLawEncoder.pcmToMuLawMap[i] = num;
					}
				}
			}

			static MuLawEncoder()
			{
				MuLawCodec.MuLawEncoder.pcmToMuLawMap = new byte[65536];
				for (int i = -32768; i <= 32767; i++)
				{
					MuLawCodec.MuLawEncoder.pcmToMuLawMap[i & 65535] = MuLawCodec.MuLawEncoder.encode(i);
				}
			}

			public MuLawEncoder()
			{
			}

			private static byte encode(int pcm)
			{
				int num = (pcm & 32768) >> 8;
				if (num != 0)
				{
					pcm = -pcm;
				}
				if (pcm > 32635)
				{
					pcm = 32635;
				}
				pcm = pcm + 132;
				int num1 = 7;
				for (int i = 16384; (pcm & i) == 0; i = i >> 1)
				{
					num1--;
				}
				int num2 = pcm >> (num1 + 3 & 31) & 15;
				byte num3 = (byte)(num | num1 << 4 | num2);
				return (byte)(~num3);
			}

			public static byte MuLawEncode(int pcm)
			{
				return MuLawCodec.MuLawEncoder.pcmToMuLawMap[pcm & 65535];
			}

			public static byte MuLawEncode(short pcm)
			{
				return MuLawCodec.MuLawEncoder.pcmToMuLawMap[pcm & 65535];
			}

			public static byte[] MuLawEncode(int[] pcm)
			{
				int length = (int)pcm.Length;
				byte[] num = USpeakPoolUtils.GetByte(length);
				for (int i = 0; i < length; i++)
				{
					num[i] = MuLawCodec.MuLawEncoder.MuLawEncode(pcm[i]);
				}
				return num;
			}

			public static byte[] MuLawEncode(short[] pcm)
			{
				int length = (int)pcm.Length;
				byte[] num = USpeakPoolUtils.GetByte(length);
				for (int i = 0; i < length; i++)
				{
					num[i] = MuLawCodec.MuLawEncoder.MuLawEncode(pcm[i]);
				}
				return num;
			}
		}
	}
}