using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MTTPolish.GameStuff.Enemies
{
    internal class Goblin
    {
        private int spd;
        private int dmg;
        private int hp;
        private Vector2 pos;
        private Rectangle box;
        private Texture2D tex;

        public Goblin(int x, int y)
        {
            spd = 5;
            dmg = 5;
            hp = 5;
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
