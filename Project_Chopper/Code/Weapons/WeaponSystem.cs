using Chopper.Code.Enemy;
using Game1Test.Code.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.Code.Weapons
{
    abstract class WeaponSystem : Sprite
    {
        protected Texture2D bulletTexture;

        private Timer fireTimer;
        private Timer restockTimer;
        protected BulletType bulletType;

        protected Queue<Bullet> bullets;

        public WeaponSystem(Texture2D texture, Vector2 position, Texture2D bulletTexture) : base(texture, position)
        {
            this.bulletTexture = bulletTexture;
            bullets = new Queue<Bullet>();

            /*fireTimer = new Timer(WeaponStatistics.MGUNFIREDELAY);
            restockTimer = new Timer(WeaponStatistics.RESTOCKTIMER);*/
            //RunTimerSetup();
        }

        public void RunTimerSetup(float fireDelay, float restockTime)
        {
            fireTimer = new Timer(fireDelay);
            restockTimer = new Timer(restockTime);
        }

        public override void Update(GameTime gameTime)
        {
            fireTimer.Update(gameTime);
            restockTimer.Update(gameTime);

            //base.Update(gameTime);
        }

        public void UpdateLocations(Vector2 position, float rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        public virtual bool Fire()
        {
            return fireTimer.CanFireAndRestart();
        }

        public Queue<Bullet> GetBullets()
        {

            Queue<Bullet> tmp = new Queue<Bullet>();
            if (bulletType.type == BulletType.bulletType.Normal)
            {
                while (bullets.Count > 0)
                {
                    tmp.Enqueue(bullets.Peek());
                    bullets.Dequeue();
                }
            }
            return tmp;
        }

        public Queue<Bullet> GetExplosiveBullets()
        {

            Queue<Bullet> tmp = new Queue<Bullet>();
            if (bulletType.type == BulletType.bulletType.Explosion)
            {
                while (bullets.Count > 0)
                {
                    tmp.Enqueue(bullets.Peek());
                    bullets.Dequeue();
                }
            }
            return tmp;
        }

        public virtual void Restock()
        {

        }

        public virtual bool OutOfAmmo()
        {
            return false;
        }

        public Texture2D BulletTexture
        {
            get
            {
                return bulletTexture;
            }
        }
    }
}
