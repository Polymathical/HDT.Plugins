
using System;

namespace HDT.Plugins.Custom.Models
{

    public class MulliganOddsModel : BindableBase 
    {
        private string _lowerOdds = String.Empty;
        private string _equalOdds = String.Empty;
        private string _higherOdds = String.Empty;

        public int CardNumber { get; set; } = 0;
        public string LowerOdds { get { return _lowerOdds; } set { Set(ref _lowerOdds, value); } }
        public string EqualOdds { get { return _equalOdds; } set { Set(ref _equalOdds, value); } }
        public string HigherOdds { get { return _higherOdds; } set { Set(ref _higherOdds, value); } }

        public MulliganOddsModel(int cardNumber, string lowerOdds, string equalOdds, string higherOdds)
        {
            cardNumber = CardNumber;
            LowerOdds = lowerOdds;
            EqualOdds = equalOdds;
            HigherOdds = higherOdds;
        }

        public MulliganOddsModel()
        {
            if (Helpers.InDesignMode)
            {
                CardNumber = 1;
                LowerOdds = "10%";
                EqualOdds = "20%";
                HigherOdds = "70%";
            }
        }
    }
}