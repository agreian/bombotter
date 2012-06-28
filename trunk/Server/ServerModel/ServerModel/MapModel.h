#ifndef MAP_MODEL_HEADER
#define MAP_MODEL_HEADER

// MapModel.h
#include "string.h"
#include <iostream>
#include "Point.h"

using namespace System;
using namespace std;

class MapItem;
class PlayerModel;
class BombItem;

class MapModel
{
	private :
		string id;
		
	protected :
		int height;
		int width;
		
	public :
		int logicalMap[13][11]; //Contein MapItem code
		MapItem* map[13][11];
		MapModel();		
		~MapModel();
		bool checkMove(PlayerModel p, Point arrive);
		void dropBonus(MapItem* bonus);
		// Factory à Map Item
		//bool checkMove(PlayerModel p, Point arrive);
//		void dropBonus(MapItem bonus);
		void createMapItem(string typeMapItem);
		void handleExplode(BombItem* b);
		void loapMap(string id);
		virtual void render() = 0;

		enum MapItemCode
		{
			VoidItemCode = 0,
			BoxItemCode = 1,
			RockItemCode = 2,
			BombItemCode = 3,
			ExplodingItemCode = 4,

			FlameUpCode = 10,
			GoldenFlameCode = 11,
			BombUpCode = 12,
			SpeedUpCode = 13,
			KickerCode = 14,
			InvisibleItemCode = 15,
			InvincibleItemCode = 16,
			ShieldItemCode = 17,
		};
		
};

#endif