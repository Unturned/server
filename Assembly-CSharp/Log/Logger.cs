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
        file.WriteLine("{0:" + DATE_PATTERN + "} {1}", System.DateTime.Now, banString);
        file.Close();
    }

    public static void LogSecurity(NetworkPlayer player, String incident) {
        NetworkUser user = NetworkUserList.getUserFromPlayer(player);
        StreamWriter file = new StreamWriter(@"Unturned_Data/Incident.txt", true);

        file.WriteLine("{0:" + DATE_PATTERN + "} User: {1} ({2}) IP: {3} {4}",
            System.DateTime.Now,
            user.name,
            user.id,
            player.ipAddress,
            incident
        );

        file.Flush();
        file.Close();
    }
}


