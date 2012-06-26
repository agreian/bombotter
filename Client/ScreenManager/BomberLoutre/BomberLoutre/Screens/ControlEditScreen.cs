using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BomberLoutre.Controls;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace BomberLoutre.Screens
{
    public class ControlEditScreen : BaseGameState
    {
        #region Field region
        Texture2D backgroundImage;
        Vector2 textPosition, errorPosition;
        string instruction, error;
        string[] keysNames;
        int counter;
        #endregion

        #region Constructor region
        public ControlEditScreen(BomberLoutre game, GameStateManager manager)
            : base(game, manager)
        {
        }
        #endregion

        #region XNA Method region

        public override void Initialize()
        {
            instruction = "Appuyez une touche pour la commande : Haut";
            error = "";
            keysNames = new string[] { "Haut", "Bas", "Gauche", "Droite", "Bombe" };
            textPosition = new Vector2(20, 20);

            base.Initialize();

            // On a besoin de MidFont initialisé pendant base.Initialize()
            errorPosition = new Vector2(20, this.MidFont.MeasureString(instruction).Y + 20);
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

            if (InputHandler.KeyPressed(Keys.Enter))
            {
                StateManager.PushState(GameRef.OptionScreen);
                counter = 0;
            }

            // L'utilisateur n'a pas encore défini tous les contrôles
            if (counter < keysNames.Length)
                instruction = "Appuyez une touche pour la commande : " + keysNames[counter];

            else
            {
                instruction = "Configuration des touches terminées : \n";
                for (int i = 0; i < keysNames.Length; i++)
                {
                    instruction += "     - " + keysNames[i] + " : ";

                    switch (i)
                    {
                        case 0:
                            instruction += Properties.App.Default.KeyUp + "\n";
                            break;

                        case 1:
                            instruction += Properties.App.Default.KeyDown + "\n";
                            break;

                        case 2:
                            instruction += Properties.App.Default.KeyLeft + "\n";
                            break;

                        case 3:
                            instruction += Properties.App.Default.KeyRight + "\n";
                            break;

                        case 4:
                            instruction += Properties.App.Default.KeySpace + "\n";
                            break;
                    }
                }

                instruction += "\nAppuyez sur \"ENTRÉE\" pour revenir au menu.";
            }
            
            if (counter < keysNames.Length && InputHandler.HavePressedKey() && InputHandler.GetPressedKeys().Length > 0)
            {
                if (InputHandler.GetPressedKeys()[0] != Keys.Enter)
                {
                    error = ""; // Réinitialisation pour les coups suivants

                    switch(counter)
                    {
                        case 0:
                            Properties.App.Default.KeyUp = InputHandler.GetPressedKeys()[0];
                        break;

                        case 1:
                            if (InputHandler.GetPressedKeys()[0] != Properties.App.Default.KeyUp)
                                Properties.App.Default.KeyDown = InputHandler.GetPressedKeys()[0];
                            else { error = "Touche déjà utilisée !"; counter--; }
                        break;

                        case 2:
                            if (InputHandler.GetPressedKeys()[0] != Properties.App.Default.KeyUp
                                && InputHandler.GetPressedKeys()[0] != Properties.App.Default.KeyDown
                                )
                                Properties.App.Default.KeyLeft = InputHandler.GetPressedKeys()[0];
                            else { error = "Touche déjà utilisée !"; counter--; }
                        break;

                        case 3:
                            if (InputHandler.GetPressedKeys()[0] != Properties.App.Default.KeyUp
                                && InputHandler.GetPressedKeys()[0] != Properties.App.Default.KeyDown
                                && InputHandler.GetPressedKeys()[0] != Properties.App.Default.KeyLeft
                                )
                            Properties.App.Default.KeyRight = InputHandler.GetPressedKeys()[0];
                            else { error = "Touche déjà utilisée !"; counter--; }
                        break;

                        case 4:
                            if (InputHandler.GetPressedKeys()[0] != Properties.App.Default.KeyUp
                                && InputHandler.GetPressedKeys()[0] != Properties.App.Default.KeyDown
                                && InputHandler.GetPressedKeys()[0] != Properties.App.Default.KeyLeft
                                && InputHandler.GetPressedKeys()[0] != Properties.App.Default.KeyRight
                                )
                            Properties.App.Default.KeySpace = InputHandler.GetPressedKeys()[0];
                            else { error = "Touche déjà utilisée !"; counter--; }
                        break;
                    }

                    counter++;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();

            base.Draw(gameTime);

            GameRef.spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, GameRef.graphics.PreferredBackBufferWidth, GameRef.graphics.PreferredBackBufferHeight), Color.White);

            ControlManager.Draw(GameRef.spriteBatch);
            Color textColor = Color.Black;

            GameRef.spriteBatch.DrawString(this.MidFont, instruction, textPosition, textColor);
            GameRef.spriteBatch.DrawString(this.MidFont, error, errorPosition, Color.Red);
            GameRef.spriteBatch.End();
        }

        #endregion

        #region Credit Screen Methods

        #endregion
    }
}
