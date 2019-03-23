using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Chopper.Code.Enemy;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Chopper.Code;
using Game1Test.Code.Weapons;

namespace Game1Test.Code.Enemy
{
    class RocketTurretHoming : Turret
    {
        public RocketTurretHoming(Texture2D texture, Vector2 position, float health, Texture2D bullet) : base (texture,position,health,bullet)
        {
            bulletSpeed = EnemyStatistics.ROCKETTURRET_PROJECTILE_HOMING_SPEED;
            bulletDelay = EnemyStatistics.ROCKETTURRET_HOMING_DELAY;
            bulletDamage = EnemyStatistics.ROCKETTURRET_HOMING_DAMAGE;
            CreateBulletTimer(bulletDelay);
            bulletType = new BulletType(BulletType.bulletType.Explosion);
        }

        Sprite targetCenter;

        public override bool Update(GameTime gameTime, Sprite target)
        {
            bool result = base.Update(gameTime, target);
            targetCenter = target;
            return result;
        }

        public override void Fire()
        {
            bulletList.Add(new BulletHoming(bullet, Vector2.Subtract(center, new Vector2(bullet.Width / 2)), rotation, bulletSpeed, bulletDamage, center,targetCenter));
            //base.Fire();
        }

    }
}