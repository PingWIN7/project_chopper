using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Chopper.Code.Enemy;
using Chopper.Code;
using Microsoft.Xna.Framework;

namespace Game1Test.Code.HeliSystem
{
    class HealthSystem : HelicopterSystem
    {
        ColouringTimer hitTimer;

        public ColouringTimer HitTimer
        {
            get
            {
                return hitTimer;
            }
        }

        public HealthSystem(int currentAmount, int maxAmount) : base(currentAmount, maxAmount)
        {
            WarningPercent = 0.3f;

            hitTimer = new ColouringTimer(0.2f, 0.2f, 3, Color.Red);
        }

        public int GetHealth()
        {
            return currentAmount;
        }

        public override void Update(int consumption)
        {
            base.Update(consumption);
            hitTimer.Start();
        }

        public void Update(GameTime gameTime)
        {
            hitTimer.Update(gameTime);
        }

        /*public void GotHit(Bullet bullet)
        {
            currentAmount -= (int)bullet.Damage;
            
        }*/
    }
}