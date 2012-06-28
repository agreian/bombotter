#include "RockItem.h"
/*
RockItem::RockItem()
{
	
}*/

RockItem::RockItem(MapModel* map):MapItem(map,false,false)
{

}

RockItem::~RockItem()
{
	/*Trololo */
}

int RockItem::getId()
{
	return MapModel::RockItemCode;
}
