#ifndef BOX_ITEM_HEADER
#define BOX_ITEM_HEADER

// BoxItem.h
#include <string>
#include <iostream>
#include "MapItem.h"

using namespace std;

class BoxItem : public MapItem
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		BoxItem(MapModel* map);
		~BoxItem();

		int getId();
		float getDropChances();
		void disappears();
		/* GETTERS */

		/* SETTERS */

	private:
		static float dropChances;
};

#endif //MAP_ITEM_HEADER