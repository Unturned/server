using CommandHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections;

namespace AdminCommands
{
	public class AdminCommands : MonoBehaviour
	{
		public System.Timers.Timer itemsTimer;
        public System.Timers.Timer announceTimer;
		public int announceIndex = 0;
		
        public int itemsResetIntervalInSeconds = 8 * 60; // 5 mins respawn time
		public int announceIntervalInSeconds = 600;
		public string[] AnnounceMessages;

        public bool usePlayerHomes = true;
		private Dictionary<string, Vector3> frozenPlayers = new Dictionary<string, Vector3> ();
		private Dictionary<string, Vector3> vanishedPlayers = new Dictionary<string, Vector3> ();

		private static System.Random random = new System.Random ((int)System.DateTime.Now.Ticks);

		public void Start ()
		{
            new CarCommands();
            new StatCommands();
            new CreditCommand();
            new KitCommands();
            new BanCommands();
			new AICommands();
			new BuildCommands();	

			Command saveCommand = new Command(8, new CommandDelegate(this.SaveAll), new String[] {
				"s",
				"save",
			});
			saveCommand.description = "Instant saves everything";
			CommandList.add(saveCommand);
			
			Command onlineCommand = new Command (0, new CommandDelegate (this.Online), new string[]
			{
				"online"
			});
			onlineCommand.description = ("Shows the number of online players");
			CommandList.add (onlineCommand);
			
			Command timeCommand = new Command (0, new CommandDelegate (this.Time), new string[]
			{
				"time"
			});
			timeCommand.description =  ("Shows in-game time");
			CommandList.add (timeCommand);

			
			Command announceCommand = new Command (6, new CommandDelegate (this.Announce), new string[]
			{
				"repeat",
				"announce",
				"ann"
			});
			announceCommand.description = ("Make the server announce something");
			CommandList.add (announceCommand);
			
			
			Command command8 = new Command (7, new CommandDelegate (this.ResetItems), new string[]
			{
				"resetitems"
			});
			command8.description = ("Deletes and respawns all items on the map");
			CommandList.add (command8);
			Command command11 = new Command (4, new CommandDelegate (this.SirensOn), new string[]
			{
				"sirens"
			});
			command11.description = ("Activates all sirens on vehicles who have it");
			CommandList.add (command11);
			Command command12 = new Command (7, new CommandDelegate (this.SirensOff), new string[]
			{
				"sirensoff"
			});
			command12.description = ("Deactivates all sirens");
			CommandList.add (command12);



			Command command16 = new Command (7, new CommandDelegate (this.SetItemsDelay), new string[]
			{
				"setitemsdelay"
			});
			command16.description = ("Sets the delay in seconds on which items reset");
			CommandList.add (command16);
			Command command17 = new Command (7, new CommandDelegate (this.SetAnnounceDelay), new string[]
			{
				"setannouncedelay"
			});
			command17.description = ("Sets the delay in seconds on the server announcements");
			CommandList.add (command17);
			Command command22 = new Command (6, new CommandDelegate (this.TeleportToPlayer), new string[]
			{
				"tp"
			});
			command22.description = ("Teleport to a player");
			CommandList.add (command22);
			Command command23 = new Command (8, new CommandDelegate (this.TeleportToMe), new string[]
			{
				"tptome"
			});
			command23.description = ("Teleport a player to you");
			CommandList.add (command23);
			Command command24 = new Command (6, new CommandDelegate (this.TeleportToCoords), new string[]
			{
				"tpto"
			});
			command24.description =  ("Teleport to coordinates x y z  (y is height)");
			CommandList.add (command24);
			Command command25 = new Command (10, new CommandDelegate (this.TeleportAll), new string[]
			{
				"tpall"
			});
			command25.description =  ("Teleport all players to you");
			CommandList.add (command25);
			CommandList.add (new Command (8, new CommandDelegate (this.Kill), new string[]
			{
				"kill"
			}));
			CommandList.add (new Command (8, new CommandDelegate (this.Heal), new string[]
			{
				"heal"
			}));

			CommandList.add (new Command (10, new CommandDelegate (this.KickAll), new string[]{
				"kickall"
			}));
			
            // Car command moved

			Command command27 = new Command (8, new CommandDelegate (this.SpawnItem), new string[]
			{
				"i"
			});
			command27.description =  ("Drops an item on the ground");
			CommandList.add (command27);
			Command command28 = new Command (3, new CommandDelegate (this.Kick), new string[]
			{
				"kick"
			});
			command28.description =  ("Kick a player name");
			CommandList.add (command28);
			Command command29 = new Command (8, new CommandDelegate (this.GodMode), new string[]
			{
				"god"
			});
			command29.description =  ("Infinite-ish health");
			CommandList.add (command29);
			Command command30 = new Command (8, new CommandDelegate (this.FreezePlayer), new string[]
			{
				"freeze"
			});
			command30.description =  ("Stop a player from moving");
			CommandList.add (command30);
			CommandList.add (new Command (8, new CommandDelegate (this.UnFreezePlayer), new string[]
			{
				"unfreeze"
			}));
			Command command31 = new Command (8, new CommandDelegate (this.UnFreezeAll), new string[]
			{
				"unfreezeall"
			});
			command31.description =  ("Unfreeze all players");
			CommandList.add (command31);
			Command command33 = new Command (10, new CommandDelegate (this.Vanish), new string[]
			{
				"vanish"
			});
			command33.description =  ("Toggles invisibility");
			CommandList.add (command33);
			Command command34 = new Command (8, new CommandDelegate (this.lag), new string[]
			{
				"lag"
			});
			command34.description =  ("Blocks all player communication to the server");
			CommandList.add (command34);
			CommandList.add (new Command (8, new CommandDelegate (this.unlag), new string[]
			{
				"unlag"
			}));
			CommandList.add (new Command (8, new CommandDelegate (this.scarePlayer), new string[]
			{
				"scare"
			}));
			Command command35 = new Command (7, new CommandDelegate (this.MaxSkills), new string[]
			{
				"maxskills"
			});
			command35.description =  ("Give yourself 10000 experience");
			CommandList.add (command35);
			CommandList.add (new Command (8, new CommandDelegate (this.SaveAndCloseServer), new string[]
			{
				"restart"
			}));
			this.ReadConfigs ();
		}

