/* Authors: Kevin Morris, Gani Parrott, Jesse Hamburger
 * File: tcp.h
 * Payload module for this [server] aspect of multiplayer at SETI-Jam */
#include "payload.h"

RandomVector2D&
RandomVector2D::randomize(int min, int max)
{
    mt = std::mt19937(RandomVector2D::rd());
    std::uniform_int_distribution<int> dist(min, max);

    Vector2D::xc = dist(mt);
    Vector2D::yc = dist(mt);

    return *this;
}

SpaceObject& Galaxy::spawn()
{
    SpaceObject object;
    object.coords().randomize(0, width * width);
    RandomVector2D& co = object.coords();

    while(fields.find(co.x()) != fields.end())
    {
        if(fields[co.x()].find(co.y()) != fields[co.x()].end())
        {
            co.randomize(0, width * width);
            continue;
        }
        break;
    }

    fields[co.x()][co.y()] = object;

    return fields[co.x()][co.y()];
}

