using HDT.Plugins.Custom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Custom.ViewModels
{
    public class MulliganOddsViewModel : ViewModelBase
    {
        private string _lowerOdds = String.Empty;
        private string _equalOdds = String.Empty;
        private string _higherOdds = String.Empty;

        public string LowerOdds { get { return _lowerOdds; } set { Set(ref _lowerOdds, value); } }
        public string EqualOdds { get { return _equalOdds; } set { Set(ref _equalOdds, value); } }
        public string HigherOdds { get { return _higherOdds; } set { Set(ref _higherOdds, value); } }

        public MulliganOddsViewModel(string lowerOdds, string equalOdds, string higherOdds)
        {
            LowerOdds = lowerOdds;
            EqualOdds = equalOdds;
            HigherOdds = higherOdds;
        }

        public MulliganOddsViewModel(MulliganOddsModel m)
        {
            LowerOdds = String.Format("{0:P0}", m.LowerOdds);
            EqualOdds = String.Format("{0:P0}", m.EqualOdds);
            HigherOdds = String.Format("{0:P0}", m.HigherOdds);
        }
    }
}