		private void Online (CommandArgs args)
		{
			Reference.Tell (args.sender.networkPlayer, "There are " + UserList.NetworkUsers.Count + " players online.");
		}

		private void Time (CommandArgs args)
		{
			NetworkChat.sendAlert ("Time: " + Sun.getTime ());
		}

		private void Announce (CommandArgs args)
		{
			string parametersAsString = args.ParametersAsString;
			NetworkChat.sendAlert (parametersAsString);
		}

		private void ResetItems(CommandArgs args)
		{
			SpawnItems.reset ();
			NetworkChat.sendAlert(args.sender.name + " has respawned all items");
		}

		private void SirensOn(CommandArgs args)
		{
			Vehicle[] array = UnityEngine.Object.FindObjectsOfType (typeof(Vehicle)) as Vehicle[];
			Vehicle[] array2 = array;
			for (int i = 0; i < array2.Length; i++) {
				Vehicle vehicle = array2 [i];
				vehicle.networkView.RPC ("tellSirens", RPCMode.All, new object[]
				{
					true
				});
			}
		}

		private void SirensOff (CommandArgs args)
		{
			Vehicle[] array = UnityEngine.Object.FindObjectsOfType (typeof(Vehicle)) as Vehicle[];
			Vehicle[] array2 = array;
			for (int i = 0; i < array2.Length; i++) {
				Vehicle vehicle = array2 [i];
				vehicle.networkView.RPC ("tellSirens", RPCMode.All, new object[]
				{
					false
				});
			}
		}

