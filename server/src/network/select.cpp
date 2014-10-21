/* Authors: Kevin Morris, Gani Parrott, Jesse Hamburger
 * File: select.cpp
 * TCP Module for this [server] aspect of multiplayer at SETI-Jam */
#include "select.h"
#include "../tools/remove_if.hpp"

Multiplexer::Multiplexer()
{
    FD_ZERO(&afds);
    FD_ZERO(&wfds);
    FD_ZERO(&rfds);
}

void Multiplexer::insert(int sock)
{
    FD_SET(sock, &afds);
}

void Multiplexer::eradicate(int sock)
{
    FD_CLR(sock, &afds);
}

int Multiplexer::poll()
{
    rfds = wfds = afds;
    return select(FD_SETSIZE, &rfds, &wfds, nullptr, nullptr);
}

bool Multiplexer::setWrite(int sock)
{
    return FD_ISSET(sock, &wfds);
}

bool Multiplexer::setRead(int sock)
{
    return FD_ISSET(sock, &rfds);
}




