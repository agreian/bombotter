using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

namespace Nuclex.UserInterface.Demo
{

    partial class GameListDialog
    {

        #region NOT Component Designer generated code

        /// <summary> 
        ///   Required method for user interface initialization -
        ///   do modify the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.joinGameButton = new Nuclex.UserInterface.Controls.Desktop.ButtonControl();
            this.gamesList = new Nuclex.UserInterface.Controls.Desktop.ListControl();
            this.refreshButton = new Nuclex.UserInterface.Controls.Desktop.ButtonControl();
            this.createGameButton = new Nuclex.UserInterface.Controls.Desktop.ButtonControl();

            this.refreshButton.Bounds = new UniRectangle(new UniScalar(15f), new UniScalar(200f), 80, 24);
            this.refreshButton.Text = "Rafraichir";
            this.refreshButton.Pressed += new EventHandler(refreshButton_Pressed);

            this.gamesList.Bounds = new UniRectangle(115.0f, 35.0f, 320.0f, 280.0f);
            this.gamesList.Slider.Bounds.Location.X.Offset -= 1.0f;
            this.gamesList.Slider.Bounds.Location.Y.Offset += 1.0f;
            this.gamesList.Slider.Bounds.Size.Y.Offset -= 2.0f;
            this.gamesList.SelectionMode = Nuclex.UserInterface.Controls.Desktop.ListSelectionMode.Single;

            this.joinGameButton.Bounds = new UniRectangle(new UniScalar(1.0f, -180.0f), new UniScalar(1.0f, -40.0f), 80, 24);
            this.joinGameButton.Text = "Rejoindre";
            this.joinGameButton.Pressed += new EventHandler(joinGameButton_Pressed);

            this.createGameButton.Bounds = new UniRectangle(new UniScalar(1.0f, -90.0f), new UniScalar(1.0f, -40.0f), 80, 24);
            this.createGameButton.Text = "Creer";
            this.createGameButton.Pressed += new EventHandler(createGameButton_Pressed);

            this.Bounds = new UniRectangle(80.0f, 10.0f, 450.0f, 384.0f);
            this.Title = "Gestion des parties";
            Children.Add(this.refreshButton);
            Children.Add(this.gamesList);
            Children.Add(this.joinGameButton);
            Children.Add(this.createGameButton);
        }

        #endregion

        private Nuclex.UserInterface.Controls.Desktop.ListControl gamesList;
        private Nuclex.UserInterface.Controls.Desktop.ButtonControl joinGameButton;
        private Nuclex.UserInterface.Controls.Desktop.ButtonControl refreshButton;
        private Nuclex.UserInterface.Controls.Desktop.ButtonControl createGameButton;
    }
}
