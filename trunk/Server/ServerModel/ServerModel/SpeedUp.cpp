#include "SpeedUp.h"
/*
SpeedUp::SpeedUp()
{
	this->speed = 1;
}*/

SpeedUp::SpeedUp(MapModel* map):Bonus(map)
{
	this->speed = 1;
}

SpeedUp::~SpeedUp()
{
	/*Trololo */
}

int SpeedUp::getId()
{
	return MapModel::MapItemCode::SpeedUpCode;
}
