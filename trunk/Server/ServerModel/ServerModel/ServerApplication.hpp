#ifndef __SERVERAPPLICATION_H__
#define __SERVERAPPLICATION_H__

#include <Ice/Ice.h>
#include <IceUtil/IceUtil.h>

#include "Bomberloutre.h"
#include "ServerModel.h"

class BomberServerApplication : 
	public Ice::Application
{
public:
	BomberServerApplication();
	virtual ~BomberServerApplication();
	
	virtual int run(int argc, char** argv);
	
private:
	::Ice::ObjectAdapterPtr m_adapter;
	ServerModel*			m_server;
};

#endif
