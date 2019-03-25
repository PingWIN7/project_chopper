using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Chopper.Code.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1Test.Code.Enemy
{
    public enum TurretType { MGun, ShotGun, Rocket, RocketHoming };

    static class EnemyMaker
    {
        static private Turret MGunTurret;
        static private ShotgunTurret ShotgunTurret;
        static private RocketTurret rocketTurret;
        static private RocketTurretHoming rocketTurretHoming;


        public static Turret GetTurret(TurretType turretType,Vector2 position, float modifier, float modifier2)
        {
            switch (turretType)
            {
                case TurretType.MGun:
                    {
                        return new Turret(MGunTurret.Texture, position, 100, MGunTurret.BulletTexture);
                    }
                case TurretType.ShotGun:
                    {
                        return new ShotgunTurret(ShotgunTurret.Texture, position, 100, ShotgunTurret.BulletTexture, (int)modifier, modifier2); //mod - pellets , mod2- spread amount (1.0f-0.1)
                    }
                case TurretType.Rocket:
                    {
                        return new RocketTurret(rocketTurret.Texture, position, 200, rocketTurret.BulletTexture);
                    }
                case TurretType.RocketHoming:
                    {
                        return new RocketTurretHoming(rocketTurretHoming.Texture, position, 200, rocketTurretHoming.BulletTexture);
                    }
            }
            return null;
        }

        public static Turret GetTurret(TurretType turretType, float modifier, float modifier2)
        {
            return GetTurret(turretType, new Vector2(0, 0), modifier, modifier2);
        }

        public static void LoadContent(ContentManager Content)
        {
            MGunTurret = new Turret(Content.Load<Texture2D>("Enemy\\Turret\\turret1"), new Vector2(0, 0), 100, Content.Load<Texture2D>("Bullets\\bullet"));
            ShotgunTurret = new ShotgunTurret(Content.Load<Texture2D>("Enemy\\Turret\\shotgunTurret"), new Vector2(500, 500), 100, Content.Load<Texture2D>("Bullets\\bullet"), 0, 0);
            rocketTurret = new RocketTurret(Content.Load<Texture2D>("Enemy\\Turret\\rocket"), new Vector2(350, 350), 200, Content.Load<Texture2D>("Bullets\\rocketBullet"));
            rocketTurretHoming = new RocketTurretHoming(Content.Load<Texture2D>("Enemy\\Turret\\rocketHoming"), new Vector2(800, 800), 200, Content.Load<Texture2D>("Bullets\\rocketBulletHoming"));
        }
    }
}