#ifndef GAME_MODEL_HEADER
#define GAME_MODEL_HEADER

#include <iostream>
#include <string>
#include <vector>

#include <Ice/Ice.h>

#include "Bomberloutre.h"

class UserModel;
class ServerModel;
class MapModel;

class GameModel : 
	public ::BomberLoutreInterface::GameInterface
{
	public:
	GameModel(ServerModel* server, UserModel* creator);
	GameModel(ServerModel* server, UserModel* creator, ::Ice::ObjectAdapterPtr a);
		~GameModel();

	std::string createMapLocal(std::string id, std::string mode);
		MapModel* map;
	//void invitePlayer(string gametag);
	//void kickPlayer(string name);
	//void render();

	void mapStart();
	void mapEnd();
		
	void addBotLocal();
	void removeBotLocal();

		void addUser(UserModel* newUser);

	/* GETTERS */
	std::string getNameLocal()			{ return m_name; }
	int getStateLocal()					{ return m_state; }
	int getRoundCountLocal()			{ return m_roundCount; }
	int getPlayerCountLocal()			{ return m_listUsers.size(); }
	MapModel* getMap()					{ return m_map; }

	/* SETTERS */
	void setNameLocal(std::string newName)	{ this->m_name = newName; }
	void setNbRoundLocal(int newRoundCount)	{ this->m_roundCount = newRoundCount; }
	void setStateLocal(int newState)		{ this->m_state = newState; }

	::BomberLoutreInterface::GameInterfacePrx getProxy() { return m_proxy; }
	

		void createMap(std::string id, std::string mod);
		void addRoom(BomberLoutreInterface::GameWaitRoomPrx room);
		void addMapObserver(BomberLoutreInterface::MapObserverPrx obs);

	/* ---------- NETWORK INTERFACE ---------- */

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

	virtual ::std::string 
	getCreatorName(const ::Ice::Current& = ::Ice::Current());

	virtual void 
	userReady(const ::BomberLoutreInterface::UserData&, const ::Ice::Current& = ::Ice::Current());

	virtual void
	leaveGame(const ::BomberLoutreInterface::UserData&, const ::Ice::Current& = ::Ice::Current());

	virtual ::BomberLoutreInterface::MapInterfacePrx 
	getMapInterface(const ::Ice::Current& = ::Ice::Current());

private:
	::std::string m_name;
	int m_roundCount;
	int m_state;

	int m_userReadyCount;

	ServerModel* m_server;
	UserModel* m_creator;

	MapModel* m_map;
	::std::vector< UserModel* > m_listUsers;
	
	::BomberLoutreInterface::GameInterfacePrx m_proxy;
	::std::vector< ::BomberLoutreInterface::GameWaitRoomPrx > m_listRooms;
	::Ice::ObjectAdapterPtr m_adapter;
};

#endif //GAME_MODEL_HEADER