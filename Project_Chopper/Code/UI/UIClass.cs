using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game1Test.Code.UI
{
    static class UIClass
    {
        static Texture2D baseButton;
        static SpriteFont hudSpriteFont;

        public static Texture2D GetBaseButtonTexture()
        {
            return baseButton;
        }

        public static SpriteFont GetHudSpriteFont()
        {
            return hudSpriteFont;
        }



        public static void LoadContent(ContentManager Content)
        {
            baseButton = Content.Load<Texture2D>("UI\\buttonBase");
            hudSpriteFont=Content.Load<SpriteFont>("hudFont");
        }
    }
}