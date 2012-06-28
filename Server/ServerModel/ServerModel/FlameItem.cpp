#include "FlameItem.h"
#include <boost/thread/thread.hpp>
#include <boost/timer.hpp>

/*FlameItem::FlameItem()
{
	
}*/

FlameItem::FlameItem(MapModel* map, PlayerModel* player):MapItem(map,true,false)
{
	this->player = player;
	this->timer = 1.0;
	
	boost::thread threadBurn(boost::bind(&FlameItem::burn, this));
}

FlameItem::~FlameItem()
{
	/*Trololo */
}

void FlameItem::burn()
{
	boost::timer t;
	
	while(this->timer > 0)
	{
		timer -= t.elapsed();
	}

	this->map->handleBurn(this);
}

float FlameItem::getTimer()
{
	return this->timer;
}

PlayerModel* FlameItem::getPlayer()
{
	return this->player;
}

int FlameItem::getId()
{
	return MapModel::FlameItemCode;
}

