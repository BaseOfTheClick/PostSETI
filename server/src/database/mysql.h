/* MySQL connector for the [server] part of SETI-Jam */
#ifndef MYSQL_H
#define MYSQL_H

#include <mysql/mysql.h>

namespace SQL
{

    struct Server
    {
        const char *host;
        unsigned int port;
    };

    struct User
    {
        const char *name;
        const char *passwd;
    };

    class Query
    {
        MYSQL_RES *res {nullptr};

    public:
        Query();
        ~Query();
    };

    class Connection
    {
        MYSQL *conn {nullptr};

    public:
        Connection();
        ~Connection();

        operator MYSQL*();

        bool connect(const Server& srv, const User& user,
                     const char *db, unsigned long flags);
    };
};

#endif /* MYSQL_H */


