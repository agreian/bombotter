#ifndef BONUS_HEADER
#define BONUS_HEADER


// Bonus.h
#include "string.h"
#include <iostream>
#include "MapItem.h"

using namespace System;
using namespace std;

class Bonus : public MapItem
{
	// TODO�: ajoutez ici vos m�thodes pour cette classe.
	public:
	//	Bonus();
		Bonus(MapModel* map);
		~Bonus();
		/* GETTERS */
		virtual int getId() = 0;

		/* SETTERS */

		/* Chgment en protected. A conserver ? */
	protected:
		/* R�ellement besoin ? -> On ne met pas directement � jour les caract�ristiques du player ?  */
		/* TRES SUREMENT oui mais � v�rifier ! */
		/* Speed int ? Pas plutot un double (avoir une notion de multiplicateur *1.4 pa rexemple) */
		int bomb;
		bool kick;
		int power;
		int speed;
};

#endif //USER_MODEL_HEADER