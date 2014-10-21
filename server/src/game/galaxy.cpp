#include "galaxy.h"
using namespace std;

Galaxy::Galaxy() = default;
#include <iostream>
using namespace std;
Player& Galaxy::newPlayer(string name)
{
//    if(this->find(name) != this->end())
//        throw std::domain_error("gg, wp");

    auto player = Player();
    auto& world = player.world();
    
    world.vector().x = randomize();
    world.vector().y = randomize();

    while(reserved.find(world.vector().x) != reserved.end())
    {
        if(reserved.find(world.vector().y) != reserved.end())
        {
            world.vector().y = randomize();
            continue;
        }

        world.vector().x = randomize();
    }

    (*this)[name] = move(player);

    cout << name;
    return (*this)[name];
}

void Galaxy::rmPlayer(std::string name)
{
    if(this->find(name) != this->end())
        this->erase(name);
}

int Galaxy::randomize()
{
    gen = mt19937(dev());
    uniform_int_distribution<int> dist(0, area);
    return dist(gen);
}


