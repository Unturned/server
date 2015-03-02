using ModLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using System.Diagnostics;

namespace CommandHandler
{
    public class CommandHandlerMain : MonoBehaviour
    {
		NetworkChat networkChat = UnityEngine.Object.FindObjectOfType(typeof(NetworkChat)) as NetworkChat;

		private static readonly string ADMIN_LIST_URL = @"config/admins.txt";

        public static bool usingHiddenChat = false;
        public static string serverName = "ZombieLand v" + Database.SERVER_VERSION;

        public void Start() {
			if (File.Exists(ADMIN_LIST_URL)) { //also read former admin file
				string[] adminLines = System.IO.File.ReadAllLines(ADMIN_LIST_URL);

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

			Assembly assembly = Assembly.GetExecutingAssembly();
			FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
			string version = fvi.FileVersion;

            UserList.NetworkUsers.Add(new NetworkUser(serverName + "-" + version, "", "", "ServerID", 21, 80, Network.player));
        }

        public void OnGUI() {
		}

        public void Update()
        {
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
			List<string> words = commandParams.ToList<string>();
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

		/// <summary>
		/// Removing ModLoader modules
		/// </summary>
		/// <param name="player">Player.</param>
		private static void removeMods(NetworkPlayer player)
		{
			CommandList.commands.Clear();
			FindObjectOfType<ModManager>().RemoveMods();
			Reference.Tell(player, "Server modules removed...");
		}

		/// <summary>
		/// Loading modules
		/// </summary>
		/// <param name="player">Player.</param>
		private static void loadMods(NetworkPlayer player) 
		{
			CommandList.commands.Clear();
			FindObjectOfType<ModManager>().RemoveMods();
			FindObjectOfType<ModManager>().LoadDefaultMods();
			Reference.Tell(player, "Mods loaded.");
		}

        public static bool ExecuteCommand(BetterNetworkUser sender, string commandString, List<String> parms)
        {
            string cmdText = commandString.ToLower();
			if (cmdText == "unload")
			{
				if (UserList.getPermission(sender.steamid) == 10) {
					removeMods(sender.networkPlayer);
					return true;
				}
			}

			if (cmdText == "load")
			{
				if (UserList.getPermission(sender.steamid) == 10) {
					removeMods(sender.networkPlayer);
					loadMods(sender.networkPlayer);
					return true;
				}
			}

            foreach (Command command in CommandList.commands)
            {
                if (command.HasAlias(cmdText))
                {
                    if (command.permission > UserList.getPermission(sender.steamid))
                    {
						if ( command.permission == 1 )
						{
							Reference.Tell(sender.networkPlayer, "You should be VIP/Donator to use this command.");
						}
						else
						{
                        	Reference.Tell(sender.networkPlayer, "You are not allowed to use that command. (Need lv." + command.permission + "/" + UserList.getPermission(sender.steamid) + ")");
						}
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
            Reference.Tell(sender.networkPlayer, "Command \"/" + commandString + "\" was not found");
            return false;
        }


        private static void LogCommand(string p)
        {
            StreamWriter file = new StreamWriter("logs/commands.log", true);
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
