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
    class Button : Sprite
    {
        protected bool pressed = false;
        protected bool lastStatePressed=false;
        protected bool disabled = false;
        protected bool visible = true;
        protected bool specialCase = false;

        public Button(Texture2D texture, Vector2 position) : base(texture, position)
        {

        }

        public bool IsItPressed()
        {
            return pressed;
        }

        public bool IsItReleased()
        {
            return !pressed && lastStatePressed;
        }

        public override void Update(GameTime gameTime)
        {
            if (!disabled)
            {
                lastStatePressed = pressed;
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
                                break;
                            }
                        }
                        //Debug.WriteLine(tl.Position.ToString());
                    }
                    //hud.UpdateCoordinates(tl.Position.ToString());
                }
                specialCase = false;
                base.Update(gameTime);
            }
        }

        public void Update(GameTime gameTime, int Width)
        {
            if (!disabled)
            {
                lastStatePressed = pressed;
                pressed = false;
                TouchCollection touchCollection = TouchPanel.GetState();

                foreach (TouchLocation tl in touchCollection)
                {
                    if ((tl.State == TouchLocationState.Pressed)
                            || (tl.State == TouchLocationState.Moved))
                    {

                        if (tl.Position.X >= position.X && tl.Position.X <= position.X + Width)
                        {
                            if (tl.Position.Y >= position.Y && tl.Position.Y <= position.Y + texture.Height)
                            {
                                pressed = true;
                                break;
                            }
                        }
                        //Debug.WriteLine(tl.Position.ToString());
                    }
                    //hud.UpdateCoordinates(tl.Position.ToString());
                }
                specialCase = false;
                base.Update(gameTime);
            }
        }

        public void ResetState()
        {
            pressed = false;
        }

        public void disable()
        {
            disabled = true;
            pressed = false;
        }

        public void Enable()
        {
            disabled = false;
        }

        public void SpecialCase()
        {
            specialCase = true;
        }

        public void Visible(bool value)
        {
            visible = value;
        }

        public override void Draw(SpriteBatch spriteBatch, float layer)
        {
            if (visible)
            {
                if (disabled)
                {
                    base.Draw(spriteBatch, Color.Red, layer);
                }
                else if (specialCase&&!pressed)
                {
                    base.Draw(spriteBatch, Color.Green, layer);
                }
                else if (!pressed)
                {
                    base.Draw(spriteBatch, Color.Black, layer);
                }
                else
                {
                    base.Draw(spriteBatch, Color.Yellow, layer);
                }
            }
        }

        public virtual void OverrideDrawDimension(SpriteBatch spriteBatch, float layer, int width, int height)
        {
            if (visible)
            {
                if (disabled)
                {
                    OverDraw(spriteBatch, Color.Red, layer,width,height);
                }
                else if (specialCase && !pressed)
                {
                    OverDraw(spriteBatch, Color.Green, layer, width, height);
                }
                else if (!pressed)
                {
                    OverDraw(spriteBatch, Color.Black, layer, width, height);
                }
                else
                {
                    OverDraw(spriteBatch, Color.Yellow, layer, width, height);
                }
            }
        }

        private void OverDraw(SpriteBatch spriteBatch, Color color, float layer, int width, int height)
        {
            spriteBatch.Draw(texture,
               new Rectangle((int)position.X, (int)position.Y, width, height),
               new Rectangle(0, 0, texture.Width, texture.Height),
               color);
        }
    }
}