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

        Random randomizer;
        #endregion

        #region Constructor region
        public GameScreen(BomberLoutreGame game, GameStateManager manager) : base(game, manager)
        {
            playerList = new List<Player>();
            bombList = new List<Bomb>();
            bonusList = new List<Bonus>();
            boxList = new List<Box>();
            rockList = new List<Rock>();
            flameList = new List<Flame>();

            randomizer = new Random();
        }
        #endregion

        #region XNA Method region

        public override void Initialize()
        {
            mapName = "map3.map";
            MapZone = new Map(GameRef, mapName, this);
            GameRef.Components.Add(MapZone);    // Ajoute la Map aux "Components", = instance qui vont appeler successivement Ctor()/Initialize()/LoadContent()

            for (int i = 0; i < Config.PlayerNumber; ++i)
                playerList.Add(new Player(i, GameRef, new Vector2(Config.PlayerPosition[i, 0], Config.PlayerPosition[i, 1]), 0, this));


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

            if (InputHandler.Maintained("Escape", PlayerIndex.One))
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

            MapZone.Draw(gameTime); // Dessin de la map


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

        public void AddRandomBonus(int x, int y)
        {
            Bonus randomBonus;
            string[] types = new string[] { "powerUp", "powerUpGold", "canKick", "speedUp", "bombUp" };
            int randomNumber = randomizer.Next(15);

            if (randomNumber < 5)
            {
                randomBonus = new Bonus(GameRef, new Vector2(x, y), types[(randomNumber)]);
                bonusList.Add(randomBonus);
            }
        }

        public void BOOM(Bomb bomb)
        {
            Map.RemoveBomb((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y);

            bool right = false, top = false, left = false, bottom = false;

            flameList.Add(new Flame((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y, this, GameRef));

            for (int i = 1; i <= bomb.BombPower; ++i)
            {
                right = ComputeExplosion(bomb, i, 0, right);
                bottom = ComputeExplosion(bomb, 0, i, bottom);
                left = ComputeExplosion(bomb, -i, 0, left);
                top = ComputeExplosion(bomb, 0, -i, top);
            }

            bombList.Remove(bomb);
            PlaySoundEffect(bombExplosionSound);
        }

        public void RemoveFlame(Flame flame)
        {
            flameList.Remove(flame);
        }

        public bool ComputeExplosion(Bomb bomb, int xOffset, int yOffset, bool stop)
        {
            bool rotateFlame = (xOffset != 0) ? true : false;
            if (!Map.IsObstacle((int)bomb.CellPosition.X + xOffset, (int)bomb.CellPosition.Y + yOffset) && !stop)
            {
                if ((int)bomb.CellPosition.X + xOffset <= Config.MapSize.X && (int)bomb.CellPosition.X + xOffset > 0)
                {
                    if ((int)bomb.CellPosition.Y + yOffset <= Config.MapSize.Y && (int)bomb.CellPosition.Y + yOffset > 0)
                    {
                        flameList.Add(new Flame((int)bomb.CellPosition.X + xOffset, (int)bomb.CellPosition.Y + yOffset, this, GameRef, rotateFlame));
                    }
                }

                if (Map.IsBomb((int)bomb.CellPosition.X + xOffset, (int)bomb.CellPosition.Y + yOffset) && !stop)
                {
                    for (int j = 0; j < bombList.Count; ++j)
                    {
                        if ((bombList[j].CellPosition.X == (bomb.CellPosition.X + xOffset) && bombList[j].CellPosition.Y == bomb.CellPosition.Y + yOffset))
                        {
                            BOOM(bombList[j]);
                            stop = true;
                        }
                    }
                }
            }

            else
            {
                // Utiliser une hashmap ou similaire plus tard pour éviter le parcours de la liste
                if (Map.IsBox((int)bomb.CellPosition.X + xOffset, (int)bomb.CellPosition.Y + yOffset) && !stop)
                {
                    for (int j = 0; j < boxList.Count; ++j)
                    {
                        if ((boxList[j].cellPosition.X == (bomb.CellPosition.X + xOffset) && boxList[j].cellPosition.Y == bomb.CellPosition.Y + yOffset))
                        {
                            boxList.RemoveAt(j);
                            Map.RemoveBox((int)bomb.CellPosition.X + xOffset, (int)bomb.CellPosition.Y + yOffset);
                            AddRandomBonus((int)bomb.CellPosition.X + xOffset, (int)bomb.CellPosition.Y + yOffset);
                        }
                    }
                }

                stop = true;
            }

            return stop;
        }

        public void PlaySoundEffect(SoundEffect sound)
        {
            if (Properties.App.Default.SoundState)
                sound.Play();
        }
        #endregion
    }
}
