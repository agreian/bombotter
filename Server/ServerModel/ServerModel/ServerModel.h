// ServerModel.h

#ifndef __SERVERMODEL_H__
#define __SERVERMODEL_H__

#include <Ice/Ice.h>
#include <IceUtil/IceUtil.h>

#include "Bomberloutre.h"

class ServerModel : 
	public ::Bomberloutre::ServerInterface
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
		
		virtual Bomberloutre::UserData 
		connect(const std::string&, const std::string&, const Ice::Current& = ::Ice::Current());
		
		virtual Bomberloutre::UserData
		createUser(const std::string&, const std::string&, const Ice::Current& = ::Ice::Current());
		
		virtual bool 
		deleteUser(const std::string&, const Ice::Current& = ::Ice::Current());
		
		virtual Bomberloutre::GameInterfacePrx
		addGame(const std::string&, const ::Bomberloutre::GameWaitRoomPrx&, const ::Bomberloutre::MapObserverPrx&, const Ice::Current& = ::Ice::Current());
		
		virtual Bomberloutre::Map 
		joinGame(const std::string&, const ::Bomberloutre::GameWaitRoomPrx&, const ::Bomberloutre::MapObserverPrx&, const Ice::Current& = ::Ice::Current());
		
		virtual Bomberloutre::GameDataList 
		getGameList(const Ice::Current& = ::Ice::Current());
		
		virtual Bomberloutre::UserDataList 
		getUserList(const Ice::Current& = ::Ice::Current());
	
	private:
		std::vector<int> m_currentGames;
		std::vector<UserModel*> m_currentUsers;

		::Ice::ObjectAdapterPtr m_adapter;
};

#endif
