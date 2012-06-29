using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

namespace Nuclex.UserInterface.Demo
{

    partial class GameDialog
    {

        #region NOT Component Designer generated code

        private void InitializeComponent()
        {
            this.playersList = new Nuclex.UserInterface.Controls.Desktop.ListControl();
            this.warningReady = new Nuclex.UserInterface.Controls.Desktop.ButtonControl();
            this.launchingGame = new Nuclex.UserInterface.Controls.Desktop.ButtonControl();
            this.cancelButton = new Nuclex.UserInterface.Controls.Desktop.ButtonControl();

            this.playersList.Bounds = new UniRectangle(new UniScalar(0.1f, 0.0f), new UniScalar(0f, 50.0f), 335, 150);
            this.playersList.Slider.Bounds.Location.X.Offset -= 1.0f;
            this.playersList.Slider.Bounds.Location.Y.Offset += 1.0f;
            this.playersList.Slider.Bounds.Size.Y.Offset -= 2.0f;
            this.playersList.SelectionMode = Nuclex.UserInterface.Controls.Desktop.ListSelectionMode.None;

            this.launchingGame.Bounds = new UniRectangle(new UniScalar(0.4f, 0.0f), new UniScalar(1.0f, -120.0f), 80, 24);
            this.launchingGame.Text = "Go!";
            this.launchingGame.Pressed += new EventHandler(launchingGame_Pressed);

            this.warningReady.Bounds = new UniRectangle(new UniScalar(0.4f, 0.0f), new UniScalar(1.0f, -80.0f), 80, 24);
            this.warningReady.Text = "Ready!";

            this.cancelButton.Bounds = new UniRectangle(new UniScalar(0.4f, 0f), new UniScalar(1.0f, -40.0f), 80, 24);
            this.cancelButton.Text = "Quitter";

            this.Bounds = new UniRectangle(80.0f, 10.0f, 400.0f, 350.0f);
            this.Title = "Salle d'attente";
            Children.Add(this.playersList);
            if (this._playerName == this._creatorName)
                Children.Add(this.launchingGame);
            Children.Add(this.warningReady);
            Children.Add(this.cancelButton);
        }

        #endregion

        private Nuclex.UserInterface.Controls.Desktop.ListControl playersList;
        private Nuclex.UserInterface.Controls.Desktop.ButtonControl warningReady;
        private Nuclex.UserInterface.Controls.Desktop.ButtonControl launchingGame;
        private Nuclex.UserInterface.Controls.Desktop.ButtonControl cancelButton;
    }
}