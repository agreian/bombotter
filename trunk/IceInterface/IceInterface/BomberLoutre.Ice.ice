#ifndef __BOMBERLOUTRE__
#define __BOMBERLOUTRE__

module BomberLoutreIce
{
	struct User
	{
		int id;
		string login;
		string password;
	};

	exception UserException
	{
		string reason;
	};
	exception BadUserInfoException extends UserException {};
	exception UserAlreadyExistsException extends UserException {};

	interface Client
	{
		User Connect(string login, string password) throws BadUserInfoException;
		User CreateUser(string login, string password) throws UserAlreadyExistsException;
	};
};

#endif