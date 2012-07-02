#ifndef __BOMBERLOUTRE__
#define __BOMBERLOUTRE__

module BomberLoutreIce
{
	struct UserInfo
	{
		string login;

		int gameCount;
		int winCount;
		int drawCount;
		int looseCount;
		
		int killCount;
		int deathCount;
		int suicideCount;
	};
	sequence<UserInfo> UserInfoList;

	struct User
	{
		int id;
		string password;
		UserInfo info;
	};

	struct MapItem
	{
		int i;
		int j;
		
		bool destructible;
	};
	sequence<MapItem> MapItemList;

	struct Bomb
	{
		int i;
		int j;

		int power;
	};
	sequence<Bomb> BombList;

	struct Bonus
	{
		int i;
		int j;

		string type;
	};
	sequence<Bonus> BonusList;

	struct Map
	{	
		string name;

		string groundTexture;
		string boxTexture;
		string rockTexture;

		MapItemList mapItems;
	};

	struct Game
	{
		string name;
		int roundCount;
		string gameMode;
		UserInfo creator;
		UserInfoList users;
		Map gameMap;
	};
	sequence<Game> GameList;

	exception UserException { };

	exception BadUserInfoException extends UserException { };
	exception UserAlreadyExistsException extends UserException { };

	exception GameException { };
	exception WTFIsThatGameException extends GameException { };
	exception GameIsFullException extends GameException { };

	interface Client
	{
		void Test();
	};

	interface Server
	{
		User Connect(string login, string password) throws BadUserInfoException;
		User CreateUser(string login, string password) throws UserAlreadyExistsException;
		GameList GetGameList();
		Game JoinGame(Game aGame, User aUser) throws WTFIsThatGameException;
		Game CreateGame(Game newGame, User aUser);
	};
};

#endif