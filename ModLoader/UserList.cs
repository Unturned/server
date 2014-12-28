using Ini;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CommandHandler
{
    public class UserList
    {
        private static List<BetterNetworkUser> _users = new List<BetterNetworkUser>();

        private static IniFile ini = new IniFile("Unturned_Data/Managed/mods/UserPermissionLevels.ini");

        /// <summary>
        /// Returns a list of BetterNetworkUsers. Cloned from NetworkUserList
        /// </summary>
        public static List<BetterNetworkUser> users
        {
            get
            {
                _users.Clear();
                foreach (NetworkUser user in NetworkUsers)
                {
                    _users.Add(new BetterNetworkUser(user));
                }
                return _users;
            } 
        }

        /// <summary>
        /// Returns NetworkUserList.users
        /// </summary>
        public static List<NetworkUser> NetworkUsers
        {
            get
            {
                return (List<NetworkUser>)typeof(NetworkUserList).GetFields()[0].GetValue(null);
            }
        }

        public static int getIndexFromSteamID(string id)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (_users[i].steamid == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public static int getIndexFromPlayer(NetworkPlayer player)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (_users[i].networkPlayer == player)
                {
                    return i;
                }
            }
            return -1;
        }


        public static BetterNetworkUser getUserFromPlayer(NetworkPlayer player)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (_users[i].networkPlayer == player)
                {
                    return _users[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Returns a player's user object. Useful for doing all kinds of stuff to players :D
        /// </summary>
        /// <param name="name">The player's name</param>
        /// <returns>The player's User object</returns>
        public static BetterNetworkUser getUserFromName(String name)
        {
            List<BetterNetworkUser> betterList = users;
            BetterNetworkUser betterThanNothing = null;

            foreach (BetterNetworkUser user in betterList)
            {
                if (user.name.ToLower().Equals(name.ToLower()))
                {
                    return user;
                }
                else if (user.name.ToLower().Contains(name.ToLower()))
                {
                    betterThanNothing = user;
                }
            }
            return betterThanNothing;
        }

        public static BetterNetworkUser getUserFromSteamID(String steamid)
        {
            List<BetterNetworkUser> betterList = users;

            foreach (BetterNetworkUser user in betterList)
            {
                if (user.steamid.Equals(steamid))
                {
                    return user;
                }
            }
            return null;
        }

        public static int getPermission(string steamID)
        {
            string temp = ini.IniReadValue("PermissionLevels", steamID);
            //NetworkChat.sendAlert("zoeke op " + steamID);
            if (temp.Equals(""))
            {
                //NetworkChat.sendAlert("ni gevonde");
                return 0;
            }
            else
            {
                return Convert.ToInt32(temp);
            }
        }

        /// <summary>
        /// Increases the target's permission level by 1
        /// </summary>
        /// <param name="steamID"></param>
        public static void promote(string steamID)
        {
            int previous = getPermission(steamID);

            ini.IniWriteValue("PermissionLevels", steamID, (previous+1).ToString());
        }

        /// <summary>
        /// Sets the target's permission level
        /// </summary>
        /// <param name="steamID"></param>
        /// <param name="permissionLevel"></param>
        public static void setPermission(string steamID, int permissionLevel)
        {
            ini.IniWriteValue("PermissionLevels", steamID, permissionLevel.ToString());
        }

    }
}
