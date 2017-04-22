using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDT.Plugins.Custom.Modules;
using static HDT.Plugins.Custom.Modules.DamageModule;

namespace HDT.Plugins.Custom.Models
{
    public class DamageModel
    {
        public int PrimaryDamage { get; set; }
        public int SecondaryDamage { get; set; }
        public int CardCount { get; set; }

        public DamageModel(int primaryDamage, int secondaryDamage, int cardCount)
        {
            PrimaryDamage = primaryDamage;
            SecondaryDamage = secondaryDamage;
            CardCount = cardCount;
        }

    }
}
