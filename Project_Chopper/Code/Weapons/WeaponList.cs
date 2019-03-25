using Chopper.Code.Weapons.WeaponSystems;
using Game1Test.Code;
using Game1Test.Code.Weapons.WeaponSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.Code.Weapons
{
    public enum WeaponType { MGun, Rocket, Shotgun };

    static class WeaponList
    {
        private static MGunSystem MGUNSYSTEM;
        private static RocketSystem ROCKETSYSTEM;
        private static ShotgunSystem SHOTGUNSYSTEM;

        public static WeaponSystem GetWeapon(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.MGun:
                    {
                        return new MGunSystem(MGUNSYSTEM.Texture, MGUNSYSTEM.Position, MGUNSYSTEM.BulletTexture);
                    }
                case WeaponType.Rocket:
                    {
                        return new RocketSystem(ROCKETSYSTEM.Texture, ROCKETSYSTEM.Position, ROCKETSYSTEM.BulletTexture);
                    }
                case WeaponType.Shotgun:
                    {
                        return new ShotgunSystem(SHOTGUNSYSTEM.Texture, SHOTGUNSYSTEM.Position, SHOTGUNSYSTEM.BulletTexture, PlayerStats.SHOTGUN_PELLET,PlayerStats.SHOTGUN_SPREAD);
                    }
            }
            return null;
        }

        public static void LoadContent(ContentManager Content)
        {
            MGUNSYSTEM = new MGunSystem(Content.Load<Texture2D>("Weapons\\mgun"), new Vector2(0, 0), Content.Load<Texture2D>("Bullets\\bullet"));
            ROCKETSYSTEM = new RocketSystem(Content.Load<Texture2D>("Weapons\\rocketgun"), new Vector2(0, 0), Content.Load<Texture2D>("Bullets\\rocketBullet"));
            SHOTGUNSYSTEM = new ShotgunSystem(Content.Load<Texture2D>("Weapons\\mgun"), new Vector2(0, 0), Content.Load<Texture2D>("Bullets\\bullet"),0,0);
        }
    }
}
