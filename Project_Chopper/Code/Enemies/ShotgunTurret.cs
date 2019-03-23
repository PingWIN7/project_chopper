using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Chopper.Code.Enemy;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game1Test.Code.Weapons;

namespace Game1Test.Code.Enemy
{
    class ShotgunTurret : Turret
    {
        
        private List<Vector2> directions = new List<Vector2>();

        public ShotgunTurret(Texture2D texture, Vector2 position, float health, Texture2D bullet, int pellets, float spreadPercent) : base(texture, position, health, bullet)
        {
            bulletSpeed = EnemyStatistics.SHOTGUNTURRET_PROJECTILE_SPEED;
            bulletDelay = EnemyStatistics.SHOTGUNTURRET_DELAY;
            bulletDamage = EnemyStatistics.SHOTGUNTURRET_DAMAGE;
            CreateBulletTimer(bulletDelay);
            bulletType = new BulletType(BulletType.bulletType.Normal);


            // Change the pellets system to be automatically generated and add a spread system aswell
            /*if (pellets == 3)
            {
                directions.Add(new Vector2(-1, -1));
                directions.Add(new Vector2(0, -1));
                directions.Add(new Vector2(1, -1));
            }
            if (pellets == 5)
            {
                directions.Add(new Vector2(-1, -1));
                directions.Add(new Vector2(-0.5f, -1));
                directions.Add(new Vector2(0, -1));
                directions.Add(new Vector2(0.5f, -1));
                directions.Add(new Vector2(1, -1));
            }*/
            float MaxSpreadWidth = 2*spreadPercent;
            float calculation = MaxSpreadWidth / (pellets - 1);
            for (int i = 0; i < pellets; i++)
            {
                directions.Add(new Vector2(spreadPercent - (i * calculation), -1));
            }
        }

        public override void Fire()
        {
            for (int i = 0; i < directions.Count; i++)
            {
                
                bulletList.Add(new Bullet(bullet, Vector2.Subtract(center, new Vector2(bullet.Width / 2)), rotation + VectorToFloat(directions[i]), bulletSpeed, bulletDamage, center));
            }
            //base.Fire();
        }
    }
}