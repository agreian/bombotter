using System;
using System.Collections.Generic;

using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Controls.Desktop;

namespace Nuclex.UserInterface.Demo
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LoginDialog : WindowControl
    {
        public delegate void ConnectionEstablishedHandler(object sender, BomberLoutreInterface.UserData userData);
        public event ConnectionEstablishedHandler ConnectionEstablished;
        public delegate void ConnectionCancelledHandler(object sender);
        public event ConnectionCancelledHandler ConnectionCancelled;

        public float Width { get; protected set; }
        public float Height { get; protected set; }

        /// <summary>Initializes a new GUI demonstration dialog</summary>
        public LoginDialog()
        {
            Width = (float) 310;
            Height = (float) 200;
            InitializeComponent();

            this.loginTextBox.Text = "paul";
            this.passwordTextBox.Text = "paulpwd";
        }

        /// <summary>Called when the user clicks on the okay button</summary>
        /// <param name="sender">Button the user has clicked on</param>
        /// <param name="arguments">Not used</param>
        private void okClicked(object sender, EventArgs arguments)
        {
            /* Check BD pour récupérer le player ? */
            string login = loginTextBox.Text;
            string password = passwordTextBox.Text;

            if(!(string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password)))
            {
                try
                {
                    BomberLoutreInterface.UserData userData = BomberLoutre.IceInterface.Main.Connection(login, password);

                    if (ConnectionEstablished != null)
                        ConnectionEstablished(this, userData);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    this.Close();
                }
            }
        }

        /// <summary>Called when the user clicks on the cancel button</summary>
        /// <param name="sender">Button the user has clicked on</param>
        /// <param name="arguments">Not used</param>
        private void cancelClicked(object sender, EventArgs arguments)
        {
            ConnectionCancelled(this);
            this.Close();
        }

    }

} // namespace Nuclex.UserInterface.Demo
