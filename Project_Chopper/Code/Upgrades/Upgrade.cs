using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;








namespace Game1Test.Code.Upgrades
{
    class Upgrade
    {
        public Upgrade()
        {

        }

        protected float baseAmount;
        protected float basePrice;

        protected float upgradeModifier = 0.5f;
        protected float costModifier = 0.65f;

        public float GetUpgrade(int level)
        {
            float multiplier = 1;
            float tmp = upgradeModifier;
            for (int i = 0; i < level; i++)
            {
                multiplier += tmp;
                tmp += tmp / 2;
            }
            return baseAmount * multiplier;
        }

        public float GetPrice(int level)
        {
            float multiplier = 1;
            float tmp = costModifier;
            for (int i = 0; i < level; i++)
            {
                multiplier += tmp;
                tmp += tmp / 2;
            }
            return basePrice;
        }
    }
}