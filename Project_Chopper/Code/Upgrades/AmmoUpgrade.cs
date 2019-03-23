using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;








namespace Game1Test.Code.Upgrades
{
    class AmmoUpgrade : Upgrade
    {
        public AmmoUpgrade()
        {
            baseAmount = 1000;
            basePrice = 1000;
            upgradeModifier = 0.5f;
            costModifier = 0.4f;
        }
    }
}