#ifndef ROCK_ITEM_HEADER
#define ROCK_ITEM_HEADER

// RockItem.h
#include <string>
#include <iostream>
#include "MapItem.h"

using namespace std;

class RockItem : public MapItem
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		//RockItem();
		RockItem(MapModel* map);
		~RockItem();

	/*	bool isDestructible();
		bool isWalkable();*/

		/* GETTERS */

		int getId();

		/* SETTERS */
};

#endif //ROCK_ITEM_HEADER