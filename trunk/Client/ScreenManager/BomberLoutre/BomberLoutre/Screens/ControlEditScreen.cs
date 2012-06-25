using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        Vector2 textPosition;
        string[] keysNames;
        string instruction;
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
            keysNames = new string[] { "Haut", "Bas", "Gauche", "Droite", "Bombe" };
            textPosition = new Vector2(20, 20);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Graphics
            ContentManager Content = GameRef.Content;
            backgroundImage = Content.Load<Texture2D>("Graphics/Screens/Credit");

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

            if (counter < keysNames.Length)
                instruction = "Appuyez une touche pour la commande : " + keysNames[counter];

            else instruction = "Configuration des touches terminées";

            if (counter < keysNames.Length && InputHandler.HavePressedKey() && InputHandler.GetPressedKeys().Length > 0)
            {
                Config.DefaultKeys[counter] = InputHandler.GetPressedKeys()[0];
                counter++;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();

            base.Draw(gameTime);

            GameRef.spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, 800, 600), Color.White);

            ControlManager.Draw(GameRef.spriteBatch);
            Color textColor = Color.Black;

            GameRef.spriteBatch.DrawString(this.MidFont, instruction, textPosition, textColor);

            GameRef.spriteBatch.End();
        }

        #endregion

        #region Credit Screen Methods

        #endregion
    }
}
