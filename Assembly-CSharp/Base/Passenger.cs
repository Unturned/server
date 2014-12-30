using System;
using UnityEngine;

public class Passenger
{
	public NetworkPlayer player;

	public Passenger(NetworkPlayer setPlayer)
	{
		this.player = setPlayer;
	}
}