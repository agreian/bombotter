using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberLoutreIce;

namespace BomberLoutre.Client.Launcher
{
    internal static class Client
    {
        internal static ServerPrx CurrentClientPrx
        {
            get;
            set;
        }

        internal static User CurrentUser
        {
            get;
            set;
        }

        static Client()
        {
            Ice.Communicator ic = null;
            try
            {
                ic = Ice.Util.initialize();
                //tcp -p 10001 -h 192.168.0.2
                Ice.ObjectPrx obj = ic.stringToProxy("ServerI:default -p 10000");
                CurrentClientPrx = ServerPrxHelper.checkedCast(obj);
                if (CurrentClientPrx == null)
                    throw new ApplicationException("Invalid proxy");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }

        internal static void ConnectToServer()
        {
            Ice.Communicator ic = null;
            try
            {
                ic = Ice.Util.initialize();
                Ice.ObjectAdapter adapter = ic.createObjectAdapterWithEndpoints("ClientAdapter", "default -p 10001");
                Ice.Object obj = new ClientI();
                adapter.add(obj, ic.stringToIdentity("ClientI"));
                adapter.activate();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
}
