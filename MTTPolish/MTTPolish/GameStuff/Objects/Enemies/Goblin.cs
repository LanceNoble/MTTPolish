using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTPolish.GameStuff.Objects.Enemies
{
    internal class Goblin
    {
        private int x;
        private int y;
        private int spd;
        private int dmg;
        private int health;
        private Rectangle box;
        private Texture2D tex;

        public Goblin(int x, int y) 
        {
            spd = 5;
            dmg = 5;
            health = 5;
        }

        public void Hurt()
        {

        }

        public void Move()
        {

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, box, Color.White);
        }
    }
}
