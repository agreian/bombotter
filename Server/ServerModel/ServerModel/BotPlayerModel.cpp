#include <time.h>
#include "BotPlayerModel.h"
#include "BombItem.h"
#include "MapModel.h"
#include "UserModel.h"

const float dangerLimit = 10.0f;
const float criticalLimit = 0.0f;

BotPlayerModel::BotPlayerModel()
{
	
}
		
BotPlayerModel::BotPlayerModel(MapModel *map, UserModel *user, int posX, int posY):PlayerModel(map, user, posX, posY)
{
}

void BotPlayerModel::autoMove()
{
	// STEP 1 : initializing the test grids
	initGrids();
	initParam();

	// STEP 2 : looking for where the guy can move
	findExplosions();
	findWays();

	// STEP 3 : testing the best actions
	findActions();


	// STEP 4 : doing the best action
	doBest();
}


void BotPlayerModel::initParam()
{
	this->x = this->posX / 60;
	this->y = this->posY / 60;

	int relativeX = this->x * 60;
	int relativeY = this->y * 60;
	int delta = 0;
	if(this->posX == relativeX)
	{
		delta = relativeY-posY;
		if(delta > 0)
		{
			target = PlayerModel::dirRight;
			this->displacement = delta;
		} else {
			target = PlayerModel::dirLeft;
			this->displacement = -delta;
		}
	} else {
		delta = relativeX-posX;
		if(delta > 0)
		{
			target = PlayerModel::dirDown;
			this->displacement = delta;
		} else {
			target = PlayerModel::dirUp;
			this->displacement = -delta;
		}
	}
}
// reset the test maps
void BotPlayerModel::initGrids()
{
	for (int i=0; i<MAPWIDTH; i++)
		for (int j=0; j<MAPHEIGHT; j++)
		{
			this->wayMap[i][j] = -1;
			this->resultMap[i][j] = 0;
			this->delayMap[i][j] = 1000.0f;
		}
}

// find all the space where the guy can move
// the grid "this->wayMap" will be fill with values.
// -1 means unreachable
// 0  means where the player stands
// >0 means a reachable place, the number means how far it is
void BotPlayerModel::findWays()
{
	testMovability(this->x,this->y,0, true);

	int x0=x, y0=y;
	if (displacement > 0)
	{
		switch (target)
		{
			case PlayerModel::dirLeft: x0--; break;
			case PlayerModel::dirRight: x0++; break;
			case PlayerModel::dirUp: y0--; break;
			case PlayerModel::dirDown: y0++; break;
		}
		if (target>=0 && target<=3 && this->map->logicalMap[x0][y0] == MapModel::BombItemCode)
			testMovability(x0,y0,1, true);
	}


}

// tested
void BotPlayerModel::testMovability(int posx, int posy, int step, bool onCell)
{
	if (this->map->logicalMap[posx][posy] == MapModel::VoidItemCode ||
		/*this->map->logicalMap[posx][posy] == MapModel::BonusItemCode ||*/ onCell)
	{
		if (this->delayMap[posx][posy] > criticalLimit)
		{
			if (this->wayMap[posx][posy] == -1)
			{
				this->wayMap[posx][posy] = step;

				// recursively looking of neighbor cells
				testMovability(posx+1,posy,step+1, false);
				testMovability(posx,posy+1,step+1, false);
				testMovability(posx-1,posy,step+1, false);
				testMovability(posx,posy-1,step+1, false);
			} else {	// "deja-vu"
				// update if we find a better way
				if (this->wayMap[posx][posy] > step)
				{
					this->wayMap[posx][posy] = step;
					// recursively looking of neighbor cells
					testMovability(posx+1,posy,step+1, false);
					testMovability(posx,posy+1,step+1, false);
					testMovability(posx-1,posy,step+1, false);
					testMovability(posx,posy-1,step+1, false);
				}
			}
		}
	}
}

