#ifndef MAP_CLASSIC_HEADER
#define MAP_CLASSIC_HEADER

// MapClassic.h
#include <string>
#include <iostream>
#include "MapModel.h"

using namespace std;

class MapClassic : public MapModel
{

	public :	
		MapClassic();
		~MapClassic();
		void render();
};

#endif