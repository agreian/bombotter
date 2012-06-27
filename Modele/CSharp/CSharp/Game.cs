using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberLoutre.Library
{
    public class Game
    {
        public string Name
        {
            get;
            private set;
        }

        private int nbRound = 0;
        private int state = 0;

        public Map MyMap
        {
            get;
            private set;
        }
        public Server MyServer
        {
            get;
            private set;
        }
        public User[] Players
        {
            get
            {
                return _players;
            }
        }
        public User Creator
        {
            get;
            private set;
        }

        private User[] _players = new User[4];

        public static Game CreateGame(string name, User creator, Server server)
        {
            return new Game(name, creator, server);
        }

        private Game(string name, User creator, Server server)
        {
            this.Name = name;
            this.Creator = creator;
            this.MyServer = server;
        }

        public void AddBot()
        {
            int cptBlank = 0;

            for (int i = 0; i < _players.Length; ++i)
            {
                if (_players[i] == null)
                {
                    ++cptBlank;
                }
            }

            if (cptBlank > 0)
            {
                //Ajouter Bot
                throw new NotImplementedException();
            }
        }
        public void CreateMap(string mod, string name)
        {
            switch (mod)
            {
                case "Classic":
                    throw new NotImplementedException();
                    break;
                case "Survival":
                    throw new NotImplementedException();
                    break;
                case "FogOfWar":
                    throw new NotImplementedException();
                    break;
                default:
                    throw new ArgumentException("Mode de jeu invalide");
            }
        }
        public void EndMap()
        {

        }
        public void InvitePlayer(string gameTag)
        {
            throw new NotImplementedException();
        }
        public void KickPlayer(string name)
        {
            throw new NotImplementedException();
        }
        public void RemoveBot()
        {
            throw new NotImplementedException();
        }
        public void Render()
        {
            //WTF
            throw new NotImplementedException();
        }
        public void StartMap()
        {
            //WTF
            throw new NotImplementedException();
        }
    }
}
