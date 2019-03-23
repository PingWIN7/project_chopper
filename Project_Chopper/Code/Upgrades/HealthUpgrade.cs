using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;








namespace Game1Test.Code.Upgrades
{
    class HealthUpgrade : Upgrade
    {
        public HealthUpgrade()
        {
            baseAmount = 2000;
            basePrice = 2000;
            upgradeModifier = 0.5f;
            costModifier = 0.65f;
        }
    }
}