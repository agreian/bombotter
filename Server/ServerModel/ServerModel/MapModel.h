#ifndef MAP_MODEL_HEADER
#define MAP_MODEL_HEADER

// MapModel.h
#include "string.h"
#include <iostream>
#include "Point.h"
#include <vector>

using namespace System;
using namespace std;

class MapItem;
class PlayerModel;
class BombItem;

#define MAPHEIGHT 11
#define MAPWIDTH 13

const int height = 60;
const int width = 60;

class MapModel
{
	private :
		string id;
		bool testCase(int x, int y, PlayerModel* p);
		
	protected :
		int height;
		int width;

	public :
		int logicalMap[MAPWIDTH][MAPHEIGHT]; //Contein MapItem code
		MapItem* map[MAPWIDTH][MAPHEIGHT];
		vector<PlayerModel*> listPlayer;
	
		MapModel();	
		MapModel(int _height, int _width);		
		~MapModel();
		bool checkMove(PlayerModel* p, Point arrive);
		void dropBonus(MapItem* bonus);
		void addPlayer(PlayerModel* newPlayer);
		void dropBonus(int bonusItemCode, Point p);
		void handleExplode(BombItem* b);
		void dropBomb(PlayerModel *p);
		// Factory à Map Item
		void createMapItem(int typeMapItem, Point p, PlayerModel *player);
		void loapMap(string id);
		virtual void render() = 0;
		


	enum MapItemCode
	{
		VoidItemCode = 0,
		BoxItemCode = 1,
		RockItemCode = 2,
		BombItemCode = 3,
		ExplosionItemCode = 4,

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