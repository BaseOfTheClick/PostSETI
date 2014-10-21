#include "planet.h"
using namespace std;

SpaceObject::SpaceObject() = default;

const int SpaceObject::x() const { return co.x; }
const int SpaceObject::y() const { return co.y; }

SpaceObject::Vector2D&
SpaceObject::vector() { return co; }

string
Resources::standing() const
{
    string temp = "Lead:";

    return temp;
}

Planet::Planet() = default;

