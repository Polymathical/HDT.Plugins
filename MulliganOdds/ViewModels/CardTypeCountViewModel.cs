using HDT.Plugins.Custom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Custom.ViewModels
{
    public class CardTypeCountViewModel : ViewModelBase
    {
        private string _cardTypeName;
        private int _cardTypeCount;
        private int _deckCardCount;

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


        public double CardCountPercent { get { return CardTypeCount / (double)DeckCardCount; } }

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
