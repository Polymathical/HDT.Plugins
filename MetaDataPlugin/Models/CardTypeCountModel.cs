
using System;
using System.Globalization;

namespace HDT.Plugins.Custom.Models
{
    public class CardTypeCountModel : BindableBase
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

        public CardTypeCountModel(string cardTypeName, int cardCount, int deckCardCount)
        {
            CardTypeName = cardTypeName;
            CardTypeCount = cardCount;
            DeckCardCount = deckCardCount;
        }

    }
}