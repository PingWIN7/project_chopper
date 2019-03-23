using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;








namespace Game1Test.Code.Weapons
{
    class BulletType
    {
        public enum bulletType { Normal, Explosion };

        public bulletType type;
        
        public BulletType(bulletType type)
        {
            this.type = type;
        }
    }
}