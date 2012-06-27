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
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		BombItem();
		~BombItem();
		
		bool isDestructible();
		bool isWalkable();
		/* GETTERS */

		/* SETTERS */

	private:
		void explod();
		int power;
		int timer;
};

#endif //BOMB_ITEM_HEADER