#include "stdafx.h"

#include "PlayerModel.h"

PlayerModel::PlayerModel()
{
	this->width = 0;
	this->height = 0;
	MapModel *map;
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
	bool alive;
}

PlayerModel::PlayerModel(MapModel* map, int width, int height, int flamePower,bool invincible,	bool invisibility,	int kicker,	int nbBomb,	int nbDeath, int nbKill, int posX, int posY, int speed, bool alive)
{
	this->width = width;
	this->height = height;
	this->map = map ;
	this->flamePower = flamePower;
	this->invincible = invincible;
	this->invisibility = invisibility;
	this->kicker = kicker;
	this->nbBomb = nbBomb;
	this->nbDeath = nbDeath;
	this->nbKill = nbKill;
	this->posX = posX;
	this->posY = posY;
	this->speed = speed;
	this->alive = alive;
}


PlayerModel::PlayerModel(int newWitdh, int newHeight)
{
	this->width = newWitdh;
	this->height = newHeight;
}

void PlayerModel::addBonus(Bonus* b)
{
	
}

void PlayerModel::die()
{
	
}

void PlayerModel::draw()
{
	
}
		
void PlayerModel::dropBomb()
{
	
}

void PlayerModel::kickBomb()
{
	
}

void PlayerModel::dropBonus()
{
	
}
		
void PlayerModel::win()
{
	
}

void PlayerModel::loose()
{
	
}

void PlayerModel::moveUp()
{
	
}

void PlayerModel::moveDown()
{
	
}

void PlayerModel::moveLeft()
{
	
}

void PlayerModel::moveRight()
{
	
}