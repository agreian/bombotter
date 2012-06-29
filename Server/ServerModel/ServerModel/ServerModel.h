#ifndef __SERVERMODEL_H__
#define __SERVERMODEL_H__

#include <vector>

#include <Ice/Ice.h>
#include <IceUtil/IceUtil.h>

#include "Bomberloutre.h"
#include "GameModel.h"
#include "UserModel.h"
#include "PlayerModel.h"

class ServerModel : 
	public ::BomberLoutreInterface::ServerInterface
{
	public :
		ServerModel(::Ice::ObjectAdapterPtr);
		~ServerModel();

		void removeGame(std::string name);
		//void sendInvitationToPlayer(PlayerModel* player, GameModel* g);
		
		virtual BomberLoutreInterface::UserData 
		connect(const std::string&, const std::string&, const Ice::Current& = ::Ice::Current());
		
		virtual BomberLoutreInterface::UserData
		createUser(const std::string&, const std::string&, const Ice::Current& = ::Ice::Current());
		
		virtual bool 
		deleteUser(const std::string&, const Ice::Current& = ::Ice::Current());
		
		virtual BomberLoutreInterface::GameInterfacePrx
		addGame(const std::string &,const BomberLoutreInterface::UserData &, const ::BomberLoutreInterface::GameWaitRoomPrx &,const BomberLoutreInterface::MapObserverPrx &,const Ice::Current & = ::Ice::Current());
		
		virtual BomberLoutreInterface::Map 
		joinGame(const std::string&, const ::BomberLoutreInterface::UserData &, const ::BomberLoutreInterface::GameWaitRoomPrx&, const ::BomberLoutreInterface::MapObserverPrx&, const Ice::Current& = ::Ice::Current());
		
		virtual BomberLoutreInterface::GameDataList 
		getGameList(const Ice::Current& = ::Ice::Current());

		virtual ::BomberLoutreInterface::GameInterfacePrx
		getUserInterface(::BomberLoutreInterface::GameData gd);
		
		virtual BomberLoutreInterface::UserDataList 
		getUserList(const Ice::Current& = ::Ice::Current());

		void loadMap(const std::string dossier);
		std::string* getMap(const std::string mapName);

		BomberLoutreInterface::GameInterfacePrx 
		getUserInterface(const BomberLoutreInterface::GameData &,const Ice::Current & = ::Ice::Current());
	
	private:
		ServerModel();

		std::vector<GameModel*> m_currentGames;
		std::vector<UserModel*> m_currentUsers;

		::Ice::ObjectAdapterPtr m_adapter;
		std::vector< std::string[5] > mapFiles;
};

#endif
