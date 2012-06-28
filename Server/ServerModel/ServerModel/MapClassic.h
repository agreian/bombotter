#ifndef MAP_CLASSIC_HEADER
#define MAP_CLASSIC_HEADER

// MapClassic.h
#include "string.h"
#include <iostream>
#include "MapModel.h"

using namespace System;
using namespace std;

class MapClassic : public MapModel
{

	public :	
		MapClassic();
		~MapClassic();
		void render();
};

#endif