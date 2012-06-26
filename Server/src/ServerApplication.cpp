#include "ServerApplication.hpp"

ServerApplication::ServerApplication()
{

}

ServerApplication::~ServerApplication()
{

}
	
int ServerApplication::run(int argc, char** argv)
{
	std::cout << "My server is running !!" << std::endl;
	
	myobject = new UserConnectionI();
	
	Ice::ObjectAdapterPtr adapter = 
		communicator()->createObjectAdapterWithEndpoints("BomberlouterServerAdapater","tcp -p 10001");
	adapter->add(myobject, communicator()->stringToIdentity("MyObject"));
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
