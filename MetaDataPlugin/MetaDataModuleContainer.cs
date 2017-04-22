using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;
using System.Collections.ObjectModel;
using HearthDb.Enums;
using System.ComponentModel;
using System.Windows;
using System.Text.RegularExpressions;
using HDT.Plugins.Custom.Models;
using HDT.Plugins.Custom.ViewModels;
using Hearthstone_Deck_Tracker.Utility.Logging;
using HDT.Plugins.Custom.Modules;

namespace HDT.Plugins.Custom
{
    public class MetaDataModuleContainer
    {

        internal List<IModule> Modules = new List<IModule>();

        internal MainView _dispView = new MainView();
        internal MainViewModel _mainViewModel = new MainViewModel();

        bool ShouldHide { get { return Config.Instance.HideInMenu && CoreAPI.Game.IsInMenu; } }

        public MetaDataModuleContainer()
        {
            if (ShouldHide)
                _dispView?.Hide();
            else
                _dispView?.Show();

            _dispView.DataContext = _mainViewModel;
        }


        public void GameStart()
        {
            _dispView?.Show();
            UpdateAllModules();

        }

        internal void TurnStart(ActivePlayer player)
        {
            UpdateAllModules();
        }

        internal void Update()
        {
            UpdateAllModules();
        }

        internal void PlayerDraw(Card c)
        {
            UpdateAllModules();
        }

        internal void GameEnd()
        {
            Modules.Clear();
            _dispView?.Hide();
        }

        internal void PlayerMulligan(Card c)
        {
            UpdateAllModules();
        }

        internal void PlayerPlay(Card c)
        {
            UpdateAllModules();
        }

        internal void OpponentPlay(Card c)
        {
            UpdateAllModules();
        }

        void UpdateAllModules()
        {
            foreach (IModule m in Modules)
            {
                m.Update();
            }

            _mainViewModel.CardInfo.Add(new CardInfoViewModel(_cardCostModule.CardInfoModel));
            _mainViewModel.CardTypeCount.Add(new CardTypeCountViewModel(_cardTypeCountModule.MinionModel));
            _mainViewModel.DamageView.Add(new DamageViewModel(_damageModule.DamageModel));
            _mainViewModel.MulliganCardOdds.Add(new MulliganOddsViewModel(_mulliganOddsModule.MulliganOddsModel));
            _mainViewModel.LocalPlayerQuestProgress = new PlayerQuestViewModel(_questTrackerModule.LocalPlayerQuestModel, true);
            _mainViewModel.OpponentQuestProgress = new PlayerQuestViewModel(_questTrackerModule.OpponentQuestModel, false);
        }

        CardCostModule _cardCostModule = new CardCostModule();
        CardTypeCountModule _cardTypeCountModule = new CardTypeCountModule();
        DamageModule _damageModule = new DamageModule();
        MulliganOddsModule _mulliganOddsModule = new MulliganOddsModule();
        QuestTrackerModule _questTrackerModule = new QuestTrackerModule();

        void AddModules()
        {
            Modules.AddRange(new IModule[] { _cardCostModule, _cardTypeCountModule, _damageModule, _mulliganOddsModule, _questTrackerModule });
        }

    }
}
