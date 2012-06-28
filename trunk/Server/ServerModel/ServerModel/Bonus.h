#ifndef BONUS_HEADER
#define BONUS_HEADER

// Bonus.h
#include "string.h"
#include <iostream>

using namespace System;
using namespace std;

class Bonus
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		Bonus();
		~Bonus();
		/* GETTERS */

		/* SETTERS */

		/* Chgment en protected. A conserver ? */
	protected:
		/* Réellement besoin ? -> On ne met pas directement à jour les caractéristiques du player ?  */
		/* TRES SUREMENT oui mais à vérifier ! */
		/* Speed int ? Pas plutot un double (avoir une notion de multiplicateur *1.4 pa rexemple) */
		int bomb;
		bool kick;
		int power;
		int speed;
};

#endif //USER_MODEL_HEADER