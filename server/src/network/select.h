/* Authors: Kevin Morris, Gani Parrott, Jesse Hamburger
 * File: select.h
 * TCP Module for this [server] aspect of multiplayer at SETI-Jam */
#ifndef SELECT_H
#define SELECT_H

#include "tcp.h"
#include <sys/select.h>
#include <vector>

class Multiplexer
{
    fd_set afds, wfds, rfds;

public:
    Multiplexer();
    void insert(int sock);
    void eradicate(int sock);

    int poll();
    bool setWrite(int sock);
    bool setRead(int sock);

};

#endif /* SELECT_H */

