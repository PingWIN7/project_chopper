using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Chopper.Code.Enemy;
using Microsoft.Xna.Framework;
using Game1Test.Code.Weapons;

namespace Chopper.Code.Weapons.WeaponSystems
{
    class MGunSystem : WeaponSystem
    {
        public MGunSystem(Texture2D texture, Vector2 position, Texture2D bulletTexture) : base(texture, position, bulletTexture)
        {
            /*fireTimer = new Timer(WeaponStatistics.MGUNFIREDELAY);
            restockTimer = new Timer(WeaponStatistics.RESTOCKTIMER);*/
            RunTimerSetup(WeaponStatistics.MGUN_FIREDELAY, WeaponStatistics.RESTOCKTIMER);
            bulletType = new BulletType(BulletType.bulletType.Normal);
        }

        public override bool Fire()
        {
            /*The base algorythm we overrided is checking if we can fire so we use that*/
            if (base.Fire())
            {
                Bullet tmp = new Bullet(bulletTexture, center, rotation, WeaponStatistics.MGUN_FIRESPEED, WeaponStatistics.MGUN_FIREDMG, position);
                    tmp.SnapCenter(center);
                bullets.Enqueue(tmp);
                return true;
            }
            return false;
        }

    }
}
