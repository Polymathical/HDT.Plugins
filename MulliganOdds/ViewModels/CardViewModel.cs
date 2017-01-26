using HDT.Plugins.Custom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Custom.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        private int _cardCost;
        private double _cardDrawPercent;
        private double _cardDrawRunningTotal;

        public int CardCost { get { return _cardCost; } set { Set(ref _cardCost, value); } }
        public double CardDrawPercent { get { return _cardDrawPercent; } set { Set(ref _cardDrawPercent, value); } }
        public double CardDrawRunningTotal { get { return _cardDrawRunningTotal; } set { Set(ref _cardDrawRunningTotal, value); } }

        public CardViewModel(int cardCost, double cardDrawPercent, double cardDrawRunningTotal)
        {
            CardCost = cardCost;
            CardDrawPercent = cardDrawPercent;
            CardDrawRunningTotal = cardDrawRunningTotal;
        }

        public CardViewModel(CardModel m)
        {
            CardCost = m.CardCost;
            CardDrawPercent = m.CardDrawPercent;
            CardDrawRunningTotal = m.CardDrawRunningTotal;
        }
    }
}
