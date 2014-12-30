using System;
using System.Collections.Generic;
using DataHolder;
using System.Globalization;
using UnityEngine;

namespace UnturnedTest
{
	class MainClass {
		public static void Main (string[] args) {
			// Database Test
			Console.WriteLine("Testing database functions");
			testBanReadWrite();
			testBanReadWrite();

			// public Color ࣈ
			int c = 'ࣈ';
			Console.WriteLine("Value: \\u{0:X02}", c);

			// public ulong ࢑;
			c = '࢑';
			Console.WriteLine("m_SteamID: \\u{0:X02}", c);

			NetworkConnectionError error = Network.InitializeServer(32, 8767, false);
		}

		private static void testBanReadWrite ()
		{
			Console.WriteLine ("Loading bans from file");
			Dictionary<String, NetworkBanned> banList = DataHolder.FileDatabase.LoadBans ();
			Console.WriteLine ("Loaded {0} entry", banList.Count);

			if (banList.Count == 0) {
				banList.Add("76561197994222727", new NetworkBanned("Julius Tiger", "76561197994222727", "Test Case of course", "76561197994222727", DateTime.Now));
			}

			Console.WriteLine ("Saving...");
			DataHolder.FileDatabase.SaveBans (banList);
			Console.WriteLine ("Done!");
		}
	}
}
