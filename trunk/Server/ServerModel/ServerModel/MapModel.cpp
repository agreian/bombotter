#include "MapModel.h"

/*MapModel::MapModel()
{
	//CODE TON PUTAIN DE BOT !
}*/

bool MapModel::checkMove(PlayerModel p, Point arrive)
{
	/*
	1 - �tudier la map actuelle et voir si le mouvement est possible (en fonction du point de destination et du Kicker du player)	
	- Regarder le couple de coordonn�es et connaitre sur quelle case cela se situe
	- Si c'est une case non Walkable, return false;
	Sinon true; (ne pas oublier les bomb, kickable dans certains cas... :/ ) 
	*/
	return true;
}

void MapModel::createMapItem(string typeMapItem)
{
	/*
	Factory de Map - A faire donc
	*/
}

void MapModel::dropBonus(MapItem* bonus)
{
	/*
	Pour le mode survival?
	*/
}

void MapModel::handleExplode(BombItem* b)
{
	/* G�rer l'explosion de la bomb */
}

void MapModel::loapMap(string id)
{
	/* Charger un fichier Map pour cr�er notre matrice initiale : voir parseur de J�r�my ?  */
}