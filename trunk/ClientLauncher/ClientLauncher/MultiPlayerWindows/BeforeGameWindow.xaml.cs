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

namespace BomberLoutre.Client.Launcher
{
    /// <summary>
    /// Logique d'interaction pour BeforeGameWindow.xaml
    /// </summary>
    public partial class BeforeGameWindow : Window
    {
        public BeforeGameWindow(Game game)
        {
            Client.ConnectToServer();
            Client.CurrentClientPrx.JoinGame(game, Client.CurrentUser);

            InitializeComponent();
        }
    }
}
