#ifndef __GAMESMANAGERI_HPP__
#define __GAMESMANAGERI_HPP__

#include "Bomberloutre.hpp"

class GamesManagerI : public Bomberloutre::GamesManager
{
public:
	virtual ::Bomberloutre::GameList 
	getGameList(const ::Ice::Current& = ::Ice::Current());
	
	virtual ::Bomberloutre::UserList 
	getUserList(const ::Ice::Current& = ::Ice::Current());
	
	virtual ::Bomberloutre::GamePtr 
	createGame(const ::std::string&, const ::Bomberloutre::GameWaitRoomPrx&, const ::Ice::Current& = ::Ice::Current());
	
	virtual bool 
	removeGame(const ::std::string&, const ::Ice::Current& = ::Ice::Current());
	
	virtual bool 
	joinGame(const ::std::string&, const ::Bomberloutre::GameWaitRoomPrx&, const ::Bomberloutre::MapObserverPrx&, const ::Ice::Current& = ::Ice::Current());
};

#endif