		private void SetItemsDelay (CommandArgs args)
		{
			string value = args.Parameters[0];
			this.setItemResetIntervalInSeconds (System.Convert.ToInt32 (value));
		}

		private void SetAnnounceDelay (CommandArgs args)
		{
			string value = args.Parameters[0];
			StopCoroutine("AnnouncerCoroutine");
			this.setAnnounceIntervalInSeconds(System.Convert.ToInt32 (value));
			StartCoroutine("AnnouncerCoroutine");
		}

		private void TeleportToPlayer (CommandArgs args)
		{
			string parametersAsString = args.ParametersAsString;
			int index = 0;
			BetterNetworkUser betterNetworkUser;
			if (parametersAsString.Length < 3 && int.TryParse (parametersAsString, out index)) {
				betterNetworkUser = UserList.users[index];
			} else {
				betterNetworkUser = UserList.getUserFromName (parametersAsString);
			}
			if (betterNetworkUser != null) {
				this.teleportUserTo (args.sender, betterNetworkUser.position, betterNetworkUser.rotation);
			}
		}

		private void TeleportToMe (CommandArgs args)
		{
			string parametersAsString = args.ParametersAsString;
			int index = 0;
			BetterNetworkUser user;
			if (parametersAsString.Length < 3 && int.TryParse (parametersAsString, out index)) {
				user = UserList.users[index];
			} else {
				user = UserList.getUserFromName (parametersAsString);
			}
			this.teleportUserTo (user, args.sender.position, args.sender.rotation);
		}

		private void TeleportToCoords (CommandArgs args)
		{
			float num = float.Parse (args.Parameters[0]);
			float num2 = float.Parse (args.Parameters[1]);
			float num3 = float.Parse (args.Parameters[2]);
			this.teleportUserTo (args.sender, new Vector3 (num, num2, num3), Quaternion.identity);
		}

		private void TeleportAll (CommandArgs args)
		{
			Vector3 position = args.sender.position;
			Quaternion rotation = args.sender.player.gameObject.transform.rotation;
			foreach (BetterNetworkUser current in UserList.users) {
				this.teleportUserTo (current, position, rotation);
			}
		}

		private void Kill (CommandArgs args) {
			string parametersAsString = args.ParametersAsString;
			UserList.getUserFromName(parametersAsString).player.gameObject.GetComponent<Life>().damage(500, "You died of retardedness");
		}
		
		private void HealPlayer(GameObject playerObject) {
			Life life = playerObject.GetComponent<Life>();
				life.heal(100, true, true);
				life.eat(100);
				life.drink(100);
				life.disinfect(100);
		}
		
		/**
		 * Improoved healing
		 */
		private void Heal(CommandArgs args) {
			if (args.Parameters.Count == 0) {
				// Health
				GameObject playerObject = args.sender.player.gameObject;
				HealPlayer(playerObject);
			} else {
				string parametersAsString = args.ParametersAsString;
				HealPlayer(UserList.getUserFromName(parametersAsString).player.gameObject);
			}
		}

		private void Promote (CommandArgs args)
		{
			string parametersAsString = args.ParametersAsString;
			BetterNetworkUser userFromName = UserList.getUserFromName (parametersAsString);
			UserList.promote (userFromName.steamid);
			Reference.Tell (args.sender.networkPlayer, parametersAsString + " has been promoted to level " + UserList.getPermission(userFromName.steamid));
			Reference.Tell (userFromName.networkPlayer, "You have been promoted to level " + UserList.getPermission (userFromName.steamid));
		}

