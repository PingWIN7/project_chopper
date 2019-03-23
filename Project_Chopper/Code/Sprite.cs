using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chopper.Code
{
    class Sprite
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 velocity;


        protected Vector2 center;
        protected Vector2 origin;
        protected float rotation;

        public Texture2D Texture
        {
            get
            {
                return texture;
            }
        }

        public Vector2 Center
        {

            get
            {
                return center;
            }
            set
            {
                center = value;
            }

        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            velocity = Vector2.Zero;
            center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void SnapCenter(Vector2 centerCoordinate)
        {
            center = centerCoordinate;
            position = new Vector2(centerCoordinate.X - texture.Width / 2, centerCoordinate.Y - texture.Height / 2);
        }

        public void ChangePosition(Vector2 position)
        {
            this.position = position;
            center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
        }

        public bool InRadius(Sprite sprite)
        {
            float hitRadius = sprite.Texture.Width;

            if (hitRadius < sprite.Texture.Height)
            {
                hitRadius = sprite.Texture.Height;
            }
            hitRadius /= 2;

            if (Vector2.Distance(Center, sprite.Center) < hitRadius)
            {
                return true;
            }
            return false;
        }

        public float VectorToFloat(Vector2 vector)
        {
            return (float)Math.Atan2(vector.X, -vector.Y);
        }

        public virtual void Update(GameTime gameTime)
        {
            this.center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
        }

        public virtual void Draw(SpriteBatch spriteBatch, float layer)
        {
            spriteBatch.Draw(texture, center, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, layer);
        }

        public virtual void Draw(SpriteBatch spriteBatch, float scale, float layer)
        {
            spriteBatch.Draw(texture, center, null, Color.White, rotation, origin, scale, SpriteEffects.None, layer);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color color, float layer)
        {
            spriteBatch.Draw(texture, center, null, color, rotation, origin, 1.0f, SpriteEffects.None, layer);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color color, float scale, float layer)
        {
            spriteBatch.Draw(texture, center, null, color, rotation, origin, scale, SpriteEffects.None, layer);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Rectangle toDraw, Rectangle source, float layer)
        {
            spriteBatch.Draw(texture, toDraw, new Rectangle(source.X, source.Y, source.Width, source.Height), Color.White, 0, origin, SpriteEffects.None, layer);
           
        }
    }
}
