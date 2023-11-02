using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTPolish.Mechanics
{
    /// <summary>
    /// Anything that is drawn on the screen
    /// </summary>
    internal abstract class Entity
    {
        private Vector2 pos;
        private Vector2 dir;
        private double spd;
        private double width;
        private double height;
        private double rad;
        private int dmg;
        private int hp;

        public Entity(Vector2 pos, Vector2 dir, double spd, double width, double height, double rad, int dmg, int hp) 
        {
            this.pos = pos;
            this.dir = dir;
            this.spd = spd;
            this.width = width;
            this.height = height;
            this.rad = rad;
            this.dmg = dmg;
            this.hp = hp;
        }

        public void Move()
        {

        }

        public void Rotate()
        {

        }

        public bool Intersects(Entity ent)
        {
            return false;
        }

        public bool Overlaps(Entity ent)
        {
            return false;
        }

        public abstract void Draw(SpriteBatch sb, Texture2D tex);
    }
}
