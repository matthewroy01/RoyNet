#ifndef CLIENT_H
#define CLIENT_H

#include "Peer.h"

class Client : public Peer
{
public:
	Client() = default;
	~Client() = default;

	void Initialize() override;
	void NetworkingReceive() override;

	char clientName[MAX_NAME_LENGTH];
	int num;
};

#endif // !CLIENT_H