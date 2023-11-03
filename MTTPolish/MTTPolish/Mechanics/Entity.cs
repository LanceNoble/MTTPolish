using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MTTPolish.Mechanics
{
    /// <summary>
    /// Anything that is drawn on the screen
    /// </summary>
    internal abstract class Entity
    {
        private Vector2 pos;
        private Vector2 dir;
        private float spd;
        private double width;
        private double height;
        private double rad;
        private int dmg;
        private int hp;

        public Entity(Vector2 pos, Vector2 dir, float spd, double width, double height, double rad, int dmg, int hp) 
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

        public void Teleport(float x, float y)
        {
            pos.X = x; 
            pos.Y = y;
        }

        public void Move()
        {
            pos += dir * spd;
        }

        public void Orient(double rad)
        {
            dir.X = dir.X * (float)Math.Cos(rad) - dir.Y * (float)Math.Sin(rad);
            dir.Y = dir.X * (float)Math.Sin(rad) - dir.Y * (float)Math.Cos(rad);
        }

        public void Rotate(double rad)
        {
            dir.X += dir.X * (float)Math.Cos(rad) - dir.Y * (float)Math.Sin(rad);
            dir.Y += dir.X * (float)Math.Sin(rad) - dir.Y * (float)Math.Cos(rad);
        }

        public void Hurt(Entity ent)
        {
            ent.hp -= dmg;
        }

        public bool Intersects(Entity ent)
        {
            if (pos.X >= ent.pos.X)
                return true;

            return false;
        }

        public bool Overlaps(Entity ent)
        {
            return false;
        }

        public abstract void Draw(SpriteBatch sb, Texture2D tex);
    }
}
