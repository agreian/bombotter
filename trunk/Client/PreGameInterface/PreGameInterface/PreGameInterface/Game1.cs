using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nuclex.Input;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Demo;

namespace PreGameInterface
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private Screen mainScreen;
        private GraphicsDeviceManager graphics;
        private GuiManager gui;
        private InputManager input;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.input = new InputManager(Services, Window.Handle);
            this.gui = new GuiManager(Services);

            Components.Add(this.input);

            Components.Add(this.gui);
            this.gui.DrawOrder = 1000;

            IsMouseVisible = true;
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

            Viewport viewport = GraphicsDevice.Viewport;
            mainScreen = new Screen(viewport.Width, viewport.Height);
            this.gui.Screen = mainScreen;

            mainScreen.Desktop.Bounds = new UniRectangle(
              new UniScalar(0, 0), new UniScalar(0, 0), // x and y = 10%
              new UniScalar(0.8f, 0.0f), new UniScalar(0.8f, 0.0f) // width and height = 80%
            );

            //createDesktopControls(mainScreen);

            LoginDialog loginDialog = new LoginDialog();
            mainScreen.Desktop.Children.Add(loginDialog);
            loginDialog.ConnectionEstablished += new LoginDialog.ConnectionEstablishedHandler(loginDialog_ConnectionEstablished);
        }

        void loginDialog_ConnectionEstablished(object sender, BomberLoutreInterface.UserData userData)
        {
            ((LoginDialog)sender).Close();

            GameListDialog gameListDialog = new GameListDialog();
            mainScreen.Desktop.Children.Add(gameListDialog);
            gameListDialog.JoinGame += new GameListDialog.JoinGameHandler(gameListDialog_JoinGame);
            gameListDialog.CreateGame += new GameListDialog.CreateGameHandler(gameListDialog_CreateGame);

        }

        void gameListDialog_CreateGame(object sender)
        {
            ((GameListDialog)sender).Close();

            CreateGameDialog createGameDialog = new CreateGameDialog();
            mainScreen.Desktop.Children.Add(createGameDialog);
            createGameDialog.MapCreated += new CreateGameDialog.MapCreatedHandler(createGameDialog_MapCreated);
        }

        void createGameDialog_MapCreated(object sender, string mapName)
        {
            ((CreateGameDialog)sender).Close();

            BomberLoutreInterface.GameData[] games = BomberLoutre.IceInterface.Main.GamesList;
            for(int i=0;i<games.Length;++i)
            {
                if(games[i].name==mapName)
                {
                    BomberLoutre.IceInterface.Main.JoinGame(games[i]);

                    mainScreen.Desktop.Children.Add(new GameDialog(BomberLoutre.IceInterface.Main.CurrentUser.gameTag, BomberLoutre.IceInterface.Main.CreatorName));
                }
            }
        }

        void gameListDialog_JoinGame(object sender, BomberLoutreInterface.GameData gameData)
        {
            ((GameListDialog)sender).Close();

            mainScreen.Desktop.Children.Add(new GameDialog(BomberLoutre.IceInterface.Main.CurrentUser.gameTag, BomberLoutre.IceInterface.Main.CreatorName));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
