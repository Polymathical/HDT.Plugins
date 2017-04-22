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

    public class WindowViewModel
    {
        public ObservableCollection<CardViewModel> CardInfo { get; set; } = new ObservableCollection<CardViewModel>();

        public ObservableCollection<CardTypeCountViewModel> CardTypeCount { get; set; } = new ObservableCollection<CardTypeCountViewModel>();

        public ObservableCollection<MulliganOddsViewModel> MulliganCardOdds { get; set; } = new ObservableCollection<MulliganOddsViewModel>();

        public ObservableCollection<string> ExtraInfo { get; set; } = new ObservableCollection<string>();

        public PlayerQuestViewModel LocalPlayerQuestProgress { get; set; } = new PlayerQuestViewModel();
        public PlayerQuestViewModel OpponentQuestProgress { get; set; } = new PlayerQuestViewModel();

        public WindowViewModel()
        {

            // Check for design mode. 
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                for (int i = 0; i < 5; i++)
                {
                    CardInfo.Add(new CardViewModel(i, "50%", "75%"));
                }
                CardTypeCount.Add(new CardTypeCountViewModel("Spell", 8, 30));
                CardTypeCount.Add(new CardTypeCountViewModel("Minions", 16, 30));

                MulliganCardOdds.Add(new MulliganOddsViewModel("16%", "12%", "72%"));
                MulliganCardOdds.Add(new MulliganOddsViewModel("16%", "12%", "72%"));
                MulliganCardOdds.Add(new MulliganOddsViewModel("16%", "12%", "72%"));
                MulliganCardOdds.Add(new MulliganOddsViewModel("16%", "12%", "72%"));

                ExtraInfo.Add("Extra Line 1");
                ExtraInfo.Add("Extra Line 2");

                LocalPlayerQuestProgress.Set("Player Quest", 0, 4);
                OpponentQuestProgress.Set("Opponent Quest", 0, 7);
            }
        }

        public void Clear()
        {
            CardInfo.Clear();
            CardTypeCount.Clear();
            MulliganCardOdds.Clear();
            ExtraInfo.Clear();
        }

    }
}

