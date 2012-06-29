#include "ServerApplication.hpp"

BomberServerApplication::BomberServerApplication()
{}

BomberServerApplication::~BomberServerApplication()
{}
	
int BomberServerApplication::run(int argc, char** argv)
{
	std::cout << "BomberLoutre server begins" << std::endl;
	
	m_adapter = communicator()->createObjectAdapter("BomberloutreServer");
	std::cout << "Adapter OK" << std::endl;

	m_server = new ServerModel(m_adapter);
	m_adapter->add(m_server,communicator()->stringToIdentity("BomberServer"));
	std::cout << "Object added" << std::endl;

	m_adapter->activate();
	std::cout << "Adapter activated" << std::endl;
	
	callbackOnInterrupt();
	communicator()->waitForShutdown();
	std::cout << "BomberLoutre server ends" << std::endl;
	return EXIT_SUCCESS;
}

void BomberServerApplication::interruptCallback(int signal)
{
	std::cout << signal << " Doing some interrupt work..." << std::endl;
	communicator()->shutdown();
}
