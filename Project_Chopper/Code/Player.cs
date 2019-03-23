using Chopper.Code.Enemy;
using Chopper.Code.Weapons;
using Game1Test.Code;
using Game1Test.Code.Choppers;
using Game1Test.Code.Effects;
using Game1Test.Code.HeliSystem;
using Game1Test.Code.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopper.Code
{
    class Player
    {
        int BASEFUELCONSUMPTION = 10;
        int MAXFUELCONSUMPTION = 20;

        Vector2 basespeed = new Vector2(0, -12);

        private ChopperBase chopperBase;

        private Vector2 speed;
        //private Sprite sprite;
        //private float hitRadius;
        //private List<Bullet> bullets;
        //private float size = 1.0f;
        public bool changeAltitude;
        public bool onLand;
        public bool landing;
        public bool lifting;
        private static Random r = new Random();

        private List<EffectManager> effects;

        public bool playerCanLand;

        private bool alive;

        //WeaponSystem weaponSystem;


        private Timer deathTimer;

        private Timer landTimer;

        private HealthSystem healthSystem;

        private FuelSystem fuelSystem;

        private AmmoSystem ammoSystem;

        public AmmoSystem Ammo_System
        {
            get
            {
                return ammoSystem;
            }
        }

        public FuelSystem Fuel_System
        {
            get
            {
                return fuelSystem;
            }
        }

        public HealthSystem Health_System
        {
            get
            {
                return healthSystem;
            }
        }

        public Timer DeathTimer
        {
            get
            {
                return deathTimer;
            }
        }

        public bool Alive
        {
            get
            {
                return alive;
            }
            set
            {
                alive = value;
            }
        }

        public Sprite PlayerSprite
        {
            get
            {
                return chopperBase.GetTexture;
            }
        }

        /*public Vector2 PlayerSpriteCenter
        {
            get
            {
                return sprite.Center;
            }
        }*/

        public Vector2 PlayerCenter
        {
            get
            {
                return chopperBase.Center;
            }
        }

        public float PlayerRotation
        {
            get
            {
                return chopperBase.Rotation;
            }
        }


        public int Health
        {
            get
            {
                return healthSystem.GetHealth();
            }
        }

        /*public List<Bullet> Bullets
        {
            get
            {
                return bullets;
            }
        }*/


        public Player(ChopperBase basicChopper, Vector2 position)
        {
            /*hitRadius = sprite.Texture.Width;

            if (hitRadius < sprite.Texture.Height)
            {
                hitRadius = sprite.Texture.Height;
            }
            hitRadius /= 2;*/

            //this.sprite = sprite;

            /*chopperBase = ChopperList.GetChopper(0);
            chopperBase.SnapCenter(position);*/
            chopperBase = basicChopper;
            chopperBase.SnapCenter(position);

            //bullets = new List<Bullet>();

            //this.weaponSystem = weaponSystem;

            alive = true;

            deathTimer = new Timer(3);

            landTimer = new Timer(2);

            fuelSystem = new FuelSystem(1000000, 1000000);
            ammoSystem = new AmmoSystem(1000, 1000);

            healthSystem = new HealthSystem(2000, 2000);

            effects = new List<EffectManager>();

            /*fuelSystem = new FuelSystem(10000, 10000);
            ammoSystem = new AmmoSystem(10, 10);*/

        }

        /* This one needs to be changed in the future*/
        /*public bool BulletHit(Bullet bullet)
        {
            if (Vector2.Distance(sprite.Center, bullet.Center) < hitRadius)
            {
                GotHit(bullet);
                return true;
            }
            return false;
        }*/

        public void GotHit(Bullet bullet)
        {
            healthSystem.Update((int)bullet.Damage);
            /*health -= bullet.Damage;
            hitTimer.Start();*/
        }

        /*
         * Speed:
         * Forward 100%
         * Backward: 80%
         * strafe left\right =50%
         * */
        public void Update(GameTime gameTime)
        {
            //healthSystem.Update(gameTime);
            //hitTimer.Update(gameTime);
            MustUpdate(gameTime);
            /*for (int i = 0; i < arrows.Count; i++)
            {
                arrows[i].Update(gameTime);
            }
            landButton.Update(gameTime);

            stick.Update(gameTime);*/

            KeyboardState key = Keyboard.GetState();
            //Matrix rotMatrix = Matrix.CreateRotationZ(sprite.Rotation);
            Matrix rotMatrix = Matrix.CreateRotationZ(chopperBase.Rotation);
            Vector2 velocity = Vector2.Transform(speed, rotMatrix);
            float rotate = 2.25f / 60.0f;
            //speed = speed / 1.035f;
            //speed = new Vector2(0, 0);
            speed /= 1.05f;
            //speed = new Vector2(0, -7);

            //sprite.Position += velocity;
            chopperBase.Position += velocity;

            //if (key.IsKeyDown(Keys.A))
            if (UI.stick.IsItPressed())
            {

                //float x = stick.GetXMultiplier();

                Vector2 YSpeed = new Vector2(basespeed.X, basespeed.Y);
                YSpeed *= UI.stick.GetYMultiplier();
                //rotMatrix = Matrix.CreateRotationZ(sprite.Rotation);
                //velocity = Vector2.Transform(YSpeed, rotMatrix);
                //sprite.Position = new Vector2(sprite.Position.X + velocity.Y, sprite.Position.Y + velocity.X);


                Vector2 XSpeed = new Vector2(basespeed.Y, basespeed.X);
                XSpeed *= UI.stick.GetXMultiplier();


                if (fuelSystem.Usable())
                {
                    speed = new Vector2(YSpeed.X + XSpeed.X, YSpeed.Y + XSpeed.Y);
                }
                //rotMatrix = Matrix.CreateRotationZ(sprite.Rotation);
                //velocity = Vector2.Transform(XSpeed, rotMatrix);
                //sprite.Position = new Vector2(sprite.Position.X + velocity.Y, sprite.Position.Y + velocity.X);

                /*if (arrows[0].IsItPressed()) // left
                {
                    rotMatrix = Matrix.CreateRotationZ(sprite.Rotation);
                    velocity = Vector2.Transform(speed, rotMatrix);
                    sprite.Position = new Vector2(sprite.Position.X + velocity.Y, sprite.Position.Y + velocity.X);
                }
                //if (key.IsKeyDown(Keys.D))
                if (arrows[2].IsItPressed()) //right
                {
                    sprite.Position = new Vector2(sprite.Position.X - velocity.Y, sprite.Position.Y - velocity.X);
                }
                //if (key.IsKeyDown(Keys.W))
                if (arrows[1].IsItPressed()) // up
                {
                    //speed = new Vector2(0, -7);
                    rotMatrix = Matrix.CreateRotationZ(sprite.Rotation);
                    velocity = Vector2.Transform(speed, rotMatrix);
                    sprite.Position += velocity;
                }
                //if (key.IsKeyDown(Keys.S))
                if (arrows[3].IsItPressed()) //down
                {
                    //speed = new Vector2(0, -7);
                    speed *= -1;
                    rotMatrix = Matrix.CreateRotationZ(sprite.Rotation);
                    velocity = Vector2.Transform(speed, rotMatrix);
                    sprite.Position += velocity;
                }*/
            }


            if (UI.rotateLeft.IsItPressed()) //rotate left
            {
                chopperBase.Rotation -= rotate;
            }

            if (UI.rotateRight.IsItPressed()) //rotate right
            {
                chopperBase.Rotation += rotate;
            }
            if (UI.landButton.IsItPressed())
            {
                changeAltitude = true;
                landing = true;
            }



            //if (key.IsKeyDown(Keys.Space))
            if (UI.fireButton.IsItPressed())
            {
                /*If we fire with the system we need to know so we can pass the bullets to the game manager*/
                chopperBase.Fire(gameTime, ammoSystem);

                /*if (weaponSystem.Fire() && ammoSystem.Usable())
                {
                    Queue<Bullet> tmp = weaponSystem.GetBullets();
                    ammoSystem.Update(tmp.Count);
                    while (tmp.Count > 0)
                    {
                        bullets.Add(tmp.Peek());
                        tmp.Dequeue();
                    }
                }*/
            }

            chopperBase.Update(gameTime);
            /*weaponSystem.Update(gameTime);
            weaponSystem.UpdateLocations(chopperBase.Center, chopperBase.Rotation);*/

            //If not on ground:

            if (fuelSystem.Usable())
            {
                float consSpeed = Math.Max((Math.Abs(speed.X)), Math.Abs(speed.Y));
                float MaxSpeed = Math.Abs(basespeed.Y);
                float calculation = consSpeed / MaxSpeed;
                float calculateFuelUsage = (MAXFUELCONSUMPTION - BASEFUELCONSUMPTION) * calculation + BASEFUELCONSUMPTION;
                int roundUsage = (int)Math.Round(calculateFuelUsage);

                fuelSystem.Update(roundUsage);
            }
        }

        public List<Bullet> GetBullets()
        {
            List<Bullet> bullets = new List<Bullet>();

            Queue<Bullet> tmp = chopperBase.GetBullets();
            ammoSystem.Update(tmp.Count);
            while (tmp.Count > 0)
            {
                bullets.Add(tmp.Peek());
                tmp.Dequeue();
            }
            return bullets;

        }

        public List<Bullet> GetExplosiveBullets()
        {
            List<Bullet> bullets = new List<Bullet>();

            Queue<Bullet> tmp = chopperBase.GetExplosiveBullets();
            ammoSystem.Update(tmp.Count);
            while (tmp.Count > 0)
            {
                bullets.Add(tmp.Peek());
                tmp.Dequeue();
            }
            return bullets;

        }

        public void MustUpdate(GameTime gameTime)
        {
            healthSystem.Update(gameTime);
            if (healthSystem.Danger())
            {
                /*This can be used when you are falling down
                Vector2 generated = new Vector2(r.Next(101)-50, r.Next(101)-50);
                effects.Add(EffectsList.GetEffect(1, new Vector2(chopperBase.Center.X+ generated.X,chopperBase.Center.Y- generated.Y)));*/
                Vector2 generated = new Vector2(r.Next(25) - 12, r.Next(25) - 12);
                effects.Add(EffectsList.GetEffect( EffectType.Smoke, new Vector2(chopperBase.Center.X + generated.X, chopperBase.Center.Y - generated.Y)));
            }
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

        public int UpdateOnHelipad(GameTime gameTime)
        {
            MustUpdate(gameTime);
            //landButton.Update(gameTime);
            float fillrateFl = 10000 / (float)gameTime.TotalGameTime.TotalSeconds;
            int fillRate = (int)Math.Round(fillrateFl);
            int cost = 0;

            cost += ammoSystem.FillAmountReturnAmmount(1);
            cost += fuelSystem.FillAmountReturnAmmount(fillRate);
            cost += healthSystem.FillAmountReturnAmmount(1);

            if (UI.landButton.IsItPressed())
            {
                lifting = true;
            }

            playerCanLand = true;
            return cost;
        }

        public void UpdateOnLand(GameTime gameTime)
        {
            MustUpdate(gameTime);
            if (UI.landButton.IsItPressed())
            {
                lifting = true;
            }
        }

        public void PlayerCanLand(bool value)
        {
            playerCanLand = value;
        }

        /*public void EmptyBullets()
        {
            bullets.Clear();
        }*/

        bool triggeredExplosion = false;

        public void KillPlayer(GameTime gameTime)
        {
            MustUpdate(gameTime);

            if (!deathTimer.HitMax())
            {
                chopperBase.scale -= ((float)gameTime.ElapsedGameTime.TotalSeconds / 3 / deathTimer.MaxSecond);
                chopperBase.Rotation += 2.25f / 60.0f;
                chopperBase.Update(gameTime);
                deathTimer.Update(gameTime);
                Vector2 generated = new Vector2(r.Next(101) - 50, r.Next(101) - 50);
                effects.Add(EffectsList.GetEffect(EffectType.Smoke, new Vector2(chopperBase.Center.X + generated.X, chopperBase.Center.Y - generated.Y)));
            }
            else
            {
                Vector2 generated = new Vector2(r.Next(25) - 12, r.Next(25) - 12);
                effects.Add(EffectsList.GetEffect(EffectType.Smoke, new Vector2(chopperBase.Center.X + generated.X, chopperBase.Center.Y - generated.Y)));
                if (!triggeredExplosion)
                {
                    effects.Add(EffectsList.GetEffect(0, chopperBase.Center));
                    triggeredExplosion = true;
                }
            }
            if (onLand)
            {
                deathTimer.GoToEnd();
            }
        }

        public void LandPlayer(GameTime gameTime)
        {
            MustUpdate(gameTime);
            UI.landButton.disable();
            if (!landTimer.CanFireAndRestart())
            {
                chopperBase.scale -= ((float)gameTime.ElapsedGameTime.TotalSeconds / 3 / landTimer.MaxSecond);
                chopperBase.Update(gameTime);
                landTimer.Update(gameTime);
            }
            else
            {
                landing = false;
                onLand = true;
                UI.ChangeLandingButtonText(false);
                UI.landButton.Enable();
            }
        }

        public void LiftPlayer(GameTime gameTime)
        {
            MustUpdate(gameTime);
            UI.landButton.disable();
            onLand = false;
            if (!landTimer.CanFireAndRestart())
            {
                chopperBase.scale += ((float)gameTime.ElapsedGameTime.TotalSeconds / 3 / landTimer.MaxSecond);
                chopperBase.Update(gameTime);
                landTimer.Update(gameTime);
            }
            else
            {
                lifting = false;
                changeAltitude = false;
                chopperBase.scale = 1.0f;
                UI.ChangeLandingButtonText(true);
                UI.landButton.Enable();
            }
        }

        /*public void DrawSize(SpriteBatch spriteBatch, float layer)
        {
            chopperBase.Draw(spriteBatch, size, layer);
        }*/

        public void Draw(SpriteBatch spriteBatch, float layer)
        {
            chopperBase.Draw(spriteBatch, healthSystem.HitTimer.DrawColour(), layer);

            for (int i = 0; i < effects.Count; i++)
            {
                effects[i].Draw(spriteBatch, layer - 0.01f);
            }
            //weaponSystem.Draw(spriteBatch, layer - 0.01f);
            /*for (int i = 0; i < arrows.Count; i++)
            {
                arrows[i].Draw(spriteBatch, layer);
            }*/
        }




    }
}
