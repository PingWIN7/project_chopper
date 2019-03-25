using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;







using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1Test
{
    class Hud
    {
        private string health;
        private string oil;
        private string ammo;
        private List<string> listCoordinates;
        private SpriteFont hudFont;

        public Hud(SpriteFont hudFont)
        {
            this.hudFont = hudFont;
            listCoordinates = new List<string>();
        }

        public void UpdateHealth(string health)
        {
            this.health = health;
        }

        public void UpdateCoordinates(string coordinates)
        {
            //this.coordinates = coordinates;
            listCoordinates.Add(coordinates);
        }

        public void UpdateOilUsage(string oil)
        {
            this.oil = oil;
        }

        public void UpdateAmmoUsage(string ammo)
        {
            this.ammo = ammo;
        }

        public void Draw(SpriteBatch spriteBatch, int resWidth, int resHeight)
        {
            spriteBatch.DrawString(hudFont, "HP: " + health, new Vector2(150, resHeight - 50), Microsoft.Xna.Framework.Color.Black);
            for (int i = 0; i < listCoordinates.Count; i++)
            {
                spriteBatch.DrawString(hudFont, listCoordinates[i], new Vector2(50, resHeight - (350+50*i) ), Microsoft.Xna.Framework.Color.Black);
            }
            listCoordinates.Clear();

            spriteBatch.DrawString(hudFont, "OIL: " + oil, new Vector2(0, 0), Microsoft.Xna.Framework.Color.Black);
            spriteBatch.DrawString(hudFont, "AMMO: " + ammo, new Vector2(0, 50), Microsoft.Xna.Framework.Color.Black);

        }
    }
}