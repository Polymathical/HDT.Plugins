using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone_Deck_Tracker.Plugins;
using System.Reflection;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker;
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;
using HearthDb.Enums;
using HDT.Plugins.Custom.Models;

namespace HDT.Plugins.Custom.Modules
{
    public class DamageModule : IModule
    {
        public DamageModel DamageModel { get; private set; }
    
        public DamageModule()
        {

        }

        SpellDamageInfo GetCardDirectDamage(Entity e)
        {
            if (e.CardId == HearthDb.CardIds.Collectible.Rogue.SinisterStrike)
                return new SpellDamageInfo(3, 3);
            else if (e.CardId == HearthDb.CardIds.Collectible.Rogue.Eviscerate)
                return new SpellDamageInfo(2, 4);
            else if (e.CardId == HearthDb.CardIds.Collectible.Rogue.JadeShuriken)
                return new SpellDamageInfo(2);
            else if (e.CardId == HearthDb.CardIds.Collectible.Rogue.Shiv)
                return new SpellDamageInfo(1);
            else if (e.CardId == HearthDb.CardIds.Collectible.Mage.Fireball)
                return new SpellDamageInfo(6);
            else if (e.CardId == HearthDb.CardIds.Collectible.Mage.Pyroblast)
                return new SpellDamageInfo(10);
            else if (e.CardId == HearthDb.CardIds.Collectible.Mage.Frostbolt)
                return new SpellDamageInfo(3);
            return null;
        }

        void UpdateHandDamageCounter()
        {
            if (CoreAPI.Game.Player.IsLocalPlayer == false)
                return;

            int sp = 0;
            foreach (Entity e in CoreAPI.Game.Player.Board)
            {
                if (e.HasTag(GameTag.SPELLPOWER) == false)
                    continue;

                sp += e.GetTag(GameTag.SPELLPOWER);
            }

            int primaryDamage = 0;
            int secondaryDamage = 0;
            int cardCount = 0;
            foreach (Entity e in Helpers.EntitiesInHand)
            {
                if (DamageCards.Contains(e.CardId) == false)
                    continue;

                var c = e.Card;
                var spellDamageInfo = GetCardDirectDamage(e);

                if (spellDamageInfo == null)
                    continue;
                cardCount++;
                primaryDamage += spellDamageInfo.PrimaryDamage + sp;
                secondaryDamage += spellDamageInfo.SecondaryDamage + sp;
            }

            DamageModel = new DamageModel(primaryDamage, secondaryDamage, cardCount);
        }

  
        public void Update()
        {
            UpdateHandDamageCounter();
        }

        List<string> DamageCards = new List<string>()
        {
            HearthDb.CardIds.Collectible.Rogue.SinisterStrike,
            HearthDb.CardIds.Collectible.Rogue.Eviscerate,
            HearthDb.CardIds.Collectible.Rogue.JadeShuriken,
            HearthDb.CardIds.Collectible.Rogue.Shiv
        };

        class SpellDamageInfo
        {

            public int PrimaryDamage { get; set; }
            public int SecondaryDamage { get; set; }

            public SpellDamageInfo(int damage, int altDamage)
            {
                PrimaryDamage = damage;
                SecondaryDamage = altDamage;
            }

            public SpellDamageInfo(int damage)
            {
                PrimaryDamage = SecondaryDamage = damage;
            }
        }
    }
}
