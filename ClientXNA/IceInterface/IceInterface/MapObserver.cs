using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberLoutre.IceInterface
{
    public class MapObserver : BomberLoutreInterface.MapObserverDisp_
    {


        public override void refreshMapItems(BomberLoutreInterface.MapItem[] mi, Ice.Current current__)
        {
            throw new NotImplementedException();
        }

        public override void refreshPlayers(BomberLoutreInterface.Player[] p, Ice.Current current__)
        {
            throw new NotImplementedException();
        }

        public override void bombHasBeenPlanted(BomberLoutreInterface.Bomb b, Ice.Current current__)
        {
            throw new NotImplementedException();
        }

        public override void bombExploded(BomberLoutreInterface.Bomb b, Ice.Current current__)
        {
            throw new NotImplementedException();
        }

        public override void bombKicked(BomberLoutreInterface.Bomb b, BomberLoutreInterface.Point dest, Ice.Current current__)
        {
            throw new NotImplementedException();
        }

        public override void bonusesDropped(BomberLoutreInterface.Bonus[] b, Ice.Current current__)
        {
            throw new NotImplementedException();
        }

        public override void playerDied(BomberLoutreInterface.Player p, Ice.Current current__)
        {
            throw new NotImplementedException();
        }
    }
}
