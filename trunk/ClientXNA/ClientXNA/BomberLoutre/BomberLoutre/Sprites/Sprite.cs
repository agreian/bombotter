using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BomberLoutre.Sprites
{
    public class Sprite
    {
        public Texture2D SpriteTexture { get; set; }
        public int SpriteWidth { get; set; }
        public int SpriteHeight { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Vector2 SpritePosition { get; set; }
        public Vector2 CellPosition { get; set; }

        public Sprite(Texture2D texture)
        {
            SpriteTexture = texture;
            SourceRectangle = new Rectangle(0, 0, SpriteWidth, SpriteHeight);
        }

        public Sprite(Texture2D texture, int width, int height)
        {
            SpriteTexture = texture;
            SpriteWidth = width;
            SpriteHeight = height;
            SourceRectangle = new Rectangle(0, 0, SpriteWidth, SpriteHeight);
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            SpriteTexture = texture;
            SpritePosition = position;
            SourceRectangle = new Rectangle(0, 0, SpriteWidth, SpriteHeight);
        }

        public Sprite(Texture2D texture, int width, int height, Vector2 position)
        {
            SpriteTexture = texture;
            SpriteWidth = width;
            SpriteHeight = height;
            SpritePosition = position;
            SourceRectangle = new Rectangle(0, 0, SpriteWidth, SpriteHeight);
        }
    }
}
