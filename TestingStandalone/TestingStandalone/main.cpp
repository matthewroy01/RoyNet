#include "../../Plugin/RoyNetPlugin/RoyNetPlugin/Server.h"
#include "../../Plugin/RoyNetPlugin/RoyNetPlugin/Client.h"

#include <iostream>

Peer* netInstance = NULL;

int rnStart(int isServer)
{
	// check if we're server or client
	if (isServer == 1)
	{
		netInstance = new Server();
		std::cout << "server start" << std::endl;
	}
	else
	{
		netInstance = new Client();
		std::cout << "client start" << std::endl;
	}

	// initialize the instance
	netInstance->Initialize();

	return 0;
}

int rnUpdate()
{
	int testCount = 100;

	while (true)
	{
		// receive messages
		netInstance->NetworkingReceive();

		// send messages
		netInstance->NetworkingSend();
	}

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

int main()
{
	bool correctInput = false;
	char tmp;

	while (!correctInput)
	{
		std::cout << "Enter 'C' for Client, 'S' for Server: ";
		std::cin >> tmp;

		if (tmp == 'C' || tmp == 'c')
		{
			rnStart(0);
			correctInput = true;
		}
		else if (tmp == 'S' || tmp == 's')
		{
			rnStart(1);
			correctInput = true;
		}
		else
		{
			std::cout << "Invalid input..." << std::endl;
		}
	}

	rnUpdate();

	std::cin.get();

	rnStop();

	std::cin.get();
}