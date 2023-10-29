using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MTTPolish.GameStuff.Enemies;
using System;
using System.Timers;

namespace MTTPolish.GameStuff.Towers
{
    /*
     * Protects the player from goblins
     */
    internal class Mage
    {
        private Rectangle box;
        private Texture2D texture;
        //private int fireRate;
        private int range;
        private int bulletDamage;
        private float bulletSpeed;

        private bool canFire;
        private Timer fireRate;

        private Vector2 currentTargetPosition;


        public Mage(Tile tile)
        {
            fireRate = new Timer(1000);
            fireRate.AutoReset = false;
            fireRate.Elapsed += (x, y) =>
            {
                canFire = true;
            };

            range = 500;
            bulletDamage = 1;

            box = new Rectangle(tile.Box.X + tile.Box.Width / 4, tile.Box.Y - tile.Box.Height / 2, tile.Box.Height, tile.Box.Height);
            
        }

        public void Fire(List<Goblin> goblins)
        {
            if (!canFire) 
                return;

            Goblin closestGoblin = goblins[0];

            float distance;
            float xDistance;
            float yDistance;
            for (int i = 0; i < goblins.Count; i++)
            {
                xDistance = goblins[i].Box.Center.X - box.Center.X;
                yDistance = goblins[i].Box.Center.Y - box.Center.Y;

                distance = (float)Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));

                if (distance < range)
                    goblins[i].Health -= bulletDamage;

                if (goblins[i].Health <= 0)
                    goblins.Remove(goblins[i]);
            }

            canFire = false;
            fireRate.Start();
        }

        public void MoveBullets()
        {

        }

        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            sb.Draw(texture, box, Color.DeepSkyBlue);
        }
    }
}
