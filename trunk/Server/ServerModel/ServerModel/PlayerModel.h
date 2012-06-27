#ifndef PLAYER_MODEL_HEADER
#define PLAYER_MODEL_HEADER

// PlayerModel.h
#include "string.h"
#include <iostream>
#include "Bomberloutre_player.h"
#include "Bonus.h"

using namespace System;
using namespace std;

class PlayerModel : public Bomberloutre::Player
{
	private :
		int height;
		int widht;
		
	protected :
		int flamePower;
		bool invincibie;
		bool invisibility;
		int kicker;
		int nbBomb;
		int nbDeath;
		int nbKill;
		int posX;
		int posY;
		int speed;
	
	public :
		PlayerModel();
		PlayerModel(int newWidth, int newHeight);

		void addBonus(Bonus b);
		void die();
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