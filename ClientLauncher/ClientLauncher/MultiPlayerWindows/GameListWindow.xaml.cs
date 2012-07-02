using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BomberLoutreIce;
using System.Collections.ObjectModel;

namespace BomberLoutre.Client.Launcher
{
    /// <summary>
    /// Logique d'interaction pour GameListWindow.xaml
    /// </summary>
    public partial class GameListWindow : Window
    {
        private Game[] _gameList;
        public ObservableCollection<GameClient> GameList
        {
            get;
            private set;
        }

        public GameListWindow()
        {
            GameList = new ObservableCollection<GameClient>();

            RefreshGameList();

            InitializeComponent();

            //listGames.ItemsSource = GameList;
        }

        private void RefreshGameList()
        {
            GameList.Clear();

            _gameList = Client.CurrentClientPrx.GetGameList();
            for (int i = 0; i < _gameList.Length; ++i)
            {
                GameList.Add(new GameClient(_gameList[i]));
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshGameList();
        }

        private void btnJoinGame_Click(object sender, RoutedEventArgs e)
        {
            BeforeGameWindow beforeGame = new BeforeGameWindow(((GameClient)this.listGames.SelectedItem).Game);
            beforeGame.Show();
            this.Close();
        }

        private void btnCreateGame_Click(object sender, RoutedEventArgs e)
        {
            CreateGameWindow createWindow = new CreateGameWindow();
            createWindow.Show();
            this.Close();
        }
    }
}
