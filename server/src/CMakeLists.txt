
add_executable(SETI-Server
    main.cpp
    game/galaxy.cpp
    game/player.cpp
    game/planet.cpp
    #    payload/payload.cpp
    #    database/mysql.cpp
    network/select.cpp
    network/client.cpp
    network/server.cpp
    network/tcp.cpp
    network/address.cpp
    log/log.cpp
)

#target_link_libraries(SETI-Server mysqlclient)

