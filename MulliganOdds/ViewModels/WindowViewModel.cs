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

namespace HDT.Plugins.Custom
{

    public class WindowViewModel
    {
        public ObservableCollection<CardViewModel> CardInfo { get; set; }

        public ObservableCollection<CardTypeCountViewModel> CardTypeCount { get; set; }

        public ObservableCollection<MulliganOddsViewModel> MulliganCardOdds { get; set; }

        public ObservableCollection<string> ExtraInfo { get; set; }

        public WindowViewModel()
        {
            CardInfo = new ObservableCollection<CardViewModel>();
            CardTypeCount = new ObservableCollection<CardTypeCountViewModel>();
            MulliganCardOdds = new ObservableCollection<MulliganOddsViewModel>();
            ExtraInfo = new ObservableCollection<string>();

            // Check for design mode. 
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                for (int i = 0; i < 5; i++)
                {
                    CardInfo.Add(new CardViewModel(i, 0.5, 0.75));
                }
                CardTypeCount.Add(new CardTypeCountViewModel("Spell", 8, 30));
                CardTypeCount.Add(new CardTypeCountViewModel("Minions", 16, 30));

                MulliganCardOdds.Add(new MulliganOddsViewModel(0.2, 0.4, 0.6));
                MulliganCardOdds.Add(new MulliganOddsViewModel(0.2, 0.4, 0.6));
                MulliganCardOdds.Add(new MulliganOddsViewModel(0.2, 0.4, 0.6));
                MulliganCardOdds.Add(new MulliganOddsViewModel(0.2, 0.4, 0.6));
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

