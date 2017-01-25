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

namespace HDT.Plugins.Custom
{

    public class MetaDataPlugin : IPlugin
    {
        private MetaDataController _mulliganOdds;
        private MetaDataView _cardInfoView;

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
                return "Mulligan Odds";
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

        public object CoreAPI { get; private set; }

        public void OnLoad()
        {
            _cardInfoView = new MetaDataView();
            _mulliganOdds = new MetaDataController(_cardInfoView);

            if (Config.Instance.HideInMenu && Hearthstone_Deck_Tracker.API.Core.Game.IsInMenu)
                _cardInfoView.Hide();
            else
                _cardInfoView.Show();

            Hearthstone_Deck_Tracker.API.Core.OverlayCanvas.Children.Add(_cardInfoView);

            GameEvents.OnGameStart.Add(_mulliganOdds.GameStart);
            GameEvents.OnTurnStart.Add(_mulliganOdds.TurnStart);
            GameEvents.OnPlayerDraw.Add(_mulliganOdds.PlayerDraw);
            GameEvents.OnPlayerMulligan.Add(_mulliganOdds.PlayerMulligan);
            GameEvents.OnGameEnd.Add(_mulliganOdds.GameEnd);

        }

        public void OnUnload()
        {
            _cardInfoView.Hide();
            Hearthstone_Deck_Tracker.API.Core.OverlayCanvas.Children.Remove(_cardInfoView);
            _cardInfoView.Dispose();

            _cardInfoView = null;
            _mulliganOdds = null;
            _cardInfoView = null;
        }

        public void OnUpdate()
        {
            if (Config.Instance.HideInMenu && Hearthstone_Deck_Tracker.API.Core.Game.IsInMenu)
            {
                _cardInfoView.Hide();
                return;
            }
            else
            {
                _cardInfoView.Show();
                //Wait(100);
               // _mulliganOdds?.Update();
            }
        }

        async void Wait(int ms)
        {
            await Task.Delay(ms);
        }
    }
}
