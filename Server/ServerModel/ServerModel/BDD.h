#ifndef BDD_HEADER
#define BDD_HEADER

#include <string>
#include <vector>
#include "sqlite3.h"

using namespace std;

class BDD
{
public:
	BDD(char* filename);
	~BDD();
	
	bool open(char* filename);
	vector<vector<string> > query(char* query);
	void close();
	
private:
	sqlite3 *database;
};

#endif ::BDD_HEADER

