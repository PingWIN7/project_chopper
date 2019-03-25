using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Chopper.Code.Weapons;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Chopper.Code.Enemy;

namespace Game1Test.Code.Weapons.WeaponSystems
{
    class ShotgunSystem : WeaponSystem
    {
        private List<Vector2> directions = new List<Vector2>();

        public ShotgunSystem(Texture2D texture, Vector2 position, Texture2D bulletTexture, int pellets, float spreadPercent) : base(texture, position,bulletTexture)
        {
            //float spreadPercent = 0.5f;

            float MaxSpreadWidth = 2 * spreadPercent;
            float calculation = MaxSpreadWidth / (pellets - 1);
            for (int i = 0; i < pellets; i++)
            {
                directions.Add(new Vector2(spreadPercent - (i * calculation), -1));
            }


            RunTimerSetup(WeaponStatistics.SHOTGUN_FIREDELAY, WeaponStatistics.RESTOCKTIMER);
            bulletType = new BulletType(BulletType.bulletType.Normal);
        }

        public override bool Fire()
        {
            /*The base algorythm we overrided is checking if we can fire so we use that*/
            if (base.Fire())
            {
                for (int i = 0; i < directions.Count; i++)
                {
                    Bullet tmp = new Bullet(bulletTexture, center, rotation + VectorToFloat(directions[i]), WeaponStatistics.SHOTGUN_FIRESPEED, WeaponStatistics.SHOTGUNT_FIREDMG, position);
                    tmp.SnapCenter(center);
                    bullets.Enqueue(tmp);
                }
                return true;
            }
            return false;
        }
    }
}