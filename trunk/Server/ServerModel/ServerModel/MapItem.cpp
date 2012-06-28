#include "MapItem.h"

/*MapItem::MapItem()
{

}*/

MapItem::MapItem(MapModel* map, bool destructible, bool walkable)
{
	this->map = map;
	this->destructible = destructible;
	this->walkable = walkable;
}

MapItem::~MapItem()
{
	/*Trololo */
}

bool MapItem::isDestructible()
{
	return this->destructible;
}

bool MapItem::isWalkable()
{
	return this->walkable;
}
