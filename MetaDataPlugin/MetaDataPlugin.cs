using System;
using Hearthstone_Deck_Tracker.Plugins;
using System.Reflection;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;

namespace HDT.Plugins.Custom
{

    public class MetaDataPlugin : IPlugin
    {
        private MetaDataModuleContainer _moduleContainer = new MetaDataModuleContainer();

        // Main User Control that Encapsulated everything (for now)
        private MainView _mainView = new MainView();

        public string Author
        {
            get
            {
                return "Nobbler";
            }
        }

        public string ButtonText
        {
            get
            {
                return "Options";
            }
        }

        public string Description
        {
            get
            {
                return "";
            }
        }

        public System.Windows.Controls.MenuItem MenuItem
        {
            get
            {
                return null;
            }
        }

        public string Name
        {
            get
            {
                return "Card MetaData";
            }
        }

        public void OnButtonPress()
        {

        }

        public Version Version
        {
            get
            {
                return Assembly.GetAssembly(typeof(MetaDataPlugin)).GetName().Version;
            }
        }

        public void OnLoad()
        {
            if (Config.Instance.HideInMenu && CoreAPI.Game.IsInMenu)
                _mainView.Hide();
            else
                _mainView.Show();

            CoreAPI.OverlayCanvas.Children.Add(_mainView);
           
            GameEvents.OnOpponentPlay.Add(_moduleContainer.OpponentPlay);
            GameEvents.OnPlayerPlay.Add(_moduleContainer.PlayerPlay);
            GameEvents.OnGameStart.Add(_moduleContainer.GameStart);
            GameEvents.OnTurnStart.Add(_moduleContainer.TurnStart);
            GameEvents.OnPlayerDraw.Add(_moduleContainer.PlayerDraw);
            GameEvents.OnPlayerMulligan.Add(_moduleContainer.PlayerMulligan);
            GameEvents.OnGameEnd.Add(_moduleContainer.GameEnd);

        }

        public void OnUnload()
        {
            _mainView.Hide();
            CoreAPI.OverlayCanvas.Children.Remove(_mainView);
        }

        public void OnUpdate()
        {
            if (Config.Instance.HideInMenu && CoreAPI.Game.IsInMenu)
            {
                _mainView.Hide();
                return;
            }
            else
            {
                _mainView.Show();
            }
        }
    }
}
