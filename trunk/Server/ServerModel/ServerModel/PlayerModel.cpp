#include "PlayerModel.h"

/*PlayerModel::PlayerModel()
{
	
}*/

PlayerModel::PlayerModel(MapModel* map, int width, int height, int flamePower,bool invincible,	bool invisibility,	bool kicker,	int nbBomb,	int nbDeath, int nbKill, int posX, int posY, int speed, bool alive)
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


PlayerModel::PlayerModel(MapModel* map, int newWidth, int newHeight)
{
	this->width = newWidth;
	this->height = newHeight;
	this->map = map ;
	this->flamePower = 1;
	this->invincible = false;
	this->invisibility = false;
	this->kicker = false;
	this->nbBomb = 1;
	this->nbDeath = 0;
	this->nbKill = 0;
	this->posX = 0;
	this->posY = 0;
	this->speed = 1;
	this->alive = true;
}

void PlayerModel::addBonus(Bonus* b)
{
	if(!hasBonus(b)) bonuses.push_back(b);
}

bool PlayerModel::hasBonus(Bonus* b)
{
	vector<Bonus*>::iterator it;

	  for ( it=this->bonuses.begin() ; it < this->bonuses.end(); it++ )
	  {
		  if(*it=b) return true;
	  }
	  return false;		
}

void PlayerModel::die(BombItem* bomb)
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