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
    public class CardInfoViewModel : ViewModelBase
    {
        private int _cardCost;
        private string _cardDrawPercent = String.Empty;
        private string _cardDrawRunningTotal = String.Empty;

        public int CardCost { get { return _cardCost; } set { Set(ref _cardCost, value, nameof(CardCost)); } }
        public string CardDrawPercent { get { return _cardDrawPercent; } set { Set(ref _cardDrawPercent, value, nameof(CardDrawPercent)); } }
        public string CardDrawRunningTotal { get { return _cardDrawRunningTotal; } set { Set(ref _cardDrawRunningTotal, value, nameof(CardDrawRunningTotal)); } }
 
       
        public CardInfoViewModel(int cost, string drawPercent, string runningTotal)
        {
            CardCost = cost;
            CardDrawPercent = drawPercent;
            CardDrawRunningTotal = runningTotal;
        }
 
        public CardInfoViewModel(CardInfoModel m)
        {
            CardCost = m.CardCost;
         
            CardDrawPercent = Helpers.ToPercentString(m.CardDrawPercent, 0);
            CardDrawRunningTotal = Helpers.ToPercentString(m.CardDrawRunningTotal);
        }
    }
}
