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
using CommandHandler;
using UnityEngine;

namespace AdminCommands
{
    public class CarCommands
    {
        public CarCommands()
        {
            // Car create command
            Command createCarCommand = new Command(1, new CommandDelegate (this.CreateCar), new string[]
            {
                "car"
            });
            createCarCommand.description =  ("Teleport a random car or a specific car name");
            CommandList.add (createCarCommand);

            // Refuel command
            Command refuelCommand = new Command(7, new CommandDelegate (this.RefuelVehicles), new string[]
            {
                "refuel",
                "refuelvehicles"
            });
            refuelCommand.description = ("Fills the gas tank of all vehicles");
            CommandList.add (refuelCommand);

            // Repair command
            Command repairCommand = new Command (7, new CommandDelegate (this.RepairVehicles), new string[]
            {
                "repair",
                "repairvehicles"
            });
            repairCommand.description = ("Repairs all vehicles");
            CommandList.add (repairCommand);

            // Respawn Vehicles command
            Command respawnVehicles = new Command (8, new CommandDelegate (this.RespawnVehicles), new string[]
            {
                "respawnvehicles"
            });
            respawnVehicles.description =  ("Delets and respawns all vehicles");
            CommandList.add (respawnVehicles);
        }

        private void CreateCar(CommandArgs args) {
            Player player = args.sender.player;
            SpawnVehicles.createNew("policeCar_0", 
                player.transform.position + 
                new Vector3(1.0f, 2.0f, 0f),
                player.transform.rotation * Quaternion.Euler(-90f, 0f, 0f));

            Reference.Tell(args.sender.networkPlayer, "Adding a police car for you!");
        }

        private void RefuelVehicles (CommandArgs args)
        {
            Vehicle[] array = UnityEngine.Object.FindObjectsOfType (typeof(Vehicle)) as Vehicle[];
            Vehicle[] array2 = array;
            for (int i = 0; i < array2.Length; i++) {
                Vehicle vehicle = array2 [i];
                vehicle.fill (1000);
            }

            NetworkChat.sendChat(string.Concat (new object[]
            {
                "Refueled ",
                array.Length,
                " vehicles!"
            }));
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

            NetworkChat.sendChat(string.Concat (new object[] {
                "Repaired ",
                array.Length,
                " vehicles!"
            }));
        }

        private void RespawnVehicles (CommandArgs args) {
            int wreckedCount = 0;
            Vehicle[] vehicles = UnityEngine.Object.FindObjectsOfType (typeof(Vehicle)) as Vehicle[];
            foreach (Vehicle vehicle in vehicles) 
            {
                if (vehicle.exploded || vehicle.getPosition().y <= (Ocean.level - 1f) || vehicle.wrecked)
                {
                    wreckedCount++;
                }
            }

            SpawnVehicles.save ();
            Reference.Tell(args.sender.networkPlayer, "Respawning " + wreckedCount + "  wrecked vehicles...");
        }

    }
}

