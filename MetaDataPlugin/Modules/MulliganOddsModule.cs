using HDT.Plugins.Custom.Models;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Custom.Modules
{
    internal class MulliganOddsModule : IModule
    {

        public MulliganOddsModel MulliganOddsModel { get; private set; }
        public string[] ModelName => new string[] { nameof(MulliganOddsModel) };
        public object[] Model => new MulliganOddsModel[] { MulliganOddsModel };

        public MulliganOddsModule() { }

        void UpdateMulliganData()
        {
            foreach (Entity e in Helpers.EntitiesInHand)
            {
                var c = e.Card;

                int cardsMulliganed = 1;
                var cardsAfterReshuffle = ((double)Helpers.DeckCardCount + cardsMulliganed);

                var lowerOdds = Helpers.DeckCostStats(c.Cost, ComparisonType.LessThan) / cardsAfterReshuffle;
                var equalOdds = Helpers.DeckCostStats(c.Cost, ComparisonType.Equal, true) / cardsAfterReshuffle;
                var higherOdds = Helpers.DeckCostStats(c.Cost, ComparisonType.GreaterThan) / cardsAfterReshuffle;
                var lowerEqualOdds = Helpers.DeckCostStats(c.Cost, ComparisonType.LessThanEqual) / cardsAfterReshuffle;
                var higherEqualOdds = Helpers.DeckCostStats(c.Cost, ComparisonType.GreaterThanEqual) / cardsAfterReshuffle;

                MulliganOddsModel = new MulliganOddsModel(lowerOdds, equalOdds, higherOdds);
               // MainWindowViewModel.MulliganCardOdds.Add(new ViewModels.MulliganOddsViewModel(MulliganOddsModel));
            }
        }

        public void Update()
        {
            UpdateMulliganData();
        }
    }
}
