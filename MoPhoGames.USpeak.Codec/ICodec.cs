using System;

namespace MoPhoGames.USpeak.Codec
{
	public interface ICodec
	{
		short[] Decode(byte[] data, BandMode bandMode);

		byte[] Encode(short[] data, BandMode bandMode);

		int GetSampleSize(int recordingFrequency);
	}
}