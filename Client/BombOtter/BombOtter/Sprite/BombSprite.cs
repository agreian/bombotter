using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BombOtter.Sprite
{
    class BombSprite : Sprite
    {
        public int playerNumber { get; set; }
        public int currentFrame { get; set; }
        public float timer { get; set; }
        public float interval { get; set; }
        public Vector2 origin { get; set; }

        public BombSprite(Texture2D texture, int width, int height, int player, Vector2 position) : base(texture, width, height, position)
        {
            playerNumber = player;
            interval = 100f;
            sourceRect = new Rectangle(0, 0, 60, 40);
            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
            timer = 0;
        }

        public void Standing(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if(timer > interval)
            {
                currentFrame++;

                if(currentFrame > 3)
                {
                    currentFrame = 0;
                }

                sourceRect = new Rectangle(currentFrame * spriteWidth, 0, 60, 40);
                timer = 0f;
            }
        }

    }
}
