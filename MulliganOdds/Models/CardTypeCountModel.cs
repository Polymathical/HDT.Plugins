

namespace DeckTrackerCustom
{
    public class CardTypeCountModel
    {
        public string CardTypeName { get; set; }
        public int CardCount { get; set; }
        public double CardCountPercent { get { return CardCount / (double)_deckCardCount; } }
        private int _deckCardCount { get; set; }

        public CardTypeCountModel(string cardTypeName, int count, int deckCardCount)
        {
            CardTypeName = cardTypeName;
            CardCount = count;
            _deckCardCount = deckCardCount;
        }
    }
}