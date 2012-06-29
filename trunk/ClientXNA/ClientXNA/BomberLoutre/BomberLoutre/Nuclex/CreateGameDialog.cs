using System;
using System.Collections.Generic;

using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Controls.Desktop;

namespace Nuclex.UserInterface.Demo
{
    public partial class CreateGameDialog : WindowControl
    {
        public delegate void MapCreatedHandler(object sender, string mapName);
        public event MapCreatedHandler MapCreated;

        public CreateGameDialog()
        {
            InitializeComponent();

            string[] mapNames = BomberLoutre.IceInterface.Main.MapsNames;

            for (int i = 0; i < mapNames.Length; ++i)
            {
                this.mapList.Items.Add(mapNames[i]);
            }
        }

        private void okClicked(object sender, EventArgs arguments)
        {
            if (!string.IsNullOrWhiteSpace(this.gameNameEntryBox.Text))
            {
                BomberLoutre.IceInterface.Main.CreateGame(this.gameNameEntryBox.Text);

                if (MapCreated != null)
                {
                    MapCreated(this, this.gameNameEntryBox.Text);
                }
            }
        }

        private void cancelClicked(object sender, EventArgs arguments)
        {
            Close();
        }

    }

} // namespace Nuclex.UserInterface.Demo
