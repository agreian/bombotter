#ifndef MAP_ITEM_HEADER
#define MAP_ITEM_HEADER

// MapItem.h
#include "string.h"
#include <iostream>

using namespace System;
using namespace std;

class MapItem
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		MapItem();
		~MapItem();

		virtual bool isDestructible() = 0;
		virtual bool isWalkable() = 0;
		/* GETTERS */

		/* SETTERS */

	protected:
		bool destructible;
		bool walkable;
};

#endif //MAP_ITEM_HEADER