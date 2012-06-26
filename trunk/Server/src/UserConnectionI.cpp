#include "UserConnectionI.hpp"

void
UserConnectionI::connect_async(const ::Bomberloutre::AMD_UserConnection_connectPtr& cb,
	const ::std::string&, const ::std::string&, const ::Ice::Current&)
{
	std::cout << "Connect" << std::endl;
	cb->ice_response(NULL);
}

void
UserConnectionI::createUser_async(const ::Bomberloutre::AMD_UserConnection_createUserPtr& cb,
	const ::std::string&, const ::std::string&, const ::Ice::Current&)
{
	std::cout << "Create User" << std::endl;
	cb->ice_response(NULL);
}
