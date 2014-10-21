#ifndef GALAXY_H
#define GALAXY_H

#include "player.h"
#include <unordered_map>
#include <random>

template<typename T1, typename T2>
struct umap : public std::unordered_map<T1, T2>
{
    using std::unordered_map<T1, T2>::unordered_map;
};

class Galaxy : public umap<std::string, Player>
{
    const int width = 4096; // 4096x4096 structure
    int area = width * width;

    std::random_device dev;
    std::mt19937 gen;

    umap<int, int> reserved;
    
public:
    Galaxy();
    Player& newPlayer(std::string name);
    void rmPlayer(std::string name);

private:
    int randomize();
};

#endif

