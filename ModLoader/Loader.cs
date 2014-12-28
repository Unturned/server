using System;
using System.IO;
using UnityEngine;

namespace ModLoader
{
    public class Loader : MonoBehaviour
    {
        public static GameObject gameobject;
        public static GameObject keepAlive;


        public static void hook()
        {
            Directory.CreateDirectory("Unturned_Data/Managed/mods");
            Directory.CreateDirectory("Unturned_Data/Managed/mods/Server mods");
            Directory.CreateDirectory("Unturned_Data/Managed/mods/Client mods");

            System.IO.File.WriteAllText(@"Unturned_Data/Managed/mods/ModLoader_logs.txt", string.Empty);

            if (Loader.gameobject == null)
            {
                gameobject = getNetworkChat().gameObject;
                UnityEngine.Object.DontDestroyOnLoad(Loader.gameobject);
                gameobject.AddComponent<Hook>();
            }  

            if (Loader.keepAlive == null)
            {
                keepAlive = new GameObject();
                UnityEngine.Object.DontDestroyOnLoad(Loader.keepAlive);
                keepAlive.AddComponent<Loader>();
            }          
        }

        
        void Update()
        {
            if (gameobject == null)
            {
                Log("GameObject was destroyed! Finding new gameobject...");
                gameobject = getNetworkChat().gameObject;
                Log("Found new GameObject :)");
                UnityEngine.Object.DontDestroyOnLoad(Loader.gameobject);
                gameobject.AddComponent<Hook>();
            }
        }

        void Start()
        {

        }

        void OnGUI()
        {

        }

        public static void Log(string p)
        {
            System.IO.StreamWriter file = new StreamWriter(@"Unturned_Data/Managed/mods/ModLoader_logs.txt", true);
            file.WriteLine(p);
            file.Close();
        }

        private static NetworkChat getNetworkChat()
        {
            NetworkChat chat = UnityEngine.Object.FindObjectOfType(typeof(NetworkChat)) as NetworkChat;
            return chat;
        }


    }
}