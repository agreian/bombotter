using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using BomberLoutre.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace BomberLoutre.Components
{
    public class Bomb
    {
        private BomberLoutre GameRef;
        public BombSprite Sprite { get; protected set; }
        private int playerId;

        #region Constructor
        public Bomb(int pId, Vector2 position, BomberLoutre gameRef)
        {
            GameRef = gameRef;
            Texture2D spriteTexture = GameRef.Content.Load<Texture2D>("Graphics/Sprites/bombSprite");
            this.playerId = pId;
            Sprite = new BombSprite(spriteTexture, position);
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
