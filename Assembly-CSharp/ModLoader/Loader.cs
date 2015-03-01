using System;
using System.IO;
using UnityEngine;
using Unturned.ThreadWorker;

namespace ModLoader
{
    public class Loader : MonoBehaviour
    {
		public const String LOADER_LOG_FILE = @"modloader.log";

        public static GameObject gameobject;
        public static GameObject keepAlive;


        public static void hook()
        {
            Directory.CreateDirectory("mods");
			File.WriteAllText(LOADER_LOG_FILE, string.Empty);

            if (Loader.gameobject == null)
            {
                gameobject = getNetworkChat().gameObject;
                UnityEngine.Object.DontDestroyOnLoad(Loader.gameobject);
                gameobject.AddComponent<ModManager>();
				gameobject.AddComponent<TaskBehaviour>();
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
                gameobject = Loader.getNetworkChat().gameObject;
                Log("Found new GameObject :)");
                UnityEngine.Object.DontDestroyOnLoad(Loader.gameobject);
                gameobject.AddComponent<ModManager>();
            }
        }

        public static void Log(string p)
        {
			StreamWriter file = new StreamWriter(LOADER_LOG_FILE, true);
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