//
//  Author:
//    Paál Gyula paalgyula@gmail.com
//
//  Copyright (c) 2015, GW-Systems Kft. All Rights Reserved
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//     * Neither the name of the [ORGANIZATION] nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
//  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
using System;
using UnityEngine;
using CommandHandler;
using Unturned;

using Unturned.Log;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace AdminCommands
{
    public class StatCommands
    {
        public StatCommands()
        {
            Command statCommand = new Command(10, new CommandDelegate(this.GameObjectStat), new String[] {
                "stat"
            });
            CommandList.add(statCommand);
        }

        public void GameObjectStat(CommandArgs args) 
        {
            if (args.Parameters.Count > 0)
            {
                String statType = args.Parameters[0];
                if( statType.ToLower().Equals("go") )
                {
                    this.PrintGOStat(args.sender.networkPlayer);
                }
                else if( statType.ToLower().Equals("player") )
                {
                    this.PrintPlayerStat(args.sender.networkPlayer);
                }
				else if( statType.ToLower().Equals("component") )
				{
					this.PrintComponentStat(args.sender.networkPlayer);
				}
                else
                {
                    this.PrintUsage(args.sender.networkPlayer);
                }
            }
            else
            {
                this.PrintUsage(args.sender.networkPlayer);
            }
        }

		private void WriteComponentsToFile()
		{
			String components = "";
			foreach ( MyComponent cmp in componentList)
			{
				if( cmp.goName != null)
				{
					components += "[" + cmp.goName + "] ";
				}
				else
				{
					components += "[null go] ";
				}

				components += "Name: (" + cmp.name + ")\n";
			}

			File.WriteAllText("components.db", components);

			componentList.Clear();
		}

		List<MyComponent> componentList;

		public void PrintComponentStat(NetworkPlayer player)
		{
			Component[] objects = UnityEngine.Object.FindObjectsOfType<Component> ();
			Reference.Tell(player, "There is " + objects.Length + " component");

			componentList = new List<MyComponent>();
			foreach (Component cmp in objects)
			{
				componentList.Add(new MyComponent(goName: cmp.gameObject.name, name: cmp.name));
			}

			Thread thread = new Thread (this.WriteComponentsToFile);
			thread.Start();
		}

		public void PrintUsage(NetworkPlayer player) 
        {
            Reference.Tell(player, "Available subcommands: [go, player, component]");
        }

        public void PrintGOStat(NetworkPlayer player) 
        {
            GameObject[] objects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            Reference.Tell(player, "There is " + objects.Length + " GameObject");

#if CREATE_DATA_FILES
            foreach ( GameObject go in objects) {
                string parent = "";
                if (go.transform.parent != null)
                    parent = go.transform.parent.name;
                Logger.LogDatabase("Name: " + go.name + " coord:" + go.transform.position + " Parent:" + parent);
            }
#endif
        }

        void PrintPlayerStat(NetworkPlayer player)
        {
            Player[] players = UnityEngine.Object.FindObjectsOfType<Player>();
            Reference.Tell(player, "There is " + players.Length + " Player objects..");

#if DEBUG
            foreach ( Player plr in players) {
				GameObject go = plr.gameObject;
                Logger.LogDatabase("Name: " + go.name + " coord:" + go.transform.position);
            }
#endif
        }
		
    }

	public class MyComponent {
		public String name {get; set;}
		public String goName {get; set;}

		public MyComponent (string name, string goName)
		{
			this.name = name;
			this.goName = goName;
		}
		
	}
}

