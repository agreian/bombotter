#ifndef PLAYER_MODEL_HEADER
#define PLAYER_MODEL_HEADER

// PlayerModel.h
#include <iostream>
#include "Bonus.h"
#include "MapModel.h"
#include <vector>

using namespace System;
using namespace std;

class MapModel;

class PlayerModel
{
	private :
		int height;
		int width;
		
	protected :
		MapModel *map;
		int flamePower;
		bool invincible;
		bool invisibility;
		bool kicker;
		int nbBomb;
		int nbDeath;
		int nbKill;
		int posX;
		int posY;
		int speed;
		bool alive;
		vector<Bonus*> bonuses;
	
	public :
		enum playerDirectionId { dirLeft=0, dirRight=1, dirUp=2, dirDown=3 };
		//PlayerModel();
		PlayerModel(MapModel* map, int newWidth, int newHeight);
		PlayerModel(MapModel* map, int width, int height, int flamePower,bool invincible,	bool invisibility,	bool kicker,	int nbBomb,	int nbDeath, int nbKill, int posX, int posY, int speed, bool alive);		

		void addBonus(Bonus* b);
		void die(BombItem* bomb);
		void draw();
		bool hasBonus(Bonus* b);
		
		void dropBomb();
		void kickBomb();
		void dropBonus();
		
		void win();
		void loose();
		
		void moveUp();
		void moveDown();
		void moveLeft();
		void moveRight();
};

#endif