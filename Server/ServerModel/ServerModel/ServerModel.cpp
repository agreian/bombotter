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
	std::cout << "ServerModel::connect BEGIN" << std::endl;
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
	std::cout << "ServerModel::connect END" << std::endl;
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
	std::cout << "ServerModel::addGame BEGIN" << std::endl;
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
	std::cout << "ServerModel::addGame END" << std::endl;
	return p;
}

BomberLoutreInterface::Map 
ServerModel::joinGame(const std::string& name, 
	const BomberLoutreInterface::UserData & us,
	const ::BomberLoutreInterface::GameWaitRoomPrx& room, 
	const ::BomberLoutreInterface::MapObserverPrx& mapobs, const Ice::Current&)
{
	std::cout << "ServerModel::joinGame BEGIN" << std::endl;
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
		::BomberLoutreInterface::Map ret;
		ret.id = curGame->getMap()->getId();
		::BomberLoutreInterface::MapItems items;
		for(unsigned int i=0;i<MAPWIDTH;i++)
		{
			for(unsigned int j=0;j<MAPHEIGHT;j++)
			{
				::BomberLoutreInterface::MapItem mi;
				mi.i = i;
				mi.j = j;
				mi.destructible = curGame->getMap()->map[i][j]->isDestructible();
				mi.walkable = curGame->getMap()->map[i][j]->isWalkable();

				items.push_back(mi);
			}
		}
		ret.items = items;
		// TODO
		ret.mi = curGame->getMap()->getInterfacePrx();
		std::cout << "ServerModel::joinGame END" << std::endl;
		return ret;
	}
	std::cout << "ServerModel::joinGame END Exception" << std::endl;
	throw std::exception();
}

BomberLoutreInterface::GameDataList 
ServerModel::getGameList(const Ice::Current&)
{
	std::cout << "ServerModel::getGameList BEGIN" << std::endl;
	BomberLoutreInterface::GameDataList list;
	for(std::vector<GameModel*>::iterator i=m_currentGames.begin();i!=m_currentGames.end();++i)
	{
		::BomberLoutreInterface::GameData gd;
		gd.name			= (*i)->getNameLocal();
		gd.roundCount	= (*i)->getRoundCountLocal();
		gd.state		= (*i)->getState();
		gd.playerCount	= (*i)->getPlayerCountLocal();
		list.push_back(gd);
	}
	std::cout << "ServerModel::getGameList END " << list.size() << std::endl;
	return list;
}

BomberLoutreInterface::UserDataList 
ServerModel::getUserList(const Ice::Current&)
{
	std::cout << "ServerModel::getUserList BEGIN" << std::endl;
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
	std::cout << "ServerModel::getUserList END " << list.size() << std::endl;
	return list;
}

BomberLoutreInterface::GameInterfacePrx 
ServerModel::getUserInterface(const ::BomberLoutreInterface::GameData & gd,const Ice::Current &)
{
	for(std::vector< GameModel*>::iterator i=m_currentGames.begin();i!=m_currentGames.end();++i)
	{
		if((*i)->getName() == gd.name)
		{
			return (*i)->getProxy();
		}
	}
	throw std::exception();
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
					vector<string> maps;
					for(int i = 0; i < 5; i++){
						maps.push_back(fileContent[i]);
					}
					this->mapFiles.push_back(maps);
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

std::vector<string> ServerModel::getMap(const std::string mapName)
{
	/*vector<string[5]>::iterator it;

	for ( it=this->mapFiles.begin() ; it < this->mapFiles.end(); it++ )
	{
		if(*it[4] == mapName) return *it;
	}
	return NULL;*/

	for(std::vector<vector<string>>::iterator ligne = this->mapFiles.begin(); ligne != this->mapFiles.end(); ++ligne) {
		for(std::vector<string>::iterator colonne = ligne->begin(); colonne != ligne->end(); ++colonne) {
			if(*colonne == mapName) return *ligne;
		}
	}
	return *(new vector<string>());
}