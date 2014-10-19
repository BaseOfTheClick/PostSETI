/* Authors: Kevin Morris, Gani Parrott, Jesse Hamburger
 * File: address.h
 * Address Module for this [server] aspect of multiplayer at SETI-Jam */
#ifndef ADDRESS_H
#define ADDRESS_H

#include <arpa/inet.h>
#include <netdb.h>
#include <vector>

struct AddressInfo
{
    int inet, type, prot;
};

class Address
{
    struct addrinfo *_res;
    AddressInfo _ai;
    
    std::vector<struct addrinfo *> _addrList;

public:
    Address();
    Address(int inet, int type, int prot);
    Address(const char *host, const char *port);
    ~Address();

    std::vector<struct addrinfo *>::iterator begin();
    std::vector<struct addrinfo *>::iterator end();

    std::vector<struct addrinfo *>::const_iterator cbegin() const;
    std::vector<struct addrinfo *>::const_iterator cend() const;

    bool getHost(const char *host, const char *port);

};

struct ClientAddress
{
    sockaddr_in *sa;
    socklen_t *len;
    ClientAddress();
    ~ClientAddress();
};

#endif /* ADDRESS_H */



