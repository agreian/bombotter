﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using BomberLoutre.Controls;

namespace BomberLoutre.Screens
{
    public abstract partial class BaseGameState : GameState
    {
        #region Fields region

        protected BomberLoutreGame GameRef;

        protected ControlManager ControlManager;

        protected SpriteFont BigFont;
        protected SpriteFont MidFont;
        protected SpriteFont SmallFont;

        protected PlayerIndex playerIndexInControl;

        #endregion

        #region Properties region
        #endregion

        #region Constructor Region

        public BaseGameState(Game game, GameStateManager manager)
            : base(game, manager)
        {
            GameRef = (BomberLoutreGame)game;

            playerIndexInControl = PlayerIndex.One;
        }

        #endregion

        #region XNA Method Region

        protected override void LoadContent()
        {
            ContentManager Content = Game.Content;

            SpriteFont menuFont = Content.Load<SpriteFont>(@"Graphics\Fonts\ControlFont");
            BigFont = Content.Load<SpriteFont>(@"Graphics\Fonts\BigFont");
            MidFont = Content.Load<SpriteFont>(@"Graphics\Fonts\MidFont");
            SmallFont = Content.Load<SpriteFont>(@"Graphics\Fonts\SmallFont");
            ControlManager = new ControlManager(menuFont);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        #endregion

        #region Method Region
        #endregion
    }
}
