using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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
        private Tile currentTile;
        private Queue<Tile> path;
        

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
            switch (path.Peek())
            {
                // case tile.direction == left
                // box.X -= spd;
                // case tile.direction == right
                // box.X += spd;
                // case tile.direction == up
                // box.Y -= spd;
                // case tile.direction == down
                // box.X += spd;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, box, Color.White);
        }
    }
}
