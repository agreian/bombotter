// ServerModel.h

#pragma once

using namespace std;

class ServerModel
{
    // TODO: ajoutez ici vos méthodes pour cette classe.
    public :
        ServerModel()
        ~ServerModel()
        
        void addGame(string name);
        void getListGame();
        void getListUser();
        void removeGame(string name);
        void sendInvitationToPlayer(PlayerModel player, Game g);
    
};