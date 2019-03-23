using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Chopper.Code;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Game1Test.Code.Upgrades;

namespace Game1Test.Code
{
    static class UpgradeSystem
    {
        static SpriteFont spriteFont;

        static string healthString = "Health Upgrade:";
        static Button healthUpgradeButton;
        static HealthUpgrade healthUpgrade;
        static string healthStatString;

        static string oilString = "Oil Upgrade:";
        static Button oilUpgradeButton;
        static OilUpgrade oilUpgrade;
        static string oilStatString;

        static string ammoString = "Ammo Upgrade:";
        static Button ammoUpgradeButton;
        static AmmoUpgrade ammoUpgrade;
        static string ammoStatString;

        static public Button finishedButton;


        static public void LoadContent(ContentManager Content, int BufferWidth, int BufferHeight)
        {
            Texture2D baseButtonTexture = Content.Load<Texture2D>("UI\\buttonBase");
            spriteFont = Content.Load<SpriteFont>("hudFont");

            healthUpgrade = new HealthUpgrade();
            healthUpgradeButton = new ButtonWithText(baseButtonTexture, new Vector2(50, 50), spriteFont, "Health+", 1.0f);

            oilUpgrade = new OilUpgrade();
            oilUpgradeButton = new ButtonWithText(baseButtonTexture, new Vector2(50, 250), spriteFont, "Oil+", 1.0f);

            ammoUpgrade = new AmmoUpgrade();
            ammoUpgradeButton = new ButtonWithText(baseButtonTexture, new Vector2(50, 450), spriteFont, "Ammo+", 1.0f);


            finishedButton = new ButtonWithText(baseButtonTexture, new Vector2(BufferWidth - baseButtonTexture.Width, BufferHeight - 50 - baseButtonTexture.Height), spriteFont, "Finish", 1.0f);

        }

        static public void Update(GameTime gameTime, Player player)
        {
            healthUpgradeButton.Update(gameTime);
            oilUpgradeButton.Update(gameTime);
            ammoUpgradeButton.Update(gameTime);

            finishedButton.Update(gameTime);

            healthStatString = player.Health_System.ToString();
            oilStatString = player.Fuel_System.ToString();
            ammoStatString = player.Ammo_System.ToString();

            if (healthUpgradeButton.IsItReleased())
            {
                RunUpgrade(player.Health_System, healthUpgrade, player.Health_System.level+1);
                //healthUpgradeButton.disable();
            }
            if (oilUpgradeButton.IsItReleased())
            {
                RunUpgrade(player.Fuel_System, oilUpgrade, player.Fuel_System.level+1);
                //oilUpgradeButton.disable();
            }
            if (ammoUpgradeButton.IsItReleased())
            {
                RunUpgrade(player.Ammo_System, ammoUpgrade, player.Ammo_System.level+1);
                //ammoUpgradeButton.disable();
            }
        }

        static private void RunUpgrade(HelicopterSystem heliSystem, Upgrade upgrade, int level)
        {
            if (GotEnoughMoney(upgrade.GetPrice(level)))
            {
                float NewStat = upgrade.GetUpgrade(level);
                heliSystem.ApplyUpgrade((int)NewStat, (int)NewStat,level);
            }
        }

        static private bool GotEnoughMoney(float cost)
        {
            return true;
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                spriteBatch.DrawString(spriteFont, healthString, new Vector2(0, 0), Color.Black);
                healthUpgradeButton.Draw(spriteBatch, 1.0f);
                spriteBatch.DrawString(spriteFont, healthStatString, new Vector2(healthUpgradeButton.Position.X + healthUpgradeButton.Texture.Width, healthUpgradeButton.Position.Y), Color.Black);

                spriteBatch.DrawString(spriteFont, oilString, new Vector2(0, 200), Color.Black);
                oilUpgradeButton.Draw(spriteBatch, 1.0f);
                spriteBatch.DrawString(spriteFont, oilStatString, new Vector2(oilUpgradeButton.Position.X + oilUpgradeButton.Texture.Width, oilUpgradeButton.Position.Y), Color.Black);

                spriteBatch.DrawString(spriteFont, ammoString, new Vector2(0, 400), Color.Black);
                ammoUpgradeButton.Draw(spriteBatch, 1.0f);
                spriteBatch.DrawString(spriteFont, ammoStatString, new Vector2(ammoUpgradeButton.Position.X + ammoUpgradeButton.Texture.Width, ammoUpgradeButton.Position.Y), Color.Black);

                finishedButton.Draw(spriteBatch, 1.0f);
            }
            catch
            {

            }
        }

    }
}