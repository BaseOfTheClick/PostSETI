/* Authors: Kevin Morris, Gani Parrott, Jesse Hamburger
 * File: tcp.h
 * TCP Module for this [server] aspect of multiplayer at SETI-Jam */
#ifndef TCP_H
#define TCP_H

#include "address.h"
#include <fcntl.h>

class Buffer
{
    char *_buffer;

public:
    Buffer();
    Buffer(int bufferSize);
    ~Buffer();

};

class Socket
{
protected:
    int _fd {0};

public:
    Socket();
    Socket(int inet, int type, int prot);
    ~Socket();

    Socket& operator=(int&& fd);

    explicit operator bool();
    operator int() const;
    operator int&();

    void setSockOpt(int opt, int on);
    void setNonBlock(int on);

    Socket& write(const char *data);
    Socket& close();

};

#endif /* TCP_H */


