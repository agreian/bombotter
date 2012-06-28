#ifndef MAP_ITEM_HEADER
#define MAP_ITEM_HEADER

// MapItem.h
#include "string.h"
#include <iostream>
#include "MapModel.h"

using namespace System;
using namespace std;


class MapItem
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		/*MapItem();*/
		MapItem(MapModel* map, bool destructible, bool walkable);
		~MapItem();

		bool isDestructible();
		bool isWalkable();
		virtual int getId() = 0;
		/* GETTERS */

		/* SETTERS */

	protected:
		MapModel* map;
		bool destructible;
		bool walkable;
};

#endif //MAP_ITEM_HEADER