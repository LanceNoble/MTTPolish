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
        private Rectangle bulletBox;
        private Texture2D texture;
        private Texture2D bulletTexture;
        
        private int range;
        private int damage;
        private float speed;

        private bool canFire;
        private Timer fireRate;

        private double closestGoblinDistance;
        private Vector2 closestGoblinPosition;

        public Mage(Tile tile)
        {
            fireRate = new Timer(1000);
            fireRate.AutoReset = false;
            fireRate.Elapsed += (x, y) =>
            {
                canFire = true;
            };

            range = 500;
            damage = 1;

            box = new Rectangle(tile.Box.X + tile.Box.Width / 4, tile.Box.Y - tile.Box.Height / 2, tile.Box.Height, tile.Box.Height);
            
        }

        public void Seek(List<Goblin> goblins)
        {
            if (!canFire) 
                return;

            closestGoblinPosition = goblins[0].Position;

            closestGoblinDistance = int.MaxValue;
            double currentGoblinDistance;
            float currentGoblinXDistance;
            float currentGoblinYDistance;
            for (int i = 0; i < goblins.Count; i++)
            {
                currentGoblinXDistance = goblins[i].Box.Center.X - box.Center.X;
                currentGoblinYDistance = goblins[i].Box.Center.Y - box.Center.Y;

                currentGoblinDistance = Math.Sqrt(Math.Pow(currentGoblinXDistance, 2) + Math.Pow(currentGoblinYDistance, 2));

                if (currentGoblinDistance < closestGoblinDistance)
                {
                    closestGoblinDistance = currentGoblinDistance;
                    closestGoblinPosition = goblins[i].Position;
                }
                /*
                if (distance < range)
                    goblins[i].Health -= damage;

                if (goblins[i].Health <= 0)
                    goblins.Remove(goblins[i]);
                */
            }

            canFire = false;
            fireRate.Start();
        }

        public void Fire()
        {
            if (closestGoblinDistance > range)
                return;

            if (canFire)
            {

            }


        }

        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            sb.Draw(texture, box, Color.DeepSkyBlue);

            if (!canFire)
            {
                // Draw bullet
            }
        }
    }
}
