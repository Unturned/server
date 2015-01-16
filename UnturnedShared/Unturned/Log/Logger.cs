using System;
using System.IO;
using UnityEngine;

public class Logger {
    private readonly static String DATE_PATTERN = "MM-dd-yy HH:mm:ss";

    static Logger() 
    {
        CreateFile(@"Unturned_Data/Connections.txt");
        CreateFile(@"Unturned_Data/Database.txt");
        CreateFile(@"Unturned_Data/Bans.txt");
        CreateFile(@"Unturned_Data/Incident.txt");
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

    public static void LogSecurity(NetworkPlayer player, String incident) 
    {
        StreamWriter file = new StreamWriter(@"Unturned_Data/Incident.txt", true);
        if ( player == null ) 
        {
            file.WriteLine("{0:" + DATE_PATTERN + "} User: {1} ({2}) IP: {3} {4}",
                           System.DateTime.Now,
                           "NullPllayer",
                           "?",
                           player.ipAddress,
                           incident
                           );
        }

        try 
        {
            file.WriteLine("{0:" + DATE_PATTERN + "} User: {1} ({2}) IP: {3} {4}",
                           System.DateTime.Now,
                           "?", // user.name,
                           "?", //user.id,
                           player.ipAddress,
                           incident
                           );
        } 
        catch 
        {
            file.WriteLine("{0:" + DATE_PATTERN + "} User: {1} ({2}) IP: {3} {4}",
                           System.DateTime.Now,
                           "UserNotFound",
                           "??",
                           player.ipAddress,
                           incident
                           );
        }

        file.Flush();
        file.Close();
    }
}


