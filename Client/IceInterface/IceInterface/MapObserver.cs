using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomberLoutre.IceInterface
{
    public class MapObserver : BomberLoutreInterface.MapObserverDisp_
    {
        public class BoxesPosition
        {
            public int X
            {
                get;
                private set;
            }
            public int Y
            {
                get;
                private set;
            }

            public BoxesPosition(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        public class Player
        {
            public string Name
            {
                get;
                private set;
            }
            public int X
            {
                get;
                private set;
            }
            public int Y
            {
                get;
                private set;
            }

            public Player(string name, int x, int y)
            {
                this.Name = name;
                this.X = x;
                this.Y = y;
            }
        }

        public class Bonus
        {
            public int X
            {
                get;
                private set;
            }
            public int Y
            {
                get;
                private set;
            }

            public int Bomb
            {
                get;
                private set;
            }
            public bool Kick
            {
                get;
                private set;
            }
            public int Power
            {
                get;
                private set;
            }
            public int Speed
            {
                get;
                private set;
            }

            public Bonus(int x, int y, int bomb, bool kick, int power, int speed)
            {
                this.X = x;
                this.Y = y;

                this.Bomb = bomb;
                this.Kick = kick;
                this.Power = power;
                this.Speed = speed;
            }
        }

        public delegate void RefreshBoxesHandler(object sender, BoxesPosition[] positions);
        public event RefreshBoxesHandler RefreshBoxes;

        public delegate void RefreshPlayersHandler(object sender, Player[] positions);
        public event RefreshPlayersHandler RefreshPlayers;

        public delegate void NewBombHandler(object sender, int x, int y);
        public event NewBombHandler NewBomb;

        public delegate void BombExplodedHandler(object sender, int x, int y);
        public event BombExplodedHandler BombExploded;

        public delegate void BombKickedHandler(object sender, int x, int y, int xDest, int yDest);
        public event BombKickedHandler BombKicked;

        public delegate void PlayerDiedHandler(object sender, Player player);
        public event PlayerDiedHandler PlayerDied;

        public delegate void BonusesDroppedHandler(object sender, Bonus[] bonuses);
        public event BonusesDroppedHandler BonusesDropped;

        public delegate void BonusDisappearedHandler(object sender, Bonus bonus);
        public event BonusDisappearedHandler BonusDisappeared;

        public override void refreshMapItems(BomberLoutreInterface.MapItem[] mi, Ice.Current current__)
        {
            List<BoxesPosition> positions = new List<BoxesPosition>();

            for(int i=0;i<mi.Length;++i)
            {
                if (mi[i].destructible)
                {
                    positions.Add(new BoxesPosition(mi[i].i, mi[i].j));
                }
            }

            if (RefreshBoxes != null)
            {
                RefreshBoxes(this, positions.ToArray());
            }
        }

        public override void refreshPlayers(BomberLoutreInterface.Player[] p, Ice.Current current__)
        {
            List<Player> players = new List<Player>();

            for (int i = 0; i < p.Length; ++i)
            {
                players.Add(new Player(p[i].related.gameTag, p[i].posX, p[i].posY));
            }

            if (RefreshPlayers != null)
            {
                RefreshPlayers(this, players.ToArray());
            }
        }

        public override void bombHasBeenPlanted(BomberLoutreInterface.Bomb b, Ice.Current current__)
        {
            if (NewBomb != null)
            {
                NewBomb(this, b.i, b.j);
            }
        }

        public override void bombExploded(BomberLoutreInterface.Bomb b, Ice.Current current__)
        {
            if (BombExploded != null)
            {
                BombExploded(this, b.i, b.j);
            }
        }

        public override void bombKicked(BomberLoutreInterface.Bomb b, BomberLoutreInterface.Point dest, Ice.Current current__)
        {
            if (BombKicked != null)
            {
                BombKicked(this, b.i, b.j, dest.x, dest.y);
            }
        }

        public override void bonusesDropped(BomberLoutreInterface.Bonus[] b, Ice.Current current__)
        {
            List<Bonus> bonuses = new List<Bonus>();

            for (int i = 0; i < b.Length; ++i)
            {
                bonuses.Add(new Bonus(b[i].i, b[i].j, b[i].bomb, b[i].kick, b[i].power, b[i].speed));
            }

            if (BonusesDropped != null)
            {
                BonusesDropped(this, bonuses.ToArray());
            }
        }

        public override void bonusDisappeared(BomberLoutreInterface.Bonus b, Ice.Current current__)
        {
            if (BonusDisappeared != null)
            {
                BonusDisappeared(this, new Bonus(b.i, b.j, b.bomb, b.kick, b.power, b.speed));
            }
        }

        public override void playerDied(BomberLoutreInterface.Player p, Ice.Current current__)
        {
            if (PlayerDied != null)
            {
                PlayerDied(this, new Player(p.related.gameTag, p.posX, p.posY));
            }
        }
    }
}
