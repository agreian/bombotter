#ifndef SPEED_UP_HEADER
#define SPEED_UP_HEADER

// SpeedUp.h
#include <string>
#include <iostream>
#include "Bonus.h"

using namespace std;

class SpeedUp : public Bonus
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		//SpeedUp();
		SpeedUp(MapModel* map);
		~SpeedUp();
		/* GETTERS */
		int getId();

		/* SETTERS */
};

#endif //SPEED_UP_HEADER