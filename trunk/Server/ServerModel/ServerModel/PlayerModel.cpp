#include "PlayerModel.h"

/*PlayerModel::PlayerModel()
{
	
}*/

PlayerModel::PlayerModel(MapModel* map, UserModel* user, int width, int height, int flamePower,bool invincible,	bool invisibility,	bool kicker,	int nbBomb,	int nbDeath, int nbKill, int posX, int posY, int speed, bool alive,int nbSuicide, int nbBombUsed)
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
	this->nbSuicide = nbSuicide;
	this->nbBombUsed = nbBombUsed;
	this->user = user;
}


PlayerModel::PlayerModel(MapModel* map, UserModel* user,  int newWidth, int newHeight)
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
	this->nbSuicide = 0;
	this->nbBombUsed = 0;
	this->user = user;
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
		if(*it == b) return true;
	}
	return false;		
}

void PlayerModel::die(PlayerModel* player)
{
	this->alive = false;
	this->nbDeath++;
	if( player==this) this->nbSuicide++;
}

		
void PlayerModel::dropBomb()
{
	this->map->deposeBombe(this);
}

void PlayerModel::addKill(int nb)
{
	this->nbKill += nb;
}

void PlayerModel::dropBonus()
{
	
}
		


void PlayerModel::moveUp()
{
	this->dir = PlayerModel::dirUp;
	this->posY -= this->speed*1;
}

void PlayerModel::moveDown()
{
	this->dir = PlayerModel::dirDown;
	this->posY += this->speed*1;
}

void PlayerModel::moveLeft()
{
	this->dir = PlayerModel::dirLeft;
	this->posX -= this->speed*1;
}

void PlayerModel::moveRight()
{
	this->dir = PlayerModel::dirRight;
	this->posX += this->speed*1;
}

int PlayerModel::getPosX()
{
	return this->posX;
}

int PlayerModel::getPosY()
{
	return this->posY;
}

int PlayerModel::getNbBomb()
{
	return this->nbBomb;
}

void PlayerModel::incNbBomb()
{
	this->nbBomb++;
}

void PlayerModel::decNbBomb()
{
	this->nbBomb--;
}

int PlayerModel::getNbBombUsed()
{
	return this->nbBombUsed;
}

void PlayerModel::incNbBombUsed()
{
	this->nbBombUsed++;
}

void PlayerModel::decNbBombUsed()
{
	this->nbBombUsed--;
}

bool PlayerModel::getKicker()
{
	return this->kicker;
}

bool PlayerModel::isAlive()
{
	return this->alive;
}

int PlayerModel::getDir()
{
	return this->dir;
}

int PlayerModel::getPower()
{
	return this->power;
}