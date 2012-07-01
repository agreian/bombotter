using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberLoutreIce;

namespace BomberLoutre.Client.Launcher
{
    public class GameClient
    {
        private Game _game;

        public string Name
        {
            get
            {
                return _game.name;
            }
        }

        public int RoundCount
        {
            get
            {
                return _game.roundCount;
            }
        }

        public string GameMode
        {
            get
            {
                return _game.gameMode;
            }
        }

        public string CreatorLogin
        {
            get
            {
                return _game.creator.login;
            }
        }

        public string MapName
        {
            get
            {
                return _game.gameMap.name;
            }
        }

        public string FirstLine
        {
            get
            {
                return string.Format("{0} crée par {1} - Partie {2}", this.Name, this.CreatorLogin, this.GameMode);
            }
        }

        public Game Game
        {
            get
            {
                return this._game;
            }
        }

        public string SecondLine
        {
            get
            {
                int freeSpace = 4;
                for (int i = 0; i < _game.users.Length; ++i)
                {
                    if (!string.IsNullOrWhiteSpace(_game.users[i].login))
                    {
                        freeSpace--;
                    }
                }

                return string.Format("Carte {0} - {1} place(s) disponible(s) - {2} manche(s)", this.MapName, freeSpace, this.RoundCount);
            }
        }

        public GameClient(Game game)
        {
            _game = game;
        }
    }
}
