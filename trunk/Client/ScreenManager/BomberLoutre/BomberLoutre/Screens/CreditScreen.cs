using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BomberLoutre.Controls;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace BomberLoutre.Screens
{
    public class CreditScreen : BaseGameState
    {
        #region Field region
        Texture2D backgroundImage;
        string creditString;
        Vector2 creditPosition;
        #endregion

        #region Constructor region
        public CreditScreen(BomberLoutre game, GameStateManager manager) : base(game, manager)
        {
        }
        #endregion

        #region XNA Method region

        public override void Initialize()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Team C# :");
            builder.AppendLine("     - Paul JAY");
            builder.AppendLine("     - Jérémy BABOUCHE");
            builder.AppendLine("     - Daniel BERLEMONT");
            builder.AppendLine("     - Simon BLIGUET");
            builder.AppendLine("     - Florent BROUCA");
            builder.AppendLine("     - Hadrien CLARAS");
            builder.AppendLine("     - Vincent GUILLOUX");
            builder.AppendLine("     - Ahmad PATEL");
            builder.AppendLine("     - Rémi PRAUD");

            creditString = builder.ToString();

            creditPosition = new Vector2(20, 20);

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

            if (InputHandler.Pushed("Escape", PlayerIndex.One))
                StateManager.PushState(GameRef.TitleScreen);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();

            base.Draw(gameTime);

            GameRef.spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, GameRef.graphics.PreferredBackBufferWidth, GameRef.graphics.PreferredBackBufferHeight), Color.White);

            ControlManager.Draw(GameRef.spriteBatch);
            Color textColor = Color.Black;
            GameRef.spriteBatch.DrawString(this.BigFont, creditString, creditPosition, textColor);

            GameRef.spriteBatch.End();
        }

        #endregion

        #region Credit Screen Methods

        #endregion
    }
}
