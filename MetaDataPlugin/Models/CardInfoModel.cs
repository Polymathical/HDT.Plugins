
using System;

namespace HDT.Plugins.Custom.Models
{
    public class CardInfoModel : BindableBase
    {
        private int _cardCost;
        private string _cardDrawPercent = String.Empty;
        private string _cardDrawRunningTotal = String.Empty;

        public int CardCost { get { return _cardCost; } set { Set(ref _cardCost, value); } }
        public string CardDrawPercent { get { return _cardDrawPercent; } set { Set(ref _cardDrawPercent, value); } }
        public string CardDrawRunningTotal { get { return _cardDrawRunningTotal; } set { Set(ref _cardDrawRunningTotal, value); } }

        public CardInfoModel(int cost, string drawPercent, string runningTotal)
        {
            CardCost = cost;
            CardDrawPercent = drawPercent;
            CardDrawRunningTotal = runningTotal;
        }

    }
   
}