		private void ShowAdminCommands (CommandArgs args)
		{
			string text = " /ban, /kick, /repeat, /reason, /resetitems, /repairvehicles, /refuelvehicles";
			string text2 = " /resetzombies, /killzombies, /kill, /setitemsdelay, /enablewhitelist";
			string text3 = " /disablewhitelist, /whitelist add & remove, /unban, /reloadCommands, ";
			string text4 = " /tptome <playername>, /promote, /online, /heal, /tp, /tpto <x> <y> <z>";
			string text5 = " /i <itemid> <amount>, /car, /kit /sethome, /home  (if enabled)";
			Reference.Tell (args.sender.networkPlayer, text);
			Reference.Tell (args.sender.networkPlayer, text2);
			Reference.Tell (args.sender.networkPlayer, text3);
			Reference.Tell (args.sender.networkPlayer, text4);
			Reference.Tell (args.sender.networkPlayer, text5);
		}

		private void TeleportCar (CommandArgs args)
		{
			/*AdminCommands.<>c__DisplayClass24 <>c__DisplayClass = new AdminCommands.<>c__DisplayClass24();
			Vector3 position = args.sender().position();
			<>c__DisplayClass.rotation = args.sender().rotation();
			<>c__DisplayClass.newPos = new Vector3(position.Item(0) + 5f, position.Item(1) + 50f, position.Item(2));
			if (args.Parameters().Count == 0)
			{
				Vehicle[] array = UnityEngine.Object.FindObjectsOfType(typeof(Vehicle)) as Vehicle[];
				int num = UnityEngine.Random.Range(0, array.Length);
				Vehicle vehicle = array[num];
				vehicle.transform().set_position(<>c__DisplayClass.newPos);
			}
			else
			{
				AdminCommands.<>c__DisplayClass26 <>c__DisplayClass2 = new AdminCommands.<>c__DisplayClass26();
				<>c__DisplayClass2.CS$<>8__locals25 = <>c__DisplayClass;
				<>c__DisplayClass2.cartype = args.Parameters()[0];
				if (!<>c__DisplayClass2.cartype.Contains("_"))
				{
					AdminCommands.<>c__DisplayClass26 expr_ED = <>c__DisplayClass2;
					expr_ED.cartype += "_0";
				}
				GameObject gameObject= (GameObject)typeof(SpawnVehicles).GetFields()[0].GetValue(null);
				try
				{
					if (gameObject.transform().FindChild("models").childCount() >= Loot.getCars() - 1)
					{
						int num2 = UnityEngine.Random.Range(0, Loot.getCars());
						Transform child = gameObject.transform().FindChild("models").GetChild(num2);
						Network.RemoveRPCs(child.networkView().viewID());
						Network.Destroy(child.networkView().viewID());
					}
				}
				catch
				{
				}
				System.Threading.Timer timer = new System.Threading.Timer(delegate(object obj)
				{
					SpawnVehicles.create(<>c__DisplayClass2.cartype, 100, 100, <>c__DisplayClass2.CS$<>8__locals25.newPos, <>c__DisplayClass2.CS$<>8__locals25.rotation * Quaternion.Euler(-90f, 0f, 0f), new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f)));
					SpawnVehicles.save();
				}, null, 400, -1);
				Reference.Tell(args.sender().networkPlayer(), "Creating " + <>c__DisplayClass2.cartype);
			}*/
            Player player = args.sender.player;
            SpawnVehicles.createNew("policeCar_0", 
                player.transform.position + 
                new Vector3(0.1f, 0.1f, 0f),
                player.transform.rotation * Quaternion.Euler(-90f, 0f, 0f));

            Reference.Tell(args.sender.networkPlayer, "Adding a police car for you!");
		}

		private void SpawnItem (CommandArgs args)
		{
			int num = System.Convert.ToInt32 (args.Parameters[0]);
			int num2 = 1;
			if (args.Parameters.Count == 2) {
				num2 = System.Convert.ToInt32 (args.Parameters[1]);
			}
			Vector3 position = args.sender.position;
			for (int i = 0; i < num2; i++) {
				SpawnItems.spawnItem (num, 1, position);
			}
		}

		private void Kick (CommandArgs args) {
			string parametersAsString = args.ParametersAsString;
			this.Kick (parametersAsString, "You were kicked off the server");
		}

