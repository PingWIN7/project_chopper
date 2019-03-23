using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Chopper.Code;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Game1Test.Code
{
    class Stick : Sprite
    {
        Sprite handler;
        float sensitivity = 0.75f;
        bool pressed = false;
        float widthHalf;
        float heightHalf;

        float xMultiplier;
        float yMultiplier;

        public Stick(Texture2D texture, Texture2D handlerTexture, Vector2 position) : base(texture, position)
        {
            handler = new Sprite(handlerTexture, center);
            widthHalf = texture.Width / 2;
            heightHalf = texture.Height / 2;
        }

        public bool IsItPressed()
        {
            return pressed;
        }

        public float GetXMultiplier()
        {
            return xMultiplier;
        }

        private void SetXMultiplier(float XC)
        {
            if (XC > 1)
            {
                xMultiplier = 1;
            }
            else if (XC < -1)
            {
                xMultiplier = -1;
            }
            else
            {
                xMultiplier = XC;
            }

        }

        private void SetYMultiplier(float YC)
        {
            if (YC > 1)
            {
                yMultiplier = 1;
            }
            else if (YC < -1)
            {
                yMultiplier = -1;
            }
            else
            {
                yMultiplier = YC;
            }

        }

        public float GetYMultiplier()
        {
            return yMultiplier;
        }

        public override void Update(GameTime gameTime)
        {
            handler.SnapCenter(center);
            pressed = false;
            TouchCollection touchCollection = TouchPanel.GetState();

            foreach (TouchLocation tl in touchCollection)
            {
                if ((tl.State == TouchLocationState.Pressed)
                        || (tl.State == TouchLocationState.Moved))
                {

                    if (tl.Position.X >= position.X && tl.Position.X <= position.X + texture.Width)
                    {

                        if (tl.Position.Y >= position.Y && tl.Position.Y <= position.Y + texture.Height)
                        {
                            pressed = true;
                            float xCoordinate = center.X - tl.Position.X;  //positive -> left, negative -> right
                            float yCoordinate = center.Y -tl.Position.Y ; //positive -> up, negative -> down
                            float XMulticalculation = xCoordinate/ (widthHalf * sensitivity);
                            float YMulticalculation = yCoordinate /(heightHalf * sensitivity);
                            SetXMultiplier(XMulticalculation);
                            SetYMultiplier(YMulticalculation);
                            handler.SnapCenter(new Vector2(center.X - xCoordinate, center.Y - yCoordinate));
                            

                            break;
                        }
                    }
                    //Debug.WriteLine(tl.Position.ToString());
                }
                //hud.UpdateCoordinates(tl.Position.ToString());
            }

            handler.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, float layer)
        {
            handler.Draw(spriteBatch, layer- 0.1f);
            base.Draw(spriteBatch, layer);
        }
    }
}