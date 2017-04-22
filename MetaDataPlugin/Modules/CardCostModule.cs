using HDT.Plugins.Custom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Custom.Modules
{
    public class CardCostModule : IModule
    {
        public CardInfoModel CardInfoModel { get; set; } = new CardInfoModel();

        public void UpdateDeckCardCostStatistics()
        {
            double runningTotal = 0;
            foreach (KeyValuePair<int, int> kv in Helpers.DeckCardCountByCost)
            {
                var equalOdds = Helpers.DeckCostStats(kv.Key, ComparisonType.Equal, false) / Helpers.DeckCardCount;
                runningTotal += equalOdds;

                CardInfoModel = new CardInfoModel(kv.Key, equalOdds, runningTotal);
            }
        }

        public void Update()
        {
            UpdateDeckCardCostStatistics();
        }
       
    }
}
