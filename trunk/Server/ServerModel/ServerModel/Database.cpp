#include <sstream>
#include <iostream>
#include "Database.h"
#include "UserModel.h"
using namespace std;

Database::Database(char* filename)
{
	database = NULL;
	open(filename);
}

Database::~Database()
{
	close();
}

bool Database::open(char* filename)
{
	if(sqlite3_open(filename, &database) == SQLITE_OK)
		return true;
		
	return false;   
}

vector<vector<string> > Database::query(const char* query)
{
	sqlite3_stmt *statement;
	vector<vector<string> > results;

	if(sqlite3_prepare_v2(database, query, -1, &statement, 0) == SQLITE_OK)
	{
		int cols = sqlite3_column_count(statement);
		int result = 0;
		while(true)
		{
			result = sqlite3_step(statement);
			
			if(result == SQLITE_ROW)
			{
				vector<string> values;
				for(int col = 0; col < cols; col++)
				{
					if(sqlite3_column_text(statement, col)!=NULL) values.push_back((char*)sqlite3_column_text(statement, col));
					else values.push_back("");
				}
				results.push_back(values);
			}
			else
			{
				break;   
			}
		}
	   
		sqlite3_finalize(statement);
	}
	
	string error = sqlite3_errmsg(database);
	//if(error != "not an error") cout << query << " " << error << endl;
	
	return results;  
}

void Database::insertUser(UserModel* user)
{
	string insertQuery =	"INSERT INTO USER(login,pwd,gametag,nbwin,nbloose,nbdraw,nbkill,nbdeath,nbsuicide) " 
							"VALUES ('" + user->login + "','" + user->password + "','" + user->gameTag + "',0,0,0,0,0,0)";
	this->query(insertQuery.c_str());
}

void Database::updateUser(UserModel* user)
{
	std::ostringstream oss;    
    oss << user->nbWin;
	string nbWin = oss.str();
	oss << user->nbLoose;
	string nbLoose = oss.str();
	oss << user->nbDraw;
	string nbDraw = oss.str();
	oss << user->nbKill;
	string nbKill = oss.str();
	oss << user->nbDeath;
	string nbDeath = oss.str();
	oss << user->nbSuicide;
	string nbSuicide = oss.str();

	string updateQuery =	"UPDATE USER SET pwd=,'" + user->password + "'"
							"gametag=,'" + user->gameTag + "'"
							"nbwin=," + nbWin + ""
							"nbloose=," + nbLoose + ""
							"nbdraw=," + nbDraw + ""
							"nbkill=," + nbKill + ""
							"nbdeath=," + nbDeath + ""
							"nbsuicide=	" + nbSuicide + ""	
							"WHERE login='" + user->login + "'"	;					
	this->query(updateQuery.c_str());
}

void Database::deleteUser(UserModel* user)
{
	string deleteQuery =	"DELETE * FROM USER"	
							"WHERE login='" + user->login + "'";					
	this->query(deleteQuery.c_str());
}

vector<vector<string>> Database::selectUser(UserModel* user)
{
	string selectQuery =	"SELECT * FROM USER"	
							"WHERE login='" + user->login + "'"	;						
	return this->query(selectQuery.c_str());
}

vector<vector<string>> Database::selectAllUsers()
{								
	return this->query("SELECT * FROM USER");
}

void Database::displayResults(vector<vector<string>> result)
{
	for(std::vector<vector<string>>::iterator ligne = result.begin(); ligne != result.end(); ++ligne) {
		for(std::vector<string>::iterator colonne = ligne->begin(); colonne != ligne->end(); ++colonne) {
			cout<<*colonne<< " | ";
		}
		std::cout<<endl;
	}
}

void Database::close()
{
	sqlite3_close(database);   
}
