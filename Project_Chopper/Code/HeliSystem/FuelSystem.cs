using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Microsoft.Xna.Framework;

namespace Game1Test.Code
{
    class FuelSystem : HelicopterSystem
    {
        public FuelSystem(int currentAmount, int maxAmount) : base(currentAmount, maxAmount)
        {
            WarningPercent = 0.2f;
        }
        

        public override string ToString()
        {
            return currentAmount / 1000 + "//" + maxAmount / 1000;
        }
    }
}