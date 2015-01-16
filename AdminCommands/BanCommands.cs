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

namespace AdminCommands
{
    public class BanCommands
    {
        private BetterNetworkUser userToBeBanned;

        public BanCommands()
        {
            Command banCommand = new Command (8, new CommandDelegate (this.Ban), new string[]
            {
                "ban"
            });
            banCommand.description = ("Ban a player. Will need confirmation with /reason");
            CommandList.add (banCommand);

            Command banReason = new Command (8, new CommandDelegate (this.ReasonForBan), new string[]
            {
                "reason",
                "r"
            });
            banReason.description = ("Bans the player set with /ban");
            CommandList.add (banReason);
        }

        private void Ban (CommandArgs args)
        {
            string parametersAsString = args.ParametersAsString;
            int index = 0;
            if (parametersAsString.Length < 3) {
                this.userToBeBanned = UserList.users[index];
                string name = this.userToBeBanned.name;
                Reference.Tell (args.sender.networkPlayer, "Reason for banning " + name + " ?  /reason <reason> to ban");
            } else {
                this.userToBeBanned = UserList.getUserFromName (parametersAsString);
                string name = this.userToBeBanned.name;
                Reference.Tell (args.sender.networkPlayer, "Reason for banning " + name + " ?  /reason <reason> to ban");
            }
        }

        private void Ban (BetterNetworkUser userToBeBanned, string reason, BetterNetworkUser bannedBy) {
            NetworkTools.ban( 
                userToBeBanned.networkPlayer, 
                userToBeBanned.name, 
                userToBeBanned.steamid, 
                reason,
                bannedBy.steamid);
        }

        private void ReasonForBan (CommandArgs args) {
            string parametersAsString = args.ParametersAsString;
            if (args.Parameters.Count > 0) {
                this.Ban(this.userToBeBanned, parametersAsString, args.sender);
            }
        }
    }
}

