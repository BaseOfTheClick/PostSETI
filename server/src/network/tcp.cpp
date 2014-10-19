/* Authors: Kevin Morris, Gani Parrott, Jesse Hamburger
 * File: tcp.cpp
 * TCP Module for this [server] aspect of multiplayer at SETI-Jam */
#include "tcp.h"
#include <sys/socket.h>
#include <sys/types.h>
#include <unistd.h>
#include <cstring>
#include <utility>
using namespace std;

Buffer::Buffer() : _buffer(nullptr)
{
}

Buffer::Buffer(int bufferSize)
    : _buffer(new char[bufferSize])
{
}

Buffer::~Buffer()
{
    if(_buffer)
        delete [] _buffer;
}

Socket::Socket()
    : _fd(0)
{
    // Default constructor
}

Socket::Socket(int inet, int type, int prot)
{
    this->close();
    _fd = socket(inet, type, prot);
}

Socket::~Socket()
{
    //this->close();
}

Socket::operator bool()
{
    return _fd > 0;
}

Socket::operator int() const
{
    return _fd;
}

Socket::operator int&()
{
    return _fd;
}

Socket& Socket::operator=(int&& fd)
{
    _fd = move(fd);
    return *this;
}

void Socket::setSockOpt(int opt, int on)
{
    int optval = on;
    setsockopt(_fd, SOL_SOCKET, opt, &optval, sizeof(int));
}

void Socket::setNonBlock(int on)
{
    int f = fcntl(_fd, F_GETFL, 0);
    fcntl(_fd, F_SETFL, on ? f | O_NONBLOCK : f | ~(O_NONBLOCK));
}

Socket& Socket::write(const char *data)
{
    ::write(_fd, data, strlen(data));
    return *this;
}

Socket& Socket::close()
{
    if(_fd > 0)
        ::close(_fd);
    return *this;
}


