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

void GameModel::addBot()
{
	/* Créer un UserModel avec isBot à true ! (classe à modifier)
	Puis ajouter à la liste des players le bot en question
	 */
	UserModel* user = UserModel::CreateUser("bot1", "pwdBot1", true);
	this->addUser(user);
}

void GameModel::addUser(UserModel* newUser) 
{
	if (listUser.size() >= 4)
		throw std::exception("La partie est complète (4 participants)");
	listUser.push_back(newUser);
}

void GameModel::removeBot()
{
	/* Pas important pour l'instant : on ajoute les bots au moment du lancement de la partie -> donc le nombre de places libres et ensuite on ne les retire plus */
}

bool GameModel::createGame(string name)
{
	return true;
}

void GameModel::createMap(string mod, string mapSkin)
{

}

void GameModel::startMap()
{}

void GameModel::endMap()
{}

/*void GameModel::kickPlayer(string name)
{}

void GameModel::render()
{}

void GameModel::invitePlayer(string gamerTag)
{}*/

/* GETTERS */
string GameModel::getName()
{
	return this->name;
}

int GameModel::getNbRound()
{
	return this->nbRound;
}

int GameModel::getState()
{
	return this->state;
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
