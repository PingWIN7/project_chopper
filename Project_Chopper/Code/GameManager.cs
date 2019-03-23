using Chopper.Code.Enemy;
using Game1Test.Code;
using Game1Test.Code.Allies;
using Game1Test.Code.Effects;
using Game1Test.Code.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.Code
{
    class GameManager
    {
        List<Bullet> bullets;
        List<Bullet> explosiveBullets;

        List<Bullet> friendlyBullets;
        List<Bullet> friendlyExplosiveBullets;

        List<EffectManager> effects;


        Player player;
        Level level;

        public Player GetPlayer
        {
            get
            {
                return player;
            }
        }

        public GameManager(Level level, Player player)
        {
            bullets = new List<Bullet>();
            friendlyBullets = new List<Bullet>();
            explosiveBullets = new List<Bullet>();
            friendlyExplosiveBullets = new List<Bullet>();

            effects = new List<EffectManager>();


            this.player = player;
            this.level = level;
        }

        public void Update(GameTime gameTime)
        {
            if (GameData.inGameState != InGameState.Paused)
            {
                UI.Update(gameTime);
                UI.upgradeButton.Visible(false);
                bool isPlayerAboveHelipad = PlayerAboveHelipad();
                player.PlayerCanLand(isPlayerAboveHelipad);
                if (isPlayerAboveHelipad)
                {
                    UI.landButton.SpecialCase();
                }
                if (player.Health > 0 && !player.changeAltitude)
                {
                    player.Update(gameTime);
                }
                else
                {
                    if (player.Health <= 0)
                    {
                        player.Alive = false;
                        player.KillPlayer(gameTime);
                    }
                    else if (player.changeAltitude)
                    {
                        if (player.landing)
                        {
                            player.LandPlayer(gameTime);
                        }
                        else if (player.lifting)
                        {
                            player.LiftPlayer(gameTime);
                        }
                        else
                        {
                            if (PlayerAboveHelipad())
                            {
                                //On helipad
                                int cost = player.UpdateOnHelipad(gameTime);
                                UI.upgradeButton.Visible(true);
                                UI.upgradeButton.SpecialCase();

                                if (UI.upgradeButton.IsItPressed())
                                {
                                    GameData.inGameState = InGameState.Paused;
                                    GameData.pauseReason = PauseReason.Upgrade;
                                }
                            }
                            else
                            {
                                player.UpdateOnLand(gameTime);
                            }

                        }
                    }
                }
                // We collect all the bullets so if the tower get destroyed we can change the tower class to a simple sprite
                if (player.Alive)
                {
                    level.Update(gameTime, player.PlayerSprite);
                }
                UpdateEnemyBullets(gameTime);
                UpdateEnemyExplosionBullets(gameTime);
                UpdateFriendlyBullets(gameTime);
                UpdateFriendlyExplosiveBullets(gameTime);
                UpdateEffects(gameTime);
            }
            else
            {
                if (GameData.pauseReason == PauseReason.Upgrade)
                {
                    UpgradeSystem.Update(gameTime, player);
                    if (UpgradeSystem.finishedButton.IsItReleased())
                    {
                        GameData.inGameState = InGameState.Going;
                    }
                }
            }
        }

        public bool PlayerAboveHelipad()
        {
            List<Helipad> tmp = level.Helipads;

            for (int i = 0; i < tmp.Count; i++)
            {
                if (tmp[i].PlayerCanLand(player.PlayerCenter))
                {
                    return true;
                }
            }
            return false;
        }

        public void UpdateEffects(GameTime gameTime)
        {
            for (int i = 0; i < effects.Count; i++)
            {
                effects[i].Update(gameTime);
                if (effects[i].finished())
                {
                    effects.RemoveAt(i);
                    i--;
                }
            }
        }

        public void UpdateEnemyBullets(GameTime gameTime)
        {
            List<Bullet> tmp = level.GetBulletsAndClear();

            for (int i = 0; i < tmp.Count; i++)
            {
                bullets.Add(tmp[i]);
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                Bullet currentBullet = bullets[i];
                currentBullet.Update(gameTime);

                if (currentBullet.IsDead())
                {
                    bullets.RemoveAt(i);
                    i--;
                }
                else if (currentBullet.PossibleHit(player.PlayerSprite))
                {
                    player.GotHit(currentBullet);
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void UpdateEnemyExplosionBullets(GameTime gameTime)
        {
            //level.Update(gameTime, player.PlayerSprite);
            List<Bullet> tmp = level.GetExplosiveBulletsAndClear();

            for (int i = 0; i < tmp.Count; i++)
            {
                explosiveBullets.Add(tmp[i]);
            }

            for (int i = 0; i < explosiveBullets.Count; i++)
            {
                Bullet currentBullet = explosiveBullets[i];
                currentBullet.Update(gameTime);

                if (currentBullet.IsDead())
                {
                    explosiveBullets.RemoveAt(i);
                    i--;
                    effects.Add(EffectsList.GetEffect(0, currentBullet.Center));
                }
                else if (currentBullet.PossibleHit(player.PlayerSprite))
                {
                    player.GotHit(currentBullet);
                    effects.Add(EffectsList.GetEffect(0, currentBullet.Center));
                    explosiveBullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void UpdateFriendlyBullets(GameTime gameTime)
        {
            List<Bullet> tmp = player.GetBullets();


            for (int i = 0; i < tmp.Count; i++)
            {
                friendlyBullets.Add(tmp[i]);
            }

            for (int i = 0; i < friendlyBullets.Count; i++)
            {
                Bullet currentBullet = friendlyBullets[i];
                currentBullet.Update(gameTime);

                if (currentBullet.IsDead())
                {
                    friendlyBullets.RemoveAt(i);
                    i--;
                }
                else
                {
                    for (int j = 0; j < level.TurretEnemies.Count; j++)
                    {
                        if (currentBullet.PossibleHit(level.TurretEnemies[j]))
                        {
                            level.TurretEnemies[j].GotHit(currentBullet);
                            friendlyBullets.RemoveAt(i);
                            i--;
                            j = level.TurretEnemies.Count;
                        }
                        else
                        {
                            for (int k = 0; k < explosiveBullets.Count; k++)
                            {
                                if (currentBullet.PossibleHit(explosiveBullets[k]))
                                {
                                    friendlyBullets.RemoveAt(i);
                                    explosiveBullets[k].BulletGotHit(1);
                                    i--;
                                    j = level.TurretEnemies.Count;
                                    k = explosiveBullets.Count;
                                }
                            }
                        }
                        //Checking if we hit the enemy rocket

                        //if (level.TurretEnemies[j].InRadius(currentBullet))
                    }

                }
            }
            //player.EmptyBullets();
        }

        public void UpdateFriendlyExplosiveBullets(GameTime gameTime)
        {
            List<Bullet> tmp = player.GetExplosiveBullets();


            for (int i = 0; i < tmp.Count; i++)
            {
                friendlyExplosiveBullets.Add(tmp[i]);
            }

            for (int i = 0; i < friendlyExplosiveBullets.Count; i++)
            {
                Bullet currentBullet = friendlyExplosiveBullets[i];
                currentBullet.Update(gameTime);

                if (currentBullet.IsDead())
                {
                    friendlyExplosiveBullets.RemoveAt(i);
                    i--;
                    effects.Add(EffectsList.GetEffect(0, currentBullet.Center));
                }
                else
                {
                    for (int j = 0; j < level.TurretEnemies.Count; j++)
                    {
                        if (currentBullet.PossibleHit(level.TurretEnemies[j]))
                        {
                            level.TurretEnemies[j].GotHit(currentBullet);
                            friendlyExplosiveBullets.RemoveAt(i);
                            effects.Add(EffectsList.GetEffect(0, currentBullet.Center));
                            currentBullet.kill();
                            i--;
                            j = level.TurretEnemies.Count;
                        }


                        //if (level.TurretEnemies[j].InRadius(currentBullet))
                    }

                }
            }
            //player.EmptyBullets();
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameData.inGameState != InGameState.Paused)
            {
                /*if (player.Health > 0 && !player.changeAltitude)
                {*/
                player.Draw(spriteBatch, 0.99f);
                /*}
                else
                {
                    //player.DrawSize(spriteBatch, 1.0f);
                }*/
                level.Draw(spriteBatch);
                for (int i = 0; i < bullets.Count; i++)
                {
                    bullets[i].Draw(spriteBatch, 0.97f);
                }

                for (int i = 0; i < explosiveBullets.Count; i++)
                {
                    explosiveBullets[i].Draw(spriteBatch, 0.97f);
                }

                for (int i = 0; i < friendlyBullets.Count; i++)
                {
                    friendlyBullets[i].Draw(spriteBatch, 0.97f);
                }

                for (int i = 0; i < friendlyExplosiveBullets.Count; i++)
                {
                    friendlyExplosiveBullets[i].Draw(spriteBatch, 0.97f);
                }
                for (int i = 0; i < effects.Count; i++)
                {
                    effects[i].Draw(spriteBatch, 0.97f);
                }

            }
        }

    }
}
