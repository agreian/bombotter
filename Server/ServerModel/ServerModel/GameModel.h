#ifndef GAME_MODEL_HEADER
#define GAME_MODEL_HEADER

// Game.h
#include "string.h"
#include <iostream>
#include "Bomberloutre_game.h"

using namespace System;
using namespace std;

class GameModel : public Bomberloutre::Game
{
	public:
		GameModel();
		~GameModel();
		void addBot();
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
		string name;
		int nbRound;
		int state;
};

#endif //GAME_MODEL_HEADER