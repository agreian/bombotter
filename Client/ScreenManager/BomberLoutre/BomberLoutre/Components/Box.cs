﻿using BomberLoutre.Sprites;
using BomberLoutre.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberLoutre.Components
{
    public class Box
    {
        private BomberLoutre GameRef;
        public Sprite Sprite { get; protected set; }
        private Vector2 cellPosition;
        private Texture2D spriteTexture;

        #region Constructor
        public Box(BomberLoutre gameRef, Vector2 position)
        {
            GameRef = gameRef;
            spriteTexture = GameRef.Content.Load<Texture2D>("Graphics/Sprites/caseSprite");

            cellPosition = Map.CellToVector((int)position.X, (int)position.Y);
            Sprite = new Sprite(spriteTexture, Config.TileWidth, Config.TileHeight, cellPosition);
        }
        #endregion

        #region XNA Methods
        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Draw(spriteTexture, Sprite.spritePosition, Sprite.sourceRect, Color.White);
        }
        #endregion

        #region Box Methods
        #endregion
    }
}
