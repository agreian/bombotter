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
        protected BomberLoutreGame GameRef;
        protected GameScreen GameScreen;
        protected Rectangle SourceRectangle;
        protected Texture2D Texture;
        protected Vector2 Cellule;
        protected TimeSpan Timer;
        protected float RotationAngle;
        protected Vector2 origin;

        public Flame(int x, int y, GameScreen gameScreen, BomberLoutreGame gameRef, bool rotate = false)
        {
            GameRef = gameRef;
            GameScreen = gameScreen;
            SourceRectangle = new Rectangle(0, 0, Config.TileWidth, Config.TileHeight);
            Cellule = new Vector2(x, y);

            RotationAngle = (rotate) ? (float)Math.Atan2(1, 0) : 0;

            Texture = GameRef.Content.Load<Texture2D>("Graphics/Sprites/flameSprite");

            Sprite = new Sprite(Texture, Config.TileWidth, Config.TileHeight, Map.CellToVector(x,y));
            origin = new Vector2(0, 0);
        }

        public void Update(GameTime gameTime)
        {
            Timer += gameTime.ElapsedGameTime;

            if (Timer.Milliseconds >= Config.BurningDelay)
            {
                GameScreen.RemoveFlame(this);
            }

            if (RotationAngle != 0) origin = new Vector2(0, 60); // "Recentrage" des flammes horizontales
        }

        public void Draw(GameTime gameTime)
        {
            //GameRef.spriteBatch.Draw(Texture, Sprite.SpritePosition, SourceRectangle, Color.White);
            GameRef.spriteBatch.Draw(Texture, Sprite.SpritePosition, null, Color.White, RotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
        }
    }
}
