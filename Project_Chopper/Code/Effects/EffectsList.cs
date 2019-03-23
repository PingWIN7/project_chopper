using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1Test.Code.Effects
{
    public enum EffectType { Explosion, Smoke };

    static class EffectsList
    {
        static EffectManager Explosion;
        static EffectManager Smoke;

        public static EffectManager GetEffect(EffectType effectType, Vector2 center)
        {
            switch (effectType)
            {
                case EffectType.Explosion:
                    {
                        return new EffectManager(Explosion.Texture, center,Explosion.length, Explosion.radius,Explosion.fadetime, center);
                    }
                case EffectType.Smoke:
                    {
                        return new EffectManager(Smoke.Texture, center, Smoke.length, Smoke.radius, Smoke.fadetime, center);
                    }
            }
            return null;
        }

        public static void LoadContent(ContentManager Content)
        {
            Explosion = new EffectManager(Content.Load<Texture2D>("Effects\\explosion"), new Vector2(0, 0), 0.25f, 64, 0.5f);
            Smoke = new EffectManager(Content.Load<Texture2D>("Effects\\smoke"), new Vector2(0, 0), 0.1f, 20, 0.15f);
            
        }
    }
}