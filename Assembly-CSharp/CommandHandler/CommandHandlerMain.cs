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
        NetworkChat networkChat = UnityEngine.Object.FindObjectOfType(typeof(NetworkChat)) as NetworkChat;

        public static bool usingHiddenChat = false;
        public static string serverName = "ZombieLand v2.4";

        public void Start() {
            if (File.Exists("Unturned_Data/Managed/mods/AdminCommands/UnturnedAdmins.txt")) { //also read former admin file
                string[] adminLines = System.IO.File.ReadAllLines(@"Unturned_Data/Managed/mods/AdminCommands/UnturnedAdmins.txt");

                for (int i = 0; i < adminLines.Length; i++)
                {
                    if (adminLines[i].Length > 10)
                    {

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
			
            UserList.NetworkUsers.Add(new NetworkUser(serverName, "", "", "ServerID", 21, 80, Network.player));
        }

        public void OnGUI() {
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
        private static void handleCommandText(String command, int type, BetterNetworkUser sender) {
            command = command.Remove(0, 1);

            String[] commandParams = command.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
			List<string> words = new List<string>();


            string cmdText = words.First();
            words.RemoveAt(0);
            
            DateTime myDate = DateTime.Now;
            string myDateString = myDate.ToString("yyyy-MM-dd HH:mm:ss");

			LogCommand(myDateString + ": " + sender.name + ": " + command);

            if (!ExecuteCommand(sender, cmdText, words)) //Command was not executed
            {
                if (!usingHiddenChat)
                    NetworkChat.sendAlert("");  //avoid looping in commands
            }
        }

        private void checkChatForCommands() {
            /*if (getLastMessageText().StartsWith("/"))
            { 
                BetterNetworkUser sender = UserList.getUserFromName(getLastMessagePlayerName());
                String commando = getLastMessageText();
                int type = Convert.ToInt32(getLastMessageType());

                handleCommandText(commando, type, sender); 
            }*/
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

        private NetworkChat getNetworkChat() {         
            if (networkChat == null)
            	networkChat = UnityEngine.Object.FindObjectOfType(typeof(NetworkChat)) as NetworkChat;
			
            return networkChat;
        }

    }
}
