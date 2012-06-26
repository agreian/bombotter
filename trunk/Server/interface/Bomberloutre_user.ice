#ifndef __BOMBERLOUTRE_USER__
#define __BOMBERLOUTRE_USER__

module Bomberloutre
{

interface GamesManager;
class User;

exception UserException { string reason; };
exception BadLoginException 				extends UserException {};
exception BadPasswordException 			extends UserException {};
exception UserAlreadyExistsException 	extends UserException {};

["amd"] interface UserConnection
{
	User connect(string login, string password)
		throws BadLoginException, BadPasswordException; // Password not clear...
		
	User createUser(string login, string password)
		throws UserAlreadyExistsException, BadPasswordException;
};

class User implements UserConnection
{
	string login;
	string password;
	
	int nbGame;
	int nbWin;
	int nbDraw;
	
	int nbKill;
	int nbDeath;
	int nbSuicide;
	
	string gameTag;
	GamesManager* mgr;
	
	bool deleteUser(); // Delete self
	
	void addWin(int nbKill, int nbDeath, int nbSuicide);
	void addDraw(int nbKill, int nbDeath, int nbSuicide);
	void addLoose(int nbKill, int nbDeath, int nbSuicide);
	
	bool commit();
};

sequence<User> UserList;

};

#endif
