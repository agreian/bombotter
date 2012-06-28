#ifndef __SERVERAPPLICATION_H__
#define __SERVERAPPLICATION_H__

#include <Ice/Ice.h>
#include <IceUtil/IceUtil.h>

#include "Bomberloutre.h"
#include "ServerModel.h"

class ServerApplication : public Ice::Application
{

public:
	ServerApplication();
	virtual ~ServerApplication();
	
	virtual int run(int argc, char** argv);
	virtual void interruptCallback(int);
	
private:
	::Ice::ObjectAdapterPtr m_adapter;
	ServerModel*	m_server;
};

#endif
