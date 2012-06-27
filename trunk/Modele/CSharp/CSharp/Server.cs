using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberLoutre.Library
{
    public class Server
    {
        private List<User> _users = new List<User>();
        private List<Game> _games = new List<Game>();

        public Game AddGame(string name)
        {
            Game current = new Game(name);

            _games.Add(current);

            return current;
        }
        public void RemoveGame(Game current)
        {
            _games.Remove(current);
        }
        public Game[] GetListGame()
        {
            return _games.ToArray();
        }

        public void SendInvitationToPlayer(Player player, Game game)
        {
            throw new NotImplementedException();
        }
        public User[] GetListUser()
        {
            return _users.ToArray();
        }
    }
}
