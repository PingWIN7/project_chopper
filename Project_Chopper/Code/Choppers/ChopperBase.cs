using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Chopper.Code;
using Chopper.Code.Weapons;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Chopper.Code.Enemy;

namespace Game1Test.Code
{
    internal class ChopperBase : Sprite
    {
        protected WeaponSystem frontWeapon;
        protected WeaponSystem leftWeapon;
        protected WeaponSystem rightWeapon;
        protected Vector2 frontWeaponPosition;
        protected Vector2 leftWeaponPosition;
        protected Vector2 rightWeaponPosition;
        public float scale = 1.0f;

        public Sprite GetTexture
        {
            get
            {
                return this;
            }
        }

        public ChopperBase(Texture2D texture, Vector2 position)
          : base(texture, position)
        {
            this.frontWeaponPosition = new Vector2(0.0f, -85f);
            this.leftWeaponPosition = new Vector2(-32f, 0.0f);
            this.rightWeaponPosition = new Vector2(32f, 0.0f);
        }

        /*public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            float sin = Math.Sin(degrees * Math.Deg2Rad);
            float cos = Math.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.X;
            float ty = v.Y;
            Vector2 tmp = new Vector2((cos * tx) - (sin * ty), (sin * tx) + (cos * ty));
            
            return v;
        }*/
        public Vector2 RotateAboutOrigin(Vector2 point, Vector2 origin, float rotation)
        {
            return Vector2.Transform(point - origin, Matrix.CreateRotationZ(rotation)) + origin;
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            frontWeapon.Update(gameTime);
            frontWeapon.UpdateLocations(this.Center + this.frontWeaponPosition * scale, this.Rotation);
            //frontWeapon.SnapCenter(this.Center + this.frontWeaponPosition);
            frontWeapon.SnapCenter(center + RotateAboutOrigin(frontWeaponPosition*scale, new Vector2(0, 0), rotation));

            leftWeapon.Update(gameTime);
            leftWeapon.UpdateLocations(this.Center + this.leftWeaponPosition * scale, this.Rotation);
            //leftWeapon.SnapCenter(this.Center + this.leftWeaponPosition);
            leftWeapon.SnapCenter(center + RotateAboutOrigin(leftWeaponPosition*scale, new Vector2(0, 0), rotation));

            rightWeapon.Update(gameTime);
            rightWeapon.UpdateLocations(this.Center + this.rightWeaponPosition * scale, this.Rotation);
            //rightWeapon.SnapCenter(this.Center + this.rightWeaponPosition);
            rightWeapon.SnapCenter(center + RotateAboutOrigin(rightWeaponPosition*scale, new Vector2(0, 0), rotation));
        }
        

        public void Fire(GameTime gameTime, AmmoSystem ammoSystem)
        {
            if (frontWeapon.Fire() && ammoSystem.Usable())
            {
            }
            if (leftWeapon.Fire() && ammoSystem.Usable())
            {

            }
            if (rightWeapon.Fire() && ammoSystem.Usable())
            {

            }
        }

        public Queue<Bullet> GetBullets()
        {
            Queue<Bullet> bullets = new Queue<Bullet>();
            Queue<Bullet> tmp = frontWeapon.GetBullets();
            while (tmp.Count > 0)
            {
                bullets.Enqueue(tmp.Peek());
                tmp.Dequeue();
            }
            tmp = leftWeapon.GetBullets();
            while (tmp.Count > 0)
            {
                bullets.Enqueue(tmp.Peek());
                tmp.Dequeue();
            }
            tmp = rightWeapon.GetBullets();
            while (tmp.Count > 0)
            {
                bullets.Enqueue(tmp.Peek());
                tmp.Dequeue();
            }
            return bullets;
        }

        public Queue<Bullet> GetExplosiveBullets()
        {
            Queue<Bullet> bullets = new Queue<Bullet>();
            Queue<Bullet> tmp = this.frontWeapon.GetExplosiveBullets();
            while (tmp.Count > 0)
            {
                bullets.Enqueue(tmp.Peek());
                tmp.Dequeue();
            }
            tmp = this.leftWeapon.GetExplosiveBullets();
            while (tmp.Count > 0)
            {
                bullets.Enqueue(tmp.Peek());
                tmp.Dequeue();
            }
            tmp = this.rightWeapon.GetExplosiveBullets();
            while (tmp.Count > 0)
            {
                bullets.Enqueue(tmp.Peek());
                tmp.Dequeue();
            }
            return bullets;
        }

        public override void Draw(SpriteBatch spriteBatch, Color color, float layer)
        {
            base.Draw(spriteBatch, color, scale, layer);
            frontWeapon.Draw(spriteBatch, color, scale, layer - 0.01f);
            leftWeapon.Draw(spriteBatch, color, scale, layer - 0.01f);
            rightWeapon.Draw(spriteBatch, color, scale, layer - 0.01f);
        }
        
    }
}