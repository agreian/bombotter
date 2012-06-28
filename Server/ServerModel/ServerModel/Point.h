#ifndef POINT_HEADER
#define POINT_HEADER

// Point.h
#include "string.h"
#include <iostream>

using namespace System;
using namespace std;


class Point {
	public:
		int x;
		int y;

		Point();
		Point(int newX, int newY);
		~Point();

};


#endif