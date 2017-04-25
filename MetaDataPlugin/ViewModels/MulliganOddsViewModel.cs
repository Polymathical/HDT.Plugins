using HDT.Plugins.Custom.Models;
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
    public class MulliganOddsViewModel : BindableBase
    {
        public ObservableCollection<MulliganOddsModel> MulliganCardOdds { get; set; } = new ObservableCollection<MulliganOddsModel>();

        public MulliganOddsViewModel()
        {
            // Check for design mode. 
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                MulliganCardOdds.Add(new MulliganOddsModel("16%", "12%", "72%"));
                MulliganCardOdds.Add(new MulliganOddsModel("16%", "12%", "72%"));
                MulliganCardOdds.Add(new MulliganOddsModel("16%", "12%", "72%"));
                MulliganCardOdds.Add(new MulliganOddsModel("16%", "12%", "72%"));
            }
        }
    }
}
