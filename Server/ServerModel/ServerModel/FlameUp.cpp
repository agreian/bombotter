#include "FlameUp.h"

/*FlameUp::FlameUp()
{
	this->power = 1;
}*/

FlameUp::FlameUp(MapModel* map):Bonus(map)
{
	this->power = 1;
}

FlameUp::~FlameUp()
{
	/*Trololo */
}

int FlameUp::getId()
{
	return MapModel::FlameUpCode;
}
