using HDT.Plugins.Custom.Models;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;

namespace HDT.Plugins.Custom.ViewModels
{
    public class CardTypeCountViewModel : BindableBase
    {
        public ObservableCollection<CardTypeCountModel> CardTypeCount { get; set; } = new ObservableCollection<CardTypeCountModel>();

        public CardTypeCountViewModel()
        {
            // Check for design mode. 
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                CardTypeCount.Add(new CardTypeCountModel("Spell", 8, 30));
                CardTypeCount.Add(new CardTypeCountModel("Minions", 16, 30));
            }
        }

       
    }
}
