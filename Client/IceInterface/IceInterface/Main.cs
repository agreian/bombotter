using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberLoutreInterface;

namespace BomberLoutre.IceInterface
{
    public static class Main
    {
        private static ServerInterfacePrx ServerInterfacePrx
        {
            get;
            set;
        }
        public static UserData CurrentUser
        {
            get;
            private set;
        }
        public static Player CurrentPlayer
        {
            get;
            private set;
        }
        public static GameData CurrentGameData
        {
            get;
            internal set;
        }
        public static Map CurrentMap
        {
            get;
            private set;
        }
        public static GameWaitRoomPrx CurrentGameWaitRoomPrx
        {
            get;
            private set;
        }
        public static GameWaitRoom CurrentGameWaitRoom
        {
            get;
            private set;
        }
        public static MapObserverPrx CurrentMapObserverPrx
        {
            get;
            private set;
        }
        public static MapObserver CurrentMapObserver
        {
            get;
            private set;
        }
        private static GameInterfacePrx GameInterfacePrx
        {
            get;
            set;
        }

        public static GameData[] GamesList
        {
            get
            {
                return BomberLoutre.IceInterface.Main.ServerInterfacePrx.getGameList();
            }
        }
        public static string[] MapsNames
        {
            get
            {
                return GameInterfacePrx.getMapList();
            }
        }
        public static string CreatorName
        {
            get
            {
                return CurrentGameData.gameui.getCreatorName();
            }
        }

        static Main()
        {
            Ice.Communicator ic = null;
            try
            {
                ic = Ice.Util.initialize();
                Ice.ObjectPrx obj = ic.stringToProxy("BomberServer: tcp -p 10001 -h 192.168.0.2");

                ServerInterfacePrx = ServerInterfacePrxHelper.checkedCast(obj);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }

            try
            {
                CurrentMapObserver = new MapObserver();
                CurrentGameWaitRoom = new GameWaitRoom();

                Ice.ObjectAdapter adapter = ic.createObjectAdapterWithEndpoints("BomberClient", "tcp");
                Ice.Object obj = CurrentMapObserver;
                Ice.ObjectPrx objPrx = adapter.add(obj, ic.stringToIdentity("BomberClient"));
                MapObserverPrx mapObserverPrx = MapObserverPrxHelper.checkedCast(objPrx);
                obj = CurrentGameWaitRoom;
                objPrx = adapter.add(obj, ic.stringToIdentity("BomberClient"));
                GameWaitRoomPrx gameWaitRoomPrx = GameWaitRoomPrxHelper.checkedCast(objPrx);

                adapter.activate();
                ic.waitForShutdown();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
            if (ic != null)
            {
                try
                {
                    ic.destroy();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                }
            }
        }

        public static UserData Connection(string login, string password)
        {
            CurrentUser = BomberLoutre.IceInterface.Main.ServerInterfacePrx.connect(login, password);

            return CurrentUser;
        }

        public static Map JoinGame(GameData selectedGame)
        {
            CurrentGameData = selectedGame;
            CurrentMap = BomberLoutre.IceInterface.Main.ServerInterfacePrx.joinGame(selectedGame.name, CurrentUser, CurrentGameWaitRoomPrx, CurrentMapObserverPrx);
            for (int i = 0; i < CurrentMap.players.Length; ++i)
            {
                if (CurrentMap.players[i].related.gameTag == CurrentUser.gameTag)
                {
                    CurrentPlayer = CurrentMap.players[i];
                }
            }

            return CurrentMap;
        }

        public static void CreateGame(string name)
        {
            GameInterfacePrx = BomberLoutre.IceInterface.Main.ServerInterfacePrx.addGame(name, CurrentUser, CurrentGameWaitRoomPrx, CurrentMapObserverPrx);
        }

        public static void LaunchGame()
        {
            for (int i = CurrentGameData.playerCount; i < 4; ++i)
            {
                GameInterfacePrx.addBot();
            }

            GameInterfacePrx.startMap();
        }

        public static void ImReady()
        {
            CurrentGameData.gameui.userReady(CurrentUser);
        }

        public static void MoveUp()
        {
            CurrentMap.mi.moveUp(CurrentPlayer);
        }

        public static void MoveDown()
        {
            CurrentMap.mi.moveDown(CurrentPlayer);
        }

        public static void MoveLeft()
        {
            CurrentMap.mi.moveLeft(CurrentPlayer);
        }

        public static void MoveRight()
        {
            CurrentMap.mi.moveRight(CurrentPlayer);
        }

        public static void DropBomb(int i, int j)
        {
            CurrentMap.mi.dropBomb(CurrentPlayer, new Bomb { i = i, j = j });
        }
    }
}
