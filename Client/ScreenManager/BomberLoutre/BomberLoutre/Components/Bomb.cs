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

            // Calcul de la case exacte, pour insérer la bombe au centre celle-ci
            CellPosition = Map.PointToVector((int)position.X+1, (int)position.Y+1);
            Vector2 perfectPosition = new Vector2((CellPosition.X * Config.TileWidth) + Config.MapLayer.X, (CellPosition.Y * Config.TileHeight) + Config.MapLayer.Y);
            CellPosition = Map.PointToVector((int)perfectPosition.X, (int)perfectPosition.Y); // On met à jour avec la bonne case
            Sprite = new BombSprite(spriteTexture, perfectPosition);
        }
        #endregion

        #region XNA Methods
        public void Update(GameTime gameTime)
        {
            Timer += gameTime.ElapsedGameTime;
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