void BotPlayerModel::findActions()
{
	if (this->delayMap[x][y] > dangerLimit)	// not in danger
	{
		for (int i=0; i<MAPWIDTH; i++)
		{
			for (int j=0; j<MAPHEIGHT; j++)
			{
				// unreachable
				if (this->wayMap[i][j] == -1)
				{
					this->resultMap[i][j] = -10000;
				// dangerous
				} else if (this->delayMap[i][j] < dangerLimit) {
					this->resultMap[i][j]=-5000;
				} else {
					bool togo = false;
					this->actionMap[i][j] = actionMove;
					// distance = handicap => malus points
					//////////////////////////////////////////////////
					this->resultMap[i][j] = 100-this->wayMap[i][j];

					if (this->nbBombUsed < this->nbBomb)
					{
						// lot of things to explode => bonus points
						int ect = explosingCount(i,j,this->flamePower);
						this->resultMap[i][j] += (3*ect);
						if (ect>=1)
						{
							this->actionMap[i][j] = actionBomb;
							togo = true;
						}

						// escape possible ?
						for (int k=0; k<MAPWIDTH; k++)
							for (int l=0; l<MAPHEIGHT; l++)
								this->simuMap[k][l] = this->delayMap[k][l];
						if (!simuBomb(i,j,this->flamePower))
						{
							this->resultMap[i][j] -= 5000;
							this->actionMap[i][j] = actionMove;
							togo = false;
						} else {
							int kc = killingCount(i,j,this->flamePower);
							this->resultMap[i][j] += (4*kc);
							if (kc >= 1)
							{
								this->actionMap[i][j] = actionBomb;
								togo = true;
							}
						}
					}

					// taking bonus => bonus points
					if (this->map->logicalMap[i][j] >= MapModel::FlameUpCode && this->map->logicalMap[i][j] <= MapModel::ShieldItemCode)
					{
						this->actionMap[i][j] = actionMove;
						this->resultMap[i][j] += 15;
					}

					if (!togo)
						this->resultMap[i][j] = -1000;

				}
			}
		}
	} else {	// danger
		for (int i=0; i<MAPHEIGHT; i++)
		{
			for (int j=0; j<MAPWIDTH; j++)
			{
				// unreachable
				if (this->wayMap[i][j]==-1)
				{
					this->resultMap[i][j] = -10000;
				} else if (this->delayMap[i][j] > dangerLimit) {
					this->resultMap[i][j] = -5000;
				} else {
					// distance = handicap => malus points
					this->resultMap[i][j] = 50-this->wayMap[i][j];
				}
			}
		}
	}
}

int BotPlayerModel::explosingCount(int posx, int posy, int range)
{
	int result = 0;

	//  4 directions exploding
	for (int dir = PlayerModel::dirLeft; dir <= PlayerModel::dirDown; dir++)
	{
		bool goOut = false;
		int distance = 1;

		while (distance <= range && !goOut)
		{
			int x0=posx, y0=posy;
			switch (dir)
			{
			case PlayerModel::dirLeft: x0-=distance; break;
			case PlayerModel::dirRight: x0+=distance; break;
			case PlayerModel::dirUp: y0-=distance; break;
			case PlayerModel::dirDown: y0+=distance; break;
			}

			switch (this->map->logicalMap[x0][y0])
			{
				case MapModel::RockItemCode: goOut=true; break;
				case MapModel::BoxItemCode:  goOut=true;result++; break;
				case MapModel::FlameUpCode:  goOut=true;result--; break;	// destruct a bonus U MAD  ?
				case MapModel::GoldenFlameCode:  goOut=true;result--; break;	// destruct a bonus U MAD  ?
				case MapModel::BombUpCode:  goOut=true;result--; break;	// destruct a bonus U MAD  ?
				case MapModel::SpeedUpCode:  goOut=true;result--; break;	// destruct a bonus U MAD  ?
				case MapModel::KickerCode:  goOut=true;result--; break;	// destruct a bonus U MAD  ?
				case MapModel::InvisibleItemCode:  goOut=true;result--; break;	// destruct a bonus U MAD  ?
				case MapModel::InvincibleItemCode:  goOut=true;result--; break;	// destruct a bonus U MAD  ?
				case MapModel::ShieldItemCode:  goOut=true;result--; break;	// destruct a bonus U MAD  ?
			}
			distance++;
		}
	}

	return result;
}

