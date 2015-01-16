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

        [XmlAttribute("Name")]
        public string Name {get; set;}

        [XmlAttribute("SteamID")]
        public string SteamID {get; set;}

        [XmlAttribute("Reason")]
        public string Reason {get; set;}

        [XmlAttribute("BannedBy")]
        public string BannedBy {get; set;}

        [XmlAttribute("BanTime")]
        public DateTime BanTime {get; set;}

        [XmlAttribute("Expires")]
        public int Expires { get; set; }
    }
}