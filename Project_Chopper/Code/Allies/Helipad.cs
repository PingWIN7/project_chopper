using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Chopper.Code;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Chopper.Code.Enemy;

namespace Game1Test.Code.Allies
{
    class Helipad : Building
    {

        float maxRadius;

        public Helipad(Texture2D texture, Vector2 position) : base (texture,position)
        {
            maxRadius = texture.Width / 2;
        }


        public bool PlayerCanLand(Vector2 center)
        {
            if (Vector2.Distance(center, this.center) <= maxRadius)
            {
                return true;
            }
            return false;
        }
    }
}