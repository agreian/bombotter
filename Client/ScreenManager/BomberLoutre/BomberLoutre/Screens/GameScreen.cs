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
        }
        #endregion

        #region XNA Method region

        public override void Initialize()
        {
            mapName = "map3.map";
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

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            int i;
            MapZone.Draw(gameTime);

            GameRef.spriteBatch.Begin();

            for (i = 0; i < bombList.Count; ++i) { bombList[i].Draw(gameTime); GameRef.spriteBatch.DrawString(this.BigFont, bombList[i].Timer.ToString(), new Vector2(bombList[i].Sprite.SpritePosition.X, bombList[i].Sprite.SpritePosition.Y+10), Color.Red);  }
            for (i = 0; i < bonusList.Count; ++i)       bonusList[i].Draw(gameTime);
            for (i = 0; i < boxList.Count; ++i)         boxList[i].Draw(gameTime);
            for (i = 0; i < rockList.Count; ++i)        rockList[i].Draw(gameTime);
            for (i = 0; i < Config.PlayerNumber; ++i)   playerList[i].Draw(gameTime);

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
            Map.SetWall((int)bomb.CellPosition.X, (int)bomb.CellPosition.Y);
        }

        public void AddBox(Box box)
        {
            boxList.Add(box);
        }

        public void AddRock(Rock rock)
        {
            rockList.Add(rock);
        }

        public void BOOM(Bomb Bomb)
        {
            Map.RemoveWall((int)Bomb.CellPosition.X, (int)Bomb.CellPosition.Y);
            bombList.Remove(Bomb);
        }
        #endregion
    }
}
