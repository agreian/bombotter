#ifndef PLAYER_MODEL_HEADER
#define PLAYER_MODEL_HEADER

// PlayerModel.h
#include <iostream>
#include "Bonus.h"
#include "MapModel.h"
#include "UserModel.h"
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
		UserModel* user;
		int flamePower;
		bool invincible;
		bool invisibility;
		bool kicker;
		int nbBomb;
		int nbBombUsed;
		int nbDeath;
		int nbSuicide;
		int nbKill;
		int posX;
		int posY;
		int speed;
		bool alive;
		vector<Bonus*> bonuses;
		int dir;
	
	public :
		enum playerDirectionId { dirLeft=0, dirRight=1, dirUp=2, dirDown=3 };
		//PlayerModel();
		PlayerModel(MapModel* map, UserModel* user, int newWidth, int newHeight);
		PlayerModel(MapModel* map, UserModel* user, int width, int height, int flamePower,bool invincible,	bool invisibility,	bool kicker,	int nbBomb,	int nbDeath, int nbKill, int posX, int posY, int speed, bool alive, int nbSuicide, int nbBombUsed);		

		void addBonus(Bonus* b);
		void die(PlayerModel* player);
		bool hasBonus(Bonus* b);
		
		void dropBomb();
		void dropBonus();
		
		void addKill(int nb);
		
		void moveUp();
		void moveDown();
		void moveLeft();
		void moveRight();

		/* GETTERS */
		int getPosX();
		int getPosY();
		int getNbBomb();
		void incNbBomb();
		void decNbBomb();
		int getNbBombUsed();
		void incNbBombUsed();
		void decNbBombUsed();
		bool getKicker();
		bool isAlive();
		int getDir();
		int getPower();
};

#endif