#include "ServerApplication.hpp"

BomberServerApplication::BomberServerApplication()
{}

BomberServerApplication::~BomberServerApplication()
{}
	
int BomberServerApplication::run(int argc, char** argv)
{
	std::cout << "My server is running !!" << std::endl;
	
	m_adapter = communicator()->createObjectAdapter("BomberloutreServer");
	
	m_server = new ServerModel(m_adapter);
	m_adapter->add(m_server,communicator()->stringToIdentity("BomberServer"));
	
	m_adapter->activate();
	
	callbackOnInterrupt();
	communicator()->waitForShutdown();
	return EXIT_SUCCESS;
}

void BomberServerApplication::interruptCallback(int signal)
{
	std::cout << signal << " Doing some interrupt work..." << std::endl;
	communicator()->shutdown();
}
