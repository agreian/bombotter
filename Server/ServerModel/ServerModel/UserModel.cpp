#include "UserModel.h"

UserModel::UserModel()
{}

UserModel::UserModel(string log, string pwd, bool isBot)
{
	this->login = (isBot ? "BOT"+log : log);
	this->password = pwd;
}

UserModel* UserModel::Connect(string login, string password)
{
	UserModel* user = NULL;
	bool badLogin = false, badPwd = false;
	/* Rechercher dans la base de données */
	if (badLogin)
		throw ::BomberLoutreInterface::BadLoginException("BOLOSS");
	if (badPwd)
		throw ::BomberLoutreInterface::BadPasswordException("Retente ta chance");
	
	return user;
}

UserModel* UserModel::CreateUser(string login, string password, bool isBot)
{
	UserModel* user = NULL;
	bool userExist = false;
	/* Si l'utilisateur existe deja : */
	if (userExist)
		throw ::BomberLoutreInterface::UserException("L'utilisateur existe déjà : sois créatif et trouve un autre pseudo.... VICTIME!");

	/* Maj de la bdd : création du user avec des params par dé */
	/* TEMP */
	user = new UserModel(login,password,isBot);
	return user;
}

void UserModel::addWin(int nbKill, int nbDeath, int nbSuicide)
{
	this->nbGame++;
	this->nbWin++;
	this->nbKill += nbKill;
	this->nbDeath += nbDeath;
	this->nbSuicide += nbSuicide;
}

void UserModel::addLoose(int nbKill, int nbDeath, int nbSuicide)
{
	this->nbGame++;
	this->nbLoose++;
	this->nbKill += nbKill;
	this->nbDeath += nbDeath;
	this->nbSuicide += nbSuicide;
}

void UserModel::addDraw(int nbKill, int nbDeath, int nbSuicide)
{
	this->nbGame++;
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
	game->addUser(this);
}

bool UserModel::save()
{
	//save user information : nbWin, nbkill ....
	return true;
}