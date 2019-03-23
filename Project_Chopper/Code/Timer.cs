using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.Code
{
    class Timer
    {
        private float currentSecond;
        private float maxSecond;

        public Timer(float maxSecond)
        {
            this.maxSecond = maxSecond;
            currentSecond = 0;
        }

        public float MaxSecond
        {
            get
            {
                return maxSecond;
            }
        }

        public void Restart()
        {
            currentSecond = 0;
        }

        public bool HitMax()
        {
            if (currentSecond >= maxSecond)
            {
                return true;
            }
            return false;
        }

        public void GoToEnd()
        {
            currentSecond = maxSecond;
        }

        //It returns a value that we can fire the event and also restarts the timer
        public bool CanFireAndRestart()
        {
            if (currentSecond >= maxSecond)
            {
                currentSecond = 0;
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
            currentSecond += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
