#ifndef POINT_HEADER
#define POINT_HEADER

// Point.h
#include "string.h"
#include <iostream>

using namespace System;
using namespace std;


class Point {

	private :
		int x;
		int y;

	public:
		Point();
		Point(int newX, int newY);
		~Point();
};


#endif