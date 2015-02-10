using System;
using System.Reflection;
using UnityEngine;

namespace CommandHandler
{
    public class Reference
    {
        //Huge thanks to  nearbeer :D  You are awesum

        public static readonly FieldInfo bleed = typeof(Life).GetFields()[0];//bool
        public static readonly FieldInfo bones = typeof(Life).GetFields()[1];//bool (broken leg)
        public static readonly FieldInfo dead = typeof(Life).GetFields()[2];//bool

        public static readonly FieldInfo name = typeof(NetworkUser).GetFields()[0];//string (steam name)
        public static readonly FieldInfo nickname = typeof(NetworkUser).GetFields()[1];//string (unturned name)
        public static readonly FieldInfo steamID64 = typeof(NetworkUser).GetFields()[3];//string
        public static readonly FieldInfo rep = typeof(NetworkUser).GetFields()[5];//int
        public static readonly FieldInfo player = typeof(NetworkUser).GetFields()[6];//int

        public static readonly FieldInfo clientItems = typeof(Inventory).GetFields()[1];//ClientItem[,]
        public static readonly FieldInfo width = typeof(Inventory).GetFields()[2];//int
        public static readonly FieldInfo height = typeof(Inventory).GetFields()[3];//int
        public static readonly FieldInfo weight = typeof(Inventory).GetFields()[4];//int (100 / 1 kg)
        public static readonly FieldInfo capacity = typeof(Inventory).GetFields()[5];//int
        public static readonly FieldInfo speedModifier = typeof(Inventory).GetFields()[6];//float (the effect items have on your speed)

        public static readonly FieldInfo skills = typeof(Skills).GetFields()[0];//Skill[]

        public static readonly FieldInfo text_5 = typeof(NetworkChat).GetFields()[6];//string
        public static readonly FieldInfo rep_5 = typeof(NetworkChat).GetFields()[9];//int
        public static readonly FieldInfo name_5 = typeof(NetworkChat).GetFields()[3];//string

        public static readonly FieldInfo pname = typeof(PlayerSettings).GetFields()[0];//Your steam name (these are static, use getValue(null))
        public static readonly FieldInfo pid = typeof(PlayerSettings).GetFields()[4];//Your steam id



        public static void Tell(NetworkPlayer p, string text)
        {
            if (p.Equals(Network.player))
            {
                getNetworkChat().tellChat_Pizza("www.zombieland.ml - [Server]", "", "", text, 0x7fffffff, 1, 20);
            }
            else
            {
				object[] args = new object[] { "www.zombieland.ml - [Server]", "", "", text, 0x7fffffff, 1, 20 };
                getNetworkChat().networkView.RPC("tellChat", p, args);
            }
        }
        public static void resetChat()
        {
            String[] texts = new String[4];
            texts[0] = getNetworkChatFieldByNum(13);
            texts[1] = getNetworkChatFieldByNum(20);
            texts[2] = getNetworkChatFieldByNum(27);
            texts[3] = getNetworkChatFieldByNum(34);

            for (int i = 0; i < 4; i++)
            {
                if (texts[i].StartsWith("/"))
                {
                    texts[i] = "";
                }
            }

            NetworkPlayer[] players = new NetworkPlayer[4];
            bool[] isServerMsg = new bool[4];
            String[] names = new String[4];

            names[0] = getNetworkChatFieldByNum(10);
            names[1] = getNetworkChatFieldByNum(17);
            names[2] = getNetworkChatFieldByNum(24);
            names[3] = getNetworkChatFieldByNum(31);


            for (int i = 0; i < 4; i++)
            {
                BetterNetworkUser temp1 = UserList.getUserFromName(names[i]);
                NetworkPlayer temp;
                if (temp1 == null)
                {
                    temp = Network.player;
                }
                else
                {
                    temp = temp1.networkPlayer;
                }

                if (temp != null)
                {
                    players[i] = temp;
                }
                else
                {
                    players[i] = Network.player;
                }

                if (names[i] != null)
                {
                    if (names[i].StartsWith("Server"))
                    {
                        isServerMsg[i] = true;
                    }
                }

            }


            if (!isServerMsg[3])
            {
                getNetworkChat().askChat(texts[3], 0, players[3]);
            }
            else
            {
                NetworkChat.sendAlert(texts[3]);
            }

            for (int i = 0; i < 4; i++)
            {
                if (!isServerMsg[3 - i])
                {
                    getNetworkChat().askChat(texts[3 - i], 0, players[3 - i]);
                }
                else
                {
                    NetworkChat.sendAlert(texts[3 - i]);

                }
            }


        }

        private static String getNetworkChatFieldByNum(int num2)
        {
            try
            {
                FieldInfo[] networkChatfields = typeof(NetworkChat).GetFields();

                return networkChatfields[num2].GetValue(getNetworkChat()).ToString();
            }
            catch
            {
                return "";
            }


        }

        private static NetworkChat getNetworkChat()
        {
            NetworkChat chat = UnityEngine.Object.FindObjectOfType<NetworkChat>();
            return chat;
        }
    }
}