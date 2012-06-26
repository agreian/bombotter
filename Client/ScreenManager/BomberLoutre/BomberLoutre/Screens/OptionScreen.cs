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
            menuString = new string[] { Config.ControlOptionString, 
                                        Config.ResolutionOptionString, 
                                        Config.FullScreenOptionString, 
                                        Config.MusicOptionString,
                                        Config.SoundOptionString, 
                                        Config.BackOptionString };
            indexMenu = 0;
        }
        #endregion

        #region XNA Method region

        public override void Initialize()
        {
            menuPosition = new Vector2(50, 70);

            // Initialisation de l'IndexResolution pour être directement sur la bonne résolution dans le menu Option
            for (int i = 0; i < Config.Resolutions.GetLength(0) - 1; ++i)
            {
                if (!(Config.Resolutions[Config.IndexResolution, 0] == Properties.App.Default.ScreenWidth))
                {
                        Config.IndexResolution++;
                }

                else break;
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            backgroundImage = GameRef.Content.Load<Texture2D>("Graphics/Screens/Menu");

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
                        break;
                    case Config.FullScreenOptionString:
                        Properties.App.Default.ScreenFullScreen = !(Properties.App.Default.ScreenFullScreen);
                        GameRef.graphics.IsFullScreen = Properties.App.Default.ScreenFullScreen;
                        break;
                    case Config.SoundOptionString:
                        Properties.App.Default.SoundState = !(Properties.App.Default.SoundState);
                        break;
                    case Config.MusicOptionString:
                        Properties.App.Default.MusicState = !(Properties.App.Default.MusicState);
                        if (Properties.App.Default.MusicState) MediaPlayer.Play(GameRef.Content.Load<Song>("Audio/Musics/Title"));
                        else MediaPlayer.Stop();
                        break;
                    case Config.BackOptionString:
                        // Changement de la résolution / Application des changements (ex: FullScreen)
                        GameRef.graphics.PreferredBackBufferWidth = Config.Resolutions[Config.IndexResolution, 0];
                        GameRef.graphics.PreferredBackBufferHeight = Config.Resolutions[Config.IndexResolution, 1];
                        GameRef.graphics.ApplyChanges();

                        // Sauvegarde dans les paramètres locaux User
                        Properties.App.Default.ScreenWidth = Config.Resolutions[Config.IndexResolution, 0];
                        Properties.App.Default.ScreenHeight = Config.Resolutions[Config.IndexResolution, 1];
                        Properties.App.Default.Save();

                        Config.UpdateMapLayer(); // Mise à jour de la position de la "zone-map", vu que la résolution a changé

                        StateManager.PushState(GameRef.TitleScreen); // Retour vers l'écran d'accueil
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

            GameRef.spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, GameRef.graphics.PreferredBackBufferWidth, GameRef.graphics.PreferredBackBufferHeight), Color.White);
            ControlManager.Draw(GameRef.spriteBatch);

            Color textColor;
            string value; // Utilisée pour afficher "Activé" (True) ou "Désactivé" (False)
            Vector2 AddedTextPosition; // Position du texte ajouté à la volée ci-dessous

            for (int i = 0; i < menuString.Length; ++i)
            {
                textColor = (i == indexMenu) ? Color.CornflowerBlue : Color.Black;
                GameRef.spriteBatch.DrawString(this.BigFont, menuString[i], new Vector2(menuPosition.X, menuPosition.Y + this.BigFont.MeasureString(menuString[i]).Y * i - this.BigFont.MeasureString(menuString[i]).Y / 2), textColor);
                AddedTextPosition = new Vector2(menuPosition.X + this.BigFont.MeasureString(menuString[i]).X, menuPosition.Y + (this.BigFont.MeasureString(menuString[i]).Y) * i - this.BigFont.MeasureString(menuString[i]).Y / 2);

                switch (menuString[i])
                {
                    case Config.ResolutionOptionString:
                        GameRef.spriteBatch.DrawString(this.BigFont, " : " + Config.Resolutions[Config.IndexResolution, 0] + "x" + Config.Resolutions[Config.IndexResolution, 1], AddedTextPosition, textColor);
                        break;
                    case Config.FullScreenOptionString:
                        value = (Properties.App.Default.ScreenFullScreen) ? "Activé" : "Désactivé";
                        GameRef.spriteBatch.DrawString(this.BigFont, " : " + value, AddedTextPosition, textColor);
                        break;
                    case Config.MusicOptionString:
                        value = (Properties.App.Default.MusicState) ? "Activée" : "Désactivée";
                        GameRef.spriteBatch.DrawString(this.BigFont, " : " + value, AddedTextPosition, textColor);
                        break;
                    case Config.SoundOptionString:
                        value = (Properties.App.Default.SoundState) ? "Activés" : "Désactivés";
                        GameRef.spriteBatch.DrawString(this.BigFont, " : " + value, AddedTextPosition, textColor);
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
