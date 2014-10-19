/* Authors: Kevin Morris, Gani Parrott, Jesse Hamburger
 * File: client.h
 * TCP Module for this [server] aspect of multiplayer at SETI-Jam */
#ifndef CLIENT_H
#define CLIENT_H

#include "tcp.h"
#include <memory>

class ClientSocket : public Socket
{
public:
    ClientSocket();
    ClientSocket(int&& fd);
    ~ClientSocket();
};

// Let's keep this for later
struct Client
{
    ClientAddress addr;
    ClientSocket sock;
};

#endif /* CLIENT_H */



