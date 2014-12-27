using System;

namespace MoPhoGames.USpeak.Core
{
	public class USpeakSettingsData
	{
		public BandMode bandMode;

		public int Codec;

		public USpeakSettingsData()
		{
			this.bandMode = BandMode.Narrow;
			this.Codec = 0;
		}

		public USpeakSettingsData(byte src)
		{
			if ((src & 1) == 1)
			{
				this.bandMode = BandMode.Narrow;
			}
			else if ((src & 2) != 2)
			{
				this.bandMode = BandMode.UltraWide;
			}
			else
			{
				this.bandMode = BandMode.Wide;
			}
			this.Codec = src >> 2;
		}

		public byte ToByte()
		{
			byte codec = 0;
			if (this.bandMode == BandMode.Narrow)
			{
				codec = (byte)(codec | 1);
			}
			else if (this.bandMode == BandMode.Wide)
			{
				codec = (byte)(codec | 2);
			}
			codec = (byte)(codec | (byte)(this.Codec << 2));
			return codec;
		}
	}
}