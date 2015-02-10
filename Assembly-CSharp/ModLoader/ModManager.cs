using CommandHandler;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;
using System.Security.Policy;

namespace ModLoader
{
    public class ModManager : MonoBehaviour
    {

        private Boolean loadedServerMods = false;
        private List<Type> serverComponents = new List<Type>();

        private static GameObject previousObject;
		private static AppDomain loaderAppDomain;

        private void Start()
        {
			if (previousObject == null)
				previousObject = Loader.gameobject;
        }

		private void CreateAppDomain()
		{
			ModManager.loaderAppDomain = AppDomain.CreateDomain("ModLoaderDomain");
		}

        private void Update()
        {
            if (!this.loadedServerMods && Network.isServer)
            {
				CreateAppDomain();
				LoadDefaultMods();
				Loader.gameobject.AddComponent<CommandHandlerMain>();
                this.loadedServerMods = true;
            }
            else if (this.loadedServerMods && !Network.isServer)
            {
				RemoveMods();
            }
        }

		public void RemoveMods()
		{
			for( int i = 0; i<serverComponents.Count; i++)
			{
				Component.Destroy(previousObject.GetComponent(serverComponents[i]));
			}
			serverComponents.Clear();

			this.loadedServerMods = false;
			previousObject = Loader.gameobject;

			AppDomain.Unload(ModManager.loaderAppDomain);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			CreateAppDomain();
		}

        public void LoadDefaultMods()
        {
            LoadModsInDirectory("mods/");
        }

		Assembly AssemblyResolve(object sender, ResolveEventArgs args)
		{
			Log("Arg: " + args.Name + " Sender: " + sender.ToString() );
			return null;
		}

        public void LoadModsInDirectory(String path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles("*.dll");
            int count = files.Length;

            String[] dllNames = new String[count];

            for (int i = 0; i < count; i++)
            {
                string filename = files[i].Name;
                dllNames[i] = files[i].Name.Substring(0, filename.Length - 4);
            }

            Log(count + " .dll files found in " + path + " folder");


            for (int i = 0; i < count; i++)
            {
				Log((i + 1) + ")");
                Log("Trying to load assembly: " + dllNames[i] + ".dll");
                Assembly assembly = null;
                try
                {
					//assembly = loaderAppDomain.Load(path + dllNames[i] + ".dll");
					/*byte[] buffer = new byte[1024];
					FileStream stream = new FileStream(path + dllNames[i] + ".dll", FileMode.Open);
					MemoryStream outStream = new MemoryStream();
					int readed = 0;

					while ((readed = stream.Read(buffer, 0, buffer.Length)) > 0)
					{
						outStream.Write(buffer, 0, readed);
					}
					stream.Close();*/

					assembly = Assembly.LoadFile(path + dllNames[i] + ".dll");

					//var asmLoaderProxy = (ProxyDomain)loaderAppDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(ProxyDomain).FullName);
					//assembly = asmLoaderProxy.GetAssembly(path + dllNames[i] + ".dll");
                }
                catch (Exception e)
                {
                    Log("Error loading class: " + e.Message);
                    Log(e.InnerException.Message);
					continue;
                }

                Log("Assembly loaded in, looking for classes to inject.. ");
                Type[] assemblytypes = null;
                try
                {
                    assemblytypes = assembly.GetTypes();
                }
                catch (Exception e)
                {
                    Log(e.Message);
                    Log(e.GetType().ToString());
                    Type[] loadedModules = ((ReflectionTypeLoadException)e).Types;
                    foreach (Type type in loadedModules)
                    {
                        try
                        {
                            Log(type.ToString());

                        }
                        catch (Exception e2)
                        {
                            Log(e2.Message);
                            Log("The .NET version of this mod is probably not compatible with unturned");
                        }
                    }
                    Log(e.Source);
                    Log(e.Data.ToString());
                    Log(e.TargetSite.ToString());
                }

                foreach (Type t in assemblytypes)
                {
					if ( t.IsSubclassOf(typeof(MonoBehaviour)) )
					{
						try
						{
							Log("Found class: \"" + t.Name + "\" in " + dllNames[i] + ".dll");
							Loader.gameobject.AddComponent(t);
							Log("Class \"" + t.Name + "\" added as Component to gameobject");
							serverComponents.Add(t);
							UnityEngine.Object.DontDestroyOnLoad(Loader.gameobject);
						}
						catch (Exception e)
						{
							Log("Error loading class: " + e.Message);
							Log(e.InnerException.Message);
							//error log
						}
					}
#if DEBUG
					else
					{
						Log("Info: The current type is: " + t.BaseType + ". Skipping!");
					}
#endif
                }
            }

            Log("Finished loading .dll files");
            Log("---------------------------------");
            Log(" ");
        }

        private void Log(string p)
        {
            StreamWriter file = new StreamWriter(Loader.LOADER_LOG_FILE, true);
            file.WriteLine(p);
			file.Close();
        }


    }  //End class
} //End namespace