		private void GodMode (CommandArgs args) {
			Life component = args.sender.player.gameObject.GetComponent<Life>();
			if (args.Parameters!= null && args.Parameters.Count > 0 && args.ParametersAsString.ToLower ().Equals ("off")) {
				component.networkView.RPC ("tellAllLife", RPCMode.All, new object[]
				{
					100,
					100,
					100,
					100,
					false,
					false
				});
				component.damage (1, "poke");
			} else {
				component.networkView.RPC ("tellAllLife", RPCMode.All, new object[]
				{
					10000,
					100,
					100,
					100,
					false,
					false
				});
				component.damage (1, "poke");
			}
		}

		private void FreezePlayer (CommandArgs args) {
			string parametersAsString = args.ParametersAsString;
			BetterNetworkUser userFromName = UserList.getUserFromName (parametersAsString);
			if (userFromName != null) {
				Player player = userFromName.player;
				this.frozenPlayers.Add (userFromName.steamid, player.transform.position);
			}
			Reference.Tell (args.sender.networkPlayer, "Froze the player " + userFromName.name);
		}

		private void UnFreezePlayer (CommandArgs args) {
			string parametersAsString = args.ParametersAsString;
			BetterNetworkUser userFromName = UserList.getUserFromName (parametersAsString);
			if (userFromName != null) {
				this.frozenPlayers.Remove (userFromName.steamid);
			}
			Reference.Tell (args.sender.networkPlayer, "Unfroze the player " + userFromName.name);
		}

		private void UnFreezeAll (CommandArgs args) {
			this.frozenPlayers.Clear ();
		}

		private void Vanish (CommandArgs args)
		{
			try {
				this.vanishedPlayers.Add (args.sender.steamid, new Vector3 (0f, 0f, 0f));
                args.sender.player.gameObject.networkView.group = 1;
				Reference.Tell (args.sender.networkPlayer, "You have vanished :D");
			} catch {
				this.vanishedPlayers.Remove(args.sender.steamid);
                args.sender.player.gameObject.networkView.group = 0;
				Reference.Tell (args.sender.networkPlayer, "You are visible again.");
			}
		}

		private void lag (CommandArgs args)
		{
			string parametersAsString = args.ParametersAsString;
			BetterNetworkUser userFromName = UserList.getUserFromName (parametersAsString);
			Network.SetReceivingEnabled (userFromName.networkPlayer, 0, false);
			Reference.Tell (args.sender.networkPlayer, "You are now lagging the shit out of " + userFromName.name);
		}

		private void unlag (CommandArgs args)
		{
			string parametersAsString = args.ParametersAsString;
			BetterNetworkUser userFromName = UserList.getUserFromName (parametersAsString);
			Network.SetReceivingEnabled (userFromName.networkPlayer, 0, true);
			Reference.Tell (args.sender.networkPlayer, "You are no longer lagging the shit out of " + userFromName.name);
		}

		private void scarePlayer (CommandArgs args)
		{
			string parametersAsString = args.ParametersAsString;
			BetterNetworkUser userFromName = UserList.getUserFromName (parametersAsString);
			Vector3 position = userFromName.position;
			for (int i = 0; i < 100; i++) {
				NetworkSounds.askSound ("Sounds/Vehicles/horn", position, 50f, 1f, 20f);
			}
		}

		private void MaxSkills (CommandArgs args)
		{
			args.sender.player.GetComponent<Skills> ().learn (10000);
		}

		private void SaveAndCloseServer (CommandArgs args)
		{
			CommandList.issueCommand ("save", args.sender);
			Application.Quit ();
		}

		private void teleportUserTo (BetterNetworkUser user, Vector3 targetposition, Quaternion targetrotation)
		{
			user.position = targetposition;
			user.player.gameObject.GetComponent<Life> ().networkView.RPC ("tellStatePosition", RPCMode.All, new object[]
			{
				targetposition,
				targetrotation
			});
			user.player.gameObject.GetComponent<NetworkInterpolation> ().tellStatePosition_Pizza (targetposition, targetrotation);
		}

