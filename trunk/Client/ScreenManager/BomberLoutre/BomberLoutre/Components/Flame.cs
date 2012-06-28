using BomberLoutre.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using BomberLoutre.Screens;
using BomberLoutre.World;

namespace BomberLoutre.Components
{
    public class Flame
    {
        protected Sprite Sprite;
        protected BomberLoutre GameRef;
        protected GameScreen GameScreen;
        protected Rectangle SourceRectangle;
        protected Texture2D Texture;
        protected Vector2 Cellule;
        protected TimeSpan Timer;

        public Flame(int x, int y, GameScreen gameScreen, BomberLoutre gameRef)
        {
            GameRef = gameRef;
            GameScreen = gameScreen;
            SourceRectangle = new Rectangle(0, 0, Config.TileWidth, Config.TileHeight);
            Cellule = new Vector2(x, y);

            Texture = GameRef.Content.Load<Texture2D>("Graphics/Sprites/flameSprite");

            Sprite = new Sprite(Texture, Config.TileWidth, Config.TileHeight, Map.CellToVector(x,y));
        }

        public void Update(GameTime gameTime)
        {
            Timer += gameTime.ElapsedGameTime;

            if (Timer.Milliseconds >= Config.BurningDelay)
            {
                GameScreen.RemoveFlame(this);
            }

        }

        public void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Draw(Texture, Sprite.SpritePosition, SourceRectangle, Color.White);
        }
    }
}
