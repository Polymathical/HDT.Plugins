using HDT.Plugins.Custom.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HDT.Plugins.Custom;

namespace HDT.Plugins.Custom.ViewModels
{
    public class CardInfoViewModel : BindableBase
    {
        public ObservableCollection<CardInfoModel> CardInfo { get; set; } = new ObservableCollection<CardInfoModel>();

        public CardInfoViewModel()
        {
            // Check for design mode. 
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                for (int i = 0; i < 5; i++)
                {
                    CardInfo.Add(new CardInfoModel(i, .5, .75));
                }
            }
        }
    }
}
