#ifndef GAME_MODEL_HEADER
#define GAME_MODEL_HEADER

// Game.h
#include <iostream>
#include <string>
#include <vector>

#include <Ice/Ice.h>

#include "Bomberloutre.h"

class UserModel;

using namespace std;

class GameModel : public ::BomberLoutreInterface::GameInterface
{
	public:
		//GameModel();
		GameModel(UserModel* creator, ::Ice::ObjectAdapterPtr obj);
		~GameModel();

		/* à conserver ? */
		void addUser(UserModel* newUser);
		bool createGame(string name);
		void addRoom(BomberLoutreInterface::GameWaitRoomPrx room);
		void addMapObserver(BomberLoutreInterface::MapObserverPrx obs);
		void addPlayer(BomberLoutreInterface::UserData us);
		BomberLoutreInterface::Map getMap();
		BomberLoutreInterface::GameInterfacePrx getProxy();

		virtual ::std::string getName(const ::Ice::Current& = ::Ice::Current());
		virtual ::Ice::Int getState(const ::Ice::Current& = ::Ice::Current());
		virtual ::Ice::Int getRoundCount(const ::Ice::Current& = ::Ice::Current());
		virtual void setName(const ::std::string&, const ::Ice::Current& = ::Ice::Current());
		virtual void setState(::Ice::Int, const ::Ice::Current& = ::Ice::Current());
		virtual void setRoundCount(::Ice::Int, const ::Ice::Current& = ::Ice::Current());
		virtual void kickPlayer(const ::std::string&, const ::Ice::Current& = ::Ice::Current());
		virtual void invitePlayer(const ::std::string&, const ::Ice::Current& = ::Ice::Current());
		virtual void addBot(const ::Ice::Current& = ::Ice::Current());
		virtual void removeBot(const ::Ice::Current& = ::Ice::Current());
		virtual ::BomberLoutreInterface::MapNameList getMapList(const ::Ice::Current& = ::Ice::Current());
		virtual ::std::string createMap(const ::std::string&, const ::std::string&, const ::Ice::Current& = ::Ice::Current());
		virtual void startMap(const ::Ice::Current& = ::Ice::Current());
		virtual void endMap(const ::Ice::Current& = ::Ice::Current());
		virtual bool removeGame(const ::Ice::Current& = ::Ice::Current());

		/* GETTERS */
		string getName();
		int getState();
		int getRoundCount();
		int getPlayerCount();

		/* SETTERS */
		void setName(string newName);
		void setNbRound(int newNbRound);
		void setState(int newState);

	private:		
		BomberLoutreInterface::GameWaitRoomPrx currentRoom;
		BomberLoutreInterface::MapObserverPrx currentObserver;
		BomberLoutreInterface::GameInterfacePrx giProxy;
		vector<UserModel*> listUser;
		UserModel* gameCreator;
		string name;
		int nbRound;
		int state;
};

#endif //GAME_MODEL_HEADER