using BomberLoutre.Sprites;
using BomberLoutre.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BomberLoutre.Components
{
    public class Bonus
    {
        protected Sprite Sprite;
        protected BomberLoutreGame GameRef;
        protected Rectangle SourceRectangle;
        protected Texture2D Texture;
        public Vector2 CellPosition { get; private set; }
        public string Type { get; set; }

        public Bonus(BomberLoutreGame gameRef, Vector2 cellule, string type)
        {
            GameRef = gameRef;
            SourceRectangle = new Rectangle(0, 0, Config.TileWidth, Config.TileHeight);
            CellPosition = cellule;
            Type = type;

            switch (type)
            {
                case Config.BonusPower:
                    Texture = gameRef.Content.Load<Texture2D>("Graphics/Bonus/flameNormal");
                break;
                case Config.BonusPowerMax:
                    Texture = gameRef.Content.Load<Texture2D>("Graphics/Bonus/flameGold");
                break;
                case Config.BonusKick:
                    Texture = gameRef.Content.Load<Texture2D>("Graphics/Bonus/kick");
                break;
                case Config.BonusSpeed:
                    Texture = gameRef.Content.Load<Texture2D>("Graphics/Bonus/boots");
                break;
                case Config.BonusBomb:
                    Texture = gameRef.Content.Load<Texture2D>("Graphics/Bonus/bombUp");
                break;
            }

            Vector2 perfectPosition = Map.CellToVector((int)cellule.X, (int)cellule.Y);
            
            Sprite = new Sprite(Texture, Config.TileWidth, Config.TileHeight, perfectPosition);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Draw(Texture, Sprite.SpritePosition, SourceRectangle, Color.White);
        }
    }
}
