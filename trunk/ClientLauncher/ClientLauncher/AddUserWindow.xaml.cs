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
    /// Logique d'interaction pour AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
        }

        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPassword.Password) || txtPassword.Password != txtPassword2.Password)
            {
            }
            else
            {
                try
                {
                    Client.CurrentUser = Client.CurrentClientPrx.CreateUser(txtLogin.Text, txtPassword.Password);

                    GameListWindow gameList = new GameListWindow();
                    gameList.Show();
                    this.Close();
                }
                catch (UserAlreadyExistsException)
                {
                    MessageBox.Show("Cet identifiant est déjà utilisé", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
