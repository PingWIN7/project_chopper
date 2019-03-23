using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Chopper.Code.Enemy;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Chopper.Code;

namespace Game1Test.Code.Enemy
{
    class BulletHoming : Bullet
    {
        Sprite target;
        public BulletHoming(Texture2D texture, Vector2 position, float rotation, float speed, float damage, Vector2 startCenter, Sprite target) : base (texture,position,rotation,speed,damage,startCenter)
        {
            this.target = target;
        }

        private float CalculateRotation()
        {
            return (float)Math.Atan2(-center.X + target.Center.X, center.Y - target.Center.Y);
        }

        public override void Update(GameTime gameTime)
        {
            //VectorToFloat(target);
            SetRotation(CalculateRotation());
            base.Update(gameTime);
        }
    }
}