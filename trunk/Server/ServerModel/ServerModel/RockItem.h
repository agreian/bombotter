#ifndef ROCK_ITEM_HEADER
#define ROCK_ITEM_HEADER

// RockItem.h
#include "string.h"
#include <iostream>
#include "MapItem.h"

using namespace System;
using namespace std;

class RockItem : public MapItem
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		RockItem();
		~RockItem();

		bool isDestructible();
		bool isWalkable();

		/* GETTERS */

		/* SETTERS */
};

#endif //ROCK_ITEM_HEADER