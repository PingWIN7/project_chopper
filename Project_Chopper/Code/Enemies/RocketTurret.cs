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
    class RocketTurret : Turret
    {
        public RocketTurret(Texture2D texture, Vector2 position, float health, Texture2D bullet) : base (texture,position,health,bullet)
        {
            bulletSpeed = EnemyStatistics.ROCKETTURRET_PROJECTILE_SPEED;
            bulletDelay = EnemyStatistics.ROCKETTURRET_DELAY;
            bulletDamage = EnemyStatistics.ROCKETTURRET_DAMAGE;
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
            bulletList.Add(new Bullet(bullet, Vector2.Subtract(center, new Vector2(bullet.Width / 2)), rotation, bulletSpeed, bulletDamage, center));
            //base.Fire();
        }

    }
}