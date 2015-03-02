using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Unturned
{
    public class ConfigFile
    {
        private Dictionary<string, string> lines;

        private ConfigFile(Dictionary<String, String> lines)
        {
            this.lines = lines;
        }

        public static ConfigFile ReadFile(String configPath)
        {
			try 
			{
	            FileStream fs;
	            if (!File.Exists(configPath))
	            {
	                fs = File.Create(configPath);
	            }

	            fs = new FileStream(configPath, FileMode.Open);
	            StreamReader reader = new StreamReader(fs);
	            String[] lines = reader.ReadToEnd().Split('\n');

	            Dictionary<String, String> configLines = new Dictionary<string, string>();

	            foreach (String line in lines)
	            {
	                String trimmed = line.Trim();
	                if (!trimmed.StartsWith(";") || !trimmed.StartsWith("#")) // comments
	                {
	                    String[] pair = trimmed.Split(new char[] { '=' }, 2);

	                    if (pair.Length >= 2)
	                        configLines.Add(pair[0], pair[1]);
	                    else
	                        configLines.Add(pair[0], String.Empty);
	                }
	            }

	            return new ConfigFile(configLines);
			}
			catch (Exception e)
			{
				Console.WriteLine("### Fatal error while loading configuration file: " + configPath + " Exception: " + e.Message);
				Console.WriteLine("### Using defaults. Strongly recommended to fix this issue!");
				return new ConfigFile(new Dictionary<string, string>());
			}
        }

		/// <summary>
		/// Gets the config.
		/// </summary>
		/// <returns>The config.</returns>
		/// <param name="key">Key for config entry</param>
        public String getConfig(String key)
        {
			return getConfig(key, String.Empty);
        }

		/// <summary>
		/// Gets the config with default value
		/// </summary>
		/// <returns>The config.</returns>
		/// <param name="key">Key config key</param>
		/// <param name="dfault">Default value</param>
		public String getConfig(String key, String dfault)
		{
			String value;
			if (lines.TryGetValue(key, out value))
				return value;
			else
				return dfault;
		}
    }
}
