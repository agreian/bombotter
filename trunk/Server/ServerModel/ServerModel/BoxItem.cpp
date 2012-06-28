#include "BoxItem.h"
#include <ctime>
#include "stdlib.h"
#define NBBONUS 4
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

void BoxItem::appears()
{
	//TODO : wtf?
}

void BoxItem::disappears()
{
	float randomDrop;
	int randomItem, itemCode;
	srand(time(NULL));
	randomDrop = rand() / float(RAND_MAX);

	if( randomDrop <= this->dropChances)
	{
		randomItem = (rand() / float(RAND_MAX)) * NBBONUS +1;
		switch(randomItem)
		{
			case 1:
				itemCode = MapModel::FlameUpCode;
				break;
			case 2:
				itemCode = MapModel::BombUpCode;
				break;
			case 3:
				itemCode = MapModel::SpeedUpCode;
				break;
			case 4:
				itemCode = MapModel::KickerCode;
				break;
		}
		this->map->dropBonus(itemCode);
	}
}

int BoxItem::getId()
{
	return MapModel::BoxItemCode;
}
