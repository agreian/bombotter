#ifndef __BOMBERLOUTRE_GAME__
#define __BOMBERLOUTRE_GAME__

#include <Bomberloutre_map.ice>
#include <Bomberloutre_user.ice>

module Bomberloutre
{

interface GameWaitRoom
{
	void newUserInRoom(string username);
	void userLeftRoom(string username);
	void kickPlayer(string username);
	void invitePlayer(string username);
	
	void userReady(string username);
	void allUsersReady();
};

sequence<GameWaitRoom*> RoomList;

class Game
{
	string name;
	int state;
	int nbRound;

	RoomList rooms;
	Map currentMap;

	void addBot();
	void removeBot();

	bool createMap(string mode, string mapSkin);
	void startMap();
	void endMap();
};

sequence<Game> GameList;

interface GamesManager
{
	GameList getGameList();
	UserList getUserList();
	
	Game createGame(string name, GameWaitRoom* room);
	bool joinGame(string name, GameWaitRoom* room, MapObserver* mo);
	bool removeGame(string name);
};

};

#endif
