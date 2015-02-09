//------------------------------------------------------------------------------
// <auto-generated>
//     Ezt a kódot eszköz generálta.
//     Futásidejű verzió:4.0.30319.0
//
//     Ennek a fájlnak a módosítása helytelen viselkedést eredményezhet, és elvész, ha
//     a kódot újragenerálják.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using CommandHandler;

namespace AdminCommands
{
	public class AICommands
	{
		public AICommands ()
		{
			Command aiCommands = new Command (7, new CommandDelegate (this.HandleAICommand), new string[]{
				"ai"
			});
			aiCommands.description = ("Ai commands");
			CommandList.add (aiCommands);
		}

		private void HandleAICommand(CommandArgs args)
		{
			if ( args.Parameters.Count < 1 )
			{
				Reference.Tell(args.sender.networkPlayer, "Available subcommands: rzombie, ranimal, szombie, sanimals");
				return;
			}

			if ( args.Parameters[0].ToLower().Equals("rzombie") ) 
				Killzombies(args);
			else if (args.Parameters[0].ToLower().Equals("ranimal"))
				RemoveAnimals(args);
			else if (args.Parameters[0].ToLower().Equals("szombie"))
				ResetZombies(args);
			else
				Reference.Tell(args.sender.networkPlayer, "Available subcommands: rzombie, ranimal, szombie, sanimals");
		}

		private void ResetZombies (CommandArgs args)
		{
			SpawnAnimals.reset ();
			NetworkChat.sendAlert (args.sender.name + " has respawned all zombies");
		}

		private void Killzombies (CommandArgs args)
		{
			Zombie[] zombies = UnityEngine.Object.FindObjectsOfType (typeof(Zombie)) as Zombie[];
			
			foreach( Zombie zombie in zombies )
			{
				Network.RemoveRPCs( zombie.networkView.viewID );
				Network.Destroy( zombie.gameObject );
				GameObject.Destroy( zombie.gameObject );
			}

			Reference.Tell( args.sender.networkPlayer, string.Concat (new object[]
			                                                          {
				"Removed ",
				zombies.Length,
				" zombies"
			}));
		}

		private void RemoveAnimals(CommandArgs args)
		{
			Animal[] animals = UnityEngine.Object.FindObjectsOfType (typeof(Animal)) as Animal[];
			
			foreach( Animal animal in animals )
			{
				Network.RemoveRPCs( animal.networkView.viewID );
				Network.Destroy( animal.gameObject );
				GameObject.Destroy( animal.gameObject );
			}

			Reference.Tell( args.sender.networkPlayer, string.Concat (new object[]{
				"Removed ",
				animals.Length,
				" animals."
			}));
		}
	}
}