        private void announceNext ()
		{
			for (int i = this.announceIndex; i < this.AnnounceMessages.Length; i++) {
				string text = this.AnnounceMessages [i];
				if (text.Equals (":")) {
					this.announceIndex = i + 1;
					return;
				}
				NetworkChat.sendAlert (text);
			}
			this.announceIndex = 0;
		}

		public void setAnnounceIntervalInSeconds (int seconds)
		{
			this.announceIntervalInSeconds = seconds;
			this.announceTimer.Stop ();
			this.announceTimer.Interval = ((double)(seconds * 1000));
			this.announceTimer.Start ();
		}

		public void setItemResetIntervalInSeconds (int seconds)
		{
			this.itemsResetIntervalInSeconds = seconds;
			this.itemsTimer.Stop ();
			this.itemsTimer.Interval = ((double)(seconds * 1000));
			this.itemsTimer.Start ();
		}

		public void resetItems ()
		{
			SpawnItems.reset ();
		}

		private string RandomString (int size)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder ();
			for (int i = 0; i < size; i++) {
				char value = System.Convert.ToChar (System.Convert.ToInt32 (System.Math.Floor (26.0 * AdminCommands.random.NextDouble () + 65.0)));
				stringBuilder.Append (value);
			}
			return stringBuilder.ToString ();
		}

		private void Log (string p)
		{
			System.IO.StreamWriter streamWriter = new System.IO.StreamWriter ("Unturned_Data/Managed/mods/AdminCommands_Log.txt", true);
			streamWriter.WriteLine (p);
			streamWriter.Close ();
		}

		private void Kick (string name, string reason)
		{
			BetterNetworkUser userFromName = UserList.getUserFromName (name);
			NetworkTools.kick (userFromName.networkPlayer, reason);
		}

		public void Update () {
			if (this.frozenPlayers.Count > 0) {
				foreach (System.Collections.Generic.KeyValuePair<string, Vector3> current in this.frozenPlayers) {
					BetterNetworkUser userFromSteamID = UserList.getUserFromSteamID (current.Key);
					if (userFromSteamID != null) {
						userFromSteamID.player.transform.position = current.Value;
						userFromSteamID.player.networkView.RPC ("tellStatePosition", userFromSteamID.player.networkView.owner, new object[]
						{
							current.Value,
							userFromSteamID.player.transform.rotation
						});
					}
				}
			}
			
			if (this.vanishedPlayers.Count > 0) {
				foreach (System.Collections.Generic.KeyValuePair<string, Vector3> current in this.vanishedPlayers) {
					BetterNetworkUser userFromSteamID = UserList.getUserFromSteamID (current.Key);
					if (userFromSteamID != null) {
						NetworkPlayer[] connections = Network.connections;
						for (int i = 0; i < connections.Length; i++) {
							NetworkPlayer networkPlayer = connections [i];
							if (Network.player != networkPlayer && networkPlayer != userFromSteamID.networkPlayer) {
								userFromSteamID.player.networkView.RPC ("tellStatePosition", networkPlayer, new object[] {
									new Vector3 (0f, 0f, 0f),
									userFromSteamID.rotation
								});
							}
						}
					}
				}
			}
		}

