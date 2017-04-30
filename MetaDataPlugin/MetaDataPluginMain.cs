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
 

namespace HDT.Plugins.Custom
{
    public class MetaDataPluginMain
    {

        #region Properties and Variables

        IEnumerable<Entity> EntitiesInHand
        {
            get
            {
                var eih = from e in CoreAPI.Game.Player.Hand orderby e.GetTag(GameTag.ZONE_POSITION) select e;

                if (eih.All(e => e.HasTag(GameTag.ZONE_POSITION)))
                    eih = eih.OrderBy(e => e.GetTag(GameTag.ZONE_POSITION));

                return eih;
            }
        }

        IEnumerable<Entity> EntitiesInHandNoCoin => EntitiesInHand.Where(c => c.Info.CardMark != CardMark.Coin);

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

        bool IsMulliganPending => CoreAPI.Game.PlayerEntity.GetTag(GameTag.MULLIGAN_STATE) == (int)Mulligan.INPUT;

        MulliganOddsView MulliganView { get; set; }
        CardInfoView CardView { get; set; }
        CardInfoViewModel CardInfoVM => (CardInfoViewModel)CardView.TryFindResource("CardInfoVM");
        MulliganOddsViewModel MulliganOddsVM => (MulliganOddsViewModel)MulliganView.TryFindResource("MulliganOddsVM");

        #endregion

        public MetaDataPluginMain(CardInfoView cv, MulliganOddsView mv)
        {
            CardView = cv;
            MulliganView = mv;
        }

        void HideAll()
        {
            CardView.Visibility = Visibility.Visible;
            MulliganView.Visibility = Visibility.Visible;
        }

        void ShowAll()
        {
            CardView.Visibility = Visibility.Hidden;
            MulliganView.Visibility = Visibility.Hidden;
        }

        public void GameStart()
        {
            CardView.Show();
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
            Reset();
            HideAll();
        }

        internal void PlayerMulligan(Card c)
        {
            UpdateCardInformation();
        }

        internal void PlayerPlay(Card c)
        {
            UpdateCardInformation();
        }

        internal void OpponentPlay(Card c)
        {

        }

        internal void Reset()
        {
            CardInfoVM.CardInfo.Clear();
            MulliganOddsVM.MulliganCardOdds.Clear();
            _mullUpdated = false;
        }

     

        void UpdateCardInformation()
        {
            if (CardInfoVM == null)
                return;

            CardInfoVM.CardInfo.Clear();

            double runningTotal = 0;
            foreach (KeyValuePair<int, int> kv in DeckCardCountByCost)
            {
                var equalOdds = DeckCostStats(kv.Key, ComparisonType.Equal, false) / DeckCardCount;
                runningTotal += equalOdds;

                var cm = new CardInfoModel(kv.Key, equalOdds, runningTotal);
                CardInfoVM.CardInfo.Add(cm);

            }

            if (IsMulliganPending)
            {
                MulliganView.Visibility = Visibility.Visible;
                UpdateMulliganData();
            }
            else if(CoreAPI.Game.IsMulliganDone)
            {
                MulliganView.Visibility = Visibility.Hidden;
            }


        }

        bool _mullUpdated = false;

        void UpdateMulliganData()
        {
            if (_mullUpdated)
                return;

            if (MulliganOddsVM == null)
                return;

            MulliganOddsVM.MulliganCardOdds.Clear();

            int cardNumber = 0;
            foreach (Entity e in EntitiesInHandNoCoin)
            {
                var c = e.Card;

                int cardsMulliganed = 1;
                var cardsAfterReshuffle = ((double)DeckCardCount + cardsMulliganed);

                var lowerOdds = DeckCostStats(c.Cost, ComparisonType.LessThan) / cardsAfterReshuffle;
                var equalOdds = DeckCostStats(c.Cost, ComparisonType.Equal, true) / cardsAfterReshuffle;
                var higherOdds = DeckCostStats(c.Cost, ComparisonType.GreaterThan) / cardsAfterReshuffle;
                var lowerEqualOdds = DeckCostStats(c.Cost, ComparisonType.LessThanEqual) / cardsAfterReshuffle;
                var higherEqualOdds = DeckCostStats(c.Cost, ComparisonType.GreaterThanEqual) / cardsAfterReshuffle;

                var mom = new MulliganOddsModel(cardNumber, Helpers.ToPercentString(lowerOdds), Helpers.ToPercentString(equalOdds), Helpers.ToPercentString(higherOdds));
                MulliganOddsVM.MulliganCardOdds.Add(mom);
            }
            _mullUpdated = true;
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
