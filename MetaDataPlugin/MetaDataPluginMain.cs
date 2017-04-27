using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;
using System.Collections.ObjectModel;
using HearthDb.Enums;
using System.ComponentModel;
using System.Windows;
using System.Text.RegularExpressions;
using HDT.Plugins.Custom.Models;
using HDT.Plugins.Custom.ViewModels;
using Hearthstone_Deck_Tracker.Utility.Logging;
using HDT.Plugins.Custom.Controls;
using HDT.Plugins.Custom.Views;

namespace HDT.Plugins.Custom
{
    public class MetaDataPluginMain
    {

        #region Properties and Variables

        IEnumerable<Entity> EntitiesInHand
        {
            get
            {
                var eih = from e in CoreAPI.Game.Player.Hand where e.Info.CardMark != CardMark.Coin orderby e.GetTag(GameTag.ZONE_POSITION) select e;

                if (eih.All(e => e.HasTag(GameTag.ZONE_POSITION)))
                    eih = eih.OrderBy(e => e.GetTag(GameTag.ZONE_POSITION));

                return eih;
            }
        }

        enum ComparisonType { LessThan, LessThanEqual, Equal, GreaterThanEqual, GreaterThan };

        int DeckCardCount => CoreAPI.Game.Player.PlayerCardList.Select(c => c.Count).Sum();

        bool ShouldHide { get { return Config.Instance.HideInMenu && CoreAPI.Game.IsInMenu; } }

        List<Card> PlayerCardList => CoreAPI.Game.Player.PlayerCardList;

        IDictionary<int, int> DeckCardCountByCost
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

        #endregion

        CompositeView MainView { get; set; }
        CardInfoViewModel CardInfoVM { get; set; }
        MulliganOddsViewModel MulliganOddsVM { get; set; }

        public MetaDataPluginMain(CompositeView mv)
        {
            MainView = mv;
            CardInfoVM = (CardInfoViewModel)MainView.TryFindResource("CardInfoVM");
            MulliganOddsVM = (MulliganOddsViewModel)MainView.TryFindResource("MulliganOddsVM");
        }

        public void GameStart()
        {
            MainView.Show();
            UpdateCardInformation();
        }

        internal void TurnStart(ActivePlayer player)
        {
            UpdateCardInformation();
        }

        internal void Update()
        {
            UpdateCardInformation();
        }

        internal void PlayerDraw(Card c)
        {
            UpdateCardInformation();
        }

        internal void GameEnd()
        {
            MainView.Hide();
        }

        internal void PlayerMulligan(Card c)
        {
            UpdateCardInformation();
        }

        internal void PlayerPlay(Card c)
        {

        }

        internal void OpponentPlay(Card c)
        {

        }


        void UpdateCardInformation()
        {

            CardInfoVM.CardInfo.Clear();
            double runningTotal = 0;
            foreach (KeyValuePair<int, int> kv in DeckCardCountByCost)
            {
                var equalOdds = DeckCostStats(kv.Key, ComparisonType.Equal, false) / DeckCardCount;
                runningTotal += equalOdds;

                var cm = new CardInfoModel(kv.Key, Helpers.ToPercentString(equalOdds), Helpers.ToPercentString(runningTotal));
                CardInfoVM.CardInfo.Add(cm);

            }

            if (CoreAPI.Game.IsMulliganDone == false)
            {
                MainView.SetMulliganVisibility(Visibility.Visible);
                UpdateMulliganData();
            }
            else
            {
                MainView.SetMulliganVisibility(Visibility.Hidden);
            }
        }

        void UpdateMulliganData()
        {
            MulliganOddsVM.MulliganCardOdds.Clear();

            foreach (Entity e in EntitiesInHand)
            {
                var c = e.Card;

                int cardsMulliganed = 1;
                var cardsAfterReshuffle = ((double)DeckCardCount + cardsMulliganed);

                var lowerOdds = DeckCostStats(c.Cost, ComparisonType.LessThan) / cardsAfterReshuffle;
                var equalOdds = DeckCostStats(c.Cost, ComparisonType.Equal, true) / cardsAfterReshuffle;
                var higherOdds = DeckCostStats(c.Cost, ComparisonType.GreaterThan) / cardsAfterReshuffle;
                var lowerEqualOdds = DeckCostStats(c.Cost, ComparisonType.LessThanEqual) / cardsAfterReshuffle;
                var higherEqualOdds = DeckCostStats(c.Cost, ComparisonType.GreaterThanEqual) / cardsAfterReshuffle;

                var mom = new MulliganOddsModel(Helpers.ToPercentString(lowerOdds), Helpers.ToPercentString(equalOdds), Helpers.ToPercentString(higherOdds));
                MulliganOddsVM.MulliganCardOdds.Add(mom);
            }
        }

        double DeckCostStats(int cost, ComparisonType comparisonType, bool countAsAddedBack = false)
        {
            double retValue = 0;

            if (comparisonType == ComparisonType.LessThan)
                retValue = (from d in DeckCardCountByCost where d.Key < cost select d.Value).Sum();
            else if (comparisonType == ComparisonType.GreaterThan)
                retValue = (from d in DeckCardCountByCost where d.Key > cost select d.Value).Sum();
            else if (comparisonType == ComparisonType.Equal)
                retValue = (from d in DeckCardCountByCost where d.Key == cost select d.Value).Sum() + (countAsAddedBack ? 1 : 0);
            else if (comparisonType == ComparisonType.LessThanEqual)
                retValue = (from d in DeckCardCountByCost where d.Key <= cost select d.Value).Sum();
            else if (comparisonType == ComparisonType.GreaterThanEqual)
                retValue = (from d in DeckCardCountByCost where d.Key >= cost select d.Value).Sum();
            else if (comparisonType == ComparisonType.GreaterThan)
                retValue = (from d in DeckCardCountByCost where d.Key < cost select d.Value).Sum();


            return retValue;
        }

    }
}
