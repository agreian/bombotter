#ifndef VOID_ITEM_HEADER
#define VOID_ITEM_HEADER

// VoidItem.h
#include "string.h"
#include <iostream>
#include "MapItem.h"

using namespace System;
using namespace std;

class VoidItem : public MapItem
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		VoidItem();
		~VoidItem();

		bool isDestructible();
		bool isWalkable();

		/* GETTERS */

		/* SETTERS */

	private:
		float dropChances;
};

#endif //VOID_ITEM_HEADER