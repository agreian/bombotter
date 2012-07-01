using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberLoutreIce;

namespace BomberLoutre.Server
{
    public class ClientI : BomberLoutreIce.ClientDisp_
    {
        private List<Game> _gameList = new List<Game>();

        public override BomberLoutreIce.User Connect(string login, string password, Ice.Current current__)
        {
            Console.WriteLine("Tentative de connexion de {0}", login);

            User user = DatabaseInterface.SelectUser(login, password);

            Console.WriteLine("{0} est connecté", login);

            return user;
        }

        public override BomberLoutreIce.User CreateUser(string login, string password, Ice.Current current__)
        {
            Console.WriteLine("Tentative d'inscription de {0}", login);

            User user = DatabaseInterface.InsertUser(login, password);

            Console.WriteLine("{0} est inscrit", login);

            return user;
        }

        public override Game[] GetGameList(Ice.Current current__)
        {
            if (_gameList.Count == 0)
            {
                UserInfo god = new UserInfo("god", 0, 0, 0, 0, 0, 0, 0);
                UserInfo agreian = new UserInfo("agreian", 0, 0, 0, 0, 0, 0, 0);
                Map firstMap = new Map("First Map", "lol", "lol", "lol", new MapItem[1]);
                Map secondMap = new Map("Nazi Map", "lol", "lol", "lol", new MapItem[1]);

                _gameList.Add(new Game("Awesome Game", 1, "Classique", god, new UserInfo[4] { god, null, null, null }, firstMap));
                _gameList.Add(new Game("Epic Game", 1, "Classique", agreian, new UserInfo[4] { null, null, null, null }, secondMap));
            }

            return _gameList.ToArray();
        }

        public override ServerPrx JoinGame(string name, Ice.Current current__)
        {
            throw new NotImplementedException();
        }

        public override ServerPrx CreateGame(Game newGame, Ice.Current current__)
        {
            throw new NotImplementedException();
        }
    }
}