int BotPlayerModel::killingCount(int posx, int posy, int range)
{
	int result = 0;

	//  4 directions exploding
	for (int dir = PlayerModel::dirLeft; dir <= PlayerModel::dirDown; dir++)
	{
		bool goOut = false;
		int distance = 1;

		while (distance <= range && !goOut)
		{
			int x0=posx, y0=posy;
			switch (dir)
			{
			case PlayerModel::dirLeft: x0-=distance; break;
			case PlayerModel::dirRight: x0+=distance; break;
			case PlayerModel::dirUp: y0-=distance; break;
			case PlayerModel::dirDown: y0+=distance; break;
			}

			switch (this->map->logicalMap[x0][y0])
			{
				case MapModel::RockItemCode:
				case MapModel::BoxItemCode:  
				case MapModel::FlameUpCode: 
				case MapModel::GoldenFlameCode:  
				case MapModel::BombUpCode:  
				case MapModel::SpeedUpCode:  
				case MapModel::KickerCode: 
				case MapModel::InvisibleItemCode:  
				case MapModel::InvincibleItemCode: 
				case MapModel::ShieldItemCode:  goOut=true; break;	// destruct a bonus U MAD  ?
			}

			//A voire demain !!!
			/*for (int i=0; i<this->map.nop; i++) 
			{
				if (i!=n)
				{
					if (x0==game.player[i]->x && y0==game.player[i]->y &&
						(game.player[i]->state==stateStanding || game.player[i]->state==stateMoving))
						result++;
				}
			}
			*/
			distance++;
		}
	}

	return result;
}

// tested
void BotPlayerModel::findExplosions()
{
	for (int i=0; i<MAPHEIGHT; i++)
		for (int j=0; j<MAPWIDTH; j++)
		{
			if (this->map->logicalMap[i][j] == MapModel::BombItemCode)
			{
				simulateExplosion(i,j,((BombItem*)(this->map->map[i][j]))->getTimer());
			}
		}
}

void BotPlayerModel::simulateExplosion(int posx, int posy, float expDelay)
{
	this->delayMap[posx][posy]=expDelay;
	int range = this->rangeMap[posx][posy];

	//  4 directions exploding
	for (int dir=PlayerModel::dirLeft; dir<= PlayerModel::dirDown; dir++)
	{
		bool goOut = false;
		int distance = 1;

		while (distance<=range && !goOut)
		{
			int x0=posx, y0=posy;
			switch (dir)
			{
				case PlayerModel::dirLeft: x0-=distance; break;
				case PlayerModel::dirRight: x0+=distance; break;
				case PlayerModel::dirUp: y0-=distance; break;
				case PlayerModel::dirDown: y0+=distance; break;
			}

			switch (this->map->logicalMap[x0][y0])
			{
				case MapModel::VoidItemCode: this->delayMap[x0][y0] = expDelay; break;
				case MapModel::RockItemCode: goOut=true; break;
				case MapModel::FlameItemCode: if (this->delayMap[x0][y0] > expDelay)this->delayMap[x0][y0] = expDelay; break;
				case MapModel::BoxItemCode:  goOut=true; break;
				case MapModel::FlameUpCode: 
				case MapModel::GoldenFlameCode:  
				case MapModel::BombUpCode:  
				case MapModel::SpeedUpCode:  
				case MapModel::KickerCode: 
				case MapModel::InvisibleItemCode:  
				case MapModel::InvincibleItemCode: 
				case MapModel::ShieldItemCode:  this->delayMap[x0][y0]=expDelay; break;
				case MapModel::BombItemCode: 
				{
					if (this->delayMap[x0][y0] < expDelay)
					{
						simulateExplosion(x0,y0, expDelay);
					}
				}break;

			}

			distance++;
		}
	}
}

