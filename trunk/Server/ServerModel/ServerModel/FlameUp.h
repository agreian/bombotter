#ifndef FLAME_UP_HEADER
#define FLAME_UP_HEADER

// FlameUp.h
#include <string>
#include <iostream>
#include "Bonus.h"

using namespace std;

class FlameUp : public Bonus
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		//FlameUp();
		FlameUp(MapModel* map);
		~FlameUp();
		/* GETTERS */
		int getId();

		/* SETTERS */
};

#endif //Flame_UP_HEADER