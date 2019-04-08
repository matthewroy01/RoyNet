#ifndef SERVER_H
#define SERVER_H

#include "Peer.h"

class Server : public Peer
{
public:
	Server() = default;
	~Server() = default;

	void Initialize() override;
	void NetworkingReceive() override;
};

#endif // !SERVER_H