using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Chopper.Code.Enemy
{
    class Bullet : Sprite
    {
        protected float damage;
        protected float age;
        protected float speed;
        protected Vector2 startCenter;
        protected Vector2 previousCenter;
        protected int bulletMaxHit;
        protected int bulletCurrentHit;


        public float Damage
        {
            get
            {
                return damage;
            }
        }

        public virtual bool IsDead()
        {
            return age > 5;
        }


        public Bullet(Texture2D texture, Vector2 position, float rotation, float speed, float damage, Vector2 startCenter)
    : base(texture, position)
        {
            this.rotation = rotation;
            this.damage = damage;
            this.speed = speed;
            this.startCenter = startCenter;
            bulletMaxHit=3;
            bulletCurrentHit=0;


            SetRotation(rotation);
        }

        public virtual void kill()
        {
            this.age = 11;
        }

        public void BulletGotHit(int amount)
        {
            bulletCurrentHit += amount;
            if (bulletCurrentHit >= bulletMaxHit)
            {
                kill();
            }
        }

        public bool PossibleHit(Sprite sprite)
        {
            int hitRadius = sprite.Texture.Height;
            if (hitRadius < sprite.Texture.Width)
            {
                hitRadius = sprite.Texture.Width;
            }
            hitRadius /= 2;
            hitRadius += texture.Width / 2;
            
            Vector2 microSteps = velocity / speed;

            for (int i = 0; i <= speed; i++)
            {
                previousCenter += microSteps;
                if (Vector2.Distance(previousCenter, sprite.Center) < hitRadius)
                {
                    SnapCenter(previousCenter);
                    //SnapCenter(sprite.Center);//This is mainly needed for explosion based weapons so they don't have to inflict double damage
                    return true;
                }
            }

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            //age++;
            age += (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += velocity;

            previousCenter = new Vector2(position.X,position.Y);
            base.Update(gameTime);
        }

        public virtual void SetRotation(float value)
        {
            rotation = value;

            velocity = Vector2.Transform(new Vector2(0, -speed), Matrix.CreateRotationZ(rotation));
        }
    }
}
