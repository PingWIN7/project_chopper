using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;








namespace Game1Test.Code.Upgrades
{
    class OilUpgrade : Upgrade
    {
        public OilUpgrade()
        {
            baseAmount = 1000000;
            basePrice = 1000;
            upgradeModifier = 1;
            costModifier = 0.7f;
        }
    }
}