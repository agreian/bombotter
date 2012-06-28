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
		//BoxItem();
		BoxItem(MapModel* map);
		~BoxItem();

		int getId();
		void appears();
		void disappears();
		/*bool isDestructible();
		bool isWalkable();*/
		/* GETTERS */

		/* SETTERS */

	private:
		static float dropChances;
};

#endif //MAP_ITEM_HEADER