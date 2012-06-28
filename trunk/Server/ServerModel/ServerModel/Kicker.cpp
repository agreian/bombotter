#include "Kicker.h"
/*
Kicker::Kicker()
{
	
	this->kick = true;
}*/

Kicker::Kicker(MapModel* map):Bonus(map)
{
	this->kick = true;
}

Kicker::~Kicker()
{
	/*Trololo */
}

int Kicker::getId()
{
	return MapModel::KickerCode;
}

