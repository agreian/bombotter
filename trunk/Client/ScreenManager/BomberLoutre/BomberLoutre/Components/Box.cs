using BomberLoutre.Sprites;
using BomberLoutre.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberLoutre.Components
{
    public class Box
    {
        private BomberLoutre GameRef;
        public Sprite Sprite { get; protected set; }
        public Vector2 cellPosition { get; set; }
        private Texture2D spriteTexture;

        #region Constructor
        public Box(BomberLoutre gameRef, Vector2 cell)
        {
            GameRef = gameRef;
            spriteTexture = GameRef.Content.Load<Texture2D>("Graphics/Sprites/caseSprite");
            cellPosition = cell;

            Vector2 perfectPosition = Map.CellToVector((int)cell.X, (int)cell.Y);
            Sprite = new Sprite(spriteTexture, Config.TileWidth, Config.TileHeight, perfectPosition);
        }
        #endregion

        #region XNA Methods
        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Draw(spriteTexture, Sprite.SpritePosition, Sprite.sourceRect, Color.White);
        }
        #endregion

        #region Box Methods
        #endregion
    }
}
