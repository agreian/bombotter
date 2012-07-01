using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberLoutre.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Ice.Communicator ic = null;
            try
            {
                ic = Ice.Util.initialize(ref args);
                Ice.ObjectAdapter adapter = ic.createObjectAdapterWithEndpoints("ServerAdapter", "default -p 10000");
                Ice.Object obj = new ClientI();
                adapter.add(obj, ic.stringToIdentity("ClientI"));
                adapter.activate();
                ic.waitForShutdown();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
}
