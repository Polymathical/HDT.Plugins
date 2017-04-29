
using System;

namespace HDT.Plugins.Custom.Models
{
    public class CardInfoModel : BindableBase
    {
        private int _cardCost = 0;
        private double _cardDrawPercent = 0;
        private double _cardDrawRunningTotal = 0;

        public int CardCost { get { return _cardCost; } set { Set(ref _cardCost, value); } }
        public double CardDrawPercent { get { return _cardDrawPercent; } set { Set(ref _cardDrawPercent, value); } }
        public double CardDrawRunningTotal { get { return _cardDrawRunningTotal; } set { Set(ref _cardDrawRunningTotal, value); } }

        public string CardDrawPercentText { get { return Helpers.ToPercentString(_cardDrawPercent); } }
        public string CardDrawRunningTotalText { get { return Helpers.ToPercentString(_cardDrawRunningTotal); } }

        public CardInfoModel(int cost, double drawPercent, double runningTotal)
        {
            CardCost = cost;
            CardDrawPercent = drawPercent;
            CardDrawRunningTotal = runningTotal;
        }

        public CardInfoModel()
        {
            if(Helpers.InDesignMode)
            {
                CardCost = 1;
                CardDrawPercent = .5;
                CardDrawRunningTotal = .8;
            }
        }

    }

}