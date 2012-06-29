using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

namespace Nuclex.UserInterface.Demo
{
    partial class CreateGameDialog
    {
        #region NOT Component Designer generated code

        private void InitializeComponent()
        {
            this.gameNameEntryLabel = new Nuclex.UserInterface.Controls.LabelControl();
            this.gameNameEntryBox = new Nuclex.UserInterface.Controls.Desktop.InputControl();
            this.classic = new Nuclex.UserInterface.Controls.Desktop.ChoiceControl();
            this.fogOfWar = new Nuclex.UserInterface.Controls.Desktop.ChoiceControl();
            this.survival = new Nuclex.UserInterface.Controls.Desktop.ChoiceControl();
            this.mapList = new Nuclex.UserInterface.Controls.Desktop.ListControl();
            this.okButton = new Nuclex.UserInterface.Controls.Desktop.ButtonControl();
            this.cancelButton = new Nuclex.UserInterface.Controls.Desktop.ButtonControl();

            this.gameNameEntryLabel.Text = "Nom de la partie :";
            this.gameNameEntryLabel.Bounds = new UniRectangle(10.0f, 30.0f, 110.0f, 24.0f);

            this.gameNameEntryBox.Bounds = new UniRectangle(10.0f, 55.0f, 492.0f, 24.0f);
            this.gameNameEntryBox.Text = string.Empty;

            this.classic.Bounds = new UniRectangle(10.0f, 125.0f, 120.0f, 16.0f);
            this.classic.Selected = true;
            this.classic.Text = "Classique";

            //this.fogOfWar.Bounds = new UniRectangle(10.0f, 145.0f, 120.0f, 16.0f);
            //this.fogOfWar.Text = "Normal";

            //this.survival.Bounds = new UniRectangle(10.0f, 165.0f, 120.0f, 16.0f);
            //this.survival.Text = "Hard";

            this.mapList.Bounds = new UniRectangle(100.0f, 100.0f, 400.0f, 230.0f);
            this.mapList.Slider.Bounds.Location.X.Offset -= 1.0f;
            this.mapList.Slider.Bounds.Location.Y.Offset += 1.0f;
            this.mapList.Slider.Bounds.Size.Y.Offset -= 2.0f;
            //this.mapList.Items.Add("Episode 1 - No Map's Land");

            this.mapList.SelectionMode = Nuclex.UserInterface.Controls.Desktop.ListSelectionMode.Single;
            //this.mapList.SelectedItems.Add(3);

            this.okButton.Bounds = new UniRectangle(new UniScalar(1.0f, -180.0f), new UniScalar(1.0f, -40.0f), 80, 24);
            this.okButton.Text = "Creer";
            this.okButton.Pressed += new EventHandler(okClicked);

            this.cancelButton.Bounds = new UniRectangle(new UniScalar(1.0f, -90.0f), new UniScalar(1.0f, -40.0f), 80, 24);
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Pressed += new EventHandler(cancelClicked);

            this.Bounds = new UniRectangle(80.0f, 10.0f, 512.0f, 384.0f);
            this.Title = "Creation d'une partie";
            Children.Add(this.gameNameEntryLabel);
            Children.Add(this.gameNameEntryBox);
            Children.Add(this.classic);
            Children.Add(this.fogOfWar);
            Children.Add(this.survival);
            Children.Add(this.mapList);
            Children.Add(this.okButton);
            Children.Add(this.cancelButton);
        }

        #endregion

        private Nuclex.UserInterface.Controls.LabelControl gameNameEntryLabel;
        private Nuclex.UserInterface.Controls.Desktop.InputControl gameNameEntryBox;
        private Nuclex.UserInterface.Controls.Desktop.ChoiceControl classic;
        private Nuclex.UserInterface.Controls.Desktop.ChoiceControl fogOfWar;
        private Nuclex.UserInterface.Controls.Desktop.ChoiceControl survival;
        private Nuclex.UserInterface.Controls.Desktop.ListControl mapList;
        private Nuclex.UserInterface.Controls.Desktop.ButtonControl okButton;
        private Nuclex.UserInterface.Controls.Desktop.ButtonControl cancelButton;
    }
}
