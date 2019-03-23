using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Chopper.Code.Weapons;

namespace Game1Test.Code.Choppers
{
    class BasicChopper : ChopperBase
    {
        public BasicChopper(Texture2D texture, Vector2 position) : base(texture, position)
        {
            frontWeapon = WeaponList.GetWeapon(WeaponType.MGun);
            leftWeapon = WeaponList.GetWeapon(WeaponType.Rocket);
            rightWeapon = WeaponList.GetWeapon(WeaponType.Rocket);
        }


    }
}