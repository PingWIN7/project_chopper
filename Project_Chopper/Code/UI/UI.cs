using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game1Test.Code.UI
{
    static class UI
    {

        /*public UI()
        {

        }*/

        //static public Stick stick;
        //static public Button rotateLeft;
        //static public Button rotateRight;
        //static public Button fireButton;
        static public ButtonWithText landButton;
        static public Button upgradeButton;
        static public Button zoomInButton;
        static public Button zoomOutButton;
        static public bool showMissionTextOnScreen;
        static private string missionText;
        static private SpriteFont spritefont;
        static private SpriteFont developerSpritefont;

        static public bool enableDeveloperStats=false;
        static private List<string> developerStats;



        static public void LoadContent(ContentManager Content, int BufferWidth, int BufferHeight)
        {
            //stick=new Stick()

            float buttonTextTransparency = 1;

            Texture2D stickBaseTexture = Content.Load<Texture2D>("UI\\base");
            Texture2D stickTexture = Content.Load<Texture2D>("UI\\stick");

            Texture2D baseButtonTexture = Content.Load<Texture2D>("UI\\buttonBase");

            Texture2D leftArrowTexture = Content.Load<Texture2D>("UI\\rotateLeft");
            Texture2D rightArrowTexture = Content.Load<Texture2D>("UI\\rotateRight");
            //Texture2D fireButtonTexture = Content.Load<Texture2D>("UI\\fire");
            //Texture2D landButtonTexture = Content.Load<Texture2D>("UI\\land");
            Texture2D upgradeButtonTexture = Content.Load<Texture2D>("UI\\upgrade");

            Texture2D zoomInTexture = Content.Load<Texture2D>("UI\\zoomIn");
            Texture2D zoomOutTexture = Content.Load<Texture2D>("UI\\zoomOut");

            spritefont = Content.Load<SpriteFont>("hudFont");

            //stick = new Stick(stickBaseTexture, stickTexture, new Vector2(BufferWidth - 50 - rightArrowTexture.Width - stickBaseTexture.Width, BufferHeight - 50 - stickBaseTexture.Height));

            //fireButton = new Button(fireButtonTexture, new Vector2(5, BufferHeight - 50 - fireButtonTexture.Height));
            //fireButton = new ButtonWithText(baseButtonTexture, new Vector2(5, BufferHeight - 50 - baseButtonTexture.Height),spritefont,"Fire",buttonTextTransparency);

           //rotateLeft = new Button(leftArrowTexture, new Vector2(BufferWidth - 50 - stickBaseTexture.Width - leftArrowTexture.Width - rightArrowTexture.Width, BufferHeight - 50 - stickBaseTexture.Height));
            //rotateRight = new Button(rightArrowTexture, new Vector2(BufferWidth - 50 - rightArrowTexture.Width, BufferHeight - 50 - stickBaseTexture.Height));

            //landButton = new Button(landButtonTexture, new Vector2(BufferWidth / 2 - landButtonTexture.Width / 2, BufferHeight - 50 - landButtonTexture.Height));
            landButton = new ButtonWithText(baseButtonTexture, new Vector2(BufferWidth / 2 - baseButtonTexture.Width / 2, BufferHeight - 50 - baseButtonTexture.Height),spritefont,"Land",buttonTextTransparency);

            //upgradeButton = new Button(upgradeButtonTexture, new Vector2(BufferWidth / 2 - landButtonTexture.Width / 2, BufferHeight - 50 - landButtonTexture.Height*2));
            upgradeButton = new ButtonWithText(baseButtonTexture, new Vector2(BufferWidth / 2 - baseButtonTexture.Width / 2, BufferHeight - 50 - baseButtonTexture.Height * 2),spritefont,"Upgrade",buttonTextTransparency);


            zoomInButton = new Button(zoomInTexture, new Vector2(BufferWidth-50 * 2 - zoomOutTexture.Width - zoomInTexture.Width, 0));
            zoomOutButton = new Button(zoomOutTexture, new Vector2(BufferWidth-50 - zoomOutTexture.Width, 0));

            developerStats = new List<string>();

            developerSpritefont = Content.Load<SpriteFont>("DeveloperFont");
        }

        static public void Update (GameTime gameTime)
        {
            //stick.Update(gameTime);
            //rotateLeft.Update(gameTime);
            //rotateRight.Update(gameTime);
            //fireButton.Update(gameTime);
            landButton.Update(gameTime);
            upgradeButton.Update(gameTime);
            zoomInButton.Update(gameTime);
            zoomOutButton.Update(gameTime);
        }

        static public void UpdateDeveloperStats(List<string> stats)
        {
            developerStats.Clear();
            developerStats = stats;
        }

        static public void EnableTextOnScreen(int triggerValue)
        {
            switch (triggerValue)
            {
                case 0:
                    showMissionTextOnScreen = true;
                    missionText = "Return and land on helipad";
                    break;
                case 100:
                    showMissionTextOnScreen = true;
                    missionText = "Mission completed";
                    break;
                default:
                    break;
            }

        }
        
        static public void ChangeLandingButtonText(bool inAir)
        {
            if (inAir)
            {
                landButton.ChangeText("Land");
            }
            else
            {
                landButton.ChangeText("Lift");
            }
        }

        static public void Draw(SpriteBatch spriteBatch,float layer)
        {
            //stick.Draw(spriteBatch, layer);
            //rotateLeft.Draw(spriteBatch, layer);
            //rotateRight.Draw(spriteBatch, layer);
            //fireButton.Draw(spriteBatch, layer);
            landButton.Draw(spriteBatch, layer);
            upgradeButton.Draw(spriteBatch, layer);
            zoomInButton.Draw(spriteBatch, layer);
            zoomOutButton.Draw(spriteBatch, layer);
            if (showMissionTextOnScreen)
            {
                spriteBatch.DrawString(spritefont, missionText , new Vector2(700, 200), Microsoft.Xna.Framework.Color.Black);
            }
            if (enableDeveloperStats)
            {
                spriteBatch.DrawString(developerSpritefont, "Developer stats:", new Vector2(0, 300), Microsoft.Xna.Framework.Color.Black);
                for (int i = 0; i < developerStats.Count; i++)
                {
                    spriteBatch.DrawString(developerSpritefont, developerStats[i], new Vector2(0, 325+i*25), Microsoft.Xna.Framework.Color.Black);
                }
            }
        }
    }
}