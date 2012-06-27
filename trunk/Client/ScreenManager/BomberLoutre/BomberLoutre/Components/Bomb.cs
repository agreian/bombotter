using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using BomberLoutre.Sprites;
using BomberLoutre.World;


namespace BomberLoutre.Components
{
    public class Bomb
    {
        private BomberLoutre GameRef;
        public BombSprite Sprite { get; protected set; }
        private int playerId;
        private Vector2 cellPosition;

        #region Constructor
        public Bomb(int pId, Vector2 position, BomberLoutre gameRef)
        {
            GameRef = gameRef;
            Texture2D spriteTexture = GameRef.Content.Load<Texture2D>("Graphics/Sprites/bombSprite");
            this.playerId = pId;

            // Calcul de la case exacte, pour insérer la bombe au centre celle-ci
            cellPosition = Map.PointToVector((int)position.X, (int)position.Y);
            Vector2 perfectPosition = new Vector2(cellPosition.X * Config.TileWidth, cellPosition.Y * Config.TileHeight);
            Sprite = new BombSprite(spriteTexture, perfectPosition);
        }
        #endregion

        #region XNA Methods
        public void Update(GameTime gameTime)
        {
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
