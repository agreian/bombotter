using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberLoutreIce;

namespace BomberLoutre.Server
{
    public class ServerI : BomberLoutreIce.ServerDisp_
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

        public override Game JoinGame(Game aGame, User aUser, Ice.Current current__)
        {
            int index = -1;
            for (int i = 0; i < _gameList.Count; ++i)
            {
                if (aGame.name == _gameList[i].name)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new WTFIsThatGameException();
            }
            aGame = _gameList[index];

            Ice.Communicator ic = null;
            try
            {
                ic = Ice.Util.initialize();
                //tcp -p 10001 -h 192.168.0.2
                Ice.ObjectPrx obj = ic.stringToProxy("ClientI:default -p 10001");
                Program.CurrentClientPrx = ClientPrxHelper.checkedCast(obj);
                if (Program.CurrentClientPrx == null)
                    throw new ApplicationException("Invalid proxy");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return aGame;
            }

            for (int i = 0; i < aGame.users.Length; ++i)
            {
                if (aGame.users[i] != null)
                {
                    aGame.users[i] = aUser.info;

                    return aGame;
                }
            }

            throw new GameIsFullException();
        }

        public override Game CreateGame(Game newGame, User aUser, Ice.Current current__)
        {
            throw new NotImplementedException();
        }
    }
}
