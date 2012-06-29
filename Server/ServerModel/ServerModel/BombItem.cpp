#include "BombItem.h"
#include <boost/thread/thread.hpp>
#include <boost/timer.hpp>

/*BombItem::BombItem()
{
	
}*/

BombItem::BombItem(MapModel* map, PlayerModel* player, int power):MapItem(map,true,false)
{
	this->player = player;
	this->power = power;
	this->timer = 2.0;
	
	boost::thread threadExplosion(boost::bind(&BombItem::explod, this));
}
BombItem::~BombItem()
{
	/*Trololo */
}

void BombItem::explod()
{
	boost::timer t;
	
	while(this->timer > 0)
	{
		timer -= (float)t.elapsed();
	}

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
