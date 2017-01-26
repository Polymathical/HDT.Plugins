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
        private double _lowerOdds;
        private double _equalOdds;
        private double _higherOdds;

        public double LowerOdds { get { return _lowerOdds; } set { Set(ref _lowerOdds, value); } } 
        public double EqualOdds { get { return _equalOdds; } set { Set(ref _equalOdds, value); } } 
        public double HigherOdds { get { return _higherOdds; } set { Set(ref _higherOdds, value); } } 

        public MulliganOddsViewModel(double lowerOdds, double equalOdds, double higherOdds)
        {
            LowerOdds = lowerOdds;
            EqualOdds = equalOdds;
            HigherOdds = higherOdds;
        }

        public MulliganOddsViewModel(MulliganOddsModel m)
        {
            LowerOdds = m.LowerOdds;
            EqualOdds = m.EqualOdds;
            HigherOdds = m.HigherOdds;
        }
    }
}
