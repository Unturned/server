using System;

public class NetworkBanned {
	public string name;
	public string id;
	public string reason;
	public string bannedBy;
	public DateTime banTime;

	public NetworkBanned(string newName, string newID, string reason, string bannedBy, DateTime banTime) {
		this.name = newName;
		this.id = newID;
		this.reason = reason;
		this.bannedBy = bannedBy;
		this.banTime = banTime;
	}
}