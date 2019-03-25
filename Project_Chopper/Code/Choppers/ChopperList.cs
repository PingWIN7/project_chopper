using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1Test.Code.Choppers
{
    public enum ChopperType { Training, Basic };

    class ChopperList
    {
        private static BasicChopper basicChopper;

        public static ChopperBase GetChopper(ChopperType chopperType)
        {
            switch (chopperType)
            {
                case ChopperType.Training:
                    {
                        return new BasicChopper(basicChopper.Texture,new Vector2(0,0));
                    }
            }
            return null;
        }

        public static void LoadContent(ContentManager Content)
        {
            basicChopper = new BasicChopper(Content.Load<Texture2D>("Choppers\\Cobra"), new Vector2(0, 0));
        }
    }
}