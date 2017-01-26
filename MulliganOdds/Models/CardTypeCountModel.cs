
namespace HDT.Plugins.Custom.Models
{
    public class CardTypeCountModel 
    {
        public string CardTypeName { get; set; }
        public int CardCount { get; set; }
        public int DeckCardCount { get; set; }

        public CardTypeCountModel(string cardTypeName, int count, int deckCardCount)
        {
            CardTypeName = cardTypeName;
            CardCount = count;
            DeckCardCount = deckCardCount;
        }
    }
}