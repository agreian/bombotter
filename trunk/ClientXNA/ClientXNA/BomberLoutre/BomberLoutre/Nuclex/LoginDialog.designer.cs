using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

namespace Nuclex.UserInterface.Demo
{

    partial class LoginDialog
    {

        #region NOT Component Designer generated code

        /// <summary> 
        ///   Required method for user interface initialization -
        ///   do modify the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loginLabel = new Nuclex.UserInterface.Controls.LabelControl();
            this.loginTextBox = new Nuclex.UserInterface.Controls.Desktop.InputControl();
            this.passwordLabel = new Nuclex.UserInterface.Controls.LabelControl();
            this.passwordTextBox = new Nuclex.UserInterface.Controls.Desktop.InputControl();
            this.okButton = new Nuclex.UserInterface.Controls.Desktop.ButtonControl();
            this.cancelButton = new Nuclex.UserInterface.Controls.Desktop.ButtonControl();
            //
            // nameEntryLabel
            //
            this.loginLabel.Text = "Identifiant :";
            this.loginLabel.Bounds = new UniRectangle(10.0f, 30.0f, 110.0f, 24.0f);
            //
            // nameEntryBox
            //
            this.loginTextBox.Bounds = new UniRectangle(10.0f, 55.0f, 280.0f, 24.0f);
            this.loginTextBox.Text = "";
            //
            // nameEntryLabel
            //
            this.passwordLabel.Text = "Mot de passe :";
            this.passwordLabel.Bounds = new UniRectangle(10.0f, 85.0f, 110.0f, 24.0f);
            //
            // nameEntryBox
            //
            this.passwordTextBox.Bounds = new UniRectangle(10.0f, 110.0f, 280.0f, 24.0f);
            this.passwordTextBox.Text = "";

            this.okButton.Bounds = new UniRectangle(
              new UniScalar(1.0f, -180.0f), new UniScalar(1.0f, -40.0f), 80, 24
            );
            this.okButton.Text = "Valider";
            this.okButton.Pressed += new EventHandler(okClicked);
            //
            // cancelButton
            //
            this.cancelButton.Bounds = new UniRectangle(
              new UniScalar(1.0f, -90.0f), new UniScalar(1.0f, -40.0f), 80, 24
            );
            this.cancelButton.Text = "Retour";
            this.cancelButton.Pressed += new EventHandler(cancelClicked);
            //
            // DemoDialog
            //
            this.Bounds = new UniRectangle(Properties.App.Default.ScreenWidth / 2 - Width / 2, Properties.App.Default.ScreenHeight / 2, Width, Height);
            this.Title = "Connexion";
            Children.Add(this.loginLabel);
            Children.Add(this.loginTextBox);
            Children.Add(this.passwordLabel);
            Children.Add(this.passwordTextBox);
            Children.Add(this.okButton);
            Children.Add(this.cancelButton);
        }

        #endregion // NOT Component Designer generated code

        private Nuclex.UserInterface.Controls.LabelControl loginLabel;
        private Nuclex.UserInterface.Controls.Desktop.InputControl loginTextBox;
        private Nuclex.UserInterface.Controls.LabelControl passwordLabel;
        private Nuclex.UserInterface.Controls.Desktop.InputControl passwordTextBox;
        private Nuclex.UserInterface.Controls.Desktop.ButtonControl okButton;
        private Nuclex.UserInterface.Controls.Desktop.ButtonControl cancelButton;

    }

} // namespace Nuclex.UserInterface.Demo
