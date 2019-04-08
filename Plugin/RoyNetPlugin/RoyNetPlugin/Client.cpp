#include "stdafx.h"

#include "Client.h"

void Client::Initialize()
{
	// buffer
	const unsigned int bufferSize = 512;
	char str[bufferSize];

	// create and return instance of peer interface
	peer = RakNet::RakPeerInterface::GetInstance();

	// global peer settings for this app
	port = DEFAULT_PORT;
	RakNet::SocketDescriptor sd;
	peer->Startup(1, &sd, 1);

	// save IP address and name
	strcpy_s(str, DEFAULT_IP_ADDRESS.c_str());
	strcpy_s(clientName, DEFAULT_CLIENT_NAME.c_str());

	// set client num to -1 by default
	num = -1;

	// start the client by connecting to the given IP and port
	peer->Connect(str, port, 0, 0);
}

void Client::NetworkingReceive()
{
	for (packet = peer->Receive(); packet; peer->DeallocatePacket(packet), packet = peer->Receive())
	{
		switch (packet->data[0])
		{
			case ID_CONNECTION_CONFIRMATION_MESSAGE:
			{
				const Msg_Connection_Confirmation* msg = (Msg_Connection_Confirmation*)packet->data;
				num = msg->num;

				break;
			}
			default:
			{
				break;
			}
		}
	}
}