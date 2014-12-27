using MoPhoGames.USpeak.Core.Utils;
using NSpeex;
using System;

namespace MoPhoGames.USpeak.Codec
{
	public class SpeexCodec : ICodec
	{
		private SpeexDecoder m_ultrawide_dec = null;//new SpeexDecoder(BandMode.UltraWide, true);

		private SpeexEncoder m_ultrawide_enc = null;//new SpeexEncoder(BandMode.UltraWide);

		private SpeexDecoder m_wide_dec = null;//new SpeexDecoder(BandMode.Wide, true);

		private SpeexEncoder m_wide_enc = null;//new SpeexEncoder(BandMode.Wide);

		private SpeexDecoder m_narrow_dec = null;//new SpeexDecoder(BandMode.Narrow, true);

		private SpeexEncoder m_narrow_enc = null;//new SpeexEncoder(BandMode.Narrow);

		public SpeexCodec()
		{
			this.m_wide_enc.Quality = 5;
			this.m_narrow_enc.Quality = 5;
			this.m_ultrawide_enc.Quality = 5;
		}

		public short[] Decode(byte[] data, BandMode mode)
		{
			return this.SpeexDecode(data, mode);
		}

		public byte[] Encode(short[] data, BandMode mode)
		{
			return this.SpeexEncode(data, mode);
		}

		public int GetSampleSize(int recordingFrequency)
		{
			int num = recordingFrequency;
			if (num == 8000)
			{
				return 320;
			}
			if (num == 16000)
			{
				return 640;
			}
			if (num == 32000)
			{
				return 1280;
			}
			return 320;
		}

		private short[] SpeexDecode(byte[] input, BandMode mode)
		{
			SpeexDecoder mNarrowDec = null;
			int num = 320;
			switch (mode)
			{
				case BandMode.Narrow:
				{
					mNarrowDec = this.m_narrow_dec;
					num = 320;
					break;
				}
				case BandMode.Wide:
				{
					mNarrowDec = this.m_wide_dec;
					num = 640;
					break;
				}
				case BandMode.UltraWide:
				{
					mNarrowDec = this.m_ultrawide_dec;
					num = 1280;
					break;
				}
			}
			byte[] numArray = USpeakPoolUtils.GetByte(4);
			Array.Copy(input, numArray, 4);
			int num1 = BitConverter.ToInt32(numArray, 0);
			USpeakPoolUtils.Return(numArray);
			byte[] numArray1 = USpeakPoolUtils.GetByte((int)input.Length - 4);
			Buffer.BlockCopy(input, 4, numArray1, 0, (int)input.Length - 4);
			short[] num2 = USpeakPoolUtils.GetShort(num);
			mNarrowDec.Decode(numArray1, 0, num1, num2, 0, false);
			USpeakPoolUtils.Return(numArray1);
			return num2;
		}

		private byte[] SpeexEncode(short[] input, BandMode mode)
		{
			SpeexEncoder mNarrowEnc = null;
			int num = 320;
			switch (mode)
			{
				case BandMode.Narrow:
				{
					mNarrowEnc = this.m_narrow_enc;
					num = 320;
					break;
				}
				case BandMode.Wide:
				{
					mNarrowEnc = this.m_wide_enc;
					num = 640;
					break;
				}
				case BandMode.UltraWide:
				{
					mNarrowEnc = this.m_ultrawide_enc;
					num = 1280;
					break;
				}
			}
			byte[] numArray = USpeakPoolUtils.GetByte(num + 4);
			int num1 = mNarrowEnc.Encode(input, 0, (int)input.Length, numArray, 4, (int)numArray.Length);
			Array.Copy(BitConverter.GetBytes(num1), numArray, 4);
			return numArray;
		}
	}
}