void BotPlayerModel::doBest()
{
	if (isInDanger())	// in danger
		goOutDanger();
	else
		doBestAction();
}

void BotPlayerModel::doBestAction()
{
	int trying = 5;
	int third=-1;
	int second=-1;
	int best=-1;
	int tx, tx1, tx2, tx3;
	int ty, ty1, ty2, ty3;
	bool danger = true;

	tx = tx1 = tx2 = tx3 = -1;
	ty = ty1 = ty2 = ty3 = -1;

	while (danger && trying>0)
	{
		trying--;

		//if (tx==-1)
		{
			for (int i=0; i<MAPHEIGHT; i++)
				for (int j=0; j<MAPWIDTH; j++)
				{
					if (this->wayMap[i][j] >= 0 && this->resultMap[i][j] > third)
					{
						best = second;
						tx1 = tx2; ty1 = ty2;
						second = third;
						tx2 = tx3; ty2 = ty3;
						third = this->resultMap[i][j];
						tx3 = i; ty3 = j;
					}
				}
		}

		//find best
			
		srand (time(NULL));
		int result = rand() % 100 + 1;
		if(this->goalX == tx1 && this->goalY == ty1)
		{
			best = best *3;
		}
		if(this->goalX == tx2 && this->goalY == ty2)
		{
			second = second *3;
		}
		if(this->goalX == tx3 && this->goalY == ty3)
		{
			third = third *3;
		}

		int total = best + second + third;
		best = (100 * best) / (total);
		second = (100 * second) / (total);
		third = (100 * third) / (total);

		if(result < third)
		{
			tx = goalX = tx3;
			ty = goalY = ty3;
		} else if(result < (third + second)) {
			tx = goalX = tx2;
			ty = goalY = ty2;
		} else {
			tx = goalX = tx1;
			ty = goalY = ty1;
		}

		//
		if (tx==-1)
		{
			trying=0;
		}
		else if (x == tx && y == ty) // good position
		{
			if (displacement > 9)
			{
				switch (target)
				{
					case PlayerModel::dirLeft: _moveRight = true; break;
					case PlayerModel::dirRight: _moveLeft = true; break;
					case PlayerModel::dirUp: _moveDown = true; break;
					case PlayerModel::dirDown: _moveUp = true; break;
				}
			} else {
				tx=-1;
				if (this->actionMap[x][y] == actionBomb)
				{
					dropBomb();
				}
			}
		} else {
			int dist = this->wayMap[tx][ty];
			int mtx=tx, mty=ty;
			danger = false;
			while (dist>1)
			{
				if (this->wayMap[mtx+1][mty] == dist-1)mtx++;
				else if (this->wayMap[mtx-1][mty] == dist-1)	mtx--;
				else if (this->wayMap[mtx][mty+1] == dist-1)	mty++;
				else if (this->wayMap[mtx][mty-1] == dist-1)	mty--;	
				if (this->delayMap[mtx][mty] < dangerLimit) danger = true;
				dist--;
			}

			if (danger)
			{
				tx = -1;
				this->resultMap[tx][ty]=-5000;
			} else if (mtx>x) {
				if (this->target==PlayerModel::dirUp && displacement > 9)
					_moveDown = true;
				else if (this->target==PlayerModel::dirDown && displacement > 9)
					_moveUp = true;
				else
					_moveRight = true;		
			} else if (mtx<x) {
				if (this->target == PlayerModel::dirUp && displacement > 9)
					_moveDown = true;
				else if (this->target == PlayerModel::dirDown && displacement > 9)
					_moveUp = true;
				else
					_moveLeft = true;	
			} else if (mty>y) {
				if (this->target==PlayerModel::dirLeft && displacement > 9)
					_moveRight=true;
				else if (this->target==PlayerModel::dirRight && displacement > 9)
					_moveLeft=true;
				else
					_moveUp=true;
			} else if (mty<y) {
				if (this->target == PlayerModel::dirLeft && displacement > 9)
					_moveRight=true;
				else if (this->target == PlayerModel::dirRight && displacement > 9)
					_moveLeft = true;
				else
					_moveDown = true;
			}

		} // end while

		if (tx == -1 && this->delayMap[x][y]<10.0f)
			goOutDanger();
		}

}

