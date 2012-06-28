using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using BomberLoutre.Controls;
using BomberLoutre.Components;
using BomberLoutre.World;
using System;

namespace BomberLoutre.Screens
{
    public class GameScreen : BaseGameState
    {
        #region Field region
        SoundEffect bombExplosionSound;
        SoundEffect itemLootSound;
        SoundEffect playerDeathSound;

        List<Player>    playerList;
        List<Bomb>      bombList;
        List<Bonus>     bonusList;
        List<Box>       boxList;
        List<Rock>      rockList;
        List<Flame>     flameList;

        string mapName;
        Map MapZone;
        #endregion

        #region Constructor region
        public GameScreen(BomberLoutre game, GameStateManager manager) : base(game, manager)
        {
            playerList = new List<Player>();
            bombList = new List<Bomb>();
            bonusList = new List<Bonus>();
            boxList = new List<Box>();
            rockList = new List<Rock>();
            flameList = new List<Flame>();
        }
        #endregion

        #region XNA Method region

        public override void Initialize()
        {
            mapName = "map1.map";
            MapZone = new Map(GameRef, mapName, this);
            GameRef.Components.Add(MapZone);    // Ajoute la Map aux "Components", = instance qui vont appeler successivement Ctor()/Initialize()/LoadContent()

            for (int i = 0; i < Config.PlayerNumber; ++i)
                playerList.Add(new Player(i, GameRef, new Vector2(Config.MapLayer.X, Config.MapLayer.Y), 0, this));


            /* Ajout de Bonus au pif
            Random randomizer = new Random();
            string[] types = new string[] { "powerUp", "powerUpGold", "canKick", "speedUp" };
            for (int i = 0; i < 15; ++i)
                bonusList.Add(new Bonus(GameRef, new Vector2((float) randomizer.Next(13)+1, (float) randomizer.Next(11)+1), types[randomizer.Next(4)]));
            -------------------- */
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            MediaPlayer.Volume = 0.40f;

            bombExplosionSound = GameRef.Content.Load<SoundEffect>("Audio/Sounds/bombExplosion");
            itemLootSound = GameRef.Content.Load<SoundEffect>("Audio/Sounds/itemLoot");
            playerDeathSound = GameRef.Content.Load<SoundEffect>("Audio/Sounds/playerDeath");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            int i;

            if(CheckIfReplayMusic())
                MediaPlayer.Play(GameRef.Content.Load<Song>("Audio/Musics/Battle"));

            ControlManager.Update(gameTime, PlayerIndex.One);            

            if (InputHandler.KeyDown(Keys.Escape))
            {
                StateManager.PushState(GameRef.TitleScreen);
                MediaPlayer.Stop();
            }

            for(i = 0; i < Config.PlayerNumber; ++i)    playerList[i].Update(gameTime);
            for (i = 0; i < bombList.Count; ++i)        bombList[i].Update(gameTime);
            for (i = 0; i < flameList.Count; ++i)       flameList[i].Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            int i;
            MapZone.Draw(gameTime);

            GameRef.spriteBatch.Begin();

            for (i = 0; i < boxList.Count; ++i)         boxList[i].Draw(gameTime);
            for (i = 0; i < bombList.Count; ++i)        bombList[i].Draw(gameTime);
            for (i = 0; i < bonusList.Count; ++i)       bonusList[i].Draw(gameTime);
            for (i = 0; i < rockList.Count; ++i)        rockList[i].Draw(gameTime);
            for (i = 0; i < Config.PlayerNumber; ++i)   playerList[i].Draw(gameTime);
            for (i = 0; i < flameList.Count; ++i)       flameList[i].Draw(gameTime);

            GameRef.spriteBatch.DrawString(this.MidFont, String.Format("({0} : {1})", playerList[0].Sprite.SpritePosition.X - Config.MapLayer.X, playerList[0].Sprite.SpritePosition.Y - Config.MapLayer.Y), new Vector2(30, 30), Color.Red);
            GameRef.spriteBatch.DrawString(this.MidFont, String.Format("({0} : {1})", playerList[0].Sprite.CellPosition.X, playerList[0].Sprite.CellPosition.Y), new Vector2(30, 60), Color.Red);
 
            GameRef.spriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion

        #region Game Screen Methods
        public void AddBomb(Bomb bomb)
        {
            bombList.Add(bomb);
            Map.SetBomb((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y);
        }

        public void AddBox(Box box)
        {
            boxList.Add(box);
        }

        public void AddRock(Rock rock)
        {
            rockList.Add(rock);
        }

        public void BOOM(Bomb bomb)
        {
            Map.RemoveBomb((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y);

            bool right = false, top = false, left = false, bottom = false;

            flameList.Add(new Flame((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y, this, GameRef));

            for (int i = 1; i <= bomb.BombPower; ++i)
            {
                #region explosion to right
                if (!Map.IsObstacle((int)bomb.CellPosition.X + i, (int)bomb.CellPosition.Y) && !right)
                {
                    if (((int)bomb.CellPosition.X + i) <= Config.MapSize.X)
                        flameList.Add(new Flame((int)bomb.CellPosition.X + i, (int)bomb.CellPosition.Y, this, GameRef));

                    if (Map.IsBomb((int)bomb.CellPosition.X + i, (int)bomb.CellPosition.Y) && !right)
                    {
                        for (int j = 0; j < bombList.Count; ++j)
                        {
                            if ((bombList[j].CellPosition.X == (bomb.CellPosition.X + i) && bombList[j].CellPosition.Y == bomb.CellPosition.Y))
                            {
                                BOOM(bombList[j]);
                                right = true;
                            }
                        }
                    }
                }

                else
                {
                    // Utiliser une hashmap ou similaire plus tard pour éviter le parcours de la liste
                    if (Map.IsBox((int)bomb.CellPosition.X + i, (int)bomb.CellPosition.Y) && !right)
                    {
                        for (int j = 0; j < boxList.Count; ++j)
                        {
                            if ((boxList[j].cellPosition.X == (bomb.CellPosition.X + i) && boxList[j].cellPosition.Y == bomb.CellPosition.Y))
                            {
                                boxList.RemoveAt(j);
                                Map.RemoveBox((int)bomb.CellPosition.X + i, (int)bomb.CellPosition.Y);
                            }
                        }
                    }

                    right = true;
                }

                #endregion

                #region explosion to bottom
                if (!Map.IsObstacle((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y + i) && !bottom)
                {
                    if (((int)bomb.CellPosition.Y + i) <= Config.MapSize.Y)
                    flameList.Add(new Flame((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y + i, this, GameRef));

                    if (Map.IsBomb((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y + i) && !bottom)
                    {
                        for (int j = 0; j < bombList.Count; ++j)
                        {
                            if ((bombList[j].CellPosition.X == (bomb.CellPosition.X) && bombList[j].CellPosition.Y == bomb.CellPosition.Y + i))
                            {
                                BOOM(bombList[j]);
                                bottom = true;
                            }
                        }
                    }
                }

                else
                {
                    // Utiliser une hashmap ou similaire plus tard pour éviter le parcours de la liste
                    if (Map.IsBox((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y + i) && !bottom)
                    {
                        for (int j = 0; j < boxList.Count; ++j)
                        {
                            if ((boxList[j].cellPosition.X == (bomb.CellPosition.X) && boxList[j].cellPosition.Y == bomb.CellPosition.Y + i))
                            {
                                boxList.RemoveAt(j);
                                Map.RemoveBox((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y + i);
                            }
                        }
                    }

                    bottom = true;
                }
                #endregion

                #region explosion to left
                if (!Map.IsObstacle((int)bomb.CellPosition.X - i, (int)bomb.CellPosition.Y) && !left)
                {
                    if (((int)bomb.CellPosition.X - i) > 0)
                    flameList.Add(new Flame((int)bomb.CellPosition.X - i, (int)bomb.CellPosition.Y, this, GameRef));

                    if (Map.IsBomb((int)bomb.CellPosition.X - i, (int)bomb.CellPosition.Y) && !left)
                    {
                        for (int j = 0; j < bombList.Count; ++j)
                        {
                            if ((bombList[j].CellPosition.X == (bomb.CellPosition.X - i) && bombList[j].CellPosition.Y == bomb.CellPosition.Y))
                            {
                                BOOM(bombList[j]);
                                left = true;
                            }
                        }
                    }
                }

                else
                {
                    // Utiliser une hashmap ou similaire plus tard pour éviter le parcours de la liste
                    if (Map.IsBox((int)bomb.CellPosition.X - i, (int)bomb.CellPosition.Y) && !left)
                    {
                        for (int j = 0; j < boxList.Count; ++j)
                        {
                            if ((boxList[j].cellPosition.X == (bomb.CellPosition.X - i) && boxList[j].cellPosition.Y == bomb.CellPosition.Y))
                            {
                                boxList.RemoveAt(j);
                                Map.RemoveBox((int)bomb.CellPosition.X - i, (int)bomb.CellPosition.Y);
                            }
                        }
                    }

                    left = true;
                }
                #endregion

                #region explosion to top

                if (!Map.IsObstacle((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y - i) && !top)
                {
                    if(((int)bomb.CellPosition.Y - i) > 0)
                    flameList.Add(new Flame((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y - i, this, GameRef));

                    if (Map.IsBomb((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y - i) && !top)
                    {
                        for (int j = 0; j < bombList.Count; ++j)
                        {
                            if ((bombList[j].CellPosition.X == (bomb.CellPosition.X) && bombList[j].CellPosition.Y == bomb.CellPosition.Y - i))
                            {
                                BOOM(bombList[j]);
                                top = true;
                            }
                        }
                    }
                }

                else
                {
                    // Utiliser une hashmap ou similaire plus tard pour éviter le parcours de la liste
                    if (Map.IsBox((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y - i) && !top)
                    {
                        for (int j = 0; j < boxList.Count; ++j)
                        {
                            if ((boxList[j].cellPosition.X == (bomb.CellPosition.X) && boxList[j].cellPosition.Y == bomb.CellPosition.Y - i))
                            {
                                boxList.RemoveAt(j);
                                Map.RemoveBox((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y - i);
                            }
                        }
                    }

                    top = true;
                }
                #endregion
            }


            bombList.Remove(bomb);
            bombExplosionSound.Play();
        }

        public void RemoveFlame(Flame flame)
        {
            flameList.Remove(flame);
        }
        #endregion
    }
}
