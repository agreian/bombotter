using System;
using System.Collections.Generic;

using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Controls.Desktop;

namespace Nuclex.UserInterface.Demo
{

    /// <summary>Dialog that demonstrates the capabilities of the GUI library</summary>
    public partial class GameDialog : WindowControl
    {
        private string _playerName = string.Empty;
        private string _creatorName = string.Empty;

        private List<string> _playersName = new List<string>();

        public GameDialog(string playerName, string creatorName)
        {
            this._playerName = playerName;
            this._creatorName = creatorName;

            InitializeComponent();

            BomberLoutre.IceInterface.Main.CurrentGameWaitRoom.NewUser += new BomberLoutre.IceInterface.GameWaitRoom.NewUserHandler(CurrentGameWaitRoom_NewUser);
            BomberLoutre.IceInterface.Main.CurrentGameWaitRoom.UserLeft += new BomberLoutre.IceInterface.GameWaitRoom.UserLeftHandler(CurrentGameWaitRoom_UserLeft);
            BomberLoutre.IceInterface.Main.CurrentGameWaitRoom.UserReady += new BomberLoutre.IceInterface.GameWaitRoom.UserReadyHandler(CurrentGameWaitRoom_UserReady);
        }

        void CurrentGameWaitRoom_UserReady(object sender, string username)
        {
            for (int i = 0; i < _playersName.Count; ++i)
            {
                if (_playersName[i] == username)
                {
                    this.playersList.Items[i] = string.Format("{0} - Pret", username);
                }
            }
        }

        void CurrentGameWaitRoom_UserLeft(object sender, string username)
        {
            BomberLoutre.IceInterface.Main.CurrentGameData.playerCount--;

            for (int i = 0; i < _playersName.Count; ++i)
            {
                if (_playersName[i] == username)
                {
                    _playersName.RemoveAt(i);
                    this.playersList.Items.RemoveAt(i);
                }
            }
        }

        void CurrentGameWaitRoom_NewUser(object sender, string username)
        {
            BomberLoutre.IceInterface.Main.CurrentGameData.playerCount++;

            _playersName.Add(username);
            this.playersList.Items.Add(string.Format("{0}", username));
        }

        public void AddNewPlayer(string player)
        {
            this.playersList.Items.Add(player);
        }

        public void RemovePlayer(string player)
        {
            for (int i = 0; i < playersList.Items.Count; ++i)
            {
                if (playersList.Items[i] == player)
                {
                    playersList.Items.Remove(player);
                    break;
                }
            }
        }

        private void cancelClicked(object sender, EventArgs arguments)
        {
            Close();
        }

        void launchingGame_Pressed(object sender, EventArgs e)
        {
            BomberLoutre.IceInterface.Main.LaunchGame();
        }

        void warningReady_Pressed(object sender, EventArgs e)
        {
            BomberLoutre.IceInterface.Main.ImReady();
        }

    }

} // namespace Nuclex.UserInterface.Demo
