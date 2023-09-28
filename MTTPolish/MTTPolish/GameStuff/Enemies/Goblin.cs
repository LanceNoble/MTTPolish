﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MTTPolish.GameStuff.Enemies
{
    internal class Goblin
    {
        private int spd; // entity speeds should be multiples of 5
        private int dmg;
        private int hp;
        private Rectangle box;
        private Texture2D texture;
        private Tile[] path;
        private int currentPathPosition;
        

        public Goblin(Tile[] path)
        {
            currentPathPosition = 0; 
            spd = 2;
            dmg = 5;
            hp = 5;
            this.path = path;

            box = new Rectangle(this.path[0].Box.X + this.path[0].Box.Width / 4, this.path[0].Box.Y - this.path[0].Box.Height / 2, this.path[0].Box.Height, this.path[0].Box.Height);
        }

        public void Hurt()
        {

        }

        /*
         * Moves the Goblin
         * 
         * Known Issues:
         * 1. If the Goblin's speed is too high, it can bypass tiles and go off course
         * 1. Can't move by float values
         * 
         * Potential Solutions:
         */
        public void Move()
        {
            /*if (!box.Intersects(path[0].Box))
            {
                box.X += spd;
                return;
            }
            else
                currentPathPosition = 0;*/

            if (currentPathPosition == path.Length)
                return;

            switch (path[currentPathPosition].Direction)
            {
                case TileDirection.West:
                    box.X -= spd;
                    break;
                case TileDirection.North:
                    box.Y -= spd / 2;
                    break;
                case TileDirection.East:
                    box.X += spd;
                    break;
                case TileDirection.South:
                    box.Y += spd / 2;
                    break;
                default:
                    break;
            }

            /*
             * This complex conditional enables the player to view enemy movement at an angle rather than a bird's eye view
             * If you want the enemies to fit inside the tiles while moving instead of their position being offset, use
             * this conditional: `!path[currentPathPosition].Box.Intersects(box)`
             */
            if (currentPathPosition + 1 < path.Length && 
                ((box.Center.X >= path[currentPathPosition + 1].Box.Center.X && path[currentPathPosition].Direction == TileDirection.East) ||
                (box.Center.X <= path[currentPathPosition + 1].Box.Center.X && path[currentPathPosition].Direction == TileDirection.West) ||
                (box.Bottom >= path[currentPathPosition + 1].Box.Center.Y && path[currentPathPosition].Direction == TileDirection.South) ||
                (box.Bottom <= path[currentPathPosition + 1].Box.Center.Y && path[currentPathPosition].Direction == TileDirection.North)))
                currentPathPosition++;
        }

        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            sb.Draw(texture, box, Color.White);
        }
    }
}
