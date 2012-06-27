using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BomberLoutre.Screens;
using BomberLoutre.Controls;

namespace BomberLoutre.World
{
    public class Map : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D GroundTexture;
        BomberLoutre GameRef;

        public Map(BomberLoutre gameRef) : base(gameRef)
        {
            GameRef = gameRef;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            GroundTexture = Game.Content.Load<Texture2D>("Graphics/Textures/2");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);
            GameRef.spriteBatch.Draw(GroundTexture, new Vector2(Config.MapLayer.X, Config.MapLayer.Y), Config.MapLayer, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            GameRef.spriteBatch.End();
            base.Draw(gameTime);
        }

        public static Vector2 PointToVector(int x, int y)
        {
            return new Vector2((float) Math.Floor((x / (float) Config.TileWidth)),  (float) Math.Floor((y / (float) Config.TileHeight))); 
        }

        public static Vector2 CellToVector(int x, int y)
        {
            return new Vector2(((x-1) * Config.TileWidth) + Config.MapLayer.X, ((y-1) * Config.TileHeight) + Config.MapLayer.Y);
        }
    }
}
