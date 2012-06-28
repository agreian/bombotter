#include "stdafx.h"

#include "VoidItem.h"

/*VoidItem::VoidItem()
{
	
}*/

VoidItem::VoidItem(MapModel* map):MapItem(map, false, true)
{
	
}


VoidItem::~VoidItem()
{
	/*Trololo */
}

int VoidItem::getId()
{
	return MapModel::MapItemCode::VoidItemCode;
}
/*
bool VoidItem::isDestructible() 
{
	return false;
}

bool VoidItem::isWalkable()
{
	return true;
}
*/