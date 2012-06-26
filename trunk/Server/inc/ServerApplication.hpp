#ifndef __SERVERAPPLICATION_H__
#define __SERVERAPPLICATION_H__

#include <Ice/Ice.h>
#include <IceUtil/IceUtil.h>

#include "Game.hpp"
#include "UserConnectionI.hpp"

class ServerApplication : public Ice::Application
{

public:
	ServerApplication();
	virtual ~ServerApplication();
	
	virtual int run(int argc, char** argv);
	virtual void interruptCallback(int);
	
private:
	UserConnectionI * myobject;
	std::vector<Game> _currentGames;

};

#endif
