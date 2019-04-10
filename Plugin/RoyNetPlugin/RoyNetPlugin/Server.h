#ifndef SERVER_H
#define SERVER_H

#include "Peer.h"

#include <vector>
#include <string>

struct ClientData
{
	RakNet::RakNetGUID address;
	int num;
};

class Server : public Peer
{
public:
	Server() = default;
	~Server() = default;

	void Initialize() override;
	void NetworkingReceive() override;
	void NetworkingSend() override;

	std::vector<ClientData> clients;
};

#endif // !SERVER_H