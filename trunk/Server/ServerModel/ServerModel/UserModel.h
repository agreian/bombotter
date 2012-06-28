#ifndef USER_MODEL_HEADER
#define USER_MODEL_HEADER

// User.h
#include <iostream>
#include "GameModel.h"
#include "Bomberloutre.h"

using namespace std;

class UserModel
{
	// TODO : ajoutez ici vos méthodes pour cette classe.
	public:
		/* Throw ::BomberLoutreInterface::BadLoginException, ::BomberLoutreInterface::BadPwdException */
		static UserModel* Connect(string login, string password);
		/* Throw UserAlreadyExistException */
		static UserModel* CreateUser(string login, string password) ;
		void addWin(int nbKill, int nbDeath, int nbSuicide);
		void addLoose(int nbKill, int nbDeath, int nbSuicide);
		void addDraw(int nbKill, int nbDeath, int nbSuicide);
		bool deleteUser();
		
		void joinGame(GameModel* game);
		
		/* GETTERS */
		std::string getLogin() { return login; }
		std::string getGameTag() { return gameTag; }
		int getGameCount() { return nbGame; }
		int getWinCount() { return nbWin; }
		int getDrawCount() { return nbDraw; }
		int getKillCount() { return nbKill; }
		int getDeathCount() { return nbDeath; }
		int getSuicideCount() { return nbSuicide; }

		/* SETTERS */
		void setGameTag(const std::string & gt) { gameTag = gt; }

	private:
		/*
		/briefOLOZ default name displayed in game
		*/
		string gameTag;
        string login;
        string password;
        
		/* Game information */
		int nbGame;
		int nbWin;
		int nbLoose;
		int nbDraw;
		int nbKill;
		int nbDeath;
		int nbSuicide;
			
		/* Private user constructor (called by Connect or CreateUser */
		UserModel();
		UserModel(string log, string pwd);
		/* Save new information of the user : new Game Tag, updates of Win, Lose or Draw number */
		bool save();
};

#endif //USER_MODEL_HEADER