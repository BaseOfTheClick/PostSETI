/* Authors: Kevin Morris, Gani Parrott, Jesse Hamburger
 * File: main.cpp
 * The main executable for a [server] portion of our SETI-Jam game */
#include "log/log.h"
#include "network/address.h"
#include "network/server.h"
#include "network/select.h"
#include "tools/remove_if.hpp"
#include <map>
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
    vector<ClientSocket> clients;
    map<int, ClientSocket*> table;
    Multiplexer select;

    server.setNonBlock(1);
    select.insert(&server);

    while(true)
    {
        if(select.poll() == -1)
        {
            cerr << "select.poll() error\n";
            break;
        }

        for(int i = 0; i < FD_SETSIZE; ++i)
        {
            if(i == server && select.setRead(i))
            {
                clients.emplace_back(ClientSocket());

                ClientSocket& client = clients.back();
                client = server.accept();

                if(client)
                {
                    client.setNonBlock(1);
                    select.insert(&client);
                    table[client] = &client;
                }
                else
                    log << "A client was rejected from the server";

            }
            else if(select.setRead(i))
            {
                char buf[512];

                int bytes = recv(i, buf, 511, 0);
                if(bytes < 0)
                {
                    select.eradicate(i);
                    tools::remove_if(clients,
                        [i](const ClientSocket& sock) {
                            return i == sock;
                        }
                    );
                }
                else
                {
                    buf[bytes - 1] = '\0';
                    cout << buf << endl;
                    table[i]->write("Yolo!\n");
                }
            }
        }

    }

    for(auto& client : clients)
        client.close();

    return 0;
}


