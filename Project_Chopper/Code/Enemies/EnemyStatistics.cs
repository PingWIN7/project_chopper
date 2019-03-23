using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.Code.Enemy
{
    public static class EnemyStatistics
    {
        public static float BASETURRET_DAMAGE = 15;
        public static float BASETURRET_DELAY = 1;
        public static float BASETURRETBULLET_PROJECTILE_SPEED = 8;

        public static float ROCKETTURRET_DAMAGE = 50;
        public static float ROCKETTURRET_DELAY = 5;
        public static float ROCKETTURRET_PROJECTILE_SPEED = 16;

        public static float ROCKETTURRET_HOMING_DAMAGE = 30;
        public static float ROCKETTURRET_HOMING_DELAY = 8;
        public static float ROCKETTURRET_PROJECTILE_HOMING_SPEED = 10;

        public static float ROCKET_EXPLOSION_RADIUS = 50;
        public static float ROCKET_EXPLOSION_RADIUS_MIN_DISTANCE_FOR_MAXIMUM_DAMAGE = 20;
        public static float ROCKET_EXPLOSION_RADIUS_MIN_DAMAGE_PERCENT = 20;

        public static float SHOTGUNTURRET_DAMAGE = 10;
        public static float SHOTGUNTURRET_DELAY = 3;
        public static float SHOTGUNTURRET_PROJECTILE_SPEED = 5;

        public static float PISTOLSOLDIER_DAMAGE = 5;
        public static float PISTOLSOLDIER_DELAY = 3;
        public static float PISTOLSOLDIER_PROJECTILE_SPEED = 5;
        public static float PISTOLSOLDIER_SPEED = 1;
    }
}
