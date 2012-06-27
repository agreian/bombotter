#ifndef BOX_ITEM_HEADER
#define BOX_ITEM_HEADER

// BoxItem.h
#include "string.h"
#include <iostream>
#include "MapItem.h"

using namespace System;
using namespace std;

class BoxItem : public MapItem
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		BoxItem();
		~BoxItem();

		void appears();
		void disappears();
		bool isDestructible();
		bool isWalkable();
		/* GETTERS */

		/* SETTERS */

	private:
		float dropChances;
};

#endif //MAP_ITEM_HEADER