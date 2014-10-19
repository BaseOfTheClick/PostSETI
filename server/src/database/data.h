#ifndef DATA_H
#define DATA_H

#include <cstring>
#include <cstdint>
#include <random>

struct Coordinates
{
    int x, y;
};

struct Aspect
{
    uint32_t level;
    const uint32_t max {100};

    Aspect()
    {
        std::random_device rd;
        std::mt19937 gen(rd());
        std::uniform_int_distribution<uint32_t> dist(50,80);
        level = dist(gen);
    }
};

class Resources
{
    Aspect economy;
    Aspect politics;
    Aspect research; 
};

class Planet
{
    Coordinates co;
    uint32_t _radius;
    Resources re;
    char *_name;
    uint64_t _age;

public:
    Planet(Coordinates&& coords, const char *pname, uint64_t age)
        : co(std::move(coords))
        , _name(new char[strlen(pname) + 1])
        , _age(age)
    {
        strncpy(_name, pname, strlen(pname));
    }

    ~Planet()
    {
        delete [] _name;
    }

    const Coordinates& coords() const
    {
        return co;
    }

    uint32_t& radius()
    {
        return _radius;
    }

    const uint32_t radius() const
    {
        return _radius;
    }

    Resources& resource()
    {
        return re;
    }

    const Resources& resource() const
    {
        return re;
    }

    const char *name() const
    {
        return _name;
    }

    uint64_t& age()
    {
        return _age;
    }

    const uint64_t age() const
    {
        return _age;
    }

};

#endif /* DATA_H */




