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

        public delegate void MapCreationCanceledHandler(object sender);
        public event MapCreationCanceledHandler MapCreationCanceled;

        private string[] _mapNames;

        public CreateGameDialog()
        {
            InitializeComponent();

            this.gameNameEntryBox.Text = BomberLoutre.IceInterface.Main.GetPartyName();

            _mapNames = BomberLoutre.IceInterface.Main.MapsNames;

            for (int i = 0; i < _mapNames.Length; ++i)
            {
                this.mapList.Items.Add(_mapNames[i]);
            }
        }

        private void okClicked(object sender, EventArgs arguments)
        {
            if ((string.IsNullOrWhiteSpace(this.gameNameEntryBox.Text)) || (this.mapList.SelectedItems.Count == 0))
            {

            }
            else
            {
                if (MapCreated != null)
                {
                    MapCreated(this, this.gameNameEntryBox.Text);
                }
            }
        }

        private void cancelClicked(object sender, EventArgs arguments)
        {
            if (MapCreationCanceled != null)
            {
                MapCreationCanceled(this);
                Close();
            }
        }

    }

} // namespace Nuclex.UserInterface.Demo