void BotPlayerModel::goOutDanger()
{
	int tx=-1, ty=-1, record = 50;
	int tx2=-1, ty2=-1, record2 = 50; 
	float bestTime = 0.0f;

	for (int i=0; i<MAPHEIGHT; i++)
		for (int j=0; j<MAPWIDTH; j++)
		{
			// distance score
			if (this->wayMap[i][j]>=0 && this->delayMap[i][j] > 10.0f && this->wayMap[i][j]<record)
			{
				record = this->wayMap[i][j];
				tx = i; ty = j;
			}
			// time score
			if (this->wayMap[i][j]>=0)
			{
				if (this->delayMap[i][j] > bestTime 
					|| (this->delayMap[i][j] == bestTime && this->wayMap[i][j]<record2) )
				{
					bestTime = this->delayMap[i][j];
					record2 = this->wayMap[i][j];
					tx2 = i; ty2 = j;
				}
			}
		}

	if (record>10)
	{
		tx = tx2;
		ty = ty2;
	}

	if (x == tx && y == ty) // good position
	{
		if (displacement > 1)
		{
			switch (this->target)
			{
				case dirLeft: 
				{
					_moveRight=true;
					break;
				}
				case dirRight:
				{
					_moveLeft=true;
					break;
				}
				case dirUp:
				{
					_moveDown=true;
					break;
				}
				case dirDown:
				{
					_moveUp=true;
					break;
				}
			}
		}
	} else if (tx>0) {
		int dist = this->wayMap[tx][ty];
		int mtx=tx, mty=ty;

		while (dist>1)
		{
			if (this->wayMap[mtx+1][mty] == dist-1)	mtx++;
			else if (this->wayMap[mtx-1][mty] == dist-1)	mtx--;
			else if (this->wayMap[mtx][mty+1] == dist-1)	mty++;
			else if (this->wayMap[mtx][mty-1] == dist-1)	mty--;	

			dist--;
		}


		if (mtx>x) 
		{
			if (this->target==PlayerModel::dirUp && displacement > 9)
				_moveDown=true;
			else if (this->target==PlayerModel::dirDown && displacement > 9)
				_moveUp=true;
			else
				_moveRight=true;		
		}
		else if (mtx<x)
		{
			if (this->target==PlayerModel::dirUp && displacement > 9)
				_moveDown = true;
			else if (this->target==PlayerModel::dirDown && displacement > 9)
				_moveUp = true;
			else
				_moveLeft = true;	
		}
		else if (mty>y) 
		{
			if (this->target==PlayerModel::dirLeft && displacement > 9)
				_moveRight=true;
			else if (this->target==PlayerModel::dirRight && displacement > 9)
				_moveLeft=true;
			else
				_moveUp=true;
		}
		else if (mty<y) 
		{
			if (this->target==PlayerModel::dirLeft && displacement > 9)
				_moveRight=true;
			else if (this->target==PlayerModel::dirRight && displacement > 9)
				_moveLeft=true;
			else
				_moveDown=true;
		}
	}
}

