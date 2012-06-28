#ifndef MAP_MODEL_HEADER
#define MAP_MODEL_HEADER

// MapModel.h
#include "string.h"
#include <iostream>
#include "MapItem.h"
#include "BombItem.h"
#include "PlayerModel.h"
#include "Point.h"

using namespace System;
using namespace std;

class MapModel
{
	private :
		string id;
		
	protected :
		int height;
		int width;
		
	public :
		/*MapModel();
		~MapModel();*/
		bool checkMove(PlayerModel p, Point arrive);
		void dropBonus(MapItem* bonus);
		// Factory à Map Item
		void createMapItem(string typeMapItem);
		void handleExplode(BombItem* b);
		void loapMap(string id);
		virtual void render() = 0;
};

#endif