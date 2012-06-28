#ifndef MAP_SURVIVAL_HEADER
#define MAP_SURVIVAL_HEADER

// MapSurvival.h
#include "string.h"
#include <iostream>
#include "MapModel.h"

using namespace System;
using namespace std;

class MapSurvival : public MapModel
{
	public:
		MapSurvival();
		~MapSurvival();		
		void render();
};

#endif