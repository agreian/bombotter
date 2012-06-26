#ifndef __SERVERAPPLICATION_H__
#define __SERVERAPPLICATION_H__

#include <Ice/Ice.h>
#include <IceUtil/IceUtil.h>

#include "Game.hpp"
#include "UserConnectionI.hpp"
#include "GamesManagerI.hpp"

class ServerApplication : public Ice::Application
{

public:
	ServerApplication();
	virtual ~ServerApplication();
	
	virtual int run(int argc, char** argv);
	virtual void interruptCallback(int);
	
private:
	Ice::ObjectAdapterPtr	m_adapter;
	
	UserConnectionI * 		m_userConnection;
	GamesManagerI	*		m_gamesManager;
	Ice::ObjectPrx			m_gamesManagerPrx;
	
	std::vector<Game> 		m_currentGames;

};

#endif
