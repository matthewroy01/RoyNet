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

struct Msg_Int : public Msg
{
	int num;
};

struct Packet
{
	int ID;
	char data[256];
};

#pragma pack(pop)

enum GameMessages
{
	ID_CUSTOM_MESSAGE_START = ID_USER_PACKET_ENUM,
	ID_TEST_MESSAGE,
	ID_CONNECTION_CONFIRMATION_MESSAGE,
};

#endif // !MESSAGES_H