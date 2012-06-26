#include "GamesManagerI.hpp"

::Bomberloutre::GameList 
GamesManagerI::getGameList(const ::Ice::Current&)
{
	::Bomberloutre::GameList empty;
	return empty;
}

::Bomberloutre::UserList 
GamesManagerI::getUserList(const ::Ice::Current&)
{
	::Bomberloutre::UserList empty;
	return empty;
}

::Bomberloutre::GamePtr 
GamesManagerI::createGame(const ::std::string& name, const ::Bomberloutre::GameWaitRoomPrx&, const ::Ice::Current&)
{
	return NULL;
}

bool 
GamesManagerI::removeGame(const ::std::string&, const ::Ice::Current&)
{
	return false;
}

bool 
GamesManagerI::joinGame(const ::std::string&, const ::Bomberloutre::GameWaitRoomPrx&, const ::Bomberloutre::MapObserverPrx&, const ::Ice::Current&)
{
	return false;
}
