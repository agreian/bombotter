#include "ServerModel.h"

ServerModel::ServerModel() : m_adapter(NULL)
{}

ServerModel::ServerModel(::Ice::ObjectAdapterPtr a) : m_adapter(a)
{}

ServerModel::~ServerModel()
{}

void ServerModel::removeGame(std::string name)
{
	for(std::vector<GameModel*>::iterator i=m_currentGames.begin();i!=m_currentGames.end();++i)
	{
		if((*i)->getName() == name)
		{
			delete (*i);
			m_currentGames.erase(i);
			return;
		}
	}
}

/*void ServerModel::sendInvitationToPlayer(PlayerModel* player, GameModel* g)
{
}*/

BomberLoutreInterface::UserData 
ServerModel::connect(const std::string& login, const std::string& password, const Ice::Current&)
{
	BomberLoutreInterface::UserData us;
	
	if(login == "tamere")
	{
		throw ::BomberLoutreInterface::BadLoginException();
	}
	
	std::cout << login << " " << password << std::endl;
	us.gameTag = "Bomber"+login;
	for(std::vector<UserModel*>::iterator i=m_currentUsers.begin();i!=m_currentUsers.end();++i)
	{
		if((*i)->getLogin() == login)
		{
			throw ::BomberLoutreInterface::BadLoginException();
		}
	}
	UserModel* um = UserModel::Connect(login,password);
	if(um == NULL)
	{
		throw ::BomberLoutreInterface::BadLoginException();
	}
	m_currentUsers.push_back(um);
	return us;
}

BomberLoutreInterface::UserData
ServerModel::createUser(const std::string& login, const std::string& password, const Ice::Current&)
{
	BomberLoutreInterface::UserData us;
	UserModel* newuser = UserModel::CreateUser(login,password);
	m_currentUsers.push_back(newuser);
	us.gameTag = "Bomber"+login;
	return us;
}

bool 
ServerModel::deleteUser(const std::string& login, const Ice::Current&)
{
	for(std::vector<UserModel*>::iterator i=m_currentUsers.begin();i!=m_currentUsers.end();++i)
	{
		if((*i)->getLogin() == login)
		{
			delete (*i);
			m_currentUsers.erase(i);
			return true;
		}
	}
	return false;
}

BomberLoutreInterface::GameInterfacePrx
ServerModel::addGame(const std::string& name, 
	const BomberLoutreInterface::UserData & us,
	const ::BomberLoutreInterface::GameWaitRoomPrx& room, 
	const ::BomberLoutreInterface::MapObserverPrx& mapobs, const Ice::Current&)
{
	GameModel* newGame = new GameModel(m_adapter,us); // Add constructor
	m_currentGames.push_back(newGame);
	newGame->addRoom(room);
	newGame->addMapObserver(mapobs);
	BomberLoutreInterface::GameInterfacePrx p = newGame->getProxy();
	return p;
}

BomberLoutreInterface::Map 
ServerModel::joinGame(const std::string& name, 
	const BomberLoutreInterface::UserData & us,
	const ::BomberLoutreInterface::GameWaitRoomPrx& room, 
	const ::BomberLoutreInterface::MapObserverPrx& mapobs, const Ice::Current&)
{
	GameModel* curGame = NULL;
	for(std::vector<GameModel*>::iterator i=m_currentGames.begin();i!=m_currentGames.end();++i)
	{
		if((*i)->getName() == name)
		{
			curGame = (*i);
		}
	}

	if(curGame != NULL)
	{
		curGame->addPlayer(us);
		curGame->addRoom(room);
		curGame->addMapObserver(mapobs);
		BomberLoutreInterface::Map m = curGame->getMap();
		return m;
	}
	throw std::exception();
}

BomberLoutreInterface::GameDataList 
ServerModel::getGameList(const Ice::Current&)
{
	BomberLoutreInterface::GameDataList list;
	for(std::vector<GameModel*>::iterator i=m_currentGames.begin();i!=m_currentGames.end();++i)
	{
		::BomberLoutreInterface::GameData gd;
		gd.name			= (*i)->getName();
		gd.roundCount	= (*i)->getRoundCount();
		gd.state		= (*i)->getState();
		gd.playerCount	= (*i)->getPlayerCount();
		gd.gameui		= (*i)->getUserProxy();
		list.push_back(gd);
	}
	return list;
}

BomberLoutreInterface::UserDataList 
ServerModel::getUserList(const Ice::Current&)
{
	BomberLoutreInterface::UserDataList list;
	for(std::vector<UserModel*>::iterator i=m_currentUsers.begin();i!=m_currentUsers.end();++i)
	{
		::BomberLoutreInterface::UserData ud;
		ud.gameTag		= (*i)->getGameTag();
		ud.gameCount	= (*i)->getGameCount();
		ud.winCount		= (*i)->getWinCount();
		ud.drawCount	= (*i)->getDrawCount();
		ud.killCount	= (*i)->getKillCount();
		ud.deathCount	= (*i)->getDeathCount();
		ud.suicideCount = (*i)->getSuicideCount();
		list.push_back(ud);
	}
	return list;
}
