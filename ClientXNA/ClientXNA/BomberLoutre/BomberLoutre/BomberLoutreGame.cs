using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using BomberLoutre.Screens;
using BomberLoutre.Controls;
using System;
using Nuclex.UserInterface;
using Nuclex.Input;
using Microsoft.Xna.Framework.Audio;

namespace BomberLoutre
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BomberLoutreGame : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        GameStateManager stateManager;

        // Screens XNA (game)
        public TitleScreen TitleScreen;
        public CreditScreen CreditScreen;
        public OptionScreen OptionScreen;
        public ControlEditScreen ControlEditScreen;
        public GameScreen GameScreen;
        public NuclexScreen NuclexScreen;

        // Nuclex screens
        public Screen nuclexMainScreen;
        public GuiManager nuclexGui;
        public InputManager nuclexInput;

        private Texture2D backgroundImage;
        public bool DisplayBackgroundImage { get; set; }

        public BomberLoutreGame()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            /*
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            */
            graphics.PreferredBackBufferWidth = Properties.App.Default.ScreenWidth;
            graphics.PreferredBackBufferHeight = Properties.App.Default.ScreenHeight;
            graphics.IsFullScreen = Properties.App.Default.ScreenFullScreen;

            // Jeu
            Components.Add(new InputHandler(this));
            stateManager = new GameStateManager(this);
            Components.Add(stateManager);

            // Nuclex
            nuclexInput = new InputManager(Services, Window.Handle);
            nuclexGui = new GuiManager(Services);
            Components.Add(nuclexInput);
            Components.Add(nuclexGui);
            nuclexGui.DrawOrder = 1000;

            // Jeu
            TitleScreen = new TitleScreen(this, stateManager);
            CreditScreen = new CreditScreen(this, stateManager);
            OptionScreen = new OptionScreen(this, stateManager);
            ControlEditScreen = new ControlEditScreen(this, stateManager);
            GameScreen = new GameScreen(this, stateManager);
            NuclexScreen = new NuclexScreen(this, stateManager);

            DisplayBackgroundImage = true;

            stateManager.ChangeState(TitleScreen);
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
            backgroundImage = Content.Load<Texture2D>("Graphics/Screens/Title");

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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (!DisplayBackgroundImage)
                GraphicsDevice.Clear(Color.DarkGray);

            else
            {
                spriteBatch.Begin();
                spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }


        public void PlaySoundEffect(SoundEffect sound)
        {
            if (Properties.App.Default.SoundState)
                sound.Play(0.40f, 0.0f, 0.0f);
        }
    }
}
