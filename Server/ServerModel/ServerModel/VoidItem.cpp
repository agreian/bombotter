#include "VoidItem.h"

/*VoidItem::VoidItem()
{
	
}*/

VoidItem::VoidItem(MapModel* map):MapItem(map, false, true)
{
	this->dropChances = 0.10;
}


VoidItem::~VoidItem()
{
	/*Trololo */
}

int VoidItem::getId()
{
	return MapModel::VoidItemCode;
}

float VoidItem::getDropChances()
{
	return this->dropChances;
}