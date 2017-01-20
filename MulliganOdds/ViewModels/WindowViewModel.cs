using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;
using Hearthstone_Deck_Tracker;
using HearthDb.Enums;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using Hearthstone_Deck_Tracker.Enums;

namespace DeckTrackerCustom
{

    public class WindowViewModel
    {
        public ObservableCollection<CardModel> CardInfo { get; set; }

        public ObservableCollection<CardTypeCountModel> CardTypeCount { get; set; }

        public ObservableCollection<MulliganOddsModel> MulliganCardOdds { get; set; }

        public ObservableCollection<string> ExtraInfo { get; set; }

        public WindowViewModel()
        {
            CardInfo = new ObservableCollection<CardModel>();
            CardTypeCount = new ObservableCollection<CardTypeCountModel>();
            MulliganCardOdds = new ObservableCollection<MulliganOddsModel>();
            ExtraInfo = new ObservableCollection<string>();

            // Check for design mode. 
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                for (int i = 0; i < 5; i++)
                {
                    CardInfo.Add(new CardModel(i, 0.5, 0.75));
                }
                CardTypeCount.Add(new CardTypeCountModel("Spell", 8, 30));
                CardTypeCount.Add(new CardTypeCountModel("Minions", 16, 30));

                MulliganCardOdds.Add(new MulliganOddsModel(0.2, 0.4, 0.6));
                MulliganCardOdds.Add(new MulliganOddsModel(0.2, 0.4, 0.6));
                MulliganCardOdds.Add(new MulliganOddsModel(0.2, 0.4, 0.6));
                MulliganCardOdds.Add(new MulliganOddsModel(0.2, 0.4, 0.6));
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

