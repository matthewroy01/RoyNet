#ifndef MESSAGES_H
#define MESSAGES_H

#include <string>

const int DEFAULT_MAX_CLIENTS = 10;
const int DEFAULT_PORT = 60000;
const std::string DEFAULT_IP_ADDRESS = "127.0.0.1";
const std::string DEFAULT_CLIENT_NAME = "RoyNet Client";

const int MAX_NAME_LENGTH = 20;

#pragma pack(push, 1)

struct Msg
{
	unsigned char typeID;
};

struct Msg_Connection_Confirmation : public Msg
{
	int num;
};

#pragma pack(pop)

enum GameMessages
{
	ID_CONNECTION_CONFIRMATION_MESSAGE
};

#endif // !MESSAGES_H