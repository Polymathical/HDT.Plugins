using HDT.Plugins.Custom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Custom.ViewModels
{
    public class DamageViewModel : ViewModelBase
    {
        private int _primaryDamage = 0;
        private int _secondaryDamage = 0;
        private int _cardCost = 0;

        public int PrimaryDamage { get { return _primaryDamage; } set { Set(ref _primaryDamage, value, nameof(PrimaryDamage)); } }
        public int SecondaryDamage { get { return _secondaryDamage; } set { Set(ref _secondaryDamage, value, nameof(SecondaryDamage)); } }
        public int CardCount { get { return _cardCost; } set { Set(ref _cardCost, value, nameof(CardCount)); } }

        public DamageViewModel(int primaryDamage, int secondaryDamage, int cardCount)
        {
            PrimaryDamage = primaryDamage;
            SecondaryDamage = secondaryDamage;
            CardCount = cardCount;
        }

        public DamageViewModel(DamageModel dm)
        {
            PrimaryDamage = dm.PrimaryDamage;
            SecondaryDamage = dm.SecondaryDamage;
            CardCount = dm.CardCount;
        }

    }
}
