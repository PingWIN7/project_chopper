using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game1Test.Code.UI;

namespace Game1Test.Code
{
    class MissionScreen
    {
        int missionNumber;
        string missionName;
        string missionDescription;
        int screenWidthHalf;
        int screenWidth;
        int screenHeight;
        Color color;
        SpriteFont spriteFont;

        ButtonWithText selectHelicopterButton;
        ButtonWithText customiseWeaponsButton;
        ButtonWithText upgradeMenuButton;
        ButtonWithText startMissionButton;
        ButtonWithText goBackButton;

        Texture2D backGround;


        public MissionScreen(int missionNumber, string missionName, string missionDescription, int screenWidth, int screenHeight, Color color)
        {
            screenWidthHalf = screenWidth / 2;

            this.missionNumber = missionNumber;
            this.missionName = missionName;
            this.missionDescription = missionDescription;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.color = color;

            int heightForButton = screenHeight / 6;

            spriteFont = UIClass.GetHudSpriteFont();
            selectHelicopterButton = new ButtonWithText(UIClass.GetBaseButtonTexture(), new Vector2(screenWidthHalf, heightForButton), UIClass.GetHudSpriteFont(), "Select Helicopter", 1);
            customiseWeaponsButton = new ButtonWithText(UIClass.GetBaseButtonTexture(), new Vector2(screenWidthHalf, heightForButton * 2), UIClass.GetHudSpriteFont(), "Customise Weapons", 1);
            upgradeMenuButton = new ButtonWithText(UIClass.GetBaseButtonTexture(), new Vector2(screenWidthHalf, heightForButton * 3), UIClass.GetHudSpriteFont(), "Upgrade Menu", 1);
            startMissionButton = new ButtonWithText(UIClass.GetBaseButtonTexture(), new Vector2(screenWidthHalf, heightForButton * 4), UIClass.GetHudSpriteFont(), "Start Mission", 1);
            goBackButton = new ButtonWithText(UIClass.GetBaseButtonTexture(), new Vector2(screenWidthHalf, heightForButton * 5), UIClass.GetHudSpriteFont(), "Go Back", 1);

            backGround = UIClass.GetBaseButtonTexture();
        }

        public void Update(GameTime gameTime)
        {
            selectHelicopterButton.Update(gameTime,screenWidthHalf);
            customiseWeaponsButton.Update(gameTime, screenWidthHalf);
            upgradeMenuButton.Update(gameTime, screenWidthHalf);
            startMissionButton.Update(gameTime, screenWidthHalf);
            goBackButton.Update(gameTime, screenWidthHalf);

            if (upgradeMenuButton.IsItReleased())
            {
                GameData.menuState = MenuState.MainMenu;
                GameData.inGameState = InGameState.Paused;
                GameData.gameState = GameState.InGame;
                GameData.pauseReason = PauseReason.Upgrade;
                //healthUpgradeButton.disable();
            }

            if (goBackButton.IsItReleased())
            {
                GameData.gameState = GameState.Quit;
            }

            if (startMissionButton.IsItReleased())
            {
                GameData.menuState = MenuState.MainMenu;
                GameData.inGameState = InGameState.Going;
                GameData.gameState = GameState.InGame;
                GameData.pauseReason = PauseReason.Upgrade;
            }
        }

        public void Draw(SpriteBatch spriteBatch, float layer)
        {
            spriteBatch.DrawString(spriteFont, missionNumber+". "+ missionName, new Vector2(screenWidthHalf/2, 0), Color.Black);

            selectHelicopterButton.OverrideDrawDimension(spriteBatch, layer + 0.01f, screenWidthHalf, selectHelicopterButton.Texture.Height);
            customiseWeaponsButton.OverrideDrawDimension(spriteBatch, layer + 0.01f, screenWidthHalf, selectHelicopterButton.Texture.Height);
            upgradeMenuButton.OverrideDrawDimension(spriteBatch, layer + 0.01f, screenWidthHalf, selectHelicopterButton.Texture.Height);
            startMissionButton.OverrideDrawDimension(spriteBatch, layer + 0.01f, screenWidthHalf, selectHelicopterButton.Texture.Height);
            goBackButton.OverrideDrawDimension(spriteBatch, layer + 0.01f, screenWidthHalf, selectHelicopterButton.Texture.Height);

            spriteBatch.DrawString(spriteFont, missionDescription, new Vector2(250, 200), Color.Black);

            //BG
            spriteBatch.Draw(backGround,
               new Rectangle(0, 0, screenWidth, screenHeight),
               new Rectangle(0, 0, backGround.Width, backGround.Height),
               color);

            //MissionTitle
            spriteBatch.Draw(backGround,
               new Rectangle(0, 0, screenWidthHalf, 100),
               new Rectangle(0, 0, backGround.Width, backGround.Height),
               color);

            //MissionDescription
            spriteBatch.Draw(backGround,
               new Rectangle(0, 200, screenWidthHalf, 700),
               new Rectangle(0, 0, backGround.Width, backGround.Height),
               color);

            //Screens
            spriteBatch.Draw(backGround,
               new Rectangle(0, 200, 250, 700),
               new Rectangle(0, 0, backGround.Width, backGround.Height),
               color);

        }
    }
}