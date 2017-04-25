using HDT.Plugins.Custom.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HDT.Plugins.Custom.ViewModels
{
    public class CardViewModel : BindableBase
    {
        private int _cardCost = 0;
        private string _cardDrawPercent = String.Empty;
        private string _cardDrawRunningTotal = String.Empty;

        public int CardCost { get { return _cardCost; } set { Set(ref _cardCost, value); } }
        public string CardDrawPercent { get { return _cardDrawPercent; } set { Set(ref _cardDrawPercent, value); } }
        public string CardDrawRunningTotal { get { return _cardDrawRunningTotal; } set { Set(ref _cardDrawRunningTotal, value); } }

        public CardViewModel(int cardCost, string cardDrawPercent, string cardDrawRunningTotal)
        {
            CardCost = cardCost;
            CardDrawPercent = cardDrawPercent;
            CardDrawRunningTotal = cardDrawRunningTotal;
        }

        public CardViewModel(CardInfoModel m)
        {
            CardCost = m.CardCost;
            CardDrawPercent = Helpers.ToPercentString(m.CardDrawPercent);
            CardDrawRunningTotal = Helpers.ToPercentString(m.CardDrawRunningTotal);
        }

        public CardViewModel()
        {
        }
    }
}
