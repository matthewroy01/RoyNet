// RoyNetPlugin.cpp : Defines the exported functions for the DLL application.

#include "stdafx.h"

#include "RoyNetPlugin.h"

#include "Server.h"
#include "Client.h"

// singleton instance for networking
Peer* netInstance = NULL;

int testAdd(int a, int b)
{
	return a + b;
}

int rnStart(int isServer)
{
	// check if we're server or client
	if (isServer == 1)
	{
		netInstance = new Server();
	}
	else
	{
		netInstance = new Client();
	}

	// initialize the instance
	netInstance->Initialize();

	return 0;
}

int rnUpdate()
{
	// receive messages
	netInstance->NetworkingReceive();

	return 0;
}

int rnStop()
{
	// shut down networking by destroying peer interface instance
	if (netInstance != NULL)
	{
		RakNet::RakPeerInterface::DestroyInstance(netInstance->peer);
		delete netInstance;
		netInstance = NULL;
		return 0;
	}

	return 1;
}