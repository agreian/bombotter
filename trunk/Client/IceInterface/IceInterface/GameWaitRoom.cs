using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberLoutre.IceInterface
{
    public class GameWaitRoom : BomberLoutreInterface.GameWaitRoomDisp_
    {
        public delegate void NewUserHandler(object sender, string username);
        public event NewUserHandler NewUser;

        public delegate void UserLeftHandler(object sender, string username);
        public event UserLeftHandler UserLeft;

        public delegate void UserReadyHandler(object sender, string username);
        public event UserReadyHandler UserReady;

        public delegate void GameStartingHandler(object sender);
        public event GameStartingHandler GameStarting;

        public delegate void GameEndingHandler(object sender);
        public event GameEndingHandler GameEnding;

        public delegate void GameDataUpdatedHandler(object sender, BomberLoutreInterface.GameData gameData);
        public event GameDataUpdatedHandler GameDataUpdated;

        public delegate void MapReceivedHandler(object sender, string map);
        public event MapReceivedHandler MapReceived;

        public override void newUserInRoom(string username, Ice.Current current__)
        {
            if (NewUser != null)
                NewUser(this, username);
        }

        public override void userLeftRoom(string username, Ice.Current current__)
        {
            if (UserLeft != null)
                UserLeft(this, username);
        }

        public override void allUsersReady(Ice.Current current__)
        {
            throw new NotImplementedException();
        }

        public override void userReady(string username, Ice.Current current__)
        {
            if (UserReady != null)
            {
                UserReady(this, username);
            }
        }

        public override void gameDataUpdated(BomberLoutreInterface.GameData g, Ice.Current current__)
        {
            BomberLoutre.IceInterface.Main.CurrentGameData = g;

            if (GameDataUpdated != null)
            {
                GameDataUpdated(this, g);
            }
        }

        public override void gameStart(Ice.Current current__)
        {
            if (GameStarting != null)
            {
                GameStarting(this);
            }
        }

        public override void gameEnd(Ice.Current current__)
        {
            if (GameEnding != null)
            {
                GameEnding(this);
            }
        }

        public override void newMapDefined(string map, Ice.Current current__)
        {
            if (MapReceived != null)
            {
                MapReceived(this, map);
            }
        }
    }
}
