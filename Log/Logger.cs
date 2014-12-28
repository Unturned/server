using System;
using System.IO;
using UnityEngine;

public class Logger : MonoBehaviour
{
	public static void LogConnection(String connectString) {
		System.IO.StreamWriter file = new StreamWriter(@"Unturned_Data/Connections.txt", true);
        file.WriteLine(connectString);
        file.Close();
	}
	
	public static void LogBan(String banString) {
		System.IO.StreamWriter file = new StreamWriter(@"Unturned_Data/Bans.txt", true);
        file.WriteLine(banString);
        file.Close();
	}
}


