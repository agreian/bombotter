#include "UserModel.h"
#include "Database.h"


UserModel::UserModel()
{}

UserModel::UserModel(string log, string pwd, bool isBot)
{
	this->login = (isBot ? "BOT"+log : log);
	this->password = pwd;
	this->db = new Database("Bomberloutre.db");
	this->nbGame = 0;
	this->nbWin = 0;
	this->nbLoose = 0;
	this->nbDraw = 0;
	this->nbKill = 0;
	this->nbDeath = 0;
	this->nbSuicide = 0;
}

void UserModel::setAttributes(vector<vector<string>> data)
{
	string result[9];
	int i = 0;
	for(std::vector<vector<string>>::iterator ligne = data.begin(); ligne != data.end(); ++ligne) {
		i = 0;
		for(std::vector<string>::iterator colonne = ligne->begin(); colonne != ligne->end(); ++colonne) {
			result[i] = *colonne;
			i++;
		}
	}

	this->login = result[0];
	this->password = result[1];
	this->gameTag = result[2];
	this->nbWin = std::stoi(result[3]);
	this->nbLoose = std::stoi(result[4]);
	this->nbDraw = std::stoi(result[5]);
	this->nbKill = std::stoi(result[6]);
	this->nbDeath = std::stoi(result[7]);
	this->nbSuicide = std::stoi(result[8]);
	this->nbGame = this->nbWin + this->nbLoose + this->nbDraw;
}

UserModel* UserModel::Connect(string login, string password)
{
	UserModel* user = new UserModel(login, password, false);
	vector<vector<string>> results;

	results = user->db->selectUser(user);

	if(results.size()==0)
	{
		delete (user);
		throw ::BomberLoutreInterface::BadLoginException("Mauvais login");
	}
	else
	{
		user->setAttributes(results);
		if(std::strcmp(user->password.c_str(),password.c_str()) != 0)
		{
			delete (user);
			throw ::BomberLoutreInterface::BadPasswordException("Mauvais password");
		}
	}

	
	return user;
}

UserModel* UserModel::CreateUser(string login, string password, bool isBot)
{
	UserModel* user = new UserModel(login, password, isBot);
	vector<vector<string>> result = user->db->selectUser(user);

	/* Si l'utilisateur existe deja : */
	if (result.size())
	{
		delete(user);
		throw ::BomberLoutreInterface::UserException("L'utilisateur existe déjà : sois créatif et trouve un autre pseudo.... VICTIME!");
	}
	else
	{
		user->db->insertUser(user);
	}

	return user;
}

void UserModel::addWin(int nbKill, int nbDeath, int nbSuicide)
{
	this->nbGame++;
	this->nbWin++;
	this->nbKill += nbKill;
	this->nbDeath += nbDeath;
	this->nbSuicide += nbSuicide;
	this->save();
}

void UserModel::addLoose(int nbKill, int nbDeath, int nbSuicide)
{
	this->nbGame++;
	this->nbLoose++;
	this->nbKill += nbKill;
	this->nbDeath += nbDeath;
	this->nbSuicide += nbSuicide;
	this->save();
}

void UserModel::addDraw(int nbKill, int nbDeath, int nbSuicide)
{
	this->nbGame++;
	this->nbDraw++;
	this->nbKill += nbKill;
	this->nbDeath += nbDeath;
	this->nbSuicide += nbSuicide;
	this->save();
}

bool UserModel::deleteUser()
{
	this->db->deleteUser(this);
	return false;
}

void UserModel::joinGame(GameModel* game)
{
	game->addUser(this);
}

bool UserModel::save()
{
	this->db->updateUser(this);
	return true;
}