using CommandHandler;
using Ini;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Timers;
using UnityEngine;

namespace AdminCommands
{
	public class AdminCommands : MonoBehaviour
	{
		private int updater3 = 0;
		public string lastUsedCommand = "none";
		public System.Timers.Timer itemsTimer;
		public System.Timers.Timer announceTimer;
		public int announceIndex = 0;
		public int itemsResetIntervalInSeconds = 2700;
		public int announceIntervalInSeconds = 600;
		public string[] AnnounceMessages;
		public System.Collections.Generic.List<string> WhitelistedSteamIDs = new System.Collections.Generic.List<string> ();
		public bool usingWhitelist = false;
		public bool showWhiteListKickMessages = true;
		public bool usingGUI = true;
		public bool usePlayerHomes = true;
		public bool respawnVehicles = false;
		private System.Collections.Generic.Dictionary<string, Vector3> frozenPlayers = new System.Collections.Generic.Dictionary<string, Vector3> ();
		private System.Collections.Generic.Dictionary<string, Vector3> vanishedPlayers = new System.Collections.Generic.Dictionary<string, Vector3> ();
		private System.Collections.Generic.Dictionary<string, float> usedHomeCommand = new System.Collections.Generic.Dictionary<string, float> ();
		public string bigAssStringWithBannedPlayerNamesAndSteamIDs = "";
		private System.Collections.Generic.Dictionary<string, Vector3> playerHomes = new System.Collections.Generic.Dictionary<string, Vector3> ();
		private BetterNetworkUser userToBeBanned;
		private static System.Random random = new System.Random ((int)System.DateTime.Now.Ticks);

