using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1Test.Code
{
    class ButtonWithText : Button
    {
        string text;
        SpriteFont spriteFont;
        Vector2 textSize;
        float textTransparency;

        public ButtonWithText(Texture2D texture, Vector2 position, SpriteFont spriteFont, string text, float textTransparency) : base(texture, position)
        {
            this.text = text;
            this.spriteFont = spriteFont;
            textSize = spriteFont.MeasureString(text);
            this.textTransparency = textTransparency;
        }
        
        public void ChangeText(string text)
        {
            this.text = text;
        }        

        public override void OverrideDrawDimension(SpriteBatch spriteBatch, float layer, int width, int height)
        {
            if (visible)
            {
                spriteBatch.DrawString(spriteFont, text, new Vector2((int)Math.Round(position.X + width / 3), (int)Math.Round(position.Y + height / 2)), Color.Black * textTransparency);
            }
            base.OverrideDrawDimension(spriteBatch, layer, width, height);
        }

        public override void Draw(SpriteBatch spriteBatch, float layer)
        {
            
            if (visible)
            {
                spriteBatch.DrawString(spriteFont, text, new Vector2((int)Math.Round(position.X + (texture.Width - textSize.X) / 2), (int)Math.Round(position.Y + (texture.Height - textSize.Y) / 2)), Color.Black*textTransparency);
            }
            base.Draw(spriteBatch, layer);
        }
    }
}