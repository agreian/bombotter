#ifndef KICKER_HEADER
#define KICKER_HEADER

// Kicker.h
#include "string.h"
#include <iostream>
#include "Bonus.h"

using namespace System;
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
		/* SETTERS */
};

#endif //KICKER_HEADER