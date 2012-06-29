#ifndef Database_HEADER
#define Database_HEADER

#include <string>
#include <vector>
#include "sqlite3.h"
#include "UserModel.h"


using namespace std;

class Database
{
	public:
		Database(char* filename);
		~Database();
	
		bool open(char* filename);
		vector<vector<string> > query(char* query);
		void displayResults(vector<vector<string>> result);
		void close();

		void insertUser(UserModel* user);
		void updateUser(UserModel* user);
		void deleteUser(UserModel* user);
		vector<vector<string>> selectUser(UserModel* user);
		vector<vector<string>> selectAllUsers();
	private:
		sqlite3 *database;
};

#endif //Database_HEADER

