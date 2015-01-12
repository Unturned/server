using System;
using Unturned;

[Serializable]
public class NetworkBanned : INetworkBanned {

    public NetworkBanned(string name, string steamId, string reason, string bannedBy, DateTime banTime) {
		this.Name = name;
		this.SteamID = steamId;
		this.Reason = reason;
		this.BannedBy = bannedBy;
		this.BanTime = banTime;
        this.Expires = -1;
	}

    public string Name {get; private set;}

    public string SteamID {get; private set;}

    public string Reason {get; private set;}

    public string BannedBy {get; private set;}
    public DateTime BanTime {get; private set;}
    public int Expires { get; set; }

}