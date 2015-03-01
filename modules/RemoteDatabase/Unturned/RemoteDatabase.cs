//------------------------------------------------------------------------------
// <auto-generated>
//     Ezt a kódot eszköz generálta.
//     Futásidejű verzió:4.0.30319.18408
//
//     Ennek a fájlnak a módosítása helytelen viselkedést eredményezhet, és elvész, ha
//     a kódot újragenerálják.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Configuration;

using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Collections;

using UnityEngine;

using Unturned;
using Unturned.Http;
using Unturned.Entity;
using System.Diagnostics;
using System.Threading;

namespace Unturned
{
    public class RemoteDatabase : IDataHolder
    {
        private String m_banUrl;
        private String m_host;
        private string m_creditUrl;

		private Dictionary<String, Player> m_playerCache;

		string m_playerUrl;

		#region Serializers
		private XmlSerializer m_banSerializer;
		private XmlSerializer m_playerSerializer;
		#endregion
        
        
        public void Init()
        {
            Unturned.ConfigFile config = Unturned.ConfigFile.ReadFile(@"Config/database.cfg");
            m_host = config.getConfig("host");
            m_banUrl = config.getConfig("banUrl");
            m_creditUrl = config.getConfig("creditUrl");
			m_playerUrl = config.getConfig("playerUrl", "/player");
            
            m_banSerializer = new XmlSerializer(typeof(BanList));
			m_playerSerializer = new XmlSerializer(typeof(Player));
            
			m_playerCache = new Dictionary<string, Player>();

            Console.WriteLine("Remote Database initialized: Host: {0} BanURL: {1}",
                              m_host,
                              m_banUrl
                              );
        }

        public int GetCredits(string steamId)
        {
            CreditMessage msg = null;

			HttpRequest request = new HttpRequest(m_host + m_creditUrl + "/" + steamId);
			Stream stream = request.DoGet();
            XmlSerializer serializer = new XmlSerializer(typeof(CreditMessage));
			msg = serializer.Deserialize(new StreamReader(stream)) as CreditMessage;
            
            return msg.Balance;
        }

        public void SaveCredits(string steamId, int count)
        {
			XmlSerializer serializer = new XmlSerializer(typeof(CreditMessage));
			MemoryStream mStream = new MemoryStream();
			serializer.Serialize(mStream, new CreditMessage(steamId, count));
			mStream.Seek(0, SeekOrigin.Begin);
			Stream reader = new HttpRequest(m_host + m_creditUrl).DoPost(new StreamReader(mStream).ReadToEnd());
            reader.Close();
        }

        public void AddBan(IBanEntry banEntry)
        {
			XmlSerializer serializer = new XmlSerializer(typeof(BanEntry));
			MemoryStream mStream = new MemoryStream();
			serializer.Serialize(mStream, banEntry);
			mStream.Seek(0, SeekOrigin.Begin);
			Stream reader = new HttpRequest(m_host + m_banUrl).DoPost(new StreamReader(mStream).ReadToEnd());
			reader.Close();
        }

        public void RemoveBan()
        {
            throw new NotImplementedException();
        }

        public void AddStructure(string structureStr)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, IBanEntry> LoadBans()
        {
			Dictionary<string, IBanEntry> bans = new Dictionary<string, IBanEntry>();

			HttpRequest req = new HttpRequest(m_host + m_banUrl);
			Stream stream = req.DoGet();

			BanList banList = m_banSerializer.Deserialize(new XmlTextReader(stream)) as BanList;
			foreach (BanEntry entry in banList.bans)
			{
				bans.Add( entry.SteamID, entry );
			}

#if DEBUG
			Console.WriteLine("Loaded bans with " + bans.Count + " entries.");
#endif

			return bans;
        }

		public void SavePlayer(Player plr)
		{
			this.AddPlayerToCache(plr.SteamID, plr);
			Thread saveThread = new Thread(delegate(){
				SavePlayerThread(plr);
			});
			saveThread.Start();
		}

		/// <summary>
		/// Threaded player save
		/// </summary>
		/// <param name="plr">Plr.</param>
		private void SavePlayerThread(Player plr)
		{
			Stopwatch watch = new Stopwatch();
			watch.Start();

			MemoryStream mStream = new MemoryStream();
			m_playerSerializer.Serialize(mStream, plr);
			mStream.Seek(0, SeekOrigin.Begin);
			HttpRequest request = new HttpRequest(m_host + m_playerUrl);
			Stream reader = request.DoPost(new StreamReader(mStream).ReadToEnd());
			reader.Close();

			if ( request.ResponseStatus != 200 ) // HTTP OK
			{
				FileStream fileStream = new FileStream(@"data/Player-" + plr.SteamID + ".xml", FileMode.OpenOrCreate);
				m_playerSerializer.Serialize(fileStream, plr);
				fileStream.Flush();
				fileStream.Close();
			}

			watch.Stop();
			Console.WriteLine("User {0} saved in {1}ms", plr.Name, watch.ElapsedMilliseconds);
		}

		public Player LoadPlayer(string steamID)
		{
			Player player;
			if (m_playerCache.TryGetValue(steamID, out player))
				return player;

			try
			{
				Stopwatch watch = new Stopwatch();
				watch.Start();
				HttpRequest req = new HttpRequest(m_host + m_playerUrl + "/" + steamID);
				Stream stream = req.DoGet();

#if DEBUG
				Console.WriteLine("Player XML: " + new StreamReader(stream).ReadToEnd());
				stream.Seek(0, SeekOrigin.Begin);
#endif
				player = m_playerSerializer.Deserialize(new XmlTextReader(stream)) as Player;

				this.AddPlayerToCache(steamID, player);

#if DEBUG
				watch.Stop();
				Console.WriteLine("Player loaded successfully in " + watch.ElapsedMilliseconds + "ms");
#endif

				return player;
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception while requesting player!" + e.Message + "\n" + e.StackTrace );
				return null;
			}
		}


		/// <summary>
		/// Adding player to local cache
		/// </summary>
		/// <param name="steamID">Steam ID</param>
		/// <param name="player">the player entity to persist in cache</param>
		private void AddPlayerToCache (string steamID, Player player)
		{
			// Evicit now
			if(m_playerCache.ContainsKey(steamID))
				m_playerCache.Remove(steamID);
			
			m_playerCache.Add( steamID, player );
			
			// TODO: timers
			new Timer(delegate {
				// Evicit from cache
				if(m_playerCache.ContainsKey(steamID))
					m_playerCache.Remove(steamID);
				
				Console.WriteLine("There is " + m_playerCache.Count + " object in the player cache!");
			}, null, 1000 * 60, Timeout.Infinite);
		}
    }
}

