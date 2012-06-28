#include "ServerModel.h"

ServerModel::ServerModel() : m_adapter(NULL)
{
}

ServerModel::ServerModel(::Ice::ObjectAdapterPtr a) : m_adapter(a)
{}

ServerModel::~ServerModel()
{
}

void ServerModel::addGame(std::string name)
{
}

void ServerModel::getListGame()
{
}

void ServerModel::getListUser()
{
}

void ServerModel::removeGame(std::string name)
{
}

/*void ServerModel::sendInvitationToPlayer(PlayerModel player, Game g)
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
	us.gameTag = "meuh";
	return us;
}

BomberLoutreInterface::UserData
ServerModel::createUser(const std::string& login, const std::string& password, const Ice::Current&)
{
	BomberLoutreInterface::UserData us;
	return us;
}

bool 
ServerModel::deleteUser(const std::string& login, const Ice::Current&)
{
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
