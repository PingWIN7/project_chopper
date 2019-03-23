using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.Code.Enemy
{
    class MovingEnemy : Turret
    {

        public MovingEnemy(Texture2D texture, Vector2 position, float health, Texture2D bullet) : base (texture,position, health, bullet)
        {

        }
        
    }
}
