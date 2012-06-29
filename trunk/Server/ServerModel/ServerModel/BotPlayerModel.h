#ifndef BOT_PLAYER_MODEL_HEADER
#define BOT_PLAYER_MODEL_HEADER

// BotPlayerModel.h
#include <string>
#include <iostream>
#include "PlayerModel.h"

class MapModel;
class UserModel;

using namespace std;

const int actionMove = 0;
const int actionBomb = 1;
const int actionRandom = 2;

class BotPlayerModel : public PlayerModel
{
public:
	BotPlayerModel();
	BotPlayerModel(MapModel* map, UserModel* user, int posX, int posY);

private:

	int displacement;	//distance to join the target 
	int state;
	enum playerStateId { stateStanding, stateMoving, stateDead, stateOut };
	int dir;
	int target;
	
	int x, y, goalX, goalY;
	bool _moveLeft, _moveRight, _moveUp, _moveDown;
	bool oldMoveDown, oldMoveUp, oldMoveLeft, oldMoveRight;
	// test maps used for the IA
	int wayMap[MAPWIDTH][MAPHEIGHT];
	float simuMap[MAPWIDTH][MAPHEIGHT];
	int resultMap[MAPWIDTH][MAPHEIGHT];
	int actionMap[MAPWIDTH][MAPHEIGHT];
	float delayMap[MAPWIDTH][MAPHEIGHT];
	int rangeMap[MAPWIDTH][MAPHEIGHT];

	void autoMove();
	void initGrids();
	void initParam();
	void findWays();
	void findActions();
	void findExplosions();
	int explosingCount(int posx, int posy, int range);
	void testMovability(int posx, int posy, int step, bool onCell);
	int killingCount(int posx, int posy, int range);
	void simulateExplosion(int posx, int posy, float expDelay);
	void doBest();
	void doBestAction();
	void goOutDanger();
	bool OnAnimate(float ElapsedTime , float AbsoluteTime);
	void randomMove();
	bool simuBomb(int posx, int posy, int range);
	bool isInDanger();
	bool action();

};

#endif