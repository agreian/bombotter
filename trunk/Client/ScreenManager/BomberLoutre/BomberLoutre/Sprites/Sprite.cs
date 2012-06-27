using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BomberLoutre.Sprites
{
    public class Sprite
    {
        public Texture2D spriteTexture { get; set; }
        public int spriteWidth { get; set; }
        public int spriteHeight { get; set; }
        public Rectangle sourceRect { get; set; }
        public Vector2 spritePosition { get; set; }

        public Sprite(Texture2D texture)
        {
            spriteTexture = texture;
            sourceRect = new Rectangle(0, 0, spriteWidth, spriteHeight);
        }

        public Sprite(Texture2D texture, int width, int height)
        {
            spriteTexture = texture;
            spriteWidth = width;
            spriteHeight = height;
            sourceRect = new Rectangle(0, 0, spriteWidth, spriteHeight);
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            spriteTexture = texture;
            spritePosition = position;
            sourceRect = new Rectangle(0, 0, spriteWidth, spriteHeight);
        }

        public Sprite(Texture2D texture, int width, int height, Vector2 position)
        {
            spriteTexture = texture;
            spriteWidth = width;
            spriteHeight = height;
            spritePosition = position;
            sourceRect = new Rectangle(0, 0, spriteWidth, spriteHeight);
        }
    }
}
