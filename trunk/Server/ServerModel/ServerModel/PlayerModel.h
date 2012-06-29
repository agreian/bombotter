#ifndef PLAYER_MODEL_HEADER
#define PLAYER_MODEL_HEADER

// PlayerModel.h
#include <iostream>
#include "Bonus.h"
#include "MapModel.h"
#include "UserModel.h"
#include <vector>

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
		UserModel *user;
		enum playerDirectionId { dirLeft=0, dirRight=1, dirUp=2, dirDown=3 };
		PlayerModel();
		PlayerModel(MapModel *map, UserModel *user, int posX, int posY);		

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

		/* GETTERS && SETTER*/
		void setPosX(int x);
		void setPosY(int y);
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
		string getGameTag();
		int getNbDeath();
		int getNbSuicide();
		int getNbKill();
};

#endif