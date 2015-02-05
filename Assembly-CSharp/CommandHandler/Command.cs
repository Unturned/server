using System;
using System.Collections.Generic;

namespace CommandHandler
{

	public delegate void CommandDelegate(CommandArgs args);

    public class Command
    {
        /// <summary>
        /// Gets or sets a description of this command.
        /// </summary>
        public string description {
			get;
			set;
		}

        /// <summary>
        /// Gets all the aliases of the command.
        /// </summary>
        public List<string> Names {
			get;
			protected set;
		}

        /// <summary>
        /// Gets the permission level requirement of the command.
        /// </summary>
        public int permission {
			get;
			protected set;
		}


        private CommandDelegate _commandDelegate;

		public CommandDelegate CommandDelegate
		{
			get { return _commandDelegate; }
			set
			{
				if (value == null)
					throw new ArgumentNullException();

				_commandDelegate = value;
			}
	 	}

        public Command(CommandDelegate method, params string[] names)
        {
            Names = new List<string>(names);
            Names = Names.ConvertAll(d => d.ToLower());
            CommandDelegate = method;
            description = "No description available";
            permission = 10;
        }

        public Command(int permissionLevelRequired,CommandDelegate method, params string[] names)
        {
            Names = new List<string>(names);
            Names = Names.ConvertAll(d => d.ToLower());
            CommandDelegate = method;
            description = "No description available";
            permission = permissionLevelRequired;
        }

        public bool HasAlias(string name)
        {
            return Names.Contains(name);
        }

    }


    public class CommandArgs : EventArgs
    {
        /// <summary>
        /// Returns the command that was used
        /// </summary>
		string commandString;
        public string CommandString {
			get {
				return commandString;
			}
			private set {
				commandString = value;
			}
		}

        /// <summary>
        /// Returns the sender of the command as a NetworkUser.
        /// </summary>
		BetterNetworkUser sender2;
        public BetterNetworkUser sender {
			get {
				return sender2;
			}
			private set {
				sender2 = value;
			}
		}

        /// <summary>
        /// Parameters passed to the arguement. Does not include the command name.
        /// IE '/kick Nessin' will only have 1 argument
        /// </summary>
		List<string> parameters;
        public List<string> Parameters {
			get {
				return parameters;
			}
			private set {
				parameters = value;
			}
		}

        /// <summary>
        /// Returns all the parameters pasted on eachother with a space between. Useful for getting playernames
        /// </summary>
        public String ParametersAsString
        {
            get
            {
                String parmstring = "";
                foreach (String parm in Parameters)
                {
                    parmstring += parm + " ";
                }
                return parmstring.TrimEnd(' ');
            }
        }


        public CommandArgs(string message, BetterNetworkUser sender, List<string> args)
        {
            CommandString = message;
            this.sender = sender;
            Parameters = args;
        }
    }

}
