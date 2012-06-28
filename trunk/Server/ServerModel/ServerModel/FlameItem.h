#ifndef FLAME_ITEM_HEADER
#define FLAME_ITEM_HEADER

// BombItem.h
#include <string>
#include <iostream>
#include "MapItem.h"

using namespace std;

class FlameItem : public MapItem
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		//FlameItem();
		FlameItem(MapModel* map, PlayerModel* player);
		~FlameItem();

		/* GETTERS */

		float getTimer();
		PlayerModel* getPlayer();
		int getId();

		/* SETTERS */

	private:
		void burn();
		float timer;
		PlayerModel* player;
};

#endif //FLAME_ITEM_HEADER