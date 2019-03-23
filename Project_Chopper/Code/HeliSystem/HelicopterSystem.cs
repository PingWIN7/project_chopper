using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;








namespace Game1Test.Code
{
    class HelicopterSystem
    {
        protected int currentAmount;
        protected int maxAmount;
        protected float WarningPercent;

        public int level;

        protected bool damaged;

        public HelicopterSystem(int currentAmount, int maxAmount)
        {
            Initialize(currentAmount, maxAmount,0);
        }

        public void Initialize(int currentAmount, int maxAmount, int level)
        {
            this.currentAmount = currentAmount;
            this.maxAmount = maxAmount;
            damaged = false;
            this.level = level;
        }


        public int FillAmountReturnAmmount(int amountIn)
        {
            int amount = maxAmount - currentAmount;

            if (amount < amountIn)
            {
                currentAmount += amount;
                return amount;
            }

            currentAmount += amountIn;
            return amountIn;
        }

        public virtual void Update(int consumption)
        {
            currentAmount -= consumption;
        }

        public bool Usable()
        {
            return currentAmount > 0;
        }

        public bool Danger()
        {
            if ((float)currentAmount / maxAmount < WarningPercent)
            {
                return true;
            }
            if (damaged)
            {
                return true;
            }
            return false;
        }

        public void ApplyUpgrade(int currentAmount, int maxAmount, int level)
        {
            Initialize(currentAmount, maxAmount,level);
        }

        public override string ToString()
        {
            return currentAmount + "//" + maxAmount;
        }
    }
}