#ifndef __BOMBERLOUTRE__
#define __BOMBERLOUTRE__

module BomberLoutreIce
{
	struct User
	{
		string login;
		string password;
	};

	exception UserException { string reason; };
	exception BadLoginException 			extends UserException {};
	exception BadPasswordException 			extends UserException {};
	exception UserAlreadyExistsException 	extends UserException {};

	interface Client
	{
		User Connect(string login, string password) throws BadLoginException, BadPasswordException;
		User CreateUser(string login, string password) throws UserAlreadyExistsException;
	};
};

#endif