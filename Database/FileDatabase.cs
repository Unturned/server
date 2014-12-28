using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

namespace Database {
	public class FileDatabase {
		private readonly static String DATE_PATTERN = "MM-dd-yy H:mm:ss";
		
		private readonly static String BAN_DATABASE_FILE = @"Unturned_Data/Database/Bans.txt";
		private readonly static String DATA_DIRECTORY = "Unturned_Data/Database";
		
		static FileDatabase () {
			if( !Directory.Exists(DATA_DIRECTORY) ) {
				Directory.CreateDirectory("Unturned_Data/Database");
			}
		}
		
		public static void SaveBans(Dictionary<String, NetworkBans> bans) {
			FileStream banFile = System.IO.FileStream(BAN_DATABASE_FILE, false);
			
			foreach (KeyValuePair<String, NetworkBans> pair in d) {
				banFile.WriteLine("{0}, {"+ DATE_PATTERN + "}", pair.Key, pair.Value);
			}
		}
		
		public static Dictionary<String, NetworkBans> LoadBans() {
			DateTime.TryParseExact(dateValue, DATE_PATTERN, null, DateTimeStyles.None, out parsedDate);
		}
	}
}

