#ifndef USER_MODEL_HEADER
#define USER_MODEL_HEADER

// User.h
#include "string.h"
#include <iostream>
#include "Bomberloutre_user.h"
#include "GameModel.h"

using namespace System;
using namespace std;

class UserModel : public Bomberloutre::User
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		/* Throw Bomberloutre::BadLoginException, Bomberloutre::BadPwdException */
		static UserModel* Connect(string login, string password);
		/* Throw UserAlreadyExistException */
		static UserModel* CreateUser(string login, string password) ;
		void addWin(int nbKill, int nbDeath, int nbSuicide);
		void addLoose(int nbKill, int nbDeath, int nbSuicide);
		void addDraw(int nbKill, int nbDeath, int nbSuicide);
		bool deleteUser();
		
		void joinGame(GameModel* game);
		
		/* GETTERS */


		/* SETTERS */

	private:
		string login;
		/*
		/briefOLOZ default name displayed in game
		*/
		string gameTag;
		string password;

		/* Game information */
		int nbGame;
		int nbWin;
		int nbDraw;
		int nbKill;
		int nbDeath;
		int nbSuicide;
			
		/* Private user constructor (called by Connect or CreateUser */
		UserModel();
		UserModel(string log, string pwd);
		/* Save new information of the user : new Game Tag, updates of Win, Lose or Draw number */
		bool Commit();
};

#endif //USER_MODEL_HEADER