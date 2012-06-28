using System;

using Nuclex.UserInterface.Controls.Desktop;

namespace Nuclex.UserInterface.Demo
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GameListDialog : WindowControl
    {
        public delegate void JoinGameHandler(object sender, BomberLoutreInterface.GameData gameData);
        public event JoinGameHandler JoinGame;

        public delegate void CreateGameHandler(object sender);
        public event CreateGameHandler CreateGame;

        private BomberLoutreInterface.GameData[] games;

        /// <summary>Initializes a new GUI demonstration dialog</summary>
        public GameListDialog()
        {
            InitializeComponent();

            RefreshList();
        }

        void joinGameButton_Pressed(object sender, EventArgs e)
        {
            BomberLoutre.IceInterface.Main.JoinGame(this.games[this.gamesList.SelectedItems[0]]);

            if (JoinGame != null)
                JoinGame(this, this.games[this.gamesList.SelectedItems[0]]);
        }

        void createGameButton_Pressed(object sender, EventArgs e)
        {
            if (CreateGame != null)
                CreateGame(this);
        }

        void refreshButton_Pressed(object sender, EventArgs e)
        {
            RefreshList();
        }

        void RefreshList()
        {
            gamesList.Items.Clear();

            games = BomberLoutre.IceInterface.Main.GamesList;

            if (games.Length == 0)
            {
                gamesList.Items.Add("Aucune partie disponible");
            }
            else
            {
                for (int i = 0; i < games.Length; ++i)
                {
                    gamesList.Items.Add(string.Format("{0} - {1}/4", games[i].name, games[i].playerCount));
                }
            }
        }
    }
}
