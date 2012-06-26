#ifndef __BOMBERLOUTRE_PLAYER__
#define __BOMBERLOUTRE_PLAYER__

#include <Bomberloutre_object.ice>

module Bomberloutre
{

class Player
{
	/* MEMBERS */
	
	int posX;
	int posY;
	
	int width;
	int height;

	int nbKill;
	int nbDeath;
	int nbSuicide;

	int speed;
	int nbBomb;
	int flamePower;
	
	bool kicker;
	bool invincible;
	bool invisible;
	
	int maxSpeed;
	int maxBomb;
	int maxPower;
	
	/* FUNCTIONS */
	
	void kickBomb();
	void addBonus(Bonus b);
	
	void die();
	void dropBonuses();
	
	void win();
	void loose();
	void draw();
};

sequence<Player> PlayerList;

class Bot extends Player
{};

};

#endif
