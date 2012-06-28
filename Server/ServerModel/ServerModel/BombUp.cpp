#include "BombUp.h"

/*BombUp::BombUp()
{
	this->bomb = 1;
}*/
BombUp::BombUp(MapModel* map):Bonus(map)
{
	this->bomb = 1;
}

BombUp::~BombUp()
{
	/*Trololo */
}

int BombUp::getId()
{
	return MapModel::BombUpCode;
}

int BombUp::getBomb()
{
	return this->bomb;
}