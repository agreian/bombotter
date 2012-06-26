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
    public class Map : BaseGameState
    {
        Texture2D GroundTexture;

        public Map(BomberLoutre gameRef, GameStateManager manager) : base(gameRef, manager)
        {
            GroundTexture = GameRef.Content.Load<Texture2D>("Graphics/Texture/2");
        }

        public override void Initialize()
        {

        }

        protected override void LoadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);
            Rectangle source = Config.MapLayer;
            GameRef.spriteBatch.Draw(GroundTexture, new Vector2(source.X, source.Y), source, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            GameRef.spriteBatch.End();
        }
    }
}
