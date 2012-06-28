#include "BombItem.h"

/*BombItem::BombItem()
{
	
}*/

BombItem::BombItem(MapModel* map, PlayerModel* player, int power):MapItem(map,true,false)
{
	this->player = player;
	this->power = power;
	this->timer = 3.0;
}
BombItem::~BombItem()
{
	/*Trololo */
}

void BombItem::explod()
{
	this->map->handleExplode(this);
}

int BombItem::getPower()
{
	return this->power;
}

float BombItem::getTimer()
{
	return this->timer;
}

PlayerModel* BombItem::getPlayer()
{
	return this->player;
}

int BombItem::getId()
{
	return MapModel::BombItemCode;
}

/*
bool BombItem::isDestructible() 
{
	return true;
}

bool BombItem::isWalkable()
{
	return false;
}
*/
