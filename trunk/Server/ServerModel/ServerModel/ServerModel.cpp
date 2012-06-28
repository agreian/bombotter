#include "ServerModel.h"

ServerModel::ServerModel() : m_adapter(NULL)
{
}

ServerModel::ServerModel(::Ice::ObjectAdapterPtr a) : m_adapter(a)
{}

ServerModel::~ServerModel()
{
	for(std::vector<UserModel*>::iterator i=m_currentUsers.begin();i!=m_currentUsers.end();++i)
	{
		delete (*i);
	}
}

ServerModel::addGame(string name)
{
}

ServerModel::getListGame()
{
}

ServerModel::getListUser()
{
}

ServerModel::removeGame(string name)
{
}

ServerModel::sendInvitationToPlayer(PlayerModel player, Game g)
{
}

BomberLoutreInterface::UserData 
ServerModel::connect(const std::string& login, const std::string& password, const Ice::Current&)
{
	BomberLoutreInterface::UserData us;
	std::cout << login << " " << password << std::endl;
	us.gameTag = "Bomber"+login;

	UserModel* newuser = new UserModel(login,password);
	newuser->setGameTag(us.gameTag);
	m_currentUsers.push_back(newuser);
	// TODO: Check user existence in list
	return us;
}

BomberLoutreInterface::UserData
ServerModel::createUser(const std::string& login, const std::string& password, const Ice::Current&)
{
	BomberLoutreInterface::UserData us;
	us.gameTag = "Bomber"+login;
	// TODO: Verify login existence
	UserModel* newuser = new UserModel(login,password);
	newuser->setGameTag(us.gameTag);
	m_currentUsers.push_back(newuser);
	return us;
}

bool 
ServerModel::deleteUser(const std::string& login, const Ice::Current&)
{
	// Delete user with login in Server UserList
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
	const ::BomberLoutreInterface::GameWaitRoomPrx& room, 
	const ::BomberLoutreInterface::MapObserverPrx& mapobs, const Ice::Current&)
{
	BomberLoutreInterface::GameInterfacePrx p;
	return p;
}

BomberLoutreInterface::Map 
ServerModel::joinGame(const std::string& name, 
	const ::BomberLoutreInterface::GameWaitRoomPrx& room, 
	const ::BomberLoutreInterface::MapObserverPrx& mapobs, const Ice::Current&)
{
	BomberLoutreInterface::Map m;
	return m;
}

BomberLoutreInterface::GameDataList 
ServerModel::getGameList(const Ice::Current&)
{
	BomberLoutreInterface::GameDataList list;
	return list;
}

BomberLoutreInterface::UserDataList 
ServerModel::getUserList(const Ice::Current&)
{
	BomberLoutreInterface::UserDataList list;
	return list;
}
