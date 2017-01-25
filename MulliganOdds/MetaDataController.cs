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

namespace HDT.Plugins.Custom
{
    public class MetaDataController
    {

        #region Properties and Variables

        internal MetaDataView _dispView;
        internal WindowViewModel MainWindowViewModel { get; set; }

        IEnumerable<Entity> EntitiesInHand
        {
            get
            {
                var eih = from e in Core.Game.Player.Hand where e.Info.CardMark != CardMark.Coin orderby e.GetTag(GameTag.ZONE_POSITION) select e;

                if (eih.All(e => e.HasTag(GameTag.ZONE_POSITION)))
                    eih = eih.OrderBy(e => e.GetTag(GameTag.ZONE_POSITION));

                return eih;
            }
        }

        enum ComparisonType { LessThan, LessThanEqual, Equal, GreaterThanEqual, GreaterThan };

        int DeckCardCount => CoreAPI.Game.Player.PlayerCardList.Select(c => c.Count).Sum();

        bool ShouldHide { get { return Config.Instance.HideInMenu && CoreAPI.Game.IsInMenu; } }


        IDictionary<int, int> DeckCardCountByCost
        {
            get
            {
                var ccbc = new SortedDictionary<int, int>();

                foreach (Card c in CoreAPI.Game.Player.PlayerCardList)
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

        public MetaDataController(MetaDataView displayView)
        {
            _dispView = displayView;
            MainWindowViewModel = new WindowViewModel();
            _dispView.DataContext = MainWindowViewModel;

            if (ShouldHide)
                _dispView?.Hide();
            else
                _dispView?.Show();
        }

        public void GameStart()
        {
            _dispView?.Show();
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
            _dispView?.Hide();
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
            UpdateHandDamageCounter();
        }

        void UpdateMulliganData()
        {
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

                MainWindowViewModel.MulliganCardOdds.Add(new MulliganOddsModel(lowerOdds, equalOdds, higherOdds));
            }
        }

        void UpdateHandDamageCounter()
        {
            if (Core.Game.Player.IsLocalPlayer == false)
                return;

            int sp = 0;
            foreach (Entity e in Core.Game.Player.Board)
            {
                if (e.HasTag(GameTag.SPELLPOWER) == false)
                    continue;

                sp += e.GetTag(GameTag.SPELLPOWER);
            }

            int primaryDamage = 0;
            int secondaryDamage = 0;
            int cardCount = 0;
            foreach (Entity e in EntitiesInHand)
            {
                if (_damageCards.Contains(e.CardId) == false)
                    continue;

                var c = e.Card;
                var spellDamageInfo = GetCardDirectDamage(e);

                if (spellDamageInfo == null)
                    continue;
                cardCount++;
                primaryDamage += spellDamageInfo.d1 + sp;
                secondaryDamage += spellDamageInfo.d2 + sp;
            }

            MainWindowViewModel.ExtraInfo.Add("Spell Dmg: " + primaryDamage + " (" + secondaryDamage + ") (C" + cardCount + ")");
        }

        static string[] _damageCards = new string[]
        {
            HearthDb.CardIds.Collectible.Rogue.SinisterStrike,
            HearthDb.CardIds.Collectible.Rogue.Eviscerate,
            HearthDb.CardIds.Collectible.Rogue.JadeShuriken,
            HearthDb.CardIds.Collectible.Rogue.Shiv
        };

        class SpellDamageInfo
        {

            public int d1 { get; set; }
            public int d2 { get; set; }

            public SpellDamageInfo(int damage, int altDamage)
            {
                d1 = damage;
                d2 = altDamage;
            }
        }
        SpellDamageInfo GetCardDirectDamage(Entity e)
        {
            if (e.CardId == HearthDb.CardIds.Collectible.Rogue.SinisterStrike)
                return new SpellDamageInfo(3, 3);
            else if (e.CardId == HearthDb.CardIds.Collectible.Rogue.Eviscerate)
                return new SpellDamageInfo(2, 4);
            else if (e.CardId == HearthDb.CardIds.Collectible.Rogue.JadeShuriken)
                return new SpellDamageInfo(2, 2);
            else if (e.CardId == HearthDb.CardIds.Collectible.Rogue.Shiv)
                return new SpellDamageInfo(1, 1);
            return null;
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
