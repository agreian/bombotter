using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BombOtter.Sprite;

namespace BombOtter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameMain : Microsoft.Xna.Framework.Game
    {
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }

        public const int BOX_SIZE = 60;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        bool bombDropped;

        Texture2D bombTexture, caseTexture, groundTexture;

        OtterSprite playerSprite;
        KeyboardState currentKBState;
        BombSprite bombToDraw;
        List<BombSprite> bombList;
        List<CaseSprite> caseList;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = false;

            WindowWidth = 13 * BOX_SIZE;
            WindowHeight = 11 * BOX_SIZE;

            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            bombToDraw = null;
            bombDropped = false;
            bombList = new List<BombSprite>();
            caseList = new List<CaseSprite>();
            fillCaseList();

            /* Résolution de la fenêtre */
            // Largeur et hauteur doivent être multiple de BOX_SIZE (largeur d'une caisse)

            if (!(WindowWidth % BOX_SIZE == 0) && (WindowHeight % BOX_SIZE == 0)) // Valeur par défaut sinon
            {
                WindowWidth = 13 * BOX_SIZE;
                WindowHeight = 11 * BOX_SIZE;
            }

            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;

            graphics.ApplyChanges(); 

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerSprite = new OtterSprite(Content.Load<Texture2D>("otter"), 0, 64, 72, WindowWidth, WindowHeight);
            bombTexture = Content.Load<Texture2D>("bomb");
            caseTexture = Content.Load<Texture2D>("case");
            groundTexture = Content.Load<Texture2D>("grass");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // " this.Exit(); " Allows the game to exit

            currentKBState = Keyboard.GetState();
            checkBombing();

            playerSprite.HandleSpriteMovement(gameTime);

            foreach (BombSprite currentBomb in bombList)
            {
                currentBomb.Standing(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);
            Rectangle source = new Rectangle(0, 0, WindowWidth, WindowHeight);
            spriteBatch.Draw(groundTexture, Vector2.Zero, source, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.End();

            spriteBatch.Begin();
            
            foreach(BombSprite currentBomb in bombList)
            {
                spriteBatch.Draw(bombTexture, currentBomb.spritePosition, currentBomb.sourceRect, Color.White, 0f, currentBomb.origin, 1.0f, SpriteEffects.None, 0);
            }

            foreach (CaseSprite currentCase in caseList)
            {
                spriteBatch.Draw(caseTexture, currentCase.spritePosition, Color.White);
            }

            spriteBatch.Draw(playerSprite.spriteTexture, playerSprite.spritePosition, playerSprite.sourceRect, Color.White, 0f, playerSprite.origin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Se base sur une map à 780*660px, à savoir matrice de 13*11 composée de carrés de BOX_SIZEpx² (= taille d'une caisse)
        // On a donc 6 colonnes et 5 lignes de caisses, alternées entre les lignes/colonnes walkable
        public void fillCaseList()
        {
            int i, j;

            for (i = BOX_SIZE; i < WindowWidth; i += 2 * BOX_SIZE)
            {
                for (j = BOX_SIZE; j < WindowHeight; j += 2 * BOX_SIZE)
                {
                    caseList.Add(new CaseSprite(caseTexture, BOX_SIZE, BOX_SIZE, new Vector2((float)i, (float)j))); // check si x = i et y = j
                }
            }
        }

        public void checkBombing()
        {
            if (currentKBState.IsKeyDown(Keys.Space))
            {
                if (!bombDropped)
                {
                    DropBomb();
                    bombDropped = true;
                }
            }

            else bombDropped = false;
        }

        public void DropBomb()
        {
            bombToDraw = new BombSprite(bombTexture, BOX_SIZE, BOX_SIZE-20, 0, new Vector2(playerSprite.spritePosition.X, playerSprite.spritePosition.Y + 15));
            bombList.Add(bombToDraw);
        }
    }
}
