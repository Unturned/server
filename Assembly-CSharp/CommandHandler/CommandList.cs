using System.Collections.Generic;
using UnityEngine;

namespace CommandHandler {
	public class CommandList {

		public static List<Command> commands = new List<Command> ();

		public static void issueCommand (string alias)
		{
			foreach (Command command in commands) {
				if (command.HasAlias (alias)) {
					command.CommandDelegate (new CommandArgs (alias, UserList.getUserFromPlayer (Network.player), null));
					return;
				}
			}
		}

		public static void issueCommand (string alias, List<string> parms)
		{
			foreach (Command command in commands) {
				if (command.HasAlias (alias)) {
					command.CommandDelegate (new CommandArgs (alias, UserList.getUserFromPlayer (Network.player), parms));
					return;
				}
			}            
		}

		public static void issueCommand (string alias, BetterNetworkUser sender, List<string> parms)
		{
			CommandHandlerMain.ExecuteCommand (sender, alias, parms);
		}

		public static void issueCommand (string alias, BetterNetworkUser sender)
		{
			CommandHandlerMain.ExecuteCommand (sender, alias, null);
		}

		public static void add (Command command)
		{
			commands.Add (command);
		}

		public static void add (CommandDelegate method, params string[] names)
		{
			commands.Add (new Command (method, names));
		}

		public static void add (int permissionLevelRequired, CommandDelegate method, params string[] names)
		{
			commands.Add (new Command (permissionLevelRequired, method, names));
		}
	}
}
