using System;

namespace MoPhoGames.USpeak.Core
{
	public struct USpeakFrameContainer
	{
		public ushort Samples;

		public byte[] encodedData;

		public void LoadFrom(byte[] source)
		{
			int num = BitConverter.ToInt32(source, 0);
			this.Samples = BitConverter.ToUInt16(source, 4);
			this.encodedData = new byte[num];
			Array.Copy(source, 6, this.encodedData, 0, num);
		}

		public byte[] ToByteArray()
		{
			byte[] numArray = new byte[6 + (int)this.encodedData.Length];
			byte[] bytes = BitConverter.GetBytes((int)this.encodedData.Length);
			bytes.CopyTo(numArray, 0);
			byte[] bytes1 = BitConverter.GetBytes(this.Samples);
			Array.Copy(bytes1, 0, numArray, 4, 2);
			for (int i = 0; i < (int)this.encodedData.Length; i++)
			{
				numArray[i + 6] = this.encodedData[i];
			}
			return numArray;
		}
	}
}