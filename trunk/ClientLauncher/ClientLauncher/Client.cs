using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberLoutreIce;

namespace BomberLoutre.Client.Launcher
{
    internal static class Client
    {
        internal static ClientPrx CurrentClientPrx
        {
            get;
            private set;
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
                Ice.ObjectPrx obj = ic.stringToProxy("ClientI:default -p 10000");
                CurrentClientPrx = ClientPrxHelper.checkedCast(obj);
                if (CurrentClientPrx == null)
                    throw new ApplicationException("Invalid proxy");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
}
