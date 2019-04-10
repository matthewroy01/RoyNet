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
				printf("connection received from client\n");

				// add the client to the connected users list
				ClientData newClient;
				newClient.address = packet->guid;
				clients.push_back(newClient);

				// send to the client that just connected the current information of the lobby and their player number
				Msg_Int msg;
				msg.typeID = ID_CONNECTION_CONFIRMATION_MESSAGE;

				// for each connected user
				for (unsigned int i = 0; i < clients.size(); i++)
				{
					// loop through vector to determine the index of the newly added client	
					if (packet->guid == clients[i].address)
					{
						msg.num = i + 1;
					}
				}

				// send message back to client to confirm connection
				peer->Send((char*)&msg, sizeof(Msg_Int), HIGH_PRIORITY, RELIABLE_ORDERED, 0, packet->systemAddress, false);

				break;
			}
			case ID_TEST_MESSAGE:
			{
				const Msg_Int* msg = (Msg_Int*)packet->data;

				printf("Message received from client\n");

				Msg_Int msg2;
				msg2.typeID = ID_TEST_MESSAGE;

				// send message back to client again
				for (unsigned int i = 0; i < clients.size(); ++i)
				{
					peer->Send((char*)&msg2, sizeof(Msg_Int), HIGH_PRIORITY, RELIABLE_ORDERED, 0, clients[i].address, false);
				}
				break;
			}
			default:
			{
				break;
			}
		}
	}
}

void Server::NetworkingSend()
{

}