using System;
using UnityEngine;

public class Server
{
	public string ip;

	public int port;

	public string guid;

	public int players;

	public int max;

	public string name;

	public bool pvp;

	public int mode;

	public bool dedicated;

	public bool save;

	public int map;

	public string version;

	public int ping;

	public int reference;

	public bool passworded;

	public Ping pinger;

	public Server(HostData data)
	{
		this.name = data.gameName;
		this.ip = data.ip[0];
		this.port = data.port;
		this.guid = data.guid;
		this.players = data.connectedPlayers;
		this.max = data.playerLimit;
		this.ping = 0;
		this.reference = -1;
		if (this.max < 1)
		{
			this.max = 1;
		}
		else if (this.max > 12)
		{
			this.max = 12;
		}
		string[] strArrays = Packer.unpack(data.comment, ';');
		this.pvp = strArrays[0] == "t";
		this.mode = int.Parse(strArrays[1]);
		this.dedicated = strArrays[2] == "t";
		this.save = strArrays[3] == "t";
		this.map = int.Parse(strArrays[4]);
		this.version = strArrays[5];
		this.passworded = strArrays[6] == "t";
		this.pinger = new Ping(this.ip);
	}
}