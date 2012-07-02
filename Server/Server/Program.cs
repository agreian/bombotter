using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberLoutreIce;
using System.Timers;

namespace BomberLoutre.Server
{
    class Program
    {
        internal static ClientPrx CurrentClientPrx
        {
            get;
            set;
        }

        static void Main(string[] args)
        {
            Ice.Communicator ic = null;
            try
            {
                ic = Ice.Util.initialize(ref args);
                Ice.ObjectAdapter adapter = ic.createObjectAdapterWithEndpoints("ServerAdapter", "default -p 10000");
                Ice.Object obj = new ServerI();
                adapter.add(obj, ic.stringToIdentity("ServerI"));
                adapter.activate();
                //ic.waitForShutdown();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }

            Timer timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();

            ic.waitForShutdown();
        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (CurrentClientPrx != null)
                CurrentClientPrx.Test();
        }
    }
}
