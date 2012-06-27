#include "stdafx.h"

#include "GameModel.h"

GameModel::GameModel()
{
}

GameModel::~GameModel()
{}

void GameModel::addBot()
{}

void GameModel::removeBot()
{}

bool GameModel::createGame(string name)
{
	return true;
}

void GameModel::createMap(string mod, string mapSkin)
{}

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
