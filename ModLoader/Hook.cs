using CommandHandler;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ModLoader
{
    public class Hook : MonoBehaviour
    {

        private Boolean loadedServerMods = false;
        private Boolean loadedClientMods = false;
        private ArrayList serverComponents = new ArrayList();
        private ArrayList clientComponents = new ArrayList();

        private static GameObject previousObject;

        private void Start()
        {
            loadDefaultMods();

            if (previousObject == null)
				previousObject = Loader.gameobject;
        }

        private void Update()
        {
            //Log("Current Boolean loadedClientMods=" + loadedClientMods.ToString());

            if (!this.loadedClientMods && Network.isClient)
            {
                loadClientMods();
                this.loadedClientMods = true;
            }
            else if (this.loadedClientMods && !Network.isClient)
            {
                foreach (String clientmod in clientComponents)
                {
                    Destroy(previousObject.GetComponent(clientmod));
                    clientComponents.Remove(clientmod);                    
                }
                this.loadedClientMods = false;
                previousObject = Loader.gameobject;
            }


            if (!this.loadedServerMods && Network.isServer)
            {
                loadServerMods();
                this.loadedServerMods = true;
            }
            else if (this.loadedServerMods && !Network.isServer)
            {
                foreach (String servermod in serverComponents)
                {
                    Destroy(previousObject.GetComponent(servermod));
                    serverComponents.Remove(servermod);
                }
                this.loadedServerMods = false;
                previousObject = Loader.gameobject;
            }
        }

        public void loadDefaultMods()
        {
            loadModsInDirectory("Unturned_Data/Managed/mods/");
        }

        public void loadServerMods()
        {
            loadModsInDirectory("Unturned_Data/Managed/mods/Server mods/",true,false);
            Loader.gameobject.AddComponent<CommandHandlerMain>();

        }

        public void loadClientMods()
        {
            loadModsInDirectory("Unturned_Data/Managed/mods/Client mods/", false, true);
        }





        public void loadModsInDirectory(String path, Boolean isServerOnly = false, Boolean isClientOnly = false)
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
                    assembly = Assembly.LoadFrom(path + dllNames[i] + ".dll");
                }
                catch (Exception e)
                {
                    Log("Error loading class: " + e.Message);
                    Exception e2 = e.InnerException;
                    Log(e2.Message);
                    //error log
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

                bool useLoaderClass = false;
                bool useLoaderMethod = false;
                MethodInfo loadmethod = null;


                foreach (Type t in assemblytypes)
                { //first loop checks if the .dll file contains a Loader class.  If it finds one it will use that for the loading and ignore all other classes
                    if (t.Name.Equals("Loader"))
                    {
                        useLoaderClass = true;
                        Log("Loader class found in " + dllNames[i] + ".dll" + ", looking for load() or attach() method...");

                        MethodInfo[] methods = t.GetMethods();

                        foreach (MethodInfo method in methods)
                        {
                            if (method.Name == "Load" || method.Name == "load" || method.Name == "attach" || method.Name == "Attach")
                            {
                                useLoaderMethod = true;
                                loadmethod = method;
                                Log(loadmethod.Name + "() method found.");
                                break;
                            }
                        }
                        break;   // loader class found. Now go and use it
                    }
                }


                foreach (Type t in assemblytypes)
                {

                    //No loader class found.  Just add all classes as components
                    if (!t.Name.Contains("PrivateImplementation") && !t.Name.Contains("StaticArray") && !t.Name.Contains("IniFile"))
                    {

                        if (useLoaderClass == false)
                        {
                            try
                            {
                                Log("Found class: \"" + t.Name + "\" in " + dllNames[i] + ".dll");
                                Loader.gameobject.AddComponent(t);
                                Log("Class \"" + t.Name + "\" added as Component to gameobject");
                                if (isServerOnly)
                                {
                                    serverComponents.Add(t.Name);
                                }
                                else if (isClientOnly)
                                {
                                    clientComponents.Add(t.Name);
                                }
                                UnityEngine.Object.DontDestroyOnLoad(Loader.gameobject);


                            }
                            catch (Exception e)
                            {
                                Log("Error loading class: " + e.Message);
                                Exception e2 = e.InnerException;
                                Log(e2.Message);
                                //error log
                            }
                        }




                        else
                        {        //Use Loader class to load the .dll file
                            if (t.Name.Equals("Loader"))
                            {
                                if (useLoaderMethod)
                                {
                                    Log("Calling " + loadmethod.Name + "() in the Loader class.");
                                    try
                                    {
                                        loadmethod.Invoke(t, null);
                                    }
                                    catch (Exception e)
                                    {
                                        Log("Error loading class: " + e.Message);
                                        Exception e2 = e.InnerException;
                                        Log(e2.Message);
                                        //error log
                                    }
                                }
                                else  //no load() method found... just load the Loader class as gameobject
                                {
                                    Log("No load() or attach() method found in Loader class. Will instead add the Loader class as Component to gameobject");
                                    try
                                    {
                                        Log("Found class: \"" + t.Name + "\" in " + dllNames[i] + ".dll");
                                        Loader.gameobject.AddComponent(t);
                                        if (isServerOnly)
                                        {
                                            serverComponents.Add(t.Name);
                                        }
                                        else if (isClientOnly)
                                        {
                                            clientComponents.Add(t.Name);
                                        }
                                        UnityEngine.Object.DontDestroyOnLoad(Loader.gameobject);

                                        Log("Class \"" + t.Name + "\" added as Component to gameobject");

                                    }
                                    catch (Exception e)
                                    {
                                        Log("Error loading class: " + e.Message);
                                        Exception e2 = e.InnerException;
                                        Log(e2.Message);
                                        //error log
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Log("Finished loading .dll files");
            Log("---------------------------------");
            Log(" ");
        }









        private void Log(string p)
        {
            System.IO.StreamWriter file = new StreamWriter(@"Unturned_Data/Managed/mods/ModLoader_logs.txt", true);
            file.WriteLine(p);

            file.Close();
        }


    }  //End class
} //End namespace