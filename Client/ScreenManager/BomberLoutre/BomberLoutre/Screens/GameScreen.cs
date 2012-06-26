﻿using Microsoft.Xna.Framework;
using BomberLoutre.Controls;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

using BomberLoutre.Sprite;
using Microsoft.Xna.Framework.Media;

namespace BomberLoutre.Screens
{
    public class GameScreen : BaseGameState
    {
        #region Field region
        
        private bool pause;

        SoundEffect bombExplosionSound;
        SoundEffect itemPickUpSound;
        SoundEffect playerDeathSound;

        Player Player; // TODO : Créer une classe Player
        
        #endregion

        #region Constructor region
        public GameScreen(BomberLoutre game, GameStateManager manager) : base(game, manager)
        {
        }
        #endregion

        #region XNA Method region

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;

            if(Properties.App.Default.MusicState)
                MediaPlayer.Play(GameRef.Content.Load<Song>("Audio/Musics/Battle"));
            MediaPlayer.Volume = 0.40f;

            bombExplosionSound = GameRef.Content.Load<SoundEffect>("Audio/Sounds/bombExplosion");


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);

            if (InputHandler.KeyDown(Keys.Escape))
            {
                StateManager.PushState(GameRef.TitleScreen);
                MediaPlayer.Stop();
            }

            if (InputHandler.KeyDown(Properties.App.Default.KeyUp))
            {
                bombExplosionSound.Play();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();
            GameRef.spriteBatch.End();
            base.Draw(gameTime);

        }

        #endregion

        #region Game Screen Methods

        #endregion
    }
}