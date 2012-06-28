#ifndef BOMB_ITEM_HEADER
#define BOMB_ITEM_HEADER

// BombItem.h
#include <string>
#include <iostream>
#include "MapItem.h"

using namespace std;

class BombItem : public MapItem
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		//BombItem();
		BombItem(MapModel* map, PlayerModel* player, int power);
		~BombItem();

		/* GETTERS */

		int getPower();
		float getTimer();
		PlayerModel* getPlayer();
		int getId();

		/* SETTERS */

	private:
		void explod();
		int power;
		float timer;
		PlayerModel* player;
};

#endif //BOMB_ITEM_HEADER