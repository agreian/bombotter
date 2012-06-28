#ifndef GAME_MODEL_HEADER
#define GAME_MODEL_HEADER

// Game.h
#include <iostream>
#include <string>
#include <vector>

#include <Ice/Ice.h>

class UserModel;

using namespace std;

class GameModel
{
	public:
		//GameModel();
		GameModel(UserModel* creator, ::Ice::ObjectAdapterPtr obj);
		~GameModel();
		void addBot();
		void addUser(UserModel* newUser);
		void removeBot();
		bool createGame(string name);
		void createMap(string mod, string mapSkin);
		void startMap();
		void endMap();
		/*
 		Dans Gamewait room ????? VAZ Y !!
		void invitePlayer(string gamerTag);
		void kickPlayer(string name);
		void render();*/

		/* GETTERS */
		string getName();
		int getNbRound();
		int getState();

		/* SETTERS */
		void setName(string newName);
		void setNbRound(int newNbRound);
		void setState(int newState);

	private:
		vector<UserModel*> listUser;
		UserModel* gameCreator;
		string name;
		int nbRound;
		int state;
};

#endif //GAME_MODEL_HEADER