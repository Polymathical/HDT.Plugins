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
    public class CardInfoViewModel : ViewModelBase
    {
        private int _cardCost;
        private string _cardDrawPercent = String.Empty;
        private string _cardDrawRunningTotal = String.Empty;

        public int CardCost { get { return _cardCost; } set { Set(ref _cardCost, value); } }
        public string CardDrawPercent { get { return _cardDrawPercent; } set { Set(ref _cardDrawPercent, value); } }
        public string CardDrawRunningTotal { get { return _cardDrawRunningTotal; } set { Set(ref _cardDrawRunningTotal, value); } }
 
       
        public CardInfoViewModel(int cost, string drawPercent, string runningTotal)
        {
            CardCost = cost;
            CardDrawPercent = drawPercent;
            CardDrawRunningTotal = runningTotal;
        }
 
        public CardInfoViewModel(CardInfoModel m)
        {
            CardCost = m.CardCost;
            CardDrawPercent = String.Format("{0:P0}", m.CardDrawPercent);
            CardDrawRunningTotal = String.Format("{0:P0}", m.CardDrawRunningTotal);
        }
    }
}
