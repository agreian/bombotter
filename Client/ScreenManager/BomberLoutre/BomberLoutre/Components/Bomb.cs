using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using BomberLoutre.Sprites;
using BomberLoutre.World;
using BomberLoutre.Screens;
using System;


namespace BomberLoutre.Components
{
    public class Bomb
    {
        private BomberLoutre GameRef;
        private GameScreen GameScreen;
        public BombSprite Sprite { get; protected set; }
        private int playerId;
        public Vector2 CellPosition { get; set; }
        public TimeSpan Timer { get; set; }
        public int BombPower { get; set; }

        #region Constructor
        public Bomb(int pId, Vector2 position, int power, BomberLoutre gameRef, GameScreen gameScreen)
        {
            GameRef = gameRef;
            GameScreen = gameScreen;
            Texture2D spriteTexture = GameRef.Content.Load<Texture2D>("Graphics/Sprites/bombSprite");
            this.playerId = pId;

            BombPower = power;
            Timer = new TimeSpan(0, 0, 0);

            CellPosition = Map.PointToVector((int)position.X, (int)position.Y); // Position en Cellule
            Sprite = new BombSprite(spriteTexture, position);
        }
        #endregion

        #region XNA Methods
        public void Update(GameTime gameTime)
        {
            Timer += gameTime.ElapsedGameTime;
            if (Timer.Seconds > 0) Sprite.DestroyingSoon = true;
            if (Timer.Seconds >= Config.ExplosionDelay) GameScreen.BOOM(this);
            Sprite.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            Sprite.Draw(gameTime, GameRef.spriteBatch);
        }
        #endregion

        #region Bomb Methods
        #endregion
    }
}
