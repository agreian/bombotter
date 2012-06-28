#ifndef __SERVER_HPP__
#define __SERVER_HPP__

#include <Ice/Ice.h>
#include <IceUtil/IceUtil.h>

#include "Bomberloutre.hpp"

/* TAKE ALL THE INHERITANCES !! \o */
class BomberServer : 
	public ::Bomberloutre::ServerInterface
{
public:
	BomberServer(::Ice::ObjectAdapterPtr);
	
	virtual Bomberloutre::UserData 
	connect(const std::string&, const std::string&, const Ice::Current& = ::Ice::Current());
	
	virtual Bomberloutre::UserData
	createUser(const std::string&, const std::string&, const Ice::Current& = ::Ice::Current());
	
	virtual bool 
	deleteUser(const std::string&, const Ice::Current& = ::Ice::Current());
	
	virtual Bomberloutre::GameInterfacePrx
	addGame(const std::string&, const ::Bomberloutre::GameWaitRoomPrx&, const ::Bomberloutre::MapObserverPrx&, const Ice::Current& = ::Ice::Current());
	
	virtual Bomberloutre::Map 
	joinGame(const std::string&, const ::Bomberloutre::GameWaitRoomPrx&, const ::Bomberloutre::MapObserverPrx&, const Ice::Current& = ::Ice::Current());
	
	virtual Bomberloutre::GameDataList 
	getGameList(const Ice::Current& = ::Ice::Current());
	
	virtual Bomberloutre::UserDataList 
	getUserList(const Ice::Current& = ::Ice::Current());
	
private:
	// Replace int by Game when ready
	std::vector<int> m_currentGames;
	::Ice::ObjectAdapterPtr m_adapter;
};

#endif
