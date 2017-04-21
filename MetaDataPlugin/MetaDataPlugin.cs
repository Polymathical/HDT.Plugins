using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearthstone_Deck_Tracker.Controls;
using Hearthstone_Deck_Tracker.Plugins;
using System.Reflection;
using Hearthstone_Deck_Tracker.API;
using System.Windows.Controls;
using System.Windows;
using Hearthstone_Deck_Tracker;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;

namespace HDT.Plugins.Custom
{

    public class MetaDataPlugin : IPlugin
    {
        private MetaDataController _metaDataPlugin;
        private MetaDataView _metaDataView;

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

        //public object CoreAPI { get; private set; }

        public void OnLoad()
        {
            _metaDataView = new MetaDataView();
            _metaDataPlugin = new MetaDataController(_metaDataView);

            if (Config.Instance.HideInMenu && Hearthstone_Deck_Tracker.API.Core.Game.IsInMenu)
                _metaDataView.Hide();
            else
                _metaDataView.Show();

            CoreAPI.OverlayCanvas.Children.Add(_metaDataView);

            GameEvents.OnGameStart.Add(_metaDataPlugin.GameStart);
            GameEvents.OnTurnStart.Add(_metaDataPlugin.TurnStart);
            GameEvents.OnPlayerDraw.Add(_metaDataPlugin.PlayerDraw);
            GameEvents.OnPlayerMulligan.Add(_metaDataPlugin.PlayerMulligan);
            GameEvents.OnGameEnd.Add(_metaDataPlugin.GameEnd);

        }

        public void OnUnload()
        {
            _metaDataView.Hide();
          CoreAPI.OverlayCanvas.Children.Remove(_metaDataView);
            _metaDataView.Dispose();

            _metaDataView = null;
            _metaDataPlugin = null;
            _metaDataView = null;
        }

        public void OnUpdate()
        {
            if (Config.Instance.HideInMenu &&  CoreAPI.Game.IsInMenu)
            {
                _metaDataView.Hide();
                return;
            }
            else
            {
                _metaDataView.Show();

                // Will this create stacking waits? 
                Wait(100);
                _metaDataPlugin?.Update();
            }
        }

        async void Wait(int ms)
        {
            await Task.Delay(ms);
        }
    }
}
