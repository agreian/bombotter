using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using BomberLoutre.Controls;
using Nuclex.UserInterface.Demo;
using Nuclex.UserInterface;

namespace BomberLoutre.Screens
{
    public class NuclexScreen : BaseGameState
    {
        #region Field region
        #endregion

        #region Constructor region
        public NuclexScreen(BomberLoutreGame game, GameStateManager manager) : base(game, manager)
        {
        }
        #endregion

        #region XNA Method region

        public override void Initialize()
        {
            base.Initialize();

            GameRef.IsMouseVisible = true;

            Viewport viewport = GraphicsDevice.Viewport;
            GameRef.nuclexMainScreen = new Screen(viewport.Width, viewport.Height);
            GameRef.nuclexGui.Screen = GameRef.nuclexMainScreen;

            GameRef.nuclexMainScreen.Desktop.Bounds = new UniRectangle(
              new UniScalar(0, 0), new UniScalar(0, 0), // x and y = 10%
              new UniScalar(0.8f, 0.0f), new UniScalar(0.8f, 0.0f) // width and height = 80%
            );

            //createDesktopControls(mainScreen);

            LoginDialog loginDialog = new LoginDialog();
            GameRef.nuclexMainScreen.Desktop.Children.Add(loginDialog);
            loginDialog.ConnectionEstablished += new LoginDialog.ConnectionEstablishedHandler(loginDialog_ConnectionEstablished);
            loginDialog.ConnectionCancelled += new LoginDialog.ConnectionCancelledHandler(loginDialog_ConnectionCancelled);
        }

        protected override void LoadContent()
        {
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

        #region Nuclex Screen Methods

        void loginDialog_ConnectionEstablished(object sender, BomberLoutreInterface.UserData userData)
        {
            ((LoginDialog)sender).Close();

            GameListDialog gameListDialog = new GameListDialog();
            GameRef.nuclexMainScreen.Desktop.Children.Add(gameListDialog);
            gameListDialog.JoinGame += new GameListDialog.JoinGameHandler(gameListDialog_JoinGame);
            gameListDialog.CreateGame += new GameListDialog.CreateGameHandler(gameListDialog_CreateGame);

        }

        void loginDialog_ConnectionCancelled(object sender)
        {
            StateManager.ChangeState(GameRef.TitleScreen);
        }

        void gameListDialog_CreateGame(object sender)
        {
            ((GameListDialog)sender).Close();

            CreateGameDialog createGameDialog = new CreateGameDialog();
            GameRef.nuclexMainScreen.Desktop.Children.Add(createGameDialog);
            createGameDialog.MapCreated += new CreateGameDialog.MapCreatedHandler(createGameDialog_MapCreated);
        }

        void createGameDialog_MapCreated(object sender, string mapName)
        {
            ((CreateGameDialog)sender).Close();

            BomberLoutreInterface.GameData[] games = BomberLoutre.IceInterface.Main.GamesList;
            for (int i = 0; i < games.Length; ++i)
            {
                if (games[i].name == mapName)
                {
                    BomberLoutre.IceInterface.Main.JoinGame(games[i]);

                    GameRef.nuclexMainScreen.Desktop.Children.Add(new GameDialog(BomberLoutre.IceInterface.Main.CurrentUser.gameTag, BomberLoutre.IceInterface.Main.CreatorName));
                }
            }
        }

        void gameListDialog_JoinGame(object sender, BomberLoutreInterface.GameData gameData)
        {
            ((GameListDialog)sender).Close();

            GameRef.nuclexMainScreen.Desktop.Children.Add(new GameDialog(BomberLoutre.IceInterface.Main.CurrentUser.gameTag, BomberLoutre.IceInterface.Main.CreatorName));
        }

        #endregion
    }
}
