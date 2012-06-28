#include "UserModel.h"

UserModel::UserModel()
{
	/*Trololo */
}

UserModel::UserModel(string log, string pwd)
{
	this->login = log;
	this->password = pwd;
}

UserModel* UserModel::Connect(string login, string password)
{
	UserModel* user = NULL;
	bool badLogin = false, badPwd = false;
	/* Rechercher dans la base de données */
	if (badLogin)
		throw Bomberloutre::BadLoginException("BOLOSS");
	if (badPwd)
		throw Bomberloutre::BadPasswordException("Retente ta chance");
	
	return user;
}

UserModel* UserModel::CreateUser(string login, string password)
{
	UserModel* user = NULL;
	bool userExist = false;
	/* Si l'utilisateur existe deja : */
	if (userExist)
		throw Bomberloutre::UserException("L'utilisateur existe déjà : sois créatif et trouve un autre pseudo.... VICTIME!");

	/* Maj de la bdd : création du user avec des params par dé */
	return user;
}

void UserModel::addWin(int nbKill, int nbDeath, int nbSuicide)
{
	this->nbWin++;
	this->nbKill += nbKill;
	this->nbDeath += nbDeath;
	this->nbSuicide += nbSuicide;
}

void UserModel::addLoose(int nbKill, int nbDeath, int nbSuicide)
{
	this->nbLoose++;
	this->nbKill += nbKill;
	this->nbDeath += nbDeath;
	this->nbSuicide += nbSuicide;
}

void UserModel::addDraw(int nbKill, int nbDeath, int nbSuicide)
{
	this->nbDraw++;
	this->nbKill += nbKill;
	this->nbDeath += nbDeath;
	this->nbSuicide += nbSuicide;
}

bool UserModel::deleteUser()
{
	return false;
}

void UserModel::joinGame(GameModel* game)
{
	this->gameTag = game->name;
}

void UserModel::save()
{
	//save user information : nbWin, nbkill ....
}