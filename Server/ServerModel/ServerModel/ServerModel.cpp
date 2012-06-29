#include "ServerModel.h"
#include <boost/filesystem.hpp>
#include <fstream>

using namespace std;
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
	UserModel* newuser = UserModel::CreateUser(login,password, false);
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
	UserModel* user = NULL;
	for(std::vector<UserModel*>::iterator i=m_currentUsers.begin();i!=m_currentUsers.end();++i)
	{
		if((*i)->getGameTag() == us.gameTag)
		{
			user = (*i);
			break;
		}
	}
	GameModel* newGame = new GameModel(this,user,m_adapter); // Add constructor
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

	UserModel* user = NULL;
	for(std::vector<UserModel*>::iterator i=m_currentUsers.begin();i!=m_currentUsers.end();++i)
	{
		if((*i)->getGameTag() == us.gameTag)
		{
			user = (*i);
			break;
		}
	}

	if(curGame != NULL)
	{
		curGame->addUser(user);
		curGame->addRoom(room);
		curGame->addMapObserver(mapobs);
		MapModel* m = curGame->getMap();
		::BomberLoutreInterface::Map ret;
		/* TODO remplir ret */
		return ret;
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
		gd.name			= (*i)->getNameLocal();
		gd.roundCount	= (*i)->getRoundCountLocal();
		gd.state		= (*i)->getState();
		gd.playerCount	= (*i)->getPlayerCountLocal();
		gd.gameui		= (*i)->getProxy()->ice_twoway();
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


void ServerModel::loadMap(const std::string dossier)
{
	string fileContent[5];
	boost::filesystem::path path(dossier);
	if(!boost::filesystem::exists(path)) {
		std::cout << "Invalid path!" << std::endl;
		return;
	}

	if(boost::filesystem::is_directory(path)) {
		for(boost::filesystem::directory_iterator it(path), end; it != end; ++it) {
			if(boost::filesystem::is_regular_file(it->status())) {				
				
				fstream fs(it->path().string());
				if(fs)  
				{
					string contenu; 
					int cpt = 1;
										
					fileContent[0] = "";
					while(!fs.eof())
					{
						getline(fs, contenu);  
						if(contenu[0] != '#') 
						{
							fileContent[0] +=contenu;
						}
						else
						{
							if(cpt==4) contenu = contenu.substr(6,contenu.length());
							fileContent[cpt] = contenu;
							cpt++;
						}

					}	
					this->mapFiles.push_back(fileContent);
					fs.close();
				}
				else
				{
					cerr << "Impossible d'ouvrir le fichier !" << endl;
				}
			}
		}
	}
}

std::string* ServerModel::getMap(const std::string mapName)
{
	vector<string[5]>::iterator it;

	for ( it=this->mapFiles.begin() ; it < this->mapFiles.end(); it++ )
	{
		if(*it[4] == mapName) return *it;
	}
	return NULL;
}