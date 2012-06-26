#ifndef __BOMBERLOUTRE_MAP__
#define __BOMBERLOUTRE_MAP__

#include <Bomberloutre_player.ice>
#include <Bomberloutre_object.ice>

module Bomberloutre
{

struct Point
{
	int x;
	int y;
};

class Map
{
	string id;
	int width;
	int height;
	
	MapItems items;
	PlayerList players;
	
	void loadMap(string id);
	
	bool checkMove(Player p, Point pt); // Starred or not ?
};

class MapClassic extends Map
{};

class MapSurvival extends Map
{};

class MapFogOfWar extends Map
{};

interface MapObserver
{
	void refreshAll(Map m);
	
	void refreshPlayers(PlayerList p);
	
	void bombHasBeenPlanted(Bomb b);
	void bombExploded(Bomb b);
	
	void bonusDropped(Bonus b);
};

};

#endif
