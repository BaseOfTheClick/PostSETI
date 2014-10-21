#include <iostream>
#include <sys/socket.h>
#include <sys/types.h>
#include <netdb.h>
#include <arpa/inet.h>
#include <map>
#include <vector>
#include <cstring>
#include <cstdlib>
#include <unistd.h>
using namespace std;

class Socket
{
    int sock;

public:
    Socket() = default;
    ~Socket() { if(sock > 0) close(sock); }

    operator int&() { return sock; }

    int& fd() { return sock; }

};

int error(string message)
{
    cout << message << endl;
    return 0;
}

int main(int argc, char *argv[])
{
    if(argc != 4)
        return error("usage: " + string(argv[0]) + " host port nick");

    string host(argv[1]), port(argv[2]);

    Socket sock;
    struct addrinfo *res, *ptr, hints;

    memset(&hints, 0, sizeof(hints));
    hints.ai_family = AF_INET;
    hints.ai_socktype = SOCK_STREAM;
    hints.ai_protocol = IPPROTO_TCP;

    if(getaddrinfo(argv[1], argv[2], &hints, &res))
        return error("resolve " + host + ":" + port + " failed");

    for(ptr = res; ptr != nullptr; ptr = ptr->ai_next)
    {
        sock.fd() = socket(ptr->ai_family, ptr->ai_socktype, ptr->ai_protocol);
        if(sock.fd() == -1)
            continue;

        if(connect(sock.fd(), ptr->ai_addr, ptr->ai_addrlen))
        {
            error("failed to connect to a server address");
            continue;
        }

    }

    char buf[1024];

    string message = "Login:" + string(argv[3]) + "\n";
    send(sock.fd(), message.c_str(), message.size(), 0);

    int b = recv(sock.fd(), buf, 1023, 0);
    buf[b - 1] = '\0';
    cout << buf << endl;

    return 0;
}



