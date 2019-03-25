using Chopper.Code;
using Chopper.Code.Enemy;
using Game1Test.Code.Allies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Chopper.Code
{
    class Level
    {
        Sprite[,] mapLayout;
        List<MovingEnemy> movingEnemies;
        List<Turret> turretEnemies;
        List<Building> buildingEnemies;

        List<Building> buildingAllies;

        List<Helipad> helipads;

        List<Bullet> bullets = new List<Bullet>();
        List<Bullet> explosiveBullets = new List<Bullet>();

        public List<Turret> TurretEnemies
        {
            get
            {
                return turretEnemies;
            }
        }

        public List<Building> BuildingEnemies
        {
            get
            {
                return buildingEnemies;
            }
        }

        public List<Helipad> Helipads
        {
            get
            {
                return helipads;
            }
        }

        public Level(int width, int height)
        {
            mapLayout = new Sprite[width, height];
            movingEnemies = new List<MovingEnemy>();
            turretEnemies = new List<Turret>();
            buildingEnemies = new List<Building>();
            helipads = new List<Helipad>();
        }

        public void AddLandingPad(List<Helipad> objects)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                helipads.Add(objects[i]);
            }
        }

        public void AddEntity(List<Sprite> objects, bool isEnemy, bool isBuilding, bool isDamaging)
        {
            if (isBuilding)
            {
                if (isDamaging)
                {
                    for (int i = 0; i < objects.Count; i++)
                    {
                        turretEnemies.Add((Turret)objects[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < objects.Count; i++)
                    {
                        buildingEnemies.Add((Building)objects[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    movingEnemies.Add((MovingEnemy)objects[i]);
                }
            }
        }

        public void Update(GameTime gameTime, Sprite player)
        {
            
            for (int i = 0; i < movingEnemies.Count; i++)
            {
                if (movingEnemies[i].Update(gameTime, player))
                {
                    List<Bullet> tmp = movingEnemies[i].GetBullets();
                    for (int k = 0; k < tmp.Count; k++)
                    {
                        bullets.Add(tmp[i]);
                    }

                    tmp = movingEnemies[i].GetExplosiveBullets();
                    for (int k = 0; k < tmp.Count; k++)
                    {
                        explosiveBullets.Add(tmp[i]);
                    }


                }
            }

            for (int i = 0; i < turretEnemies.Count; i++)
            {
                if (!turretEnemies[i].isAlive())
                {
                    turretEnemies.RemoveAt(i);
                    i--;
                }
                else if (turretEnemies[i].Update(gameTime, player))
                {
                    List<Bullet> tmp = turretEnemies[i].GetBullets();
                    for (int k = 0; k < tmp.Count; k++)
                    {
                        bullets.Add(tmp[k]);
                    }

                    tmp = turretEnemies[i].GetExplosiveBullets();
                    for (int k = 0; k < tmp.Count; k++)
                    {
                        explosiveBullets.Add(tmp[k]);
                    }
                }
            }

            for (int i = 0; i < buildingEnemies.Count; i++)
            {
                buildingEnemies[i].Update(gameTime);
            }
        }

        public List<Bullet> GetBulletsAndClear()
        {
            List<Bullet> tmp = new List<Bullet>();
            for (int i = 0; i < bullets.Count; i++)
            {
                tmp.Add(bullets[i]);
            }

            bullets.Clear();
            return tmp;
        }

        public List<Bullet> GetExplosiveBulletsAndClear()
        {
            List<Bullet> tmp = new List<Bullet>();
            for (int i = 0; i < explosiveBullets.Count; i++)
            {
                tmp.Add(explosiveBullets[i]);
            }

            explosiveBullets.Clear();
            return tmp;
        }

        public bool IsEveryoneDead()
        {
            if (movingEnemies.Count == 0 && turretEnemies.Count == 0)
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < helipads.Count; i++)
            {
                helipads[i].Draw(spriteBatch, 0.81f);
            }
            for (int i = 0; i < turretEnemies.Count; i++)
            {
                turretEnemies[i].Draw(spriteBatch, 0.8f);
            }
            for (int i = 0; i < buildingEnemies.Count; i++)
            {
                buildingEnemies[i].Draw(spriteBatch, 0.79f);
            }
            for (int i = 0; i < movingEnemies.Count; i++)
            {
                movingEnemies[i].Draw(spriteBatch, 0.75f);
            }
        }
    }
}