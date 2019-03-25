using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Chopper.Code;

namespace Game1Test.Code
{
    class EffectManager : Sprite
    {
        public float length;
        private float timer;
        private float alltimer;
        public float radius;
        public float fadetime;
        private float ftimer;
        private float fadebonus;


        public bool finished()
        {
            return alltimer > length + fadetime;
        }

        public EffectManager(Texture2D texture, Vector2 position, float length, float radius, float fadetime)
            : base(texture, position)
        {
            this.texture = texture;
            this.length = length;
            this.position = position;
            this.radius = radius;
            this.fadetime = fadetime;
            timer = 0;
            fadebonus = 0;
            Random r = new Random();
            rotation = r.Next(180);

        }

        public EffectManager(Texture2D texture, Vector2 position, float length, float radius, float fadetime, Vector2 center)
            : base(texture, position)
        {
            this.texture = texture;
            this.length = length;
            this.position = position;
            this.radius = radius;
            this.fadetime = fadetime;
            timer = 0;
            fadebonus = 0;
            Random r = new Random();
            rotation = r.Next(180);
            this.center = center;

        }

        public override void Update(GameTime gameTime)
        {

            if (timer > length)
            {
                //fadetime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                ftimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                fadebonus += (float)gameTime.ElapsedGameTime.TotalSeconds / 1.5f;
            }
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            alltimer += (float)gameTime.ElapsedGameTime.TotalSeconds;


            /*if (position != null)
            {
                base.Update(gameTime);
            }*/
        }

        public override void Draw(SpriteBatch spriteBatch, float layer)
        {

            //spriteBatch.Draw(texture, center, null, Color.White * (1.0f - (fadetime - (length - timer))), rotation, origin, ((timer / length) / texture.Width * radius) + fadebonus, SpriteEffects.None, 1f);

            

            //spriteBatch.Draw(texture, center, null, Color.White * (1.0f - (ftimer / fadetime)), rotation, origin, ((timer / length) / texture.Width * radius) + fadebonus, SpriteEffects.None, 1f);

            spriteBatch.Draw(texture, center, null, Color.White * (1.0f - (ftimer / fadetime)), rotation, origin,  0.5f+ ((timer / length)*0.5f) / texture.Width * radius, SpriteEffects.None, 1f);
            //spriteBatch.Draw(texture, center, null, Color.White * (1.0f -  (ftimer==0? 0: (ftimer/fadetime))), rotation, origin, ((timer / length) / texture.Width * radius) + fadebonus, SpriteEffects.None, 1f);
        }

    }
}
