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

        /// <summary>Initializes a new GUI demonstration dialog</summary>
        public LoginDialog()
        {
            InitializeComponent();
        }

        /// <summary>Called when the user clicks on the okay button</summary>
        /// <param name="sender">Button the user has clicked on</param>
        /// <param name="arguments">Not used</param>
        private void okClicked(object sender, EventArgs arguments)
        {
            /* Check BD pour récupérer le player ? */
            string login = loginTextBox.Text;
            string password = passwordTextBox.Text;

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

        /// <summary>Called when the user clicks on the cancel button</summary>
        /// <param name="sender">Button the user has clicked on</param>
        /// <param name="arguments">Not used</param>
        private void cancelClicked(object sender, EventArgs arguments)
        {
            this.Close();
        }

    }

} // namespace Nuclex.UserInterface.Demo
