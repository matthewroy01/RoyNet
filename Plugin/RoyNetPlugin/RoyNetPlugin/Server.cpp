#include "stdafx.h"

#include "Server.h"

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
			case ID_NEW_INCOMING_CONNECTION:
			{
				// add the client to the connected users list
				ClientData newClient;
				newClient.address = packet->guid;
				clients.push_back(newClient);

				// send to the client that just connected the current information of the lobby and their player number
				Msg_Connection_Confirmation connectMsg;
				connectMsg.typeID = ID_CONNECTION_CONFIRMATION_MESSAGE;

				// for each connected user
				for (int i = 0; i < clients.size(); i++)
				{
					// loop through vector to determine the index of the newly added client	
					if (packet->guid == clients[i].address)
					{
						connectMsg.num = i + 1;
					}
				}

				// send message back to client to confirm connection
				peer->Send((char*)&connectMsg, sizeof(Msg_Connection_Confirmation), HIGH_PRIORITY, RELIABLE_ORDERED, 0, packet->systemAddress, false);

				break;
			}
			default:
			{
				break;
			}
		}
	}
}