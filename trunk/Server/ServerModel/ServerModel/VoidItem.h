#ifndef VOID_ITEM_HEADER
#define VOID_ITEM_HEADER

// VoidItem.h
#include <string>
#include <iostream>
#include "MapItem.h"

using namespace std;

class VoidItem : public MapItem
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		VoidItem();
		VoidItem(MapModel* map);
		~VoidItem();

		bool isDestructible();
		bool isWalkable();

		/* GETTERS */
		int getId();
		float getDropChances();

		/* SETTERS */

	private:
		float dropChances;
};

#endif //VOID_ITEM_HEADER