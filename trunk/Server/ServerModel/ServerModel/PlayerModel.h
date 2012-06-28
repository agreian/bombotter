#ifndef PLAYER_MODEL_HEADER
#define PLAYER_MODEL_HEADER

// PlayerModel.h
#include <iostream>
#include "Bonus.h"
#include "MapModel.h"

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
		int kicker;
		int nbBomb;
		int nbDeath;
		int nbKill;
		int posX;
		int posY;
		int speed;
		bool alive;
	
	public :
		PlayerModel();
		PlayerModel(MapModel* map):
		PlayerModel(int newWidth, int newHeight);

		void addBonus(Bonus* b);
		void die(BombItem* bomb);
		void draw();
		
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