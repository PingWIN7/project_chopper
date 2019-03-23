using Game1Test.Code.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.Code.Enemy
{
    class Turret : Sprite
    {
        protected List<Bullet> bulletList = new List<Bullet>();
        protected Texture2D bullet;
        protected BulletType bulletType;
        protected Timer bulletTimer;
        private float health;
        ColouringTimer hitTimer;
        protected float bulletSpeed = EnemyStatistics.BASETURRETBULLET_PROJECTILE_SPEED;
        protected float bulletDamage = EnemyStatistics.BASETURRET_DAMAGE;
        protected float bulletDelay = EnemyStatistics.BASETURRET_DELAY;

        public Texture2D BulletTexture
        {
            get
            {
                return bullet;
            }
        }

        public Turret(Texture2D texture, Vector2 position, float health, Texture2D bullet) : base(texture, position)
        {
            this.bullet = bullet;
            this.health = health;
            bulletType = new BulletType(BulletType.bulletType.Normal);
            //bulletTimer = new Timer(EnemyStatistics.BASETURRETDELAY);
            CreateBulletTimer(bulletDelay);
            hitTimer = new ColouringTimer(GlobalColouring.TURRETHITTIMER, GlobalColouring.TURRETHITBREAKTIMER, GlobalColouring.TURRETHITCYCLE, GlobalColouring.TURRETHITCOLOR);
        }

        protected void CreateBulletTimer(float bulletDelay)
        {
            bulletTimer = new Timer(bulletDelay);
        }


        public List<Bullet> GetBullets()
        {

            List<Bullet> tmp = new List<Bullet>();
            if (bulletType.type == BulletType.bulletType.Normal)
            {
                for (int i = 0; i < bulletList.Count; i++)
                {
                    tmp.Add(bulletList[i]);
                }
                bulletList.Clear();
            }
            return tmp;
        }

        public List<Bullet> GetExplosiveBullets()
        {
            List<Bullet> tmp = new List<Bullet>();

            if (bulletType.type == BulletType.bulletType.Explosion)
            {
                for (int i = 0; i < bulletList.Count; i++)
                {
                    tmp.Add(bulletList[i]);
                }
                bulletList.Clear();
            }
            return tmp;
        }



        public bool isAlive()
        {
            return health > 0;
        }

        public void GotHit(Bullet bullet)
        {
            health -= bullet.Damage;
            hitTimer.Start();
        }

        protected void FaceTarget(Sprite target)
        {
            Vector2 direction = center - target.Center;
            direction.Normalize();

            rotation = (float)Math.Atan2(-direction.X, direction.Y);
        }

        public virtual bool Update(GameTime gameTime, Sprite target)
        {
            base.Update(gameTime);
            hitTimer.Update(gameTime);

            FaceTarget(target);

            bulletTimer.Update(gameTime);

            if (bulletTimer.CanFireAndRestart())
            {
                Fire();
                return true;
            }

            return false;
        }

        public virtual void Fire()
        {
            bulletList.Add(new Bullet(bullet, Vector2.Subtract(center, new Vector2(bullet.Width / 2)), rotation, bulletSpeed, bulletDamage, center));
        }

        public override void Draw(SpriteBatch spriteBatch, float layer)
        {
            base.Draw(spriteBatch, hitTimer.DrawColour(), layer);
        }
    }
}
