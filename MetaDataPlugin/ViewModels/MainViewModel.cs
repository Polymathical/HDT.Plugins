using HDT.Plugins.Custom.ViewModels;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HDT.Plugins.Custom.ViewModels
{

    public class MainViewModel
    {
        public ObservableCollection<CardInfoViewModel> CardInfo { get; set; } = new ObservableCollection<CardInfoViewModel>();
        public ObservableCollection<CardTypeCountViewModel> CardTypeCount { get; set; } = new ObservableCollection<CardTypeCountViewModel>();
        public ObservableCollection<MulliganOddsViewModel> MulliganCardOdds { get; set; } = new ObservableCollection<MulliganOddsViewModel>();
        public ObservableCollection<DamageViewModel> DamageView { get; set; } = new ObservableCollection<DamageViewModel>();
        public PlayerQuestViewModel LocalPlayerQuestProgress { get; set; } = new PlayerQuestViewModel();
        public PlayerQuestViewModel OpponentQuestProgress { get; set; } = new PlayerQuestViewModel();
        public ObservableCollection<string> QuestTriggersList { get; set; } = new ObservableCollection<string>();

        public MainViewModel()
        {
            // Check for design mode. 
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                PopulateDesignerDefaultValues();
            }
        }

        private void PopulateDesignerDefaultValues()
        {
            for (int i = 0; i < 5; i++)
            {
                CardInfo.Add(new CardInfoViewModel(i, "50%", "75%"));
            }
            CardTypeCount.Add(new CardTypeCountViewModel("Spell", 8, 30));
            CardTypeCount.Add(new CardTypeCountViewModel("Minions", 16, 30));

            MulliganCardOdds.Add(new MulliganOddsViewModel("16%", "12%", "72%"));
            MulliganCardOdds.Add(new MulliganOddsViewModel("16%", "12%", "72%"));
            MulliganCardOdds.Add(new MulliganOddsViewModel("16%", "12%", "72%"));
            MulliganCardOdds.Add(new MulliganOddsViewModel("16%", "12%", "72%"));

            DamageView.Add(new DamageViewModel(1, 2, 3));
            DamageView.Add(new DamageViewModel(2, 3, 4));

            LocalPlayerQuestProgress.Set("Player Quest", 0, 4);
            OpponentQuestProgress.Set("Opponent Quest", 0, 7);
        }

        public void Clear()
        {
            CardInfo.Clear();
            CardTypeCount.Clear();
            MulliganCardOdds.Clear();
            DamageView.Clear();
           
        }

    }
}

