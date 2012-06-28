#include "BoxItem.h"
#include <ctime>
#include "stdlib.h"
using namespace std;

float BoxItem::dropChances = 0.5;
/*
BoxItem::BoxItem()
{
	
}*/

BoxItem::BoxItem(MapModel* map):MapItem(map,true,false)
{

}

BoxItem::~BoxItem()
{
	/*Trololo */
}
/*
bool BoxItem::isDestructible() 
{
	return true;
}

bool BoxItem::isWalkable()
{
	return false;
}*/

float BoxItem::getDropChances()
{
	return dropChances;
}

void BoxItem::disappears()
{
	this->map->dropBonus(this);
}

int BoxItem::getId()
{
	return MapModel::BoxItemCode;
}
