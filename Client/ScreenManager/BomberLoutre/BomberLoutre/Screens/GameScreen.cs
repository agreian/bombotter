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

        List<Player> playerList;
        List<Bomb> bombList;
        List<Bonus> bonusList;

        Map MapZone;
        #endregion

        #region Constructor region
        public GameScreen(BomberLoutre game, GameStateManager manager) : base(game, manager)
        {
            MapZone = new Map(GameRef); 
            GameRef.Components.Add(MapZone);    // Ajoute la Map aux "Components", = instance qui vont appeler successivement Ctor()/Initialize()/LoadContent()
        }
        #endregion

        #region XNA Method region

        public override void Initialize()
        {
            playerList = new List<Player>();
            bombList = new List<Bomb>();
            bonusList = new List<Bonus>();

            for (int i = 0; i < Config.PlayerNumber; ++i)
            {
                // Pour le moment, la loutre est placée au coin gauche supérieure, donc (X + largeur/2) et (Y + hauteur/2)
                playerList.Add(new Player(i, GameRef, new Vector2(Config.MapLayer.X + (Config.OtterWidth/2), Config.MapLayer.Y + (Config.OtterHeight/2)), 0, this));
            }

            string[] types = new string[] { "powerUp", "powerUpGold", "canKick", "speedUp" };
            Random randomizer = new Random();


            for (int i = 0; i < 15; ++i)
            {               
                bonusList.Add(new Bonus(GameRef, new Vector2((float) randomizer.Next(13)+1, (float) randomizer.Next(11)+1), types[randomizer.Next(4)]));
            }
                        
            base.Initialize();
        }

        protected override void LoadContent()
        {
            if(Properties.App.Default.MusicState)
                MediaPlayer.Play(GameRef.Content.Load<Song>("Audio/Musics/Battle"));
            MediaPlayer.Volume = 0.40f;

            bombExplosionSound = GameRef.Content.Load<SoundEffect>("Audio/Sounds/bombExplosion");
            itemLootSound = GameRef.Content.Load<SoundEffect>("Audio/Sounds/itemLoot");
            playerDeathSound = GameRef.Content.Load<SoundEffect>("Audio/Sounds/playerDeath");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            int i;

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

            for (i = 0; i < bombList.Count; ++i)        bombList[i].Draw(gameTime);
            for (i = 0; i < bonusList.Count; ++i)       bonusList[i].Draw(gameTime);
            for(i = 0; i < Config.PlayerNumber; ++i)    playerList[i].Draw(gameTime);

            GameRef.spriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion

        #region Game Screen Methods
        public void AddBomb(Bomb bomb)
        {
            bombList.Add(bomb);
        }
        #endregion
    }
}
