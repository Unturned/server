using System;
using System.Collections.Generic;

namespace MoPhoGames.USpeak.Core.Utils
{
	public class USpeakPoolUtils
	{
		private static List<byte[]> BytePool;

		private static List<short[]> ShortPool;

		private static List<float[]> FloatPool;

		static USpeakPoolUtils()
		{
			USpeakPoolUtils.BytePool = new List<byte[]>();
			USpeakPoolUtils.ShortPool = new List<short[]>();
			USpeakPoolUtils.FloatPool = new List<float[]>();
		}

		public USpeakPoolUtils()
		{
		}

		public static byte[] GetByte(int length)
		{
			for (int i = 0; i < USpeakPoolUtils.BytePool.Count; i++)
			{
				if ((int)USpeakPoolUtils.BytePool[i].Length == length)
				{
					byte[] item = USpeakPoolUtils.BytePool[i];
					USpeakPoolUtils.BytePool.RemoveAt(i);
					return item;
				}
			}
			return new byte[length];
		}

		public static float[] GetFloat(int length)
		{
			for (int i = 0; i < USpeakPoolUtils.FloatPool.Count; i++)
			{
				if ((int)USpeakPoolUtils.FloatPool[i].Length == length)
				{
					float[] item = USpeakPoolUtils.FloatPool[i];
					USpeakPoolUtils.FloatPool.RemoveAt(i);
					return item;
				}
			}
			return new float[length];
		}

		public static short[] GetShort(int length)
		{
			for (int i = 0; i < USpeakPoolUtils.ShortPool.Count; i++)
			{
				if ((int)USpeakPoolUtils.ShortPool[i].Length == length)
				{
					short[] item = USpeakPoolUtils.ShortPool[i];
					USpeakPoolUtils.ShortPool.RemoveAt(i);
					return item;
				}
			}
			return new short[length];
		}

		public static void Return(float[] d)
		{
			USpeakPoolUtils.FloatPool.Add(d);
		}

		public static void Return(byte[] d)
		{
			USpeakPoolUtils.BytePool.Add(d);
		}

		public static void Return(short[] d)
		{
			USpeakPoolUtils.ShortPool.Add(d);
		}
	}
}