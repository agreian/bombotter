
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

vector<vector<string> > Database::query(char* query)
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
	string insertQuery =	"INSERT INTO USER(usrid,login,pwd,gametag,nbwin,nbloose,nbdraw,nbkill,nbdeath,nbsuicide) " 
							"VALUES (1,'" + user->login + "','" + user->password + "','" + user->gameTag + "'," + user->nbWin + "," + user->nbLoose + "," + user->nbDraw + "," + user->nbKill + "," + user->nbDeath + "," + user->nbSuicide + ")";
	this->query(insertQuery);
}

void Database::updateUser(UserModel* user)
{
	string updateQuery =	"UPDATE USER SET usrid = ,"							
							"pwd=,'" + user->password + "'"
							"gametag=,'" + user->gameTag + "'"
							"nbwin=," + user->nbWin + ""
							"nbloose=," + user->nbLoose + ""
							"nbdraw=," + user->nbDraw + ""
							"nbkill=," + user->nbKill + ""
							"nbdeath=," + user->nbDeath + ""
							"nbsuicide=	" + user->nbSuicide + ""	
							"WHERE login='" + user->login + "'"	;					
	this->query(updateQuery);
}

void Database::deleteUser(UserModel* user)
{
	string deleteQuery =	"DELETE FROM USER"	
							"WHERE login='" + user->login + "'";					
	this->query(deleteQuery);
}

vector<vector<string>> Database::selectUser(UserModel* user)
{
	string selectQuery =	"SELECT * FROM USER"	
							"WHERE login='" + user->login + "'"	;						
	return this->query(selectQuery);
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
