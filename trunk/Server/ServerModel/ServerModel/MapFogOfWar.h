#ifndef MAP_FOG_OF_WAR_HEADER
#define MAP_FOG_OF_WAR_HEADER

// MapFogOfWar.h
#include "string.h"
#include <iostream>
#include "MapModel.h"

using namespace System;
using namespace std;

class MapFogOfWar : public MapModel
{
	public:
		MapFogOfWar();
		~MapFogOfWar();		
		void render();
};

#endif