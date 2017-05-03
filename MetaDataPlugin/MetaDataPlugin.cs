﻿using System;
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
using System.ComponentModel;

namespace HDT.Plugins.Custom
{

    public class MetaDataPlugin : IPlugin
    {
        private MetaDataPluginMain _metaDataPlugin;
        CardInfoView _cv;
        MulliganOddsView _mv;

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
            _cv = new CardInfoView();
            _mv = new MulliganOddsView();
            _cv.Hide();
            _mv.Hide();

            _metaDataPlugin = new MetaDataPluginMain(_cv, _mv);

            CoreAPI.OverlayCanvas.Children.Add(_cv);
            CoreAPI.OverlayCanvas.Children.Add(_mv);

            _cv.UpdatePosition(new object(), new EventArgs());

            GameEvents.OnGameStart.Add(_metaDataPlugin.GameStart);
            GameEvents.OnOpponentPlay.Add(_metaDataPlugin.OpponentPlay);
            GameEvents.OnPlayerPlay.Add(_metaDataPlugin.PlayerPlay);
            GameEvents.OnTurnStart.Add(_metaDataPlugin.TurnStart);
            GameEvents.OnPlayerDraw.Add(_metaDataPlugin.PlayerDraw);
            GameEvents.OnPlayerMulligan.Add(_metaDataPlugin.PlayerMulligan);
            GameEvents.OnGameEnd.Add(_metaDataPlugin.GameEnd);

          
        }

        public void OnUnload()
        {
            CoreAPI.OverlayCanvas.Children.Remove(_cv);
            CoreAPI.OverlayCanvas.Children.Remove(_mv);
            _cv = null;
            _mv = null;
            _metaDataPlugin = null;
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
            _mv.Visibility = _cv.Visibility = Visibility.Visible;
        }

        void Hide()
        {
            _mv.Visibility = _cv.Visibility = Visibility.Hidden;
        }
    }
}
