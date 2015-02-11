using System;
using System.IO;
using UnityEngine;

namespace Unturned.Log {
	public class Logger {
	    private readonly static String DATE_PATTERN = "MM-dd-yy HH:mm:ss";

	    static Logger() 
	    {
	        CreateFile(@"Unturned_Data/Connections.txt");
	        CreateFile(@"Unturned_Data/Database.txt");
	        CreateFile(@"Unturned_Data/Bans.txt");
	        CreateFile(@"Unturned_Data/Incident.txt");
	        CreateFile(@"Unturned_Data/Instantiate.log");
	    }

	    private static void CreateFile(String path)
	    {
	        if (!File.Exists(path))
	        {
	            File.Create(path);
	        }
	    }

	    public static void LogConnection(String connectString) 
	    {
	        StreamWriter file = new StreamWriter(@"Unturned_Data/Connections.txt", true);
	        file.WriteLine("{0:" + DATE_PATTERN + "} {1}", System.DateTime.Now, connectString);
	        file.Close();
	    }

	    public static void LogDatabase(String databaseLog) 
	    {
	        StreamWriter file = new StreamWriter(@"Unturned_Data/Database.txt", true);
	        file.WriteLine("{0:" + DATE_PATTERN + "} {1}", System.DateTime.Now, databaseLog);
	        file.Close();
	    }

	    public static void LogBan(String banString) 
	    {
	        StreamWriter file = new StreamWriter(@"Unturned_Data/Bans.txt", true);
	        file.WriteLine("{0:" + DATE_PATTERN + "} {1}", System.DateTime.Now, banString);
	        file.Close();
	    }

	    public static void LogSecurity(String steamId, String nick, String incident) 
	    {
	        StreamWriter file = new StreamWriter(@"Unturned_Data/Incident.txt", true);
			file.WriteLine("{0:" + DATE_PATTERN + "} User: {1} ({2}) {3}",
	                           System.DateTime.Now,
	                           nick,
	                           steamId,
	                           incident
	                           );
	        file.Flush();
	        file.Close();
	    }

	    public static void Instantiate(String logString) 
	    {
	        StreamWriter file = new StreamWriter(@"Unturned_Data/Insatntiate.log", true);
	        file.WriteLine("{0:" + DATE_PATTERN + "} {1}", System.DateTime.Now, logString);
	        file.Close();
	    }
	}
}