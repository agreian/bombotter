using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberLoutre.Sprites
{
    public class BombSprite : Sprite
    {
        public int currentFrame { get; set; }
        public float timer { get; set; }
        public float interval { get; set; }

        public BombSprite(Texture2D texture, Vector2 position) : base(texture, position)
        {
            spriteWidth = 60;
            spriteHeight = 40;
            interval = 150f;
            timer = 0;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if(timer > interval)
            {
                currentFrame++;

                if(currentFrame > 3)
                {
                    currentFrame = 0;
                }

                sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
                timer = 0f;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spritePosition, sourceRect, Color.White);
        }

    }
}
