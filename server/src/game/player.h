#ifndef PLAYER_H
#define PLAYER_H

#include "planet.h"
#include <string>

class Player
{
    std::string pname;
    Planet planet;

public:
    Player();
    Player(std::string&& name);

    const std::string& name() const;
    Planet& world();

    int score{0};

};

#endif


