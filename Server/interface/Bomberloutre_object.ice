#ifndef __BOMBERLOUTRE_OBJECT__
#define __BOMBERLOUTRE_OBJECT__

module Bomberloutre
{

class MapItem
{
	bool destructible;
	bool walkable;
};

sequence<MapItem> MapItems;

class Bomb extends MapItem
{
	int power;
	int timer;
};

sequence<Bomb> Bombs;

class Empty extends MapItem
{
	
};

class Box extends MapItem
{

};

class Rock extends MapItem
{
	
};

class Bonus extends MapItem
{
	int bomb;
	bool kick;
	int power;
	int speed;
};

sequence<Bonus> Bonuses;

/* ---------- BONUS ---------- */
class Kicker extends Bonus
{};

class FlameUp extends Bonus
{};

class SpeedUp extends Bonus
{};

class BombUp extends Bonus
{};

};

#endif
