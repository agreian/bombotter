﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ScreenManager
{
    public class TitleScreen : BaseGameState
    {
        #region Field region
        Texture2D backgroundImage;
        string[] menuString;
        int indexMenu;
        Vector2 menuPosition;
        bool enableMenu;
        #endregion

        #region Constructor region
        public TitleScreen(BomberLoutre game, GameStateManager manager)
            : base(game, manager)
        {
            menuString = new string[] { "Jouer", "Options", "Crédits", "Quitter" };
            indexMenu = 0;
            enableMenu = false;
        }
        #endregion

        #region XNA Method region

        public override void Initialize()
        {
            menuPosition = new Vector2(150, 33);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Graphics
            ContentManager Content = GameRef.Content;
            backgroundImage = Content.Load<Texture2D>("Graphics/Titles/Title2");

            // Music
            MediaPlayer.IsRepeating = true;
            //MediaPlayer.Play(GameRef.Content.Load<Song>("Audio/Musics/Title"));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);

            if (InputHandler.KeyPressed(Keys.Enter))
            {
                if (!enableMenu)
                    enableMenu = true;
                else
                {
                    switch (menuString[indexMenu])
                    {
                        case "Jouer":
                            //StateManager.ChangeState(GameRef.GameModeMenuScreen);
                            break;
                        case "Options":
                            //StateManager.ChangeState(GameRef.OptionMenuScreen);
                            break;
                        case "Crédits":
                            //StateManager.ChangeState(GameRef.CreditMenuScreen);
                            break;
                        case "Quitter":
                            GameRef.Exit();
                            break;
                    }
                }
            }

            if (InputHandler.KeyPressed(Keys.Up))
            {
                if (indexMenu <= 0)
                    indexMenu = menuString.Length - 1;
                else
                    indexMenu--;
            }
            else if (InputHandler.KeyPressed(Keys.Down))
            {
                indexMenu = (indexMenu + 1) % menuString.Length;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();

            base.Draw(gameTime);

            GameRef.spriteBatch.Draw(backgroundImage, new Rectangle(0,0,800,600), Color.White);

            ControlManager.Draw(GameRef.spriteBatch);

            if (enableMenu)
            {
                for (int i = 0; i < menuString.Length; i++)
                {
                    Color textColor = Color.Black;
                    if (i == indexMenu)
                        textColor = Color.Pink;
                    GameRef.spriteBatch.DrawString(this.BigFont, menuString[i],
                        new Vector2(menuPosition.X - this.BigFont.MeasureString(menuString[i]).X / 2,
                            menuPosition.Y + this.BigFont.MeasureString(menuString[i]).Y * i - this.BigFont.MeasureString(menuString[i]).Y / 2), textColor);
                }
            }

            GameRef.spriteBatch.End();
        }

        #endregion

        #region Title Screen Methods

        #endregion
    }
}