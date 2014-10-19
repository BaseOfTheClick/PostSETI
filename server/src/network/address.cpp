/* Authors: Kevin Morris, Gani Parrott, Jesse Hamburger
 * File: address.cpp
 * Address Module for this [server] aspect of multiplayer at SETI-Jam */
#include "address.h"
#include <cstring>
using namespace std;

Address::Address() : _res(nullptr)
{
}

Address::Address(int inet, int type, int prot)
    : _res(nullptr)
    , _ai({inet, type, prot})
{
}

Address::Address(const char *host, const char *port)
{
}

Address::~Address()
{
    freeaddrinfo(_res);
}

vector<struct addrinfo *>::iterator
Address::begin()
{
    return _addrList.begin();
}

vector<struct addrinfo *>::iterator
Address::end()
{
    return _addrList.end();
}

vector<struct addrinfo *>::const_iterator
Address::cbegin() const
{
    return _addrList.cbegin();
}

vector<struct addrinfo *>::const_iterator
Address::cend() const
{
    return _addrList.cend();
}

bool Address::getHost(const char *host, const char *port)
{
    struct addrinfo hints;
    memset(&hints, 0, sizeof(struct addrinfo));
    hints.ai_family = _ai.inet;
    hints.ai_socktype = _ai.type;
    hints.ai_protocol = _ai.prot;

    if(getaddrinfo(host, port, &hints, &_res))
        return false;

    struct addrinfo *ptr;
    for(ptr = _res; ptr != nullptr; ptr = ptr->ai_next)
        _addrList.emplace_back(ptr);

    return true;
}

// ClientAddress defs
ClientAddress::ClientAddress()
    : sa(new struct sockaddr_in)
    , len(new socklen_t)
{
    *len = sizeof(struct sockaddr_in);
}

ClientAddress::~ClientAddress()
{
    if(len)
        delete len;

    if(sa)
        delete sa;
}





