using System;
using System.IO;
using UnityEngine;

public class Logger : MonoBehaviour {
	private readonly static String DATE_PATTERN = "MM-dd-yy HH:mm:ss";
	
	public static void LogConnection(String connectString) {
		StreamWriter file = new StreamWriter(@"Unturned_Data/Connections.txt", true);
        file.WriteLine("{0:" + DATE_PATTERN + "} {1}", System.DateTime.Now, connectString);
        file.Close();
	}
	
	public static void LogBan(String banString) {
		StreamWriter file = new StreamWriter(@"Unturned_Data/Bans.txt", true);
        file.WriteLine("{0:" + DATE_PATTERN + "} {1}", System.DateTime.Now);
        file.Close();
	}

		public static void LogSecurity(NetworkPlayer player, String incident) {
				// TODO: log out
		}
}


