using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using BomberLoutre.Controls;

namespace BomberLoutre.Screens
{
    public class OptionScreen : BaseGameState
    {
        #region Field region
        Texture2D backgroundImage;
        string[] menuString;
        int indexMenu;
        Vector2 menuPosition;
        #endregion

        #region Constructor region
        public OptionScreen(BomberLoutre game, GameStateManager manager)
            : base(game, manager)
        {
            menuString = new string[] { Config.ControlOptionString, Config.ResolutionOptionString, Config.FullScreenOptionString, Config.BackOptionString };
            indexMenu = 0;
        }
        #endregion

        #region XNA Method region

        public override void Initialize()
        {
            menuPosition = new Vector2(50, 70);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Graphics
            ContentManager Content = GameRef.Content;
            backgroundImage = Content.Load<Texture2D>("Graphics/Screens/Option");

            // Music
            MediaPlayer.IsRepeating = true;
            //MediaPlayer.Play(GameRef.Content.Load<Song>("Audio/Musics/Title"));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);

            if (InputHandler.KeyDown(Keys.Escape))
                StateManager.PushState(GameRef.TitleScreen);

            if (InputHandler.KeyPressed(Keys.Enter))
            {
                switch (menuString[indexMenu])
                {
                    case Config.ControlOptionString:
                        StateManager.ChangeState(GameRef.ControlEditScreen);
                        break;
                    case Config.ResolutionOptionString:
                        Config.IndexResolution = (Config.IndexResolution < Config.Resolutions.GetLength(0) - 1) ? ++Config.IndexResolution : 0;
                        GameRef.graphics.PreferredBackBufferWidth = Config.Resolutions[Config.IndexResolution, 0];
                        GameRef.graphics.PreferredBackBufferHeight = Config.Resolutions[Config.IndexResolution, 1];
                        break;
                    case Config.FullScreenOptionString:
                        Config.FullScreen = !(Config.FullScreen);
                        GameRef.graphics.IsFullScreen = Config.FullScreen;
                        break;
                    case Config.SoundOptionString:
                        Config.StateVolume = !(Config.StateVolume);
                        break;
                    case Config.BackOptionString:
                        StateManager.PushState(GameRef.TitleScreen);
                        GameRef.graphics.ApplyChanges();
                        break;
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

            GameRef.spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, 800, 600), Color.White);
            ControlManager.Draw(GameRef.spriteBatch);

            Color textColor;

            for (int i = 0; i < menuString.Length; i++)
            {
                textColor = (i == indexMenu) ? Color.CornflowerBlue : Color.Black;

                GameRef.spriteBatch.DrawString(this.BigFont, menuString[i], new Vector2(menuPosition.X, menuPosition.Y + this.BigFont.MeasureString(menuString[i]).Y * i - this.BigFont.MeasureString(menuString[i]).Y / 2), textColor);

                Vector2 AddedTextPosition = new Vector2(menuPosition.X + this.BigFont.MeasureString(menuString[i]).X, menuPosition.Y + (this.BigFont.MeasureString(menuString[i]).Y) * i - this.BigFont.MeasureString(menuString[i]).Y / 2);

                switch (menuString[i])
                {
                    case Config.ResolutionOptionString:
                        GameRef.spriteBatch.DrawString(this.BigFont, " : " + Config.Resolutions[Config.IndexResolution, 0] + "x" + Config.Resolutions[Config.IndexResolution, 1], AddedTextPosition, textColor);
                        break;
                    case Config.FullScreenOptionString:
                        string value = (Config.FullScreen) ? "Activé" : "Désactivé";
                        GameRef.spriteBatch.DrawString(this.BigFont, " : " + value, AddedTextPosition, textColor);
                        break;
                    case Config.SoundOptionString:
                        break;
                }
            }            

            GameRef.spriteBatch.End();
        }

        #endregion

        #region Option Screen Methods

        #endregion
    }
}
