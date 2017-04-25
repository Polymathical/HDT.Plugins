using HDT.Plugins.Custom.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Custom.ViewModels
{
    public class CardTypeCountViewModel : BindableBase
    {
        private string _cardTypeName = String.Empty;
        private int _cardTypeCount = 0;
        private int _deckCardCount = 0;
        public string CardTypeName { get { return _cardTypeName; } set { Set(ref _cardTypeName, value); } }

        public int CardTypeCount
        {
            get { return _cardTypeCount; }
            set
            {
                if (Set(ref _cardTypeCount, value))
                    RaiseChange(nameof(CardCountPercent));
            }
        }
        public int DeckCardCount
        {
            get { return _deckCardCount; }
            set
            {
                if (Set(ref _deckCardCount, value))
                    RaiseChange(nameof(CardCountPercent));
            }
        }


        public string CardCountPercent
        {
            get
            {
                var cardCountPercent = CardTypeCount / (double)DeckCardCount;

                var nfi = new NumberFormatInfo();
                nfi.PercentDecimalDigits = 0;
                nfi.PercentPositivePattern = 1;

                return String.Format(nfi, "{0:P}", cardCountPercent);
            }
        }

        public CardTypeCountViewModel(string cardTypeName, int cardCount, int deckCardCount)
        {
            CardTypeName = cardTypeName;
            CardTypeCount = cardCount;
            DeckCardCount = deckCardCount;
        }

        public CardTypeCountViewModel(CardTypeCountModel model)
        {
            CardTypeName = model.CardTypeName;
            CardTypeCount = model.CardCount;
            DeckCardCount = model.DeckCardCount;
        }



    }
}
