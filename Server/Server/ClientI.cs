using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberLoutre.Server
{
    public class ClientI : BomberLoutreIce.ClientDisp_
    {
        public override BomberLoutreIce.User Connect(string login, string password, Ice.Current current__)
        {
            Console.WriteLine(string.Format("Tentative de connexion de {0}", login));

            return DatabaseInterface.SelectUser(login, password);
        }

        public override BomberLoutreIce.User CreateUser(string login, string password, Ice.Current current__)
        {
            Console.WriteLine(string.Format("Tentative d'inscription de {0}", login));

            return DatabaseInterface.InsertUser(login, password);
        }
    }
}
