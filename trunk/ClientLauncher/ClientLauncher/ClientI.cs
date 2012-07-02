using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberLoutre.Client.Launcher
{
    class ClientI : BomberLoutreIce.ClientDisp_
    {
        public override void Test(Ice.Current current__)
        {
            Console.WriteLine("toto");
        }
    }
}
