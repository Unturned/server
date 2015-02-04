using System;
using System.Xml;
using System.Xml.Serialization;

namespace Unturned {
    [Serializable]
    [XmlRoot("ban")]
    public class BanEntry : IBanEntry {
        public BanEntry(string name, string steamId, string reason, string bannedBy, DateTime banTime) {
        	this.Name = name;
        	this.SteamID = steamId;
        	this.Reason = reason;
        	this.BannedBy = bannedBy;
        	this.BanTime = banTime;
            this.Expires = -1;
        }

        public BanEntry()
        {
        }

		string name;
        [XmlAttribute ("Name")]
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		string steamID;
        [XmlAttribute ("SteamID")]
		public string SteamID {
			get {
				return steamID;
			}
			set {
				steamID = value;
			}
		}

		string reason;
        [XmlAttribute ("Reason")]
		public string Reason {
			get {
				return reason;
			}
			set {
				reason = value;
			}
		}

		string bannedBy;
        [XmlAttribute ("BannedBy")]
		public string BannedBy {
			get {
				return bannedBy;
			}
			set {
				bannedBy = value;
			}
		}

		DateTime banTime;
        [XmlAttribute ("BanTime")]
		public DateTime BanTime {
			get {
				return banTime;
			}
			set {
				banTime = value;
			}
		}

		int expires;
        [XmlAttribute ("Expires")]
		public int Expires {
			get {
				return expires;
			}
			set {
				expires = value;
			}
		}
    }
}