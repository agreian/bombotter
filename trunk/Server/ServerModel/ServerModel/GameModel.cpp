#include "GameModel.h"
#include "UserModel.h"

/*GameModel::GameModel()
{
	
}*/

GameModel::~GameModel()
{
	
}

GameModel::GameModel(UserModel* creator, ::Ice::ObjectAdapterPtr obj)
{
	this->gameCreator = creator;
}

void GameModel::addUser(UserModel* newUser) 
{
	if (listUser.size() >= 4)
		throw std::exception("La partie est compl�te (4 participants)");
	listUser.push_back(newUser);
}

bool GameModel::createGame(string name)
{
	return true;
}

void GameModel::createMap(string mod, string mapSkin)
{

}

void GameModel::addRoom(BomberLoutreInterface::MapObserverPrx room)
{
	this->currentRoom = room;
}

void GameModel::addMapObserver(BomberLoutreInterface::MapObserverPrx obs)
{
	this->currentObserver = obs;
}

//V�rifier avec Daniel si on peut passer tous les param�tres d'un coup au lieu de faire de la recopie champs par champs.
void GameModel::addPlayer(BomberLoutreInterface::UserData us) // But : r�cup�rer depuis la liste des Users connect�s au serveur
{
	
}

BomberLoutreInterface::GameInterfacePrx GameModel::getProxy()
{
	return this->giProxy;
}

/* GETTERS */
string GameModel::getName()
{
	return this->name;
}

int GameModel::getRoundCount()
{
	return->nbRound;
}

int GameModel::getState()
{
	return this->state;
}

//V�rifier le champs "map"
BomberLoutreInterface::Map GameModel::getMap()
{
	return this->BomberLoutreInterface::Map m;
}

int GameModel::getPlayerCount()
{
	return listUser.size();
}

/* SETTERS */
void GameModel::setName(string newName)
{
	this->name = newName;
}

void GameModel::setNbRound(int newNbRound)
{
	this->nbRound = newNbRound;
}

void GameModel::setState(int newState)
{
	this->state = newState;
}
