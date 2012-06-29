#ifndef MAP_MODEL_HEADER
#define MAP_MODEL_HEADER

// MapModel.h
#include <string>
#include <iostream>
#include <vector>

#include "Bomberloutre.h"
#include "GameModel.h"

using namespace std;

class MapItem;
class PlayerModel;
class BombItem;
class BoxItem;
class VoidItem;
class Bonus;
class FlameItem;

#define MAPHEIGHT 11
#define MAPWIDTH 13

const int height = 60;
const int width = 60;

class MapModel :
	public ::BomberLoutreInterface::MapInterface
{
	private :
		string id;
		bool testCase(int x, int y, PlayerModel* p);
		void dropBonus(::BomberLoutreInterface::Point P);
		
	protected :
		int height;
		int width;

	public :
		int logicalMap[MAPWIDTH][MAPHEIGHT]; //Contein MapItem code
		MapItem* map[MAPWIDTH][MAPHEIGHT];
		vector<PlayerModel*> listPlayer;
		vector< ::BomberLoutreInterface::MapObserverPrx> observers;
		GameModel* game;
	
		MapModel();	
		MapModel(GameModel* _game, string logicalMap);
		~MapModel();
		void checkMove(PlayerModel* p, ::BomberLoutreInterface::Point arrive);
		void dropBonus(BoxItem* bonus);
		void dropBonus(VoidItem* voidItem);
		void addPlayer(PlayerModel* newPlayer);
		void dropBonus(int bonusItemCode, ::BomberLoutreInterface::Point p);
		void handleExplode(BombItem* b);
		void handleBurn(FlameItem* f);
		void dropBomb(PlayerModel *p);
		// Factory à Map Item
		void createMapItem(int typeMapItem, ::BomberLoutreInterface::Point p, PlayerModel *player);
		void loadMap(string logicalMap);
		//virtual void render() = 0;
		
		virtual ::std::string getId(const ::Ice::Current& = ::Ice::Current());
		virtual ::Ice::Int getWidth(const ::Ice::Current& = ::Ice::Current());
		virtual ::Ice::Int getHeight(const ::Ice::Current& = ::Ice::Current());
		virtual void moveUp(const ::BomberLoutreInterface::Player&, const ::Ice::Current& = ::Ice::Current());
		virtual void moveDown(const ::BomberLoutreInterface::Player&, const ::Ice::Current& = ::Ice::Current()); 
		virtual void moveLeft(const ::BomberLoutreInterface::Player&, const ::Ice::Current& = ::Ice::Current());
		virtual void moveRight(const ::BomberLoutreInterface::Player&, const ::Ice::Current& = ::Ice::Current());
		virtual void dropBomb(const ::BomberLoutreInterface::Player&, const ::BomberLoutreInterface::Bomb&, const ::Ice::Current& = ::Ice::Current());
		void bombKicked(BombItem* b, int x, int y, ::BomberLoutreInterface::Point p);
		void bombExploded(BombItem* b, int x, int y);
		void bombHasBeenPlanted(BombItem* b, int x, int y);
		void bonusDisappeared(Bonus* b, int x, int y);
		void refreshMapItems();
		void refreshPlayers();
		void bonusesDropped(Bonus* b, int x, int y);
		void playerDied(PlayerModel* p);

		void mapEnd();
		bool testWin();

		enum MapItemCode
		{
			VoidItemCode = 0,
			BoxItemCode = 1,
			RockItemCode = 2,
			BombItemCode = 3,
			FlameItemCode = 4,

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