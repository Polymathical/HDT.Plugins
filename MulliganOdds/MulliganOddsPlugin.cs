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

namespace DeckTrackerCustom
{

    public class MulliganOddsPlugin : IPlugin
    {
        private MulliganOdds _mulliganOdds;
        private MulliganOddsWindow _cardInfoView;

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
                return Assembly.GetAssembly(typeof(MulliganOddsPlugin)).GetName().Version;
            }
        }

        public object CoreAPI { get; private set; }

        public void OnLoad()
        {
            _cardInfoView = new MulliganOddsWindow();
            _mulliganOdds = new MulliganOdds(_cardInfoView);

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
              //  _mulliganOdds?.Update();
            }
        }
    }
}
