using HDT.Plugins.Custom.Models;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;

namespace HDT.Plugins.Custom.Modules
{
    public class CardTypeCountModule : IModule
    {
       
        public CardTypeCountModel SpellModel { get; private set; }
        public CardTypeCountModel MinionModel { get; private set; }

        public string[] ModelName => new string[] { nameof(SpellModel), nameof(MinionModel) };
        public object[] Model => new CardTypeCountModel[] { SpellModel, MinionModel };

        private void UpdateCardMetaData()
        {
            int spellCount = 0;
            int mininionCount = 0;

            foreach (Card c in CoreAPI.Game.Player.PlayerCardList)
            {
                if (c.Type == "Spell")
                    spellCount += c.Count;
                else if (c.Type == "Minion")
                    mininionCount += c.Count;
            }

            SpellModel = new CardTypeCountModel("Spells", spellCount, Helpers.DeckCardCount);
            MinionModel = new CardTypeCountModel("Minions", mininionCount, Helpers.DeckCardCount);

            // MainWindowViewModel.CardTypeCount.Add(new ViewModels.CardTypeCountViewModel(minionModel));

        }
        public void Update()
        {
            UpdateCardMetaData();
        }
    }
}


