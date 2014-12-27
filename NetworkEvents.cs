using System;
using UnityEngine;

public class NetworkEvents
{
	public NetworkEvents()
	{
	}

	public static void clear()
	{
		NetworkEvents.onRegionUpdate = null;
		NetworkEvents.onConnecting = null;
		NetworkEvents.onConnected = null;
		NetworkEvents.onDisconnecting = null;
		NetworkEvents.onDisconnected = null;
		NetworkEvents.onHosting = null;
		NetworkEvents.onHosted = null;
		NetworkEvents.onReady = null;
		NetworkEvents.onFailed = null;
		NetworkEvents.onPlayerConnected = null;
		NetworkEvents.onPlayerDisconnected = null;
		NetworkEvents.onPlayersChanged = null;
	}

	public static void triggerOnConnected()
	{
		if (NetworkEvents.onConnected != null)
		{
			NetworkEvents.onConnected();
		}
	}

	public static void triggerOnConnecting()
	{
		if (NetworkEvents.onConnecting != null)
		{
			NetworkEvents.onConnecting();
		}
	}

	public static void triggerOnDisconnected()
	{
		if (NetworkEvents.onDisconnected != null)
		{
			NetworkEvents.onDisconnected();
		}
	}

	public static void triggerOnDisconnecting()
	{
		if (NetworkEvents.onDisconnecting != null)
		{
			NetworkEvents.onDisconnecting();
		}
	}

	public static void triggerOnFailed(int id)
	{
		if (NetworkEvents.onFailed != null)
		{
			NetworkEvents.onFailed(id);
		}
	}

	public static void triggerOnHosted()
	{
		if (NetworkEvents.onHosted != null)
		{
			NetworkEvents.onHosted();
		}
	}

	public static void triggerOnHosting()
	{
		if (NetworkEvents.onHosting != null)
		{
			NetworkEvents.onHosting();
		}
	}

	public static void triggerOnPlayerConnected(NetworkPlayer player)
	{
		if (NetworkEvents.onPlayerConnected != null)
		{
			NetworkEvents.onPlayerConnected(player);
		}
	}

	public static void triggerOnPlayerDisconnected(NetworkPlayer player)
	{
		if (NetworkEvents.onPlayerDisconnected != null)
		{
			NetworkEvents.onPlayerDisconnected(player);
		}
	}

	public static void triggerOnPlayersChanged()
	{
		if (NetworkEvents.onPlayersChanged != null)
		{
			NetworkEvents.onPlayersChanged();
		}
	}

	public static void triggerOnReady()
	{
		if (NetworkEvents.onReady != null)
		{
			NetworkEvents.onReady();
		}
	}

	public static void triggerOnRegionUpdate()
	{
		if (NetworkEvents.onRegionUpdate != null)
		{
			NetworkEvents.onRegionUpdate();
		}
	}

	public static event NetworkEventDelegate onConnected;

	public static event NetworkEventDelegate onConnecting;

	public static event NetworkEventDelegate onDisconnected;

	public static event NetworkEventDelegate onDisconnecting;

	public static event NetworkErrorDelegate onFailed;

	public static event NetworkEventDelegate onHosted;

	public static event NetworkEventDelegate onHosting;

	public static event NetworkPlayerDelegate onPlayerConnected;

	public static event NetworkPlayerDelegate onPlayerDisconnected;

	public static event NetworkEventDelegate onPlayersChanged;

	public static event NetworkEventDelegate onReady;

	public static event NetworkEventDelegate onRegionUpdate;
}