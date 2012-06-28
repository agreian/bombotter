#ifndef __BOMBERLOUTRE_OBJECT__
#define __BOMBERLOUTRE_OBJECT__

module BomberLoutreInterface
{
/* This file contains pure data structures */

struct MapItem
{
	int i;
	int j;
	
	bool destructible;
	bool walkable;
};
sequence<MapItem> MapItems;

struct Bomb
{
	int i;
	int j;
	
	int power;
	int timer;
};
sequence<Bomb> Bombs;

struct Bonus
{
	int i;
	int j;
	
	int bomb;
	bool kick;
	int power;
	int speed;
};
sequence<Bonus> Bonuses;

};

#endif
