using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.Timers;
using MTTPolish.Mechanics.Goblins;

namespace MTTPolish.Mechanics.Mages
{
    /*
     * Protects the player from goblins
     */
    internal class Mage
    {
        private Rectangle box;
        private Texture2D texture;
        private Vector2 magePosition;

        private Rectangle bulletBox;
        private Vector2 bulletDirection;
        private Vector2 bulletPosition;
        private Texture2D bulletTexture;
        private Timer bulletTime;
        private bool bulletActive;
        private float bulletSpeed;

        private int range;
        private int damage;

        private bool canFire;
        private Timer fireRate;

        public Mage(Tile tile)
        {
            bulletActive = false;

            bulletTime = new Timer(2000);
            bulletTime.AutoReset = false;
            bulletTime.Elapsed += (x, y) =>
            {
                bulletActive = false;
            };

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

        public Vector2 Seek(List<Goblin> goblins)
        {
            if (!canFire)
                return -Vector2.One;

            Vector2 closestGoblinPosition = goblins[0].Position;

            double closestGoblinDistance = double.MaxValue;
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
            }

            if (closestGoblinDistance > range)
                return -Vector2.One;

            bulletDirection = Vector2.Normalize(closestGoblinPosition - magePosition);

            bulletActive = true;
            bulletTime.Start();

            canFire = false;
            fireRate.Start();

            return closestGoblinPosition;
        }

        public void Fire()
        {
            if (!bulletActive)
                return;



            // if bullet hits target, make it disappear

            // 

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
