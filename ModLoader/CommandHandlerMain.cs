using Ini;
using ModLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CommandHandler
{
    public class CommandHandlerMain : MonoBehaviour
    {
        public FieldInfo[] networkChatfields = typeof(NetworkChat).GetFields();
        public static String lastUsedCommand = "None";
        NetworkChat networkChat = UnityEngine.Object.FindObjectOfType(typeof(NetworkChat)) as NetworkChat;

        Dictionary<String, int> userPermissions = new Dictionary<String, int>();

        public static bool usingHiddenChat = false;
        public static string serverName = "GW-Systems admin v1.0";


        public void Start() {
            IniFile tempIni;
            if (!File.Exists("Unturned_Data/Managed/mods/UserPermissionLevels.ini")) { //create config file
                tempIni = new IniFile("Unturned_Data/Managed/mods/UserPermissionLevels.ini");
				tempIni.IniWriteValue("PermissionLevels", "; Example:", "");
				tempIni.IniWriteValue("PermissionLevels", ";76561197976976379", "10");
            }

            if (File.Exists("Unturned_Data/Managed/mods/AdminCommands/UnturnedAdmins.txt")) { //also read former admin file
                string[] adminLines = System.IO.File.ReadAllLines(@"Unturned_Data/Managed/mods/AdminCommands/UnturnedAdmins.txt");

                for (int i = 0; i < adminLines.Length; i++)
                {
                    if (adminLines[i].Length > 10)
                    {
                        tempIni = new IniFile("Unturned_Data/Managed/mods/UserPermissionLevels.ini");

                        tempIni.IniWriteValue("PermissionLevels", adminLines[i].Split(':')[1], "10");
                    }
                }
            }

            foreach (BetterNetworkUser user in UserList.users)
            {
                if (user.networkPlayer == Network.player)
                {
                    return;
                }
            }
            UserList.NetworkUsers.Add(new NetworkUser(serverName, "", "", "ServerID", 0, 0, Network.player));
        }




        public void OnGUI()
        {

        }

        public void Update()
        {
            //NetworkChat.sendAlert("CHATLISTENER UPDATE");

            try
            {
                if (!usingHiddenChat && Network.isServer)
                    checkChatForCommands();
            }
            catch (Exception e)
            {
                Loader.Log(e.Message);
                Loader.Log(e.StackTrace);

                Loader.Log(e.Source);
                Loader.Log(e.InnerException.Message);
            }

        }

        public static Boolean isCommand(string text, int type, NetworkPlayer networkplayer) {
            usingHiddenChat = true;
            if ( !text.StartsWith("/") ) {
                return false; //show the text sent
            } else {
                try
                {
                    handleCommandText(text, type, UserList.getUserFromPlayer(networkplayer));
                }
                catch (Exception e)
                {
                    Loader.Log(e.Message);
                }
                return true;  //dont show the text sent
            }

        }
		
		/**
		 * Trimmed the text
		 */
        private static void handleCommandText(String commando, int type, BetterNetworkUser sender) {
            lastUsedCommand = commando.Trim();
            commando = commando.Remove(0, 1);

            List<String> words = commando.Split(' ').ToList<String>();
            string cmdText = words.First();
            words.RemoveAt(0);
            
            DateTime myDate = DateTime.Now;
            string myDateString = myDate.ToString("yyyy-MM-dd HH:mm:ss");

			LogCommand(myDateString + ": " + sender + ": " + commando);

            if (!ExecuteCommand(sender, cmdText, words)) //Command was not executed
            {
                if (!usingHiddenChat)
                    NetworkChat.sendAlert("");  //avoid looping in commands
            }
        }

        private void checkChatForCommands() {
            if (getLastMessageText().StartsWith("/"))
            { 
                BetterNetworkUser sender = UserList.getUserFromName(getLastMessagePlayerName());
                String commando = getLastMessageText();
                int type = Convert.ToInt32(getLastMessageType());

                handleCommandText(commando, type, sender); 
            } 
        }


        public static bool ExecuteCommand(BetterNetworkUser sender, string commando, List<String> parms)
        {
            string cmdText = commando.ToLower();

            foreach (Command command in CommandList.commands)
            {
                if (command.HasAlias(cmdText))
                {
                    if (command.permission > UserList.getPermission(sender.steamid))
                    {
                        Reference.Tell(sender.networkPlayer, "You are not allowed to use that command. (Need lv." + command.permission + "/" + UserList.getPermission(sender.steamid) + ")");
                        return false;
                    }
                    if (!usingHiddenChat)
                        Reference.resetChat();
                    try
                    {
                        command.CommandDelegate(new CommandArgs(cmdText, sender, parms));
                    }
                    catch(Exception e)
                    {
                        Reference.Tell(sender.networkPlayer, "Something went wrong while executing your command;");
                        Reference.Tell(sender.networkPlayer, e.Message);
                    }
                    return true;
                }
            }
            Reference.Tell(sender.networkPlayer, "Command \"/" + commando + "\" was not found");
            return false;
        }


        private static void LogCommand(string p)
        {
            System.IO.StreamWriter file = new StreamWriter("Unturned_Data/Managed/mods/Commands_Log.txt", true);
            file.WriteLine(p);

            file.Close();
        }



        private String getLastMessagePlayerName()
        {
            return getNetworkChatFieldByNum(3);
        }

        private String getLastMessageGroup()
        {
            return getNetworkChatFieldByNum(5);
        }

        private String getLastMessageType()
        {
            return getNetworkChatFieldByNum(8);
        }

        private String getLastMessageText()
        {
            return getNetworkChatFieldByNum(6);
        }


        private String getNetworkChatFieldByNum(int num2)
        {
            try
            {
                return networkChatfields[num2].GetValue(getNetworkChat()).ToString();
            }
            catch
            {
                return "";
            }


        }

        private NetworkChat getNetworkChat()
        {         
            if (networkChat == null)
            networkChat = UnityEngine.Object.FindObjectOfType(typeof(NetworkChat)) as NetworkChat;
            return networkChat;
        }

    }
}
