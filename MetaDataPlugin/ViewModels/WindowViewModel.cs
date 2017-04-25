using HDT.Plugins.Custom.Models;
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
        public CardInfoViewModel CardInfoVM { get; set; } = new CardInfoViewModel();

        public CardTypeCountViewModel CardTypeCountVM { get; set; } = new CardTypeCountViewModel();

        public MulliganOddsViewModel MulliganOddsVM { get; set; } = new MulliganOddsViewModel();

        public ObservableCollection<string> ExtraInfo { get; set; } = new ObservableCollection<string>();

        public PlayerQuestViewModel LocalPlayerQuestProgress { get; set; } = new PlayerQuestViewModel();
        public PlayerQuestViewModel OpponentQuestProgress { get; set; } = new PlayerQuestViewModel();

        public WindowViewModel()
        {

            // Check for design mode. 
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {

                ExtraInfo.Add("Extra Line 1");
                ExtraInfo.Add("Extra Line 2");

                LocalPlayerQuestProgress.Set("Player Quest", 0, 4);
                OpponentQuestProgress.Set("Opponent Quest", 0, 7);
            }
        }

        public void Clear()
        {
            CardInfoVM.CardInfo.Clear();
            CardTypeCountVM.CardTypeCount.Clear();
            MulliganOddsVM.MulliganCardOdds.Clear();
            ExtraInfo.Clear();
        }

    }
}

