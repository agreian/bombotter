#ifndef KICKER_HEADER
#define KICKER_HEADER

// Kicker.h
#include <string>
#include <iostream>
#include "Bonus.h"

using namespace std;

class Kicker : public Bonus
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		//Kicker();
		Kicker(MapModel* map);
		~Kicker();
		/* GETTERS */
		int getId();
		bool getKick();
		/* SETTERS */
};

#endif //KICKER_HEADER