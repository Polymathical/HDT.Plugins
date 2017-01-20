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

namespace DeckTrackerCustom
{
    public class MulliganOdds
    {
        internal MulliganOddsWindow _dispControl;
        internal WindowViewModel MainWindowViewModel { get; set; }

        IEnumerable<Entity> EntitiesInHand
        {
            get
            {
                var eih = from e in Core.Game.Player.Hand where e.Info.CardMark != CardMark.Coin orderby e.GetTag(GameTag.ZONE_POSITION) select e;

                if (eih.All(e => e.HasTag(GameTag.ZONE_POSITION)))
                    eih = EntitiesInHand.OrderBy(e => e.GetTag(GameTag.ZONE_POSITION));

                return eih;
            }
        }

        enum ComparisonType { LessThan, LessThanEqual, Equal, GreaterThanEqual, GreaterThan };

        int DeckCardCount => CoreAPI.Game.Player.PlayerCardList.Select(c => c.Count).Sum();

        IDictionary<int, int> DeckCardCountByCost
        {
            get
            {
                var ccbc = new SortedDictionary<int, int>();

                foreach (Card c in CoreAPI.Game.Player.PlayerCardList)
                {
                    if (ccbc.ContainsKey(c.Cost))
                        ccbc[c.Cost] += c.Count;
                    else
                        ccbc.Add(c.Cost, c.Count);
                }

                return ccbc;
            }
        }

        public MulliganOdds(MulliganOddsWindow displayControl)
        {
            _dispControl = displayControl;
            MainWindowViewModel = new WindowViewModel();
            _dispControl.DataContext = MainWindowViewModel;

            if (Config.Instance.HideInMenu && CoreAPI.Game.IsInMenu)
                _dispControl?.Hide();
            else
                _dispControl?.Show();
        }

        public void GameStart()
        {
            _dispControl?.Show();
            UpdateCardInformation();
        }

        internal void TurnStart(ActivePlayer player)
        {
            UpdateCardInformation();
        }

        internal void Update()
        {
            if (CoreAPI.Game.IsRunning)
            {
                UpdateCardInformation();
            }
            else
                _dispControl?.Hide();
        }

        internal void PlayerDraw(Card c)
        {
            if (CoreAPI.Game.Player.IsLocalPlayer)
                UpdateCardInformation();
        }

        internal void GameEnd()
        {
            if (Config.Instance.HideInMenu && CoreAPI.Game.IsInMenu)
                _dispControl?.Hide();
        }

        internal void PlayerMulligan(Card c)
        {
            UpdateCardInformation();
        }

        private void UpdateCardMetaData()
        {
            int spellCount = 0;
            int mininionCount = 0;

            foreach (Card c in CoreAPI.Game.Player.PlayerCardList)
            {
                if (c.Type == "Spell")
                {
                    spellCount += c.Count;
                }

                if (c.Type == "Minion")
                {
                    mininionCount += c.Count;
                }
            }

            MainWindowViewModel.CardTypeCount.Add(new CardTypeCountModel("Spells", spellCount, DeckCardCount));
            MainWindowViewModel.CardTypeCount.Add(new CardTypeCountModel("Minions", mininionCount, DeckCardCount));
        }

        public void UpdateCardInformation()
        {
            MainWindowViewModel.Clear();

            double runningTotal = 0;
            foreach (KeyValuePair<int, int> kv in DeckCardCountByCost)
            {
                var equalOdds = DeckCostStats(kv.Key, ComparisonType.Equal, false) / DeckCardCount;
                runningTotal += equalOdds;

                MainWindowViewModel.CardInfo.Add(new CardModel(kv.Key, equalOdds, runningTotal));

            }

            if (Core.Game.IsMulliganDone == false)
            {
                UpdateMulliganData();
            }
            

            UpdateCardMetaData();
        }

        void UpdateMulliganData()
        {
            var entitiesInHand = from e in Core.Game.Player.Hand where e.Info.CardMark != CardMark.Coin select e;

            if (entitiesInHand.All(e => e.HasTag(GameTag.ZONE_POSITION)))
                entitiesInHand = entitiesInHand.OrderBy(e => e.GetTag(GameTag.ZONE_POSITION));

            foreach (Entity e in entitiesInHand)
            {
                var c = e.Card;

                int cardsMulliganed = 1;
                var cardsAfterReshuffle = ((double)DeckCardCount + cardsMulliganed);

                var lowerOdds = DeckCostStats(c.Cost, ComparisonType.LessThan) / cardsAfterReshuffle;
                var equalOdds = DeckCostStats(c.Cost, ComparisonType.Equal, true) / cardsAfterReshuffle;
                var higherOdds = DeckCostStats(c.Cost, ComparisonType.GreaterThan) / cardsAfterReshuffle;
                var lowerEqualOdds = DeckCostStats(c.Cost, ComparisonType.LessThanEqual) / cardsAfterReshuffle;
                var higherEqualOdds = DeckCostStats(c.Cost, ComparisonType.GreaterThanEqual) / cardsAfterReshuffle;

                MainWindowViewModel.MulliganCardOdds.Add(new MulliganOddsModel(lowerOdds, equalOdds, higherOdds));
            }
        }

        private double DeckCostStats(int cost, ComparisonType comparisonType, bool countAsAddedBack = false)
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
