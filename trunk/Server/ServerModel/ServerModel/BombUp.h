#ifndef BOMB_UP_HEADER
#define BOMB_UP_HEADER

// BombUp.h
#include <string>
#include <iostream>
#include "Bonus.h"

using namespace std;

class BombUp : public Bonus
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		//BombUp();
		BombUp(MapModel* map);
		~BombUp();

		/* GETTERS */
		int getId();
		int getBomb();
		/* SETTERS */
};

#endif //BOMB_UP_HEADER