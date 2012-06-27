#ifndef MAP_MODEL_HEADER
#define MAP_MODEL_HEADER

// MapModel.h
#include "string.h"
#include <iostream>
#include "MapModel.h"

using namespace System;
using namespace std;

class MapModel
{
	MapModel();
	~MapModel();
	
	private :
		string id;
		
	protected :
		int height;
		int width;
		
	public :
		bool checkMove(PlayerModel p, Point arrive);
		void dropBonus(MapItem bonus);
		void createMapItem(string typeMapItem);
		void handleExplode(Bomb b);
		void loapMap(string id);
		void render();
};

#endif