/* MySQL connector for the [server] part of SETI-Jam */
#include "mysql.h"

SQL::Query::Query()
{
}

SQL::Query::~Query()
{
    if(res)
        mysql_free_result(res);
}

SQL::Connection::Connection()
{
    conn = mysql_init(conn);
}

SQL::Connection::~Connection()
{
    mysql_close(conn);
}

SQL::Connection::operator MYSQL*()
{
    return conn;
}

bool
SQL::Connection::connect(const Server& srv, const User& user,
                         const char *db, unsigned long flags)
{
    conn = mysql_real_connect(conn, srv.host, user.name,
                              user.passwd, db, srv.port,
                              nullptr, flags);
    return conn;
}

