using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using BomberLoutre.Controls;

namespace BomberLoutre.Screens
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
        public TitleScreen(BomberLoutreGame game, GameStateManager manager)
            : base(game, manager)
        {
            menuString = new string[] { "Jouer", "Options", "Crédits", "Quitter" };
            indexMenu = 0;
            enableMenu = true;
        }
        #endregion

        #region XNA Method region

        public override void Initialize()
        {
            menuPosition = new Vector2(150, 350);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            backgroundImage = GameRef.Content.Load<Texture2D>("Graphics/Screens/Title");

            // TODO : Gérer la musique pas uniquement lors du LoadContent, sinon en revenant sur le Title on n'a pas de musique
            MediaPlayer.IsRepeating = true;
            if (Properties.App.Default.MusicState)
                MediaPlayer.Play(GameRef.Content.Load<Song>("Audio/Musics/Title"));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);

            if (CheckIfReplayMusic())
                MediaPlayer.Play(GameRef.Content.Load<Song>("Audio/Musics/Title"));

            if (InputHandler.Pushed("Enter", PlayerIndex.One))
            {
                if (!enableMenu)
                    enableMenu = true;
                else
                {
                    switch (menuString[indexMenu])
                    {
                        case "Jouer":
                            MediaPlayer.Stop(); 
                            StateManager.ChangeState(GameRef.NuclexScreen);
                            //StateManager.ChangeState(GameRef.GameScreen);
                            break;
                        case "Options":
                            StateManager.ChangeState(GameRef.OptionScreen);
                            break;
                        case "Crédits":
                            StateManager.ChangeState(GameRef.CreditScreen);
                            break;
                        case "Quitter":
                            GameRef.Exit();
                            break;
                    }
                }
            }

            if (InputHandler.Pushed("Up", PlayerIndex.One))
            {
                if (indexMenu <= 0)
                    indexMenu = menuString.Length - 1;
                else
                    indexMenu--;
            }
            else if (InputHandler.Pushed("Down", PlayerIndex.One))
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

            if (enableMenu)
            {
                for (int i = 0; i < menuString.Length; i++)
                {
                    Color textColor = Color.Black;
                    if (i == indexMenu)
                        textColor = Color.CornflowerBlue;
                    GameRef.spriteBatch.DrawString(this.BigFont, menuString[i],
                        new Vector2(menuPosition.X - this.BigFont.MeasureString(menuString[i]).X / 2,
                            menuPosition.Y + this.BigFont.MeasureString(menuString[i]).Y * i - this.BigFont.MeasureString(menuString[i]).Y / 2), textColor);
                }
            }

            if (InputHandler.xboxPadState.IsConnected)
            {
                GameRef.spriteBatch.DrawString(this.SmallFont, "Manette connectée", new Vector2(0, 0), Color.Red);
            }


            GameRef.spriteBatch.End();
        }

        #endregion

        #region Title Screen Methods

        #endregion
    }
}
