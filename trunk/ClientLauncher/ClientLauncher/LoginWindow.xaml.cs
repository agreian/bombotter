﻿using System;
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
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUser = new AddUserWindow();
            addUser.Show();
            this.Close();
        }

        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
            {
            }
            else
            {
                try
                {
                    Client.CurrentUser = Client.CurrentClientPrx.Connect(txtLogin.Text, txtPassword.Password);

                    GameListWindow gameList = new GameListWindow();
                    gameList.Show();
                    this.Close();
                }
                catch (BadUserInfoException)
                {
                    MessageBox.Show("Identifiant ou mot de passe erroné", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
