#include <iostream>

#include "ServerApplication.hpp"

int main(int argc, char** argv)
{
	std::cout << "Hello World!" << std::endl;
	try
	{
		ServerApplication app;
		return app.main(argc,argv);
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
