using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberLoutre.Library
{
    public class User
    {
        public string GameTag
        {
            get;
            private set;
        }
        public string Login
        {
            get;
            private set;
        }
        public string Password
        {
            get;
            private set;
        }

        public int KillCount
        {
            get;
            private set;
        }
        public int DeathCount
        {
            get;
            private set;
        }
        public int SuicideCount
        {
            get;
            private set;
        }

        public int WinCount
        {
            get;
            private set;
        }
        public int DrawCount
        {
            get;
            private set;
        }
        public int GameCount
        {
            get;
            private set;
        }

        public Server MyServeur
        {
            get;
            private set;
        }

        public static User Connect(string login, string password)
        {
            //...

            /*
             * throw BadLoginException
             * throw BadPasswordException
             * 
             * Capt'n Obvious!
             */

            throw new NotImplementedException();
        }
        public static User CreateUser(string login, string password)
        {
            //...

            /*
             * throw UserAlreadyExistException
             */

            throw new NotImplementedException();
        }

        public User(string gametag, string login, string password)
        {
            this.GameTag = gametag;
            this.Login = login;
            this.Password = password;

            this.KillCount = 0;
            this.DeathCount = 0;
            this.SuicideCount = 0;

            this.WinCount = 0;
            this.DrawCount = 0;
            this.GameCount = 0;
        }

        public void AddDraw(int nbKill, int nbDeath, int nbSuicide)
        {
            this.KillCount += nbKill;
            this.DeathCount += nbDeath;
            this.SuicideCount += nbSuicide;

            this.DrawCount++;
            this.GameCount++;
        }
        public void AddLoose(int nbKill, int nbDeath, int nbSuicide)
        {
            this.KillCount += nbKill;
            this.DeathCount += nbDeath;
            this.SuicideCount += nbSuicide;

            this.GameCount++;
        }
        public void AddWin(int nbKill, int nbDeath, int nbSuicide)
        {
            this.KillCount += nbKill;
            this.DeathCount += nbDeath;
            this.SuicideCount += nbSuicide;

            this.WinCount++;
            this.GameCount++;
        }

        public bool DeleteUser()
        {
            //...

            throw new NotImplementedException();
        }
        public void JoinGame(Game game)
        {
            //...
        }
        private void Save()
        {
            //...
        }
    }
}
