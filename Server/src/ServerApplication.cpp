#include "ServerApplication.hpp"

ServerApplication::ServerApplication()
{}

ServerApplication::~ServerApplication()
{}
	
int ServerApplication::run(int argc, char** argv)
{
	std::cout << "My server is running !!" << std::endl;
	
	m_userConnection = new UserConnectionI();
	m_gamesManager = new GamesManagerI();
	
	Ice::ObjectAdapterPtr adapter = 
		communicator()->createObjectAdapter("UserConnection");
		
	adapter->add(m_userConnection, communicator()->stringToIdentity("UserConnectionInterface"));
	m_gamesManagerPrx = adapter->add(m_gamesManager, communicator()->stringToIdentity("GamesManagerInterface"));
	
	adapter->activate();
	
	callbackOnInterrupt();
	communicator()->waitForShutdown();
	return EXIT_SUCCESS;
}

void ServerApplication::interruptCallback(int signal)
{
	std::cout << signal << " Doing some interrupt work..." << std::endl;
	communicator()->shutdown();
}
