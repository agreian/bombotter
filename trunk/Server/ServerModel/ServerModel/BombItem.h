#ifndef BOMB_ITEM_HEADER
#define BOMB_ITEM_HEADER

// BombItem.h
#include "string.h"
#include <iostream>
#include "MapItem.h"

using namespace System;
using namespace std;

class BombItem : public MapItem
{
	// TODO�: ajoutez ici vos m�thodes pour cette classe.
	public:
		//BombItem();
		BombItem(MapModel* map, PlayerModel* player, int power, int timer);
		~BombItem();

		/* GETTERS */

		int getPower();
		int getTimer();
		PlayerModel* getPlayer();
		int getId();

		/* SETTERS */

	private:
		void explod();
		int power;
		int timer;
		PlayerModel* player;
};

#endif //BOMB_ITEM_HEADER