#include "player.h"
#include <utility>
using namespace std;

Player::Player() = default;
Player::Player(string&& name) : pname(move(name)) { /* Initialize name */ }

const string& Player::name() const { return pname; }
Planet& Player::world() { return planet; }


