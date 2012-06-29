#include <iostream>

#include "ServerApplication.hpp"

int main(int argc, char** argv)
{
	std::cout << "Hello World!" << std::endl;
	try
	{
		std::cout << "App creation" << std::endl;
		BomberServerApplication app;
		std::cout << "App created" << std::endl;
		return app.main(argc,argv,"server.cfg");
	}
	catch(const std::exception& e)
	{
		std::cerr << e.what() << std::endl;
	}
	catch(...)
	{
		std::cerr << "Exception" << std::endl;
	}
}
