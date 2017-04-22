using HearthDb.Enums;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;

namespace HDT.Plugins.Custom
{
    public static class Helpers
    {
        public static IEnumerable<Entity> EntitiesInHand
        {
            get
            {
                var eih = from e in CoreAPI.Game.Player.Hand where e.Info.CardMark != CardMark.Coin orderby e.GetTag(GameTag.ZONE_POSITION) select e;

                if (eih.All(e => e.HasTag(HearthDb.Enums.GameTag.ZONE_POSITION)))
                    eih = eih.OrderBy(e => e.GetTag(GameTag.ZONE_POSITION));

                return eih;
            }
        }

        public static int DeckCardCount => CoreAPI.Game.Player.PlayerCardList.Select(c => c.Count).Sum();

        public static string ToPercentString(double d, int precision = 0, bool includePercentSign = true)
        {
            var nfi = new NumberFormatInfo()
            {
                PercentDecimalDigits = precision
            };

            nfi.PercentSymbol = includePercentSign ? nfi.PercentSymbol : String.Empty;
            nfi.PercentPositivePattern = 1;

            return String.Format(nfi, "{0:P}", d);
        }

        public static List<Card> PlayerCardList => CoreAPI.Game.Player.PlayerCardList;

        public static IDictionary<int, int> DeckCardCountByCost
        {
            get
            {
                var ccbc = new SortedDictionary<int, int>();

                foreach (Card c in PlayerCardList)
                {
                    if (c.Count == 0)
                        continue;

                    if (ccbc.ContainsKey(c.Cost))
                        ccbc[c.Cost] += c.Count;
                    else
                        ccbc.Add(c.Cost, c.Count);
                }

                return ccbc;
            }
        }

        public static double DeckCostStats(int cost, ComparisonType comparisonType, bool countAsAddedBack = false)
        {
            double retValue = 0;

            if (comparisonType == ComparisonType.LessThan)
                retValue = (from d in Helpers.DeckCardCountByCost where d.Key < cost select d.Value).Sum();
            else if (comparisonType == ComparisonType.GreaterThan)
                retValue = (from d in Helpers.DeckCardCountByCost where d.Key > cost select d.Value).Sum();
            else if (comparisonType == ComparisonType.Equal)
                retValue = (from d in Helpers.DeckCardCountByCost where d.Key == cost select d.Value).Sum() + (countAsAddedBack ? 1 : 0);
            else if (comparisonType == ComparisonType.LessThanEqual)
                retValue = (from d in Helpers.DeckCardCountByCost where d.Key <= cost select d.Value).Sum();
            else if (comparisonType == ComparisonType.GreaterThanEqual)
                retValue = (from d in Helpers.DeckCardCountByCost where d.Key >= cost select d.Value).Sum();
            else if (comparisonType == ComparisonType.GreaterThan)
                retValue = (from d in Helpers.DeckCardCountByCost where d.Key < cost select d.Value).Sum();


            return retValue;
        }
    }

    public enum ComparisonType { LessThan, LessThanEqual, Equal, GreaterThanEqual, GreaterThan };
}