		private void ReadConfigs () {
			/*System.IO.Directory.CreateDirectory ("Unturned_Data/Managed/mods/AdminCommands");
			if (!System.IO.File.Exists ("Unturned_Data/Managed/mods/AdminCommands/config.ini")) {
				IniFile iniFile = new IniFile ("Unturned_Data/Managed/mods/AdminCommands/config.ini");
				iniFile.IniWriteValue ("Config", "Using Whitelist", "false");
				iniFile.IniWriteValue ("Config", "Using Player Homes", "false");
				iniFile.IniWriteValue ("Config", "Show gui", "true");
				iniFile.IniWriteValue ("Config", "Show whitelist kick messages", "true");
				iniFile.IniWriteValue ("Security", "Using_console", "true");
				iniFile.IniWriteValue ("Security", "Console_password", this.RandomString (8));
				iniFile.IniWriteValue ("Security", "Require_command_confirmation", "false");
				iniFile.IniWriteValue ("Timers", "Time between item respawns in seconds", "2700");
				iniFile.IniWriteValue ("Timers", "Time between announces in seconds", "600");
			}
			IniFile iniFile2 = new IniFile ("Unturned_Data/Managed/mods/AdminCommands/config.ini");
			if (iniFile2.IniReadValue ("Config", "Show whitelist kick messages").Equals ("")) {
				iniFile2.IniWriteValue ("Config", "Show whitelist kick messages", "true");
			}
			if (iniFile2.IniReadValue ("Security", "Using_console").Equals ("")) {
				iniFile2.IniWriteValue ("Security", "Using_console", "true");
			}
			if (iniFile2.IniReadValue ("Security", "Console_password").Equals ("")) {
				iniFile2.IniWriteValue ("Security", "Console_password", this.RandomString (8));
			}
			if (iniFile2.IniReadValue ("Security", "Require_command_confirmation").Equals ("")) {
				iniFile2.IniWriteValue ("Security", "Require_command_confirmation", "false");
			}*/
			
            this.usePlayerHomes = true;
            this.itemsResetIntervalInSeconds = 2700;
            this.announceIntervalInSeconds = 600;

			if (!System.IO.File.Exists ("Unturned_Data/Managed/mods/AdminCommands/UnturnedAnnounces.txt")) {
				System.IO.StreamWriter streamWriter = new System.IO.StreamWriter ("Unturned_Data/Managed/mods/AdminCommands/UnturnedAnnounces.txt", true);
				streamWriter.WriteLine ("This line will be announced 10 minutes after injecting (or whatever you change the interval to)");
				streamWriter.WriteLine ("This line will be announced at the same time");
				streamWriter.WriteLine (":");
				streamWriter.WriteLine ("This line will be announced 20 minutes after injecting  (2x interval)");
				streamWriter.WriteLine (":");
				streamWriter.WriteLine (":");
				streamWriter.WriteLine ("This line will be announced 40 minutes after injecting  (4x interval)");
				streamWriter.WriteLine ("And so forth.. then it will go back to the 1st line      (4x interval)");
				streamWriter.Close ();
			}
			
			string[] array3 = File.ReadAllLines("Unturned_Data/Managed/mods/AdminCommands/UnturnedAnnounces.txt");
			this.AnnounceMessages = new string[array3.Length];
			
			for (int i = 0; i < array3.Length; i++) {
				this.AnnounceMessages [i] = array3 [i];
			}

			StartCoroutine("ItemRespawnCoroutine");
			StartCoroutine("AnnouncerCoroutine");
		}

		private IEnumerator AnnouncerCoroutine()
		{
			while (true)
			{
				yield return new WaitForSeconds(announceIntervalInSeconds);
				this.announceNext ();
			}
		}

		private IEnumerator ItemRespawnCoroutine()
		{
			Debug.Log("Item respawn coroutine started!");
			while (true)
			{
				yield return new WaitForSeconds(this.itemsResetIntervalInSeconds);
				NetworkChat.sendChat("Respawning items...");
				this.resetItems();
			}
		}
		
		public void SaveAll(CommandArgs args) {
			NetworkChat.sendChat("Saving world to database...");
            NetworkTools.save();
		}

		public void KickAll(CommandArgs args) 
		{
			foreach (NetworkUser user in NetworkUserList.users)
			{
				Debug.LogError("Releasing all connection!");
				Network.CloseConnection(user.player, false);
			}
		}
	}
}