bool BotPlayerModel::action()
{
	if(isAlive())
	{
		autoMove();

		if (!_moveDown && !_moveUp && !_moveRight && !_moveLeft)
		{
	
		}
		else
		{
		
		}

		// testing the directional-keys
		if (_moveDown)
		{
			moveDown();
		} else if(_moveUp) {
			moveUp();
		} else if (_moveLeft) {
			moveLeft();
		} else if (_moveRight) {
			moveRight();
		}
	}
	return true;
}


void BotPlayerModel::randomMove()
{
	int i=0;
	bool goOut = false;
	srand (time(NULL));
	int randDir =  rand() % 3;
	
	while (!goOut && i<4)
	{
		int x0=x, y0=y;
		switch (target)
		{
			case PlayerModel::dirDown: y0++; break;
			case PlayerModel::dirUp: y0--; break;
			case PlayerModel::dirLeft: x0--; break;
			case PlayerModel::dirRight: x0++; break;
		}
		if (this->map->logicalMap[x0][y0] <= MapModel::VoidItemCode) 
		{
			switch (target)
			{
				case PlayerModel::dirDown: _moveDown = true; break;
				case PlayerModel::dirUp: _moveUp = true; break;
				case PlayerModel::dirLeft: _moveLeft = true; break;
				case PlayerModel::dirRight: _moveRight = true; break;
			}
			goOut = true;
		}
		i++;
		randDir++;
		if (randDir>3) randDir=0;
	}
}

// tested
bool BotPlayerModel::simuBomb(int posx, int posy, int range)
{
	this->simuMap[posx][posy] = 3;

	//  4 directions exploding
	for (int dir=PlayerModel::dirLeft; dir<= PlayerModel::dirDown; dir++)
	{
		bool goOut = false;
		int distance = 1;

		while (distance<=range && !goOut)
		{
			int x0=posx, y0=posy;
			switch (dir)
			{
			case PlayerModel::dirLeft: x0-=distance; break;
			case PlayerModel::dirRight: x0+=distance; break;
			case PlayerModel::dirUp: y0+=distance; break;
			case PlayerModel::dirDown: y0-=distance; break;
			}

			switch (this->map->logicalMap[x0][y0])
			{
			case MapModel::VoidItemCode: this->simuMap[x0][y0] = 3 ; break;
			case MapModel::RockItemCode: goOut=true; break;
			case MapModel::FlameItemCode: this->simuMap[x0][y0]= 3; break;
			case MapModel::BoxItemCode:  goOut=true; break;
			case MapModel::FlameUpCode: 
			case MapModel::GoldenFlameCode:  
			case MapModel::BombUpCode:  
			case MapModel::SpeedUpCode:  
			case MapModel::KickerCode: 
			case MapModel::InvisibleItemCode:  
			case MapModel::InvincibleItemCode: 
			case MapModel::ShieldItemCode:  this->simuMap[x0][y0] = 3; break;
			}
			distance++;
		}
	}

	for (int i=0; i<MAPHEIGHT; i++)
		for (int j=0; j<MAPWIDTH; j++)
		{	
			if (this->wayMap[i][j] >= 0 && this->resultMap[i][j] >= 0)
			{
				if (this->simuMap[i][j] > 10.0f) return true;
			}
		}
		
	return false;
}
		

bool BotPlayerModel::isInDanger()
{
	if (this->delayMap[x][y] < dangerLimit || this->map->logicalMap[x][y] == MapModel::BombItemCode) return true;

	if (displacement < 6) return false;

	int x0=x, y0=y;
	switch (this->target)
	{
		case PlayerModel::dirDown: y0++; break;
		case PlayerModel::dirUp: y0--; break;
		case PlayerModel::dirLeft: x0--; break;
		case PlayerModel::dirRight: x0++; break;
	}
	if (this->delayMap[x0][y0]<dangerLimit || this->map->logicalMap[x0][y0] == MapModel::BombItemCode) return true;

	return false;
}
