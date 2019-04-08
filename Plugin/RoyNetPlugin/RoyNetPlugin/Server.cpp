#include "stdafx.h"

#include "Server.h"

#include "Messages.h"

void Server::Initialize()
{
	// create and return instance of peer interface
	peer = RakNet::RakPeerInterface::GetInstance();

	// global peer settings for this app
	port = DEFAULT_PORT;
	RakNet::SocketDescriptor sd(port, 0);
	peer->Startup(DEFAULT_MAX_CLIENTS, &sd, 1);

	// we need to let the server accept incoming connections from the clients
	peer->SetMaximumIncomingConnections(DEFAULT_MAX_CLIENTS);
}

void Server::NetworkingReceive()
{
	for (packet = peer->Receive(); packet; peer->DeallocatePacket(packet), packet = peer->Receive())
	{
		switch (packet->data[0])
		{
			case 0:
			{

			}
			default:
			{
				break;
			}
		}
	}
}