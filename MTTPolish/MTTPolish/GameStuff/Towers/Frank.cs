using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MTTPolish.GameStuff.Enemies;
using System;

namespace MTTPolish.GameStuff.Towers
{
    /*
     * Protects the player from goblins
     */
    internal class Frank
    {
        private Rectangle box;
        private Texture2D texture;
        private int range;
        private int damage;
        private int fireRate;
        private int health;

        public Frank(Tile tile) 
        {
            range = 500;
            damage = 1;

            box = new Rectangle(tile.Box.X + tile.Box.Width / 4, tile.Box.Y - tile.Box.Height / 2, tile.Box.Height, tile.Box.Height);
        }

        public void Fire(List<Goblin> goblins)
        {
            float distance;
            float xDistance;
            float yDistance;
            for (int i = 0; i < goblins.Count; i++)
            {
                xDistance = goblins[i].Box.Center.X - box.Center.X;
                yDistance = goblins[i].Box.Center.Y - box.Center.Y;

                distance = (float)Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));

                if (distance < range)
                    goblins[i].Health -= damage;

                if (goblins[i].Health <= 0)
                    goblins.Remove(goblins[i]);
            }
        }

        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            sb.Draw(texture, box, Color.DeepSkyBlue);
        }
    }
}
