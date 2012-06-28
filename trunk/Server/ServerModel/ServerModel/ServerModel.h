// ServerModel.h

#ifndef __SERVERMODEL_H__
#define __SERVERMODEL_H__

#include <Ice/Ice.h>
#include <IceUtil/IceUtil.h>

#include "Bomberloutre.h"

class ServerModel : 
	public ::BomberLoutreInterface::ServerInterface
{
	public :
		ServerModel();
		ServerModel(::Ice::ObjectAdapterPtr);
		~ServerModel();

		void addGame(std::string name);
		void getListGame();
		void getListUser();
		void removeGame(std::string name);
		void sendInvitationToPlayer(PlayerModel player, Game g);
		
		virtual BomberLoutreInterface::UserData 
		connect(const std::string&, const std::string&, const Ice::Current& = ::Ice::Current());
		
		virtual BomberLoutreInterface::UserData
		createUser(const std::string&, const std::string&, const Ice::Current& = ::Ice::Current());
		
		virtual bool 
		deleteUser(const std::string&, const Ice::Current& = ::Ice::Current());
		
		virtual BomberLoutreInterface::GameInterfacePrx
		addGame(const std::string&, const ::BomberLoutreInterface::GameWaitRoomPrx&, const ::BomberLoutreInterface::MapObserverPrx&, const Ice::Current& = ::Ice::Current());
		
		virtual BomberLoutreInterface::Map 
		joinGame(const std::string&, const ::BomberLoutreInterface::GameWaitRoomPrx&, const ::BomberLoutreInterface::MapObserverPrx&, const Ice::Current& = ::Ice::Current());
		
		virtual BomberLoutreInterface::GameDataList 
		getGameList(const Ice::Current& = ::Ice::Current());
		
		virtual BomberLoutreInterface::UserDataList 
		getUserList(const Ice::Current& = ::Ice::Current());
	
	private:
		std::vector<int> m_currentGames;
		std::vector<UserModel*> m_currentUsers;

		::Ice::ObjectAdapterPtr m_adapter;
};

#endif
