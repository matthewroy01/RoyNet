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
	Peer()
	{
		tt.px = tt.py = tt.pz = 0;
		tt.rx = tt.ry = tt.rz = 0;
		tt.typeID = ID_TEST_TRANSFORM;
	};
	~Peer() = default;

	RakNet::RakPeerInterface *peer;
	RakNet::Packet *packet;

	Msg_TestTransform tt;

	unsigned short port = 60000;

	virtual void Initialize() = 0;
	virtual void NetworkingReceive() = 0;
	virtual void NetworkingSend() = 0;

	Msg_TestTransform getTestTransform() { return tt; };
};

#endif // !PEER_H