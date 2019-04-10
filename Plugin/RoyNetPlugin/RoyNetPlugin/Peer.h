#ifndef PEER_H
#define PEER_H

// RakNet includes (linked to Dev_SDKs created by Dan Buckstein)
#include "RakNet/RakPeerInterface.h"
#include "RakNet/MessageIdentifiers.h"
#include "RakNet/RakNetTypes.h"
#include "RakNet/BitStream.h"
#include "RakNet/GetTime.h"

#include <string>

#include "Messages.h"

class Peer
{
public:
	Peer() = default;
	~Peer() = default;

	RakNet::RakPeerInterface *peer;
	RakNet::Packet *packet;

	unsigned short port = 60000;

	virtual void Initialize() = 0;
	virtual void NetworkingReceive() = 0;
	virtual void NetworkingSend() = 0;
};

#endif // !PEER_H