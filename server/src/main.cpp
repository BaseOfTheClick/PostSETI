/* Authors: Kevin Morris, Gani Parrott, Jesse Hamburger
 * File: main.cpp
 * The main executable for a [server] portion of our SETI-Jam game */
#include "log/log.h"
#include "network/address.h"
#include "network/server.h"
#include "network/select.h"
#include "tools/remove_if.hpp"
#include "game/galaxy.h"
#include <map>
#include <memory>
#include <iostream>
using namespace std;

const char * const HOST = "0.0.0.0";
const char * const PORT = "31337";

void logHost(LogFile& log, const string& message)
{
    log << message + " " + string(HOST) + ":" + string(PORT);
}

#include <unistd.h>

int main(int argc, char *argv[])
{
    LogFile log("test.log");

    log << "**************************"
        << "New server session started";

    Address addr(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if(!addr.getHost(HOST, PORT))
    {
        logHost(log, "Unable to resolve");
        return 1;
    }
    else
    {
        logHost(log, "Resolved");
    }

    ServerSocket server;
    if(server.bind(addr) <= 0)
    {
        logHost(log, "Unable to bind to");
        return 2;
    }
    else
    {
        logHost(log, "Bound to");
    }

    cout << "Server FD: " << server << endl;

    if(!server.listen(10))
    {
        logHost(log, "Unable to listen on");
        return 3;
    }
    else
    {
        logHost(log, "Listening on");
        cout << "Listening on " << HOST << ":" << PORT << endl;
    }

    // Client and select poll structure setup
    //vector<ClientSocket> clients;
    map<int, unique_ptr<ClientSocket>> table;
    map<int, string> names;
    Multiplexer select;

    server.setNonBlock(1);
    select.insert(server);

    Galaxy galaxy;

    while(true)
    {
        if(select.poll() == -1)
        {
            cerr << "select.poll() error\n";
            break;
        }

        for(int i = 0; i < FD_SETSIZE; ++i)
        {
            if(select.setRead(i))
            {
                if(server == i)
                {
                    ClientSocket *client = new ClientSocket(server.accept());

                    if(client)
                    {
                        client->setNonBlock(1);
                        select.insert(*client);
                        table[*client] = unique_ptr<ClientSocket>(client);
                    }
                    else
                        log << "A client was rejected from the server";

                    continue;
                }

                char buf[256];
                int bytes = recv(i, &buf[0], 255, 0);
                if(bytes <= 0)
                {
                    table[i]->close();
                    galaxy.rmPlayer(names[i]);
                    select.eradicate(i);
                    continue;
                }

                buf[bytes] = '\0';
                string buffer(buf);

                cout << "Client: " << buffer;
                auto pos = buffer.find(':');
                if(buffer.substr(0, pos) == "Login")
                {
                    string name = buffer.substr(pos + 1,
                                                buffer.size() - pos);

                    Player *p;
                    try { p = &galaxy.newPlayer(name); }
                    catch(...)
                    {
                        table[i]->close();
                        select.eradicate(i);
                    } 

                    for(auto& e : name)
                    {
                        if(e == '\n')
                            e = ' ';
                    }
                    names[i] = name;

                    string planet = "Planet:" + to_string(p->world().x())
                                    + ":" + to_string(p->world().y()) + "\n";
                    table[i]->write(planet.c_str());

                }
                else if(buffer.substr(0, pos) == "Year")
                {
                    string years = buffer.substr(pos + 1,
                                                 buffer.size() - pos - 1);

                    int year = stoi(years);
                    galaxy[names[i]].score = year * 3.3;

                    string chart = "";
                    for(auto& e : names)
                    {
                        chart += e.second + ": "
                                 + to_string(galaxy[names[e.first]].score) + "\n";
                    }

                    for(auto& client : table)
                    {
                        client.second->write(chart.c_str());
                    }
                }
                // End of client handler block
            }
        }
    }

    for(auto& client : table)
    {
        if(client.second > 0)
            client.second->close();
    }

    return 0;
}


