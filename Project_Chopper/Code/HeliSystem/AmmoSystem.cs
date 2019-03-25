using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;








namespace Game1Test.Code
{
    class AmmoSystem : HelicopterSystem
    {

        public AmmoSystem(int currentAmount, int maxAmount) : base (currentAmount,maxAmount)
        {
            WarningPercent = 0.1f;
        }
    }
}