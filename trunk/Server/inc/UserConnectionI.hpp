#ifndef __USERCONNECTIONI_HPP__
#define __USERCONNECTIONI_HPP__

#include <Ice/Ice.h>
#include "Bomberloutre_user.hpp"
#include "Bomberloutre_game.hpp"

class UserConnectionI : public ::Bomberloutre::UserConnection
{
public:
	virtual void 
		connect_async(const ::Bomberloutre::AMD_UserConnection_connectPtr&,
			const ::std::string&, const ::std::string&, const ::Ice::Current&);
		
	virtual void 
		createUser_async(const ::Bomberloutre::AMD_UserConnection_createUserPtr&,
			const ::std::string&, const ::std::string&, const ::Ice::Current&);
};

#endif
