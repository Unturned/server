using System;
using UnityEngine;

public class NetworkMasterServer
{
	public readonly static uint OCTET_0;

	public readonly static uint OCTET_1;

	public readonly static uint OCTET_2;

	public readonly static bool STEAM_MASTER_SERVER;

	public readonly static bool UNITY_UP;

	static NetworkMasterServer() {
		NetworkMasterServer.OCTET_0 = (uint)Mathf.Pow(256f, 3f);
		NetworkMasterServer.OCTET_1 = (uint)Mathf.Pow(256f, 2f);
		NetworkMasterServer.OCTET_2 = 256;
		NetworkMasterServer.STEAM_MASTER_SERVER = false;
		NetworkMasterServer.UNITY_UP = true;
	}

	public NetworkMasterServer() {
	}

	public static uint getUInt32FromIP(string ip)
	{
		ip = string.Concat(ip, ".");
		string[] strArrays = Packer.unpack(ip, '.');
		uint num = 0;
		num = num + uint.Parse(strArrays[0]) * NetworkMasterServer.OCTET_0;
		num = num + uint.Parse(strArrays[1]) * NetworkMasterServer.OCTET_1;
		num = num + uint.Parse(strArrays[2]) * NetworkMasterServer.OCTET_2;
		return num + uint.Parse(strArrays[3]);
	}

	public static void register(string ip, int port) {
		NetworkMasterServer.getUInt32FromIP(ip);
		ushort num = (ushort)port;
		ushort num1 = (ushort)(num + 1);
		ushort num2 = (ushort)(num + 2);
	}
}