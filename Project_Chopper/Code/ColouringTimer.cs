using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.Code
{
    class ColouringTimer
    {
        bool eventGoing;
        Timer alternativeTimer;
        bool alternativeBreak;
        Timer breakTimer;
        int currentCycle;
        int maxCycle;
        Color colour;

        public ColouringTimer(float alternativeTime, float breakTime, int maxCycle, Color colour)
        {
            eventGoing = false;
            alternativeBreak = false;
            alternativeTimer = new Timer(alternativeTime);
            breakTimer = new Timer(breakTime);
            this.maxCycle = maxCycle;
            this.colour = colour;
            currentCycle = 0;
        }

        public void Start()
        {
            Restart();
            eventGoing = true;
        }

        public void Update(GameTime gameTime)
        {
            if (eventGoing)
            {
                if (!alternativeBreak)
                {
                    alternativeTimer.Update(gameTime);
                }
                else
                {
                    breakTimer.Update(gameTime);

                    //if brake timer finished...
                    if (breakTimer.CanFireAndRestart())
                    {
                        alternativeBreak = false;
                        currentCycle++;
                        if (currentCycle >= maxCycle)
                        {
                            RestartAndStop();
                        }
                    }
                }
                
                //if alternativeTimer finished
                if (alternativeTimer.CanFireAndRestart())
                {
                    alternativeBreak = true;
                }
            }
        }

        private void Restart()
        {
            alternativeBreak = false;
            currentCycle = 0;
            alternativeTimer.Restart();
            breakTimer.Restart();
        }

        private void RestartAndStop()
        {
            eventGoing = false;
            Restart();
        }

        public Color DrawColour()
        {
            if (eventGoing && !alternativeBreak)
            {
                return colour;
            }
            return Color.White;

        }
    }
}
