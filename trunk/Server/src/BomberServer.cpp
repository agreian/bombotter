#include "BomberServer.hpp"

BomberServer::BomberServer(::Ice::ObjectAdapterPtr a) : m_adapter(a)
{}

Bomberloutre::UserData 
BomberServer::connect(const std::string& login, const std::string& password, const Ice::Current&)
{
	Bomberloutre::UserData us;
	std::cout << login << " " << password << std::endl;
	us.gameTag = "meuh";
	return us;
}

Bomberloutre::UserData
BomberServer::createUser(const std::string& login, const std::string& password, const Ice::Current&)
{
	Bomberloutre::UserData us;
	return us;
}

bool 
BomberServer::deleteUser(const std::string& login, const Ice::Current&)
{
	return false;
}

Bomberloutre::GameInterfacePrx
BomberServer::addGame(const std::string& name, 
	const ::Bomberloutre::GameWaitRoomPrx& room, 
	const ::Bomberloutre::MapObserverPrx& mapobs, const Ice::Current&)
{
	Bomberloutre::GameInterfacePrx p;
	return p;
}

Bomberloutre::Map 
BomberServer::joinGame(const std::string& name, 
	const ::Bomberloutre::GameWaitRoomPrx& room, 
	const ::Bomberloutre::MapObserverPrx& mapobs, const Ice::Current&)
{
	Bomberloutre::Map m;
	return m;
}

Bomberloutre::GameDataList 
BomberServer::getGameList(const Ice::Current&)
{
	Bomberloutre::GameDataList list;
	return list;
}

Bomberloutre::UserDataList 
BomberServer::getUserList(const Ice::Current&)
{
	Bomberloutre::UserDataList list;
	return list;
}
	
