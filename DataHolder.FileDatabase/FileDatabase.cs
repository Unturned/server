using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

namespace DataHolder {
	public class FileDatabase {
		private readonly static String DATE_PATTERN = "MM-dd-yy H:mm:ss";
		
		private readonly static String BAN_DATABASE_FILE = "Unturned_Data/Database/Bans.txt";
		private readonly static String DATA_DIRECTORY = "Unturned_Data/Database";
		
		static FileDatabase () {
			if( !Directory.Exists(DATA_DIRECTORY) ) {
				Directory.CreateDirectory("Unturned_Data/Database");
			}
		}
		
		public static void SaveBans(Dictionary<String, NetworkBanned> bans) {
			FileStream banFile = new FileStream(BAN_DATABASE_FILE, FileMode.Create);
			StreamWriter writer = new StreamWriter(banFile);
			
			foreach (KeyValuePair<String, NetworkBanned> pair in bans) {
				NetworkBanned bannedPlayer = pair.Value;
				writer.WriteLine("{0}, {1:"+ DATE_PATTERN + "}", 
				                 bannedPlayer.id,
				                 bannedPlayer.banTime );
			}
		}
		
		public static Dictionary<String, NetworkBanned> LoadBans() {
			//DateTime.TryParseExact(dateValue, DATE_PATTERN, null, DateTimeStyles.None, out parsedDate);
			return new Dictionary<String, NetworkBanned>();
		}
	}
}

