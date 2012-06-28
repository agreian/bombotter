#ifndef __SERVERAPPLICATION_H__
#define __SERVERAPPLICATION_H__

#include <Ice/Ice.h>
#include <IceUtil/IceUtil.h>

#include "Bomberloutre.hpp"
#include "BomberServer.hpp"

class ServerApplication : public Ice::Application
{

public:
	ServerApplication();
	virtual ~ServerApplication();
	
	virtual int run(int argc, char** argv);
	virtual void interruptCallback(int);
	
private:
	::Ice::ObjectAdapterPtr m_adapter;
	BomberServer*	m_server;
};

#endif
