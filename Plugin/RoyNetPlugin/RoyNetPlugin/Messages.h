#ifndef MESSAGES_H
#define MESSAGES_H

#include <string>

const int DEFAULT_MAX_CLIENTS = 10;
const int DEFAULT_PORT = 60000;
const std::string DEFAULT_IP_ADDRESS = "127.0.0.1";
const std::string DEFAULT_CLIENT_NAME = "RoyNet Client";

enum GameMessages
{
	ID_TEST_MESSAGE
};

#endif // !MESSAGES_H