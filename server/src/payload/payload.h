/* Authors: Kevin Morris, Gani Parrott, Jesse Hamburger
 * File: tcp.h
 * Payload module for this [server] aspect of multiplayer at SETI-Jam */
#ifndef PAYLOAD_H
#define PAYLOAD_H

#include <random>
#include <map>

class Vector2D
{
protected:
    int xc {-1}, yc {-1};

public:
    const int x() const { return xc; }
    const int y() const { return yc; }
};

class RandomVector2D : public Vector2D
{
    std::random_device rd;
    std::mt19937 mt;
    
public:
    RandomVector2D()
    {
    }

    RandomVector2D& operator=(const RandomVector2D& other)
    {
        xc = other.xc;
        yc = other.yc;
        return *this;
    }

    RandomVector2D& randomize(int min, int max);
};

class SpaceObject
{
    double radius = 1;
    RandomVector2D co;

public:
    SpaceObject& operator=(const SpaceObject& other)
    {
        radius = other.radius;
        co = other.co;
        return *this;
    }

    RandomVector2D& coords()
    {
        return co;
    }

};

class Galaxy
{
    int width = 4096; // width = length, we're square
    std::map<int, std::map<int, SpaceObject>> fields;

public:
    const int W() const { return width; }
    const int L() const { return width; }

    SpaceObject& spawn();
};

#endif /* PAYLOAD_H */

