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

        public bool DisableBarNormalization { get; set; }

        public double PercentNormalizationFactor
        {
            get
            {
                if (DisableBarNormalization)
                    return 1;

                var m = CardInfo.Select(dp => dp.CardDrawPercent).Max();
                return m <= 0 ? 0 : (double)(1 / m);
            }
        }


        public CardInfoViewModel()
        {

            // Check for design mode. 
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            { 
                for(int i = 0; i < 5; i++)
                {
                    CardInfo.Add(new CardInfoModel(i, (i + 1) * .1, (i + 1) * .1 + i * .1));
                }
                
            }
        }
    }
}
