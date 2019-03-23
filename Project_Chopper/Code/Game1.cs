using Chopper.Code;
using Chopper.Code.Enemy;
using Chopper.Code.Weapons;
using Game1Test.Code;
using Game1Test.Code.Allies;
using Game1Test.Code.Choppers;
using Game1Test.Code.Effects;
using Game1Test.Code.Enemy;
using Game1Test.Code.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Diagnostics;

namespace Game1Test
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Hud hud;
        Camera camera;
        GameManager gameManager;
        MissionScreen missionScreen;
        //WeaponList weaponList;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //hud = new Hud(Content.Load<SpriteFont>("Hud"));
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            UI.LoadContent(Content, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            UIClass.LoadContent(Content);
            UpgradeSystem.LoadContent(Content, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            
            WeaponList.LoadContent(Content);
            ChopperList.LoadContent(Content);
            EffectsList.LoadContent(Content);

            EnemyMaker.LoadContent(Content);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Level level = new Level(500, 500);
           
            
            Player player = new Player(ChopperList.GetChopper(ChopperType.Training), new Vector2(500, 2000));
            //Player player = new Player(ChopperList.GetChopper(0), new Vector2(0, 0));
            hud = new Hud(Content.Load<SpriteFont>("hudFont"));
            camera = new Camera();
            //camera.Position = player.PlayerSpriteCenter;
            camera.Position = player.PlayerCenter;
            gameManager = new GameManager(level, player);
            
            List<Sprite> listOfEnemies = new List<Sprite>();
            listOfEnemies.Add(EnemyMaker.GetTurret(TurretType.MGun, new Vector2(0, 0), 0, 0));
            listOfEnemies.Add(EnemyMaker.GetTurret(TurretType.MGun, new Vector2(0, 700), 0, 0));
            listOfEnemies.Add(EnemyMaker.GetTurret(TurretType.MGun, new Vector2(700, 700), 0, 0));
            listOfEnemies.Add(EnemyMaker.GetTurret(TurretType.MGun, new Vector2(700, 0), 0, 0));
            listOfEnemies.Add(EnemyMaker.GetTurret(TurretType.Rocket, new Vector2(350, 350), 0, 0));
            listOfEnemies.Add(EnemyMaker.GetTurret(TurretType.RocketHoming, new Vector2(800, 800), 0, 0));
            listOfEnemies.Add(EnemyMaker.GetTurret(TurretType.ShotGun, new Vector2(500, 500), 3, 1));
            listOfEnemies.Add(EnemyMaker.GetTurret(TurretType.ShotGun, new Vector2(250, 450), 5, 0.5f));
            
            level.AddEntity(listOfEnemies, true, true, true);

            listOfEnemies.Clear();
            listOfEnemies.Add(new Building(Content.Load<Texture2D>("building"), new Vector2(1000, 1000)));
            List<Helipad> listOfHelipads = new List<Helipad>();
            listOfHelipads.Add(new Helipad(Content.Load<Texture2D>("Allies\\helipad"), new Vector2(500, 2000)));
            level.AddLandingPad(listOfHelipads);

            level.AddEntity(listOfEnemies, true, true, false);

            missionScreen = new MissionScreen(1, "GameTest", "Yarrr mission is to blow up\neverything\nBecause we want to\nAnd because you can\nBecause you have a\nfockin attackin\nchoppaaaaa!\nduhhh\n\n\n- General Kaminsky", graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, Color.SaddleBrown);
            GameData.menuState = MenuState.MissionMenu;
            // TODO: use this.Content to load your game content here

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (GameData.menuState == MenuState.MissionMenu)
            {
                missionScreen.Update(gameTime);
            }
            //camera.CameraFollow(gameManager.GetPlayer.PlayerSpriteCenter);
            if (GameData.gameState == GameState.InGame)
            {
                camera.CameraFollow(gameManager.GetPlayer.PlayerCenter);
                if (gameManager.GetPlayer.Alive)
                {
                    //camera.Rotation = -gameManager.GetPlayer.PlayerSprite.Rotation;
                    camera.Rotation = -gameManager.GetPlayer.PlayerRotation;
                }
                camera.update(gameTime);
                gameManager.Update(gameTime);
                TouchCollection touchCollection = TouchPanel.GetState();

                foreach (TouchLocation tl in touchCollection)
                {
                    if ((tl.State == TouchLocationState.Pressed)
                            || (tl.State == TouchLocationState.Moved))
                    {
                        //Debug.WriteLine(tl.Position.ToString());
                    }
                    hud.UpdateCoordinates(tl.Position.ToString());
                }

                hud.UpdateHealth(gameManager.GetPlayer.Health.ToString());

                hud.UpdateOilUsage(gameManager.GetPlayer.Fuel_System.ToString());

                hud.UpdateAmmoUsage(gameManager.GetPlayer.Ammo_System.ToString());

            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || GameData.gameState==GameState.Quit)
            {
                GameData.gameState = GameState.Menu;
                Exit();
            }
            // TODO: Add your update logic here

            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            /* GraphicsDevice.Clear(Color.CornflowerBlue);
             spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, null);
             hud.Draw(spriteBatch,graphics.PreferredBackBufferWidth,graphics.PreferredBackBufferHeight);

             // TODO: Add your drawing code here
             spriteBatch.End();
             base.Draw(gameTime);*/

            
            GraphicsDevice.Clear(Color.WhiteSmoke);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, camera.transformation(GraphicsDevice));

            //spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, camera.transformation(GraphicsDevice));

            // TODO: Add your drawing code here

            gameManager.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();

            /*for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw(spriteBatch, 1);
            }
            stick.Draw(spriteBatch, 1);*/
            if (GameData.menuState == MenuState.MissionMenu)
            {
                missionScreen.Draw(spriteBatch, 0.99f);
            }
            else if (GameData.inGameState != InGameState.Paused)
            {
                hud.Draw(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                UI.Draw(spriteBatch, 1);
            }
            else
            {
                //UI.Draw(spriteBatch, 0.9f);
                UpgradeSystem.Draw(spriteBatch);
            }
            


            /*if (gameManager.GetPlayer.playerCanLand)
            {
                landButton.Draw(spriteBatch, 1);
            }*/
            spriteBatch.End();

            base.Draw(gameTime);

        }
    }
}
