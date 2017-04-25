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
using HDT.Plugins.Custom.Controls;

namespace HDT.Plugins.Custom
{

    public class MetaDataPlugin : IPlugin
    {
        private MetaDataPluginMain _metaDataPlugin;
        MulliganOddsView MulliganView { get; set; }
        CardInfoView CardView { get; set; }
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
            MulliganView = new MulliganOddsView();
            CardView = new CardInfoView();

            _metaDataPlugin = new MetaDataPluginMain(MulliganView, CardView);

            MulliganView = new MulliganOddsView();
            CardView = new CardInfoView();

            _metaDataPlugin.MulligansView = MulliganView;
            _metaDataPlugin.CardView = CardView;

            Hide();

            CoreAPI.OverlayCanvas.Children.Add(MulliganView);
            CoreAPI.OverlayCanvas.Children.Add(CardView);

            GameEvents.OnOpponentPlay.Add(_metaDataPlugin.OpponentPlay);
            GameEvents.OnPlayerPlay.Add(_metaDataPlugin.PlayerPlay);
            GameEvents.OnGameStart.Add(_metaDataPlugin.GameStart);
            GameEvents.OnTurnStart.Add(_metaDataPlugin.TurnStart);
            GameEvents.OnPlayerDraw.Add(_metaDataPlugin.PlayerDraw);
            GameEvents.OnPlayerMulligan.Add(_metaDataPlugin.PlayerMulligan);
            GameEvents.OnGameEnd.Add(_metaDataPlugin.GameEnd);

            Show();
        }

        public void OnUnload()
        {
            CoreAPI.OverlayCanvas.Children.Remove(MulliganView);
            CoreAPI.OverlayCanvas.Children.Add(CardView);
        }

        public void OnUpdate()
        {
            SetVisibility();
        }

        void SetVisibility()
        {
            if (Config.Instance.HideInMenu && CoreAPI.Game.IsInMenu)
            {
                Hide();
                return;
            }
            else
            {
                Show();
                _metaDataPlugin?.Update();
            }
        }
        void Show()
        {
            CardView.Visibility = Visibility.Visible;
            MulliganView.Visibility = Visibility.Visible;
        }

        void Hide()
        {
            CardView.Visibility = Visibility.Hidden;
            MulliganView.Visibility = Visibility.Hidden;
        }
    }
}
