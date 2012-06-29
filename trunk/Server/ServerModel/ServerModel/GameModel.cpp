#include "GameModel.h"
#include "ServerModel.h"
#include "UserModel.h"
#include "MapModel.h"

GameModel::GameModel(ServerModel* server, UserModel* creator) : m_server(server), m_creator(creator), m_userReadyCount(0)
{ /* Something to do ? */ }
	
GameModel::GameModel(ServerModel* server, UserModel* creator, ::Ice::ObjectAdapterPtr a) : m_server(server), m_creator(creator), m_adapter(a), m_userReadyCount(0)
{
	m_proxy = BomberLoutreInterface::GameInterfacePrx::checkedCast(
		m_adapter->add(this,m_adapter->getCommunicator()->stringToIdentity(m_name))
		);
}

void GameModel::addBotLocal()
{
	UserModel* bot = UserModel::CreateUser("Bot","Game",true);
	addUser(bot);
}

void GameModel::removeBotLocal()
{
	for(std::vector<UserModel*>::iterator i=m_listUsers.begin();i!=m_listUsers.end();++i)
	{
		if((*i)->getLogin().find("BOT") == 0)
		{
			m_listUsers.erase(i);
		}
	}
}

void GameModel::addUser(UserModel* user)
{
	m_listUsers.push_back(user);
}

void GameModel::createMap(string id, string mod)
{
	string* s = this->m_server->getMap(id); 
	this->map = new MapModel(this, s[0]);
}

std::string GameModel::createMapLocal(std::string id, std::string mode)
{
	return "";
}

void GameModel::mapStart()
{
	// TODO : launch the game...
}

void GameModel::mapEnd()
{
	// TODO : clean the map and notify the client
}

void GameModel::addRoom(BomberLoutreInterface::GameWaitRoomPrx room)
{
	m_listRooms.push_back(room);
}

void GameModel::addMapObserver(BomberLoutreInterface::MapObserverPrx obs)
{
	if(m_map != NULL)
	{
		m_map->addObserver(obs);
}
}

/* NETWORK INTERFACE */

::std::string GameModel::getName(const ::Ice::Current&)
{ return this->m_name; }
::Ice::Int GameModel::getState(const ::Ice::Current&)
{ return this->m_state; }
::Ice::Int GameModel::getRoundCount(const ::Ice::Current&)
{ return this->m_roundCount; }

void GameModel::setName(const ::std::string& name, const ::Ice::Current&)
{ this->m_name = name; }
void GameModel::setState(::Ice::Int state, const ::Ice::Current&)
{ this->m_state = state; }
void GameModel::setRoundCount(::Ice::Int rc, const ::Ice::Current&)
{ this->m_roundCount = rc; }

void GameModel::kickPlayer(const ::std::string&, const ::Ice::Current&)
{/* Not implemented */}
void GameModel::invitePlayer(const ::std::string&, const ::Ice::Current&)
{/* Not implemented */}

void GameModel::addBot(const ::Ice::Current&)
{ this->addBotLocal(); }
void GameModel::removeBot(const ::Ice::Current&)
{ this->removeBotLocal(); }

::BomberLoutreInterface::MapNameList GameModel::getMapList(const ::Ice::Current&)
{
	// Ask list to server
	::BomberLoutreInterface::MapNameList list;
	// TODO
	return list;
}

::std::string GameModel::createMap(const ::std::string& id, const ::std::string& mode, const ::Ice::Current&)
{
	return this->createMapLocal(id,mode);
}

void GameModel::startMap(const ::Ice::Current&)
{
	this->mapStart();
	for(::std::vector< ::BomberLoutreInterface::GameWaitRoomPrx >::iterator i=m_listRooms.begin();i!=m_listRooms.end();++i)
	{
		(*i)->gameStart();
	}
}

void GameModel::endMap(const ::Ice::Current&)
{
	this->mapEnd();
	for(::std::vector< ::BomberLoutreInterface::GameWaitRoomPrx >::iterator i=m_listRooms.begin();i!=m_listRooms.end();++i)
	{
		(*i)->gameEnd();
	}
}

bool GameModel::removeGame(const ::Ice::Current&)
{
	return false;
}

::std::string GameModel::getCreatorName(const ::Ice::Current&)
{
	return this->m_creator->getLogin();
}

void GameModel::userReady(const ::BomberLoutreInterface::UserData& us, const ::Ice::Current&)
{
	UserModel* user = NULL;
	for(std::vector<UserModel*>::iterator i=m_listUsers.begin();i!=m_listUsers.end();++i)
	{
		if((*i)->getGameTag() == us.gameTag)
		{
			user = (*i);
			break;
		}
	}

	if(user != NULL)
	{
		for(::std::vector< ::BomberLoutreInterface::GameWaitRoomPrx >::iterator i=m_listRooms.begin();i!=m_listRooms.end();++i)
		{
			(*i)->userReady(user->getGameTag());
			m_userReadyCount++;
			if(m_userReadyCount == 4)
			{
				(*i)->allUsersReady();
			}
		}
	}
}

void GameModel::leaveGame(const ::BomberLoutreInterface::UserData& us, const ::Ice::Current&)
{
	UserModel* user = NULL;
	for(std::vector<UserModel*>::iterator i=m_listUsers.begin();i!=m_listUsers.end();++i)
	{
		if((*i)->getGameTag() == us.gameTag)
		{
			m_listUsers.erase(i);
			break;
		}
	}

	m_userReadyCount--;
	if(m_userReadyCount < 0)
		m_userReadyCount = 0;
}

::BomberLoutreInterface::MapInterfacePrx GameModel::getMapInterface(const ::Ice::Current&)
{
	return this->getMap()->getInterfacePrx();
}