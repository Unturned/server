using MoPhoGames.USpeak.Core.Utils;
using System;

namespace MoPhoGames.USpeak.Codec
{
	[Serializable]
	internal class ADPCMCodec : ICodec
	{
		private static int[] indexTable;

		private static int[] stepsizeTable;

		private int predictedSample;

		private int stepsize = 7;

		private int index;

		private int newSample;

		static ADPCMCodec()
		{
			ADPCMCodec.indexTable = new int[] { -1, -1, -1, -1, 2, 4, 6, 8, -1, -1, -1, -1, 2, 4, 6, 8 };
			ADPCMCodec.stepsizeTable = new int[] { 7, 8, 9, 10, 11, 12, 14, 16, 17, 19, 21, 23, 25, 28, 31, 34, 37, 41, 45, 50, 55, 60, 66, 73, 80, 88, 97, 107, 118, 130, 143, 157, 173, 190, 209, 230, 253, 279, 307, 337, 371, 408, 449, 494, 544, 598, 658, 724, 796, 876, 963, 1060, 1166, 1282, 1411, 1522, 1707, 1876, 2066, 2272, 2499, 2749, 3024, 3327, 3660, 4026, 4428, 4871, 5358, 5894, 6484, 7132, 7845, 8630, 9493, 10442, 11487, 12635, 13899, 15289, 16818, 18500, 203500, 22385, 24623, 27086, 29794, 32767 };
		}

		public ADPCMCodec()
		{
		}

		private short ADPCM_Decode(byte originalSample)
		{
			int num = 0;
			num = this.stepsize * originalSample / 4 + this.stepsize / 8;
			if ((originalSample & 4) == 4)
			{
				num = num + this.stepsize;
			}
			if ((originalSample & 2) == 2)
			{
				num = num + (this.stepsize >> 1);
			}
			if ((originalSample & 1) == 1)
			{
				num = num + (this.stepsize >> 2);
			}
			num = num + (this.stepsize >> 3);
			if ((originalSample & 8) == 8)
			{
				num = -num;
			}
			this.newSample = num;
			if (this.newSample > 32767)
			{
				this.newSample = 32767;
			}
			else if (this.newSample < -32768)
			{
				this.newSample = -32768;
			}
			ADPCMCodec aDPCMCodec = this;
			aDPCMCodec.index = aDPCMCodec.index + ADPCMCodec.indexTable[originalSample];
			if (this.index < 0)
			{
				this.index = 0;
			}
			if (this.index > 88)
			{
				this.index = 88;
			}
			this.stepsize = ADPCMCodec.stepsizeTable[this.index];
			return (short)this.newSample;
		}

		private byte ADPCM_Encode(short originalSample)
		{
			int num = originalSample - this.predictedSample;
			if (num < 0)
			{
				this.newSample = 8;
				num = -num;
			}
			else
			{
				this.newSample = 0;
			}
			byte num1 = 4;
			int num2 = this.stepsize;
			for (int i = 0; i < 3; i++)
			{
				if (num >= num2)
				{
					ADPCMCodec aDPCMCodec = this;
					aDPCMCodec.newSample = aDPCMCodec.newSample | num1;
					num = num - num2;
				}
				num2 = num2 >> 1;
				num1 = (byte)(num1 >> 1);
			}
			num = this.stepsize >> 3;
			if ((this.newSample & 4) != 0)
			{
				num = num + this.stepsize;
			}
			if ((this.newSample & 2) != 0)
			{
				num = num + (this.stepsize >> 1);
			}
			if ((this.newSample & 1) != 0)
			{
				num = num + (this.stepsize >> 2);
			}
			if ((this.newSample & 8) != 0)
			{
				num = -num;
			}
			ADPCMCodec aDPCMCodec1 = this;
			aDPCMCodec1.predictedSample = aDPCMCodec1.predictedSample + num;
			if (this.predictedSample > 32767)
			{
				this.predictedSample = 32767;
			}
			if (this.predictedSample < -32768)
			{
				this.predictedSample = -32768;
			}
			ADPCMCodec aDPCMCodec2 = this;
			aDPCMCodec2.index = aDPCMCodec2.index + ADPCMCodec.indexTable[this.newSample];
			if (this.index < 0)
			{
				this.index = 0;
			}
			else if (this.index > 88)
			{
				this.index = 88;
			}
			this.stepsize = ADPCMCodec.stepsizeTable[this.index];
			return (byte)this.newSample;
		}

		public short[] Decode(byte[] data, BandMode mode)
		{
			this.Init();
			short[] num = USpeakPoolUtils.GetShort((int)data.Length * 2);
			for (int i = 0; i < (int)data.Length; i++)
			{
				byte num1 = data[i];
				byte num2 = (byte)(num1 & 15);
				byte num3 = (byte)(num1 >> 4);
				num[i * 2] = this.ADPCM_Decode(num2);
				num[i * 2 + 1] = this.ADPCM_Decode(num3);
			}
			return num;
		}

		public byte[] Encode(short[] data, BandMode mode)
		{
			this.Init();
			int length = (int)data.Length / 2;
			if (length % 2 != 0)
			{
				length++;
			}
			byte[] num = USpeakPoolUtils.GetByte(length);
			int num1 = 0;
			while (num1 < (int)num.Length)
			{
				if (num1 * 2 < (int)data.Length)
				{
					byte num2 = this.ADPCM_Encode(data[num1 * 2]);
					byte num3 = 0;
					if (num1 * 2 + 1 < (int)data.Length)
					{
						num3 = this.ADPCM_Encode(data[num1 * 2 + 1]);
					}
					num[num1] = (byte)(num3 << 4 | num2);
					num1++;
				}
				else
				{
					break;
				}
			}
			return num;
		}

		public int GetSampleSize(int recordingFrequency)
		{
			return 0;
		}

		private void Init()
		{
			this.predictedSample = 0;
			this.stepsize = 7;
			this.index = 0;
			this.newSample = 0;
		}
	}
}