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
    class RocketSystem : WeaponSystem
    {
        public RocketSystem(Texture2D texture, Vector2 position, Texture2D bulletTexture) : base(texture, position, bulletTexture)
        {
            RunTimerSetup(WeaponStatistics.ROCKET_FIREDELAY, WeaponStatistics.RESTOCKTIMER);
            bulletType = new BulletType(BulletType.bulletType.Explosion);
        }

        public override bool Fire()
        {
            /*The base algorythm we overrided is checking if we can fire so we use that*/
            if (base.Fire())
            {
                Bullet tmp = new Bullet(bulletTexture, center, rotation, WeaponStatistics.ROCKET_FIRESPEED, WeaponStatistics.ROCKET_FIREDMG, position);
                tmp.SnapCenter(center);
                bullets.Enqueue(tmp);
                return true;
            }
            return false;
        }
    }
}