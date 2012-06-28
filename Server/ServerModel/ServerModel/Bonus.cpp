#include "Bonus.h"
/*
Bonus::Bonus()
{
	
}*/

Bonus::Bonus(MapModel* map):MapItem(map, true, true)
{
	this->bomb = 0;
	this->kick = false;
	this->power = 0;
	this->speed = 0;
}

Bonus::~Bonus()
{
	/*Trololo */
}

int Bonus::getBomb()
{
	return this->bomb;
}

bool Bonus::getKick()
{
	return this->kick;
}

int Bonus::getPower()
{
	return this->power;
}

int Bonus::getSpeed()
{
	return this->speed;
}