		public void Start ()
		{
			Command command = new Command (0, new CommandDelegate (this.Online), new string[]
			{
				"online"
			});
			command.description = ("Shows the number of online players");
			CommandList.add (command);
			Command command2 = new Command (0, new CommandDelegate (this.Time), new string[]
			{
				"time"
			});
			command2.description =  ("Shows in-game time");
			CommandList.add (command2);
			Command command3 = new Command (0, new CommandDelegate (this.SetHome), new string[]
			{
				"sethome"
			});
			command3.description =  ("Sets your home (if enabled)");
			CommandList.add (command3);
			Command command4 = new Command (0, new CommandDelegate (this.Home), new string[]
			{
				"home"
			});
			command4.description = ("Teleports to your home (if enabled)");
			CommandList.add (command4);
			Command command5 = new Command (6, new CommandDelegate (this.Announce), new string[]
			{
				"repeat",
				"announce",
				"ann"
			});
			command5.description = ("Make the server announce something");
			CommandList.add (command5);
			Command command6 = new Command (8, new CommandDelegate (this.Ban), new string[]
			{
				"ban"
			});
			command6.description = ("Ban a player. Will need confirmation with /reason");
			CommandList.add (command6);
			Command command7 = new Command (8, new CommandDelegate (this.ReasonForBan), new string[]
			{
				"reason",
				"r"
			});
			command7.description = ("Bans the player set with /ban");
			CommandList.add (command7);
			Command command8 = new Command (7, new CommandDelegate (this.ResetItems), new string[]
			{
				"resetitems"
			});
			command8.description = ("Deletes and respawns all items on the map");
			CommandList.add (command8);
			Command command9 = new Command (7, new CommandDelegate (this.RepairVehicles), new string[]
			{
				"repairvehicles"
			});
			command9.description = ("Repairs all vehicles");
			CommandList.add (command9);
			Command command10 = new Command (7, new CommandDelegate (this.RefuelVehicles), new string[]
			{
				"refuelvehicles"
			});
			command10.description = ("Fills the gas tank of all vehicles");
			CommandList.add (command10);
			Command command11 = new Command (7, new CommandDelegate (this.SirensOn), new string[]
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
			Command command13 = new Command (7, new CommandDelegate (this.ResetZombies), new string[]
			{
				"resetzombies"
			});
			command13.description = ("Deletes and respawns all zombies");
			CommandList.add (command13);
			Command command14 = new Command (7, new CommandDelegate (this.Killzombies), new string[]
			{
				"killzombies"
			});
			command14.description = ("Kills all zombies");
			CommandList.add (command14);
			Command command15 = new Command (7, new CommandDelegate (this.ReloadBans), new string[]
			{
				"reloadbans"
			});
			command15.description = ("Load bans from registry");
			CommandList.add (command15);
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
			Command command18 = new Command (8, new CommandDelegate (this.EnableWhitelist), new string[]
			{
				"enablewhitelist"
			});
			command18.description = ("Enables the whitelist");
			CommandList.add (command18);
			Command command19 = new Command (8, new CommandDelegate (this.DisableWhitelist), new string[]
			{
				"disablewhitelist"
			});
			command19.description = ("Disables the whitelist");
			CommandList.add (command19);
			Command command20 = new Command (6, new CommandDelegate (this.Whitelist), new string[]
			{
				"whitelist"
			});
			command20.description = ("Add or Remove  a player from the whitelist");
			CommandList.add (command20);
			Command command21 = new Command (6, new CommandDelegate (this.UnBan), new string[]
			{
				"unban"
			});
			command21.description = ("unbans a playername");
			CommandList.add (command21);
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
			Command command26 = new Command (6, new CommandDelegate (this.TeleportCar), new string[]
			{
				"car"
			});
			command26.description =  ("Teleport a random car or a specific car name");
			CommandList.add (command26);
			Command command27 = new Command (8, new CommandDelegate (this.SpawnItem), new string[]
			{
				"i"
			});
			command27.description =  ("Drops an item on the ground");
			CommandList.add (command27);
			CommandList.add (new Command (8, new CommandDelegate (this.SpawnKit), new string[]
			{
				"kit"
			}));
			Command command28 = new Command (7, new CommandDelegate (this.Kick), new string[]
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
			Command command32 = new Command (8, new CommandDelegate (this.RespawnVehicles), new string[]
			{
				"respawnvehicles"
			});
			command32.description =  ("Delets and respawns all vehicles");
			CommandList.add (command32);
			Command command33 = new Command (8, new CommandDelegate (this.Vanish), new string[]
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

		private void SetHome (CommandArgs args)
		{
			if (this.usePlayerHomes) {
				Vector3 position = args.sender.position;
				this.setHome(args.sender, position);
			} else {
				Reference.Tell(args.sender.networkPlayer, "PlayerHomes are not enabled on this server.");
			}
		}

		private void Home (CommandArgs args)
		{
			if (this.usePlayerHomes) {
				this.home (args.sender);
			} else {
				Reference.Tell(args.sender.networkPlayer, "PlayerHomes are not enabled on this server.");
			}
		}

		private void Announce (CommandArgs args)
		{
			string parametersAsString = args.ParametersAsString;
			NetworkChat.sendAlert (parametersAsString);
		}

		private void Ban (CommandArgs args)
		{
			string parametersAsString = args.ParametersAsString;
			int index = 0;
			if (parametersAsString.Length < 3 && int.TryParse(base.name, out index)) {
				this.userToBeBanned = UserList.users[index];
				string name = this.userToBeBanned.name;
				Reference.Tell (args.sender.networkPlayer, "Reason for banning " + name + " ?  /reason <reason> to ban");
			} else {
				this.userToBeBanned = UserList.getUserFromName (parametersAsString);
				string name = this.userToBeBanned.name;
				Reference.Tell (args.sender.networkPlayer, "Reason for banning " + name + " ?  /reason <reason> to ban");
			}
		}

		private void ReasonForBan (CommandArgs args)
		{
			string parametersAsString = args.ParametersAsString;
			if (args.Parameters.Count > 0) {
				this.Ban (this.userToBeBanned, parametersAsString);
			}
		}

		private void ResetItems(CommandArgs args)
		{
			SpawnItems.reset ();
			NetworkChat.sendAlert(args.sender.name + " has respawned all items");
		}

		private void RepairVehicles (CommandArgs args)
		{
			Vehicle[] array = UnityEngine.Object.FindObjectsOfType (typeof(Vehicle)) as Vehicle[];
			Vehicle[] array2 = array;
			for (int i = 0; i < array2.Length; i++) {
				Vehicle vehicle = array2 [i];
				vehicle.networkView.RPC ("tellExploded", RPCMode.All, new object[] {
					false
				});
				
				vehicle.networkView.RPC ("tellWrecked", RPCMode.All, new object[] {
					false
				});
				vehicle.heal(1000);
			}
			
			NetworkChat.sendAlert(string.Concat (new object[] {
				"Repaired ",
				array.Length,
				" vehicles!"
			}));
		}

		private void RefuelVehicles (CommandArgs args)
		{
			Vehicle[] array = UnityEngine.Object.FindObjectsOfType (typeof(Vehicle)) as Vehicle[];
			Vehicle[] array2 = array;
			for (int i = 0; i < array2.Length; i++) {
				Vehicle vehicle = array2 [i];
				vehicle.fill (1000);
			}
			NetworkChat.sendAlert (string.Concat (new object[]
			{
				"Refueled ",
				array.Length,
				" vehicles!"
			}));
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

		private void ResetZombies (CommandArgs args)
		{
			SpawnAnimals.reset ();
			NetworkChat.sendAlert (args.sender.name + " has respawned all zombies");
		}

		private void Killzombies (CommandArgs args)
		{
			Zombie[] array = UnityEngine.Object.FindObjectsOfType (typeof(Zombie)) as Zombie[];
			Zombie[] array2 = array;
			for (int i = 0; i < array2.Length; i++) {
				Zombie zombie = array2 [i];
				zombie.damage (500);
			}
			NetworkChat.sendAlert (string.Concat (new object[]
			{
				args.sender.name,
				" has killed ",
				array.Length,
				" zombies"
			}));
		}

		private void ReloadBans (CommandArgs args)
		{
			NetworkBans.load ();
		}

		private void SetItemsDelay (CommandArgs args)
		{
			string value = args.Parameters[0];
			this.setItemResetIntervalInSeconds (System.Convert.ToInt32 (value));
		}

		private void SetAnnounceDelay (CommandArgs args)
		{
			string value = args.Parameters[0];
			this.setAnnounceIntervalInSeconds (System.Convert.ToInt32 (value));
		}

		private void EnableWhitelist (CommandArgs args) {
			this.usingWhitelist = true;
			NetworkChat.sendAlert ("Whitelist enabled.");
		}

		private void DisableWhitelist (CommandArgs args) {
			this.usingWhitelist = false;
			NetworkChat.sendAlert ("Whitelist disabled.");
		}

		private void Whitelist (CommandArgs args) {
			if (args.Parameters [0].Equals ("add")) {
				args.Parameters.RemoveAt (0);
				string text = args.ParametersAsString;
				string text2 = "";
				try {
					text2 = UserList.getUserFromName (text).steamid;
					text = UserList.getUserFromName (text).name;
				} catch {
					if (!text.StartsWith ("765611")) {
						Reference.Tell (args.sender.networkPlayer, "That player is either not online or you provided a bad steamid");
						return;
					}
					text2 = text;
				}
				System.IO.StreamWriter streamWriter = new System.IO.StreamWriter ("Unturned_Data/Managed/mods/AdminCommands/UnturnedWhitelist.txt", true);
				streamWriter.WriteLine ("");
				streamWriter.WriteLine (text2);
				streamWriter.Close ();
				this.WhitelistedSteamIDs.Add (text2);
				Reference.Tell (args.sender.networkPlayer, string.Concat (new string[]
				{
					"Added ",
					text,
					" (",
					text2,
					") to the whitelist"
				}));
			} else {
				if (args.Parameters[0].ToLower ().Equals ("remove") || args.Parameters[0].ToLower ().Equals ("del") || args.Parameters[0].ToLower ().Equals ("rem")) {
					args.Parameters.RemoveAt (0);
					string text = args.ParametersAsString;
					string text2 = "";
					
					try {
						text2 = UserList.getUserFromName (text).steamid;
						text = UserList.getUserFromName (text).name;
					} catch {
						if (!text.StartsWith ("765611")) {
							Reference.Tell (args.sender.networkPlayer, "That player is either not online or you provided a bad steamid");
							return;
						}
						text2 = text;
					}
					
					this.WhitelistedSteamIDs.Remove (text2);
					System.IO.File.Delete ("Unturned_Data/Managed/mods/AdminCommands/UnturnedWhitelist.txt");
					System.IO.StreamWriter streamWriter = new System.IO.StreamWriter ("Unturned_Data/Managed/mods/AdminCommands/UnturnedWhitelist.txt", true);
					for (int i = 0; i < this.WhitelistedSteamIDs.Count; i++) {
						streamWriter.WriteLine (this.WhitelistedSteamIDs [i]);
					}
					streamWriter.Close ();
					Reference.Tell (args.sender.networkPlayer, string.Concat (new string[]
					{
						"Removed ",
						text,
						" (",
						text2,
						") from the whitelist"
					}));
				}
			}
		}

		private void UnBan (CommandArgs args)
		{
			string parametersAsString = args.ParametersAsString;
			if (this.unban (parametersAsString)) {
				Reference.Tell (args.sender.networkPlayer, parametersAsString + " was removed from the ban list.");
			} else {
				Reference.Tell (args.sender.networkPlayer, "Could not find " + parametersAsString + " in the list of banned players");
			}
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
			this.teleportUserTo (args.sender, new Vector3 (num, num2, num3));
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
			Reference.Tell (args.sender.networkPlayer, parametersAsString + " has been promoted to level " + UserList.getPermission (userFromName.steamid));
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

		private void SpawnKit (CommandArgs args)
		{
			int[] array = new int[]
			{
				2004,
				7008,
				12000,
				9004,
				10001,
				11003,
				18014,
				18014,
				18014,
				18014,
				18014,
				18014,
				18014,
				4017,
				5017,
				11,
				13000,
				13000,
				14022,
				14022,
				8015,
				8013,
				3002
			};
			Vector3 position = args.sender.position;
			int[] array2 = array;
			for (int i = 0; i < array2.Length; i++) {
				int num = array2 [i];
				SpawnItems.spawnItem (num, 1, position);
			}
		}

		private void Kick (CommandArgs args)
		{
			string parametersAsString = args.ParametersAsString;
			this.Kick (parametersAsString, "You were kicked off the server");
		}

		private void GodMode (CommandArgs args)
		{
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

		private void RespawnVehicles (CommandArgs args) {
			GameObject gameObject= (GameObject)typeof(SpawnVehicles).GetFields () [0].GetValue (null);
			try {
				Vehicle[] array = UnityEngine.Object.FindObjectsOfType (typeof(Vehicle)) as Vehicle[];
				Vehicle[] array2 = array;
				for (int i = 0; i < array2.Length; i++) {
					Vehicle vehicle = array2 [i];
					vehicle.networkView.RPC ("tellExploded", RPCMode.All, new object[]
					{
						true
					});
					vehicle.networkView.RPC ("tellWrecked", RPCMode.All, new object[]
					{
						true
					});
				}
				int childCount = gameObject.transform.FindChild ("models").childCount;
				for (int j = 0; j < childCount; j++) {
					Transform child = gameObject.transform.FindChild ("models").GetChild (j);
					Network.RemoveRPCs (child.networkView.viewID);
					Network.Destroy (child.networkView.viewID);
				}
			} catch {
			}
			SpawnVehicles.save ();
			Reference.Tell (args.sender.networkPlayer, "Respawning " + Loot.getCars () + " vehicles in 3 seconds...");
			System.Threading.Timer timer = new System.Threading.Timer (delegate(object obj)
			{
				this.respawnVehicles = true;
			}, null, 3000, -1);
		}

		private void Vanish (CommandArgs args)
		{
			try {
				this.vanishedPlayers.Add (args.sender.steamid, new Vector3 (0f, 0f, 0f));
				Reference.Tell (args.sender.networkPlayer, "You have vanished :D");
			} catch {
				this.vanishedPlayers.Remove (args.sender.steamid);
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

		private void teleportUserTo (BetterNetworkUser user, Vector3 target)
		{
			user.position = target;
			user.player.gameObject.GetComponent<Life> ().networkView.RPC ("tellStatePosition", RPCMode.All, new object[]
			{
				target,
				user.rotation
			});
			user.player.gameObject.GetComponent<NetworkInterpolation> ().tellStatePosition_Pizza (target, user.rotation);
			Network.SetReceivingEnabled (user.networkPlayer, 0, false);
			System.Threading.Timer timer = new System.Threading.Timer (delegate(object obj)
			{
				Network.SetReceivingEnabled (user.networkPlayer, 0, true);
			}, null, 2000, -1);
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
			Network.SetReceivingEnabled (user.networkPlayer, 0, false);
			System.Threading.Timer timer = new System.Threading.Timer (delegate(object obj)
			{
				Network.SetReceivingEnabled (user.networkPlayer, 0, true);
			}, null, 2000, -1);
		}

		private void Ban (BetterNetworkUser userToBeBanned, string reason)
		{
			NetworkTools.ban (userToBeBanned.networkPlayer, userToBeBanned.name, userToBeBanned.steamid, reason);
		}

		public void announcesTimeElapsed (object sender, ElapsedEventArgs e)
		{
			this.announceNext ();
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

		private void itemsTimeElapsed (object sender, ElapsedEventArgs e)
		{
			this.resetItems ();
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

		private void kickNonWhitelistedPlayers ()
		{
			if (this.usingWhitelist && this.updater3 <= 1) {
				foreach (BetterNetworkUser current in UserList.users) {
					if (current.networkPlayer != Network.player && !this.WhitelistedSteamIDs.Contains (current.steamid)) {
						if (this.showWhiteListKickMessages) {
							this.Kick (current.name, "You are not whitelisted on this server!");
						} else {
							NetworkChat.sendNotification (current.networkPlayer, "You are not whitelisted on this server!");
							Network.CloseConnection (current.networkPlayer, true);
						}
					}
				}
				this.updater3 = 100;
			}
			this.updater3--;
		}

		public void loadBans ()
		{
			this.bigAssStringWithBannedPlayerNamesAndSteamIDs = PlayerPrefs.GetString ("bans");
		}

		public void saveBans ()
		{
			PlayerPrefs.SetString ("bans", this.bigAssStringWithBannedPlayerNamesAndSteamIDs);
		}

		public bool unban (string name)
		{
			this.loadBans ();
			string text = this.bigAssStringWithBannedPlayerNamesAndSteamIDs;
			bool result;
			if (text.Contains (name)) {
				int num = text.IndexOf (name);
				int num2 = name.Length + 1 + 17 + 2;
				string str = text.Substring (0, num);
				string str2 = text.Substring (num + num2);
				text = str + str2;
				this.bigAssStringWithBannedPlayerNamesAndSteamIDs = text;
				this.saveBans ();
				NetworkBans.load ();
				result = true;
			} else {
				result = false;
			}
			return result;
		}

		private void Kick (string name, string reason)
		{
			BetterNetworkUser userFromName = UserList.getUserFromName (name);
			NetworkTools.kick (userFromName.networkPlayer, reason);
		}

		private void setHome (BetterNetworkUser user, Vector3 location)
		{
			Reference.Tell (user.networkPlayer, "Setting home... Stand still for 20 seconds.");
			System.Threading.Timer timer = new System.Threading.Timer (delegate(object obj)
			{
				this.setPlayerhome (user, location);
			}, null, 500, -1);
		}

		private bool setPlayerhome (BetterNetworkUser user, Vector3 location)
		{
			int i = 20;
			bool result;
			while (i > 0) {
				if (!user.position.Equals (location)) {
					Reference.Tell (user.networkPlayer, "Sethome cancelled");
					result = false;
					return result;
				}
				Reference.Tell (user.networkPlayer, "Setting home in " + i + " seconds");
				i--;
				System.Threading.Thread.Sleep (1000);
			}
			string steamid = user.steamid;
			if (this.playerHomes.ContainsKey (steamid)) {
				string[] array = System.IO.File.ReadAllLines ("Unturned_Data/Managed/mods/AdminCommands/PlayerHomes.txt");
				System.IO.File.Delete ("Unturned_Data/Managed/mods/AdminCommands/PlayerHomes.txt");
				System.IO.StreamWriter streamWriter = new System.IO.StreamWriter ("Unturned_Data/Managed/mods/AdminCommands/PlayerHomes.txt", true);
				for (int j = 0; j < array.Length; j++) {
					if (!array [j].StartsWith (steamid)) {
						streamWriter.WriteLine (array [j]);
					}
				}
				streamWriter.Close ();
			}
			System.IO.StreamWriter streamWriter2 = new System.IO.StreamWriter ("Unturned_Data/Managed/mods/AdminCommands/PlayerHomes.txt", true);
			streamWriter2.WriteLine (string.Concat (new object[]
			{
				steamid,
				":",
				location.x,
				",",
				location.y,
				",",
				location.z
			}));
			streamWriter2.Close ();
			this.playerHomes [steamid] = location;
			Reference.Tell (user.networkPlayer, "Home set.");
			result = true;
			return result;
		}

		private void home (BetterNetworkUser user)
		{
			if (!this.usedHomeCommand.ContainsKey (user.steamid)) {
				Vector3 originalposition = user.position;
				Reference.Tell (user.networkPlayer, "Teleporting home... Stand still for 5 seconds.");
				System.Threading.Timer timer = new System.Threading.Timer (delegate(object obj)
				{
					if (this.teleportHome (user, originalposition)) {
						this.usedHomeCommand.Add (user.steamid, UnityEngine.Time.realtimeSinceStartup);
					}
				}, null, 500, -1);
			} else {
				if (UnityEngine.Time.realtimeSinceStartup - this.usedHomeCommand[user.steamid] > 600f || user.permissionLevel > 4L) {
					Vector3 originalposition = user.position;
					Reference.Tell (user.networkPlayer, "Teleporting home... Stand still for 5 seconds.");
					System.Threading.Timer timer = new System.Threading.Timer (delegate(object obj)
					{
						if (this.teleportHome (user, originalposition)) {
							this.usedHomeCommand [user.steamid] = UnityEngine.Time.realtimeSinceStartup;
						}
					}, null, 500, -1);
				} else {
					Reference.Tell(
						user.networkPlayer, 
						"You need to wait " + 
						System.Math.Round ((double)(600f - (UnityEngine.Time.realtimeSinceStartup - this.usedHomeCommand[user.steamid]))).ToString() + 
						" more seconds before you can teleport home again.");
				}
			}
		}

		private bool teleportHome (BetterNetworkUser user, Vector3 originalposition)
		{
			int i = 10;
			bool result;
			while (i > 0) {
				if (!user.position.Equals (originalposition)) {
					Reference.Tell (user.networkPlayer, "Teleportation cancelled");
					result = false;
					return result;
				}
				i--;
				System.Threading.Thread.Sleep (500);
			}
			if (user.position.Equals (originalposition)) {
				this.teleportUserTo (user, this.playerHomes [user.steamid]);
				Reference.Tell (user.networkPlayer, "Teleported home.");
				result = true;
				return result;
			}
			Reference.Tell (user.networkPlayer, "Teleportation cancelled");
			result = false;
			return result;
		}

		public void Update ()
		{
			this.kickNonWhitelistedPlayers ();
			if (this.respawnVehicles) {
				this.respawnVehicles = false;
				SpawnVehicles spawnVehicles = UnityEngine.Object.FindObjectOfType<SpawnVehicles> ();
				spawnVehicles.onReady ();
			}
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
								userFromSteamID.player.networkView.RPC ("tellStatePosition", networkPlayer, new object[]
								{
									new Vector3 (0f, 0f, 0f),
									userFromSteamID.rotation
								});
							}
						}
					}
				}
			}
		}

		public void OnGUI ()
		{
			if (this.usingGUI) {
				GUI.BeginGroup (new Rect (50f, 100f, 600f, 70f));
				GUI.Box (new Rect (0f, 0f, 530f, 400f), "Admin Commands v2 running! Last command used: " + CommandList.lastUsedCommand);
				GUI.EndGroup ();
			}
		}

		private void ReadConfigs ()
		{
			System.IO.Directory.CreateDirectory ("Unturned_Data/Managed/mods/AdminCommands");
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
			}
			this.usingWhitelist = bool.Parse (iniFile2.IniReadValue ("Config", "Using Whitelist"));
			this.usingGUI = bool.Parse (iniFile2.IniReadValue ("Config", "Show gui"));
			this.usePlayerHomes = bool.Parse (iniFile2.IniReadValue ("Config", "Using Player Homes"));
			this.showWhiteListKickMessages = bool.Parse (iniFile2.IniReadValue ("Config", "Show whitelist kick messages"));
			this.itemsResetIntervalInSeconds = int.Parse (iniFile2.IniReadValue ("Timers", "Time between item respawns in seconds"));
			this.announceIntervalInSeconds = int.Parse (iniFile2.IniReadValue ("Timers", "Time between announces in seconds"));
			if (!System.IO.File.Exists ("Unturned_Data/Managed/mods/AdminCommands/UnturnedWhitelist.txt")) {
				System.IO.StreamWriter streamWriter = new System.IO.StreamWriter ("Unturned_Data/Managed/mods/AdminCommands/UnturnedWhitelist.txt", true);
				streamWriter.WriteLine ("76561197976976379");
				streamWriter.Close ();
			}
			string[] array = System.IO.File.ReadAllLines ("Unturned_Data/Managed/mods/AdminCommands/UnturnedWhitelist.txt");
			for (int i = 0; i < array.Length; i++) {
				if (array [i].Length > 10) {
					this.WhitelistedSteamIDs.Add (array [i]);
				}
			}
			if (!System.IO.File.Exists ("Unturned_Data/Managed/mods/AdminCommands/playerHomes.txt")) {
				System.IO.StreamWriter streamWriter = new System.IO.StreamWriter ("Unturned_Data/Managed/mods/AdminCommands/playerHomes.txt", true);
				streamWriter.WriteLine ("");
				streamWriter.Close ();
			}
			string[] array2 = System.IO.File.ReadAllLines ("Unturned_Data/Managed/mods/AdminCommands/playerHomes.txt");
			for (int i = 0; i < array2.Length; i++) {
				if (array2 [i].Length > 5) {
					string key = array2 [i].Split (new char[]
					{
						':'
					}) [0];
					string text = array2 [i].Split (new char[]
					{
						':'
					}) [1];
					string value = text.Split (new char[]
					{
						','
					}) [0];
					string value2 = text.Split (new char[]
					{
						','
					}) [1];
					string value3 = text.Split (new char[]
					{
						','
					}) [2];
					Vector3 value4 = new Vector3 (System.Convert.ToSingle (value), System.Convert.ToSingle (value2), System.Convert.ToSingle (value3));
					this.playerHomes [key] = value4;
				}
			}
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
			string[] array3 = System.IO.File.ReadAllLines ("Unturned_Data/Managed/mods/AdminCommands/UnturnedAnnounces.txt");
			this.AnnounceMessages = new string[array3.Length];
			for (int i = 0; i < array3.Length; i++) {
				this.AnnounceMessages [i] = array3 [i];
			}
			this.itemsTimer = new System.Timers.Timer ((double)(this.itemsResetIntervalInSeconds * 1000));
			this.itemsTimer.Elapsed += new ElapsedEventHandler (this.itemsTimeElapsed);
			this.itemsTimer.Enabled = true;
			this.announceTimer = new System.Timers.Timer ((double)(this.announceIntervalInSeconds * 1000));
			this.announceTimer.Elapsed += (new ElapsedEventHandler (this.announcesTimeElapsed));
			this.announceTimer.Enabled = true;
		}
	}
}
