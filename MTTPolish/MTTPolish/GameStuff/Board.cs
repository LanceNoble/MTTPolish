using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MTTPolish.GameStuff
{
    /*
     * Generates a tile-based map and enemy path and draws the environment
     */
    internal class Board
    {
        private Random rng;
        private Tile[] map, path;

        // Make the environment look more varied
        private float[] grassRotations;
        private SpriteEffects[] grassFlips;

        private int sizeX, sizeY;

        public Board(Random rng, int sizeX, int sizeY)
        {
            this.rng = rng;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            
            map = new Tile[this.sizeX * this.sizeY];

            grassRotations = new float[this.sizeX * this.sizeY];
            for (int i = 0; i < grassRotations.Length; i++)
                grassRotations[i] = rng.Next(0, 4) * (float)(Math.PI / 2);

            grassFlips = new SpriteEffects[this.sizeX * this.sizeY];
            for (int i = 0; i < grassFlips.Length; i++)
                grassFlips[i] = (SpriteEffects)rng.Next(0, 3) | (SpriteEffects)rng.Next(0, 3) | (SpriteEffects)rng.Next(0, 3);
        }

        public Tile[] Path { get { return path; } }
        public Tile[] Map { get { return map; } }

        /*
         * Generates the path
         * 
         * To-Do:
         * 1. Add looping
         */
        public void Generate()
        {
            for (int i = 0; i < sizeX * sizeY; i++)
                map[i] = new Tile(i % sizeX, i / sizeX);

            Stack<Tile> path = new Stack<Tile>();
            Tile lastTile = map[sizeX * rng.Next(1, sizeY - 2)];
            lastTile.Direction = Vector2.UnitX;

            while (true) // I put the end path condition `lastTile.Y + 1 != sizeY` inside the loop, so it gets checked before the potentially program crashing conditional occurs
            {
                if (lastTile.Visited && lastTile.PossibleDirections.Count == 0)
                {
                    lastTile.Reset();
                    lastTile = path.Pop();
                    lastTile.PossibleDirections.Remove(lastTile.Direction);
                    continue;
                }
                else if (lastTile.Visited && lastTile.PossibleDirections.Count > 0)
                {
                    path.Push(lastTile);
                    lastTile.SetRandomDirection(rng);
                }

                if (!lastTile.Visited)
                {
                    path.Push(lastTile);
                    lastTile.Visited = true;
                }

                lastTile = map[(path.Peek().X + (int)path.Peek().Direction.X) + (sizeX * (path.Peek().Y + (int)path.Peek().Direction.Y))];
                lastTile.PossibleDirections.Remove(-path.Peek().Direction);

                // End path condition
                if (lastTile.X + 1 == sizeX)
                {
                    path.Push(lastTile);
                    lastTile.Direction = Vector2.UnitX;
                    break;
                }

                // Constraints
                if (lastTile.Y - 1 == -1 || lastTile.X - 1 == -1 || lastTile.Y + 1 == sizeY || 
                    (map[(lastTile.X - 1) + (sizeX * lastTile.Y)] != path.Peek() && map[(lastTile.X - 1) + (sizeX * lastTile.Y)].Direction != Vector2.Zero) || 
                    (map[(lastTile.X + 1) + (sizeX * lastTile.Y)] != path.Peek() && map[(lastTile.X + 1) + (sizeX * lastTile.Y)].Direction != Vector2.Zero) || 
                    (map[lastTile.X + (sizeX * (lastTile.Y - 1))] != path.Peek() && map[lastTile.X + (sizeX * (lastTile.Y - 1))].Direction != Vector2.Zero) || 
                    (map[lastTile.X + (sizeX * (lastTile.Y + 1))] != path.Peek() && map[lastTile.X + (sizeX * (lastTile.Y + 1))].Direction != Vector2.Zero)) 
                {
                    lastTile = path.Pop();
                    lastTile.PossibleDirections.Remove(lastTile.Direction);
                    continue;
                }

                lastTile.SetRandomDirection(rng);
            }

            this.path = new Tile[path.Count];

            // I transferred the Stack data to an array so that any tile on the path can be accessed at anytime 
            for (int i = this.path.Length - 1; path.Count != 0; i--) // Reverse because Stack is FILO
                this.path[i] = path.Pop();
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D grassPath, Texture2D lPath, Texture2D tPath, Texture2D straightPath)
        {
            // Draw grass layer
            for (int i = 0; i < sizeX * sizeY; i++)
            {
                Rectangle offset = map[i].Box;
                offset.X += offset.Width / 2;
                offset.Y += offset.Height / 2;

                /*
                 * Note: The Vector2 origin parameter refers to the TEXTURE's local origin, not the destination rectangle's local origin.
                 * The destination rectangle's position offset (which is a result of the Vector2 origin parameter) then scales with your TEXTURE's local origin offset.
                 * In other words, the destination rectangle's offset is proportional to the TEXTURE's local origin offset
                 */
                spriteBatch.Draw(grassPath, offset, null, Color.White, grassRotations[i], new Vector2(grassPath.Width / 2, grassPath.Height / 2), grassFlips[i], 0);
            }
                
            // Draw path
            for (int i = 0; i < path.Length; i++)
            {
                Rectangle offset = path[i].Box;
                offset.X += offset.Width / 2;
                offset.Y += offset.Height / 2;

                if (i == 0 || i == path.Length - 1 ||
                    (path[i - 1].Direction == Vector2.UnitX && path[i].Direction == Vector2.UnitX) ||
                    (path[i - 1].Direction == -Vector2.UnitX && path[i].Direction == -Vector2.UnitX))
                    spriteBatch.Draw(straightPath, path[i].Box, Color.White);
                else if ((path[i - 1].Direction == -Vector2.UnitY && path[i].Direction == -Vector2.UnitY) || (path[i - 1].Direction == Vector2.UnitY && path[i].Direction == Vector2.UnitY))
                    spriteBatch.Draw(straightPath, offset, null, Color.White, (float)((Math.PI / 2)), new Vector2(straightPath.Width / 2, straightPath.Height / 2), SpriteEffects.None, 1);
                else if ((path[i - 1].Direction == Vector2.UnitX && path[i].Direction == -Vector2.UnitY) || (path[i - 1].Direction == Vector2.UnitY && path[i].Direction == -Vector2.UnitX))
                    spriteBatch.Draw(lPath, path[i].Box, Color.White);
                else if ((path[i - 1].Direction == -Vector2.UnitX && path[i].Direction == -Vector2.UnitY) || (path[i - 1].Direction == Vector2.UnitY && path[i].Direction == Vector2.UnitX))
                    spriteBatch.Draw(lPath, offset, null, Color.White, (float)((Math.PI / 2) * 1), new Vector2(lPath.Width / 2, lPath.Height / 2), SpriteEffects.None, 1);
                else if ((path[i - 1].Direction == -Vector2.UnitX && path[i].Direction == Vector2.UnitY) || (path[i - 1].Direction == -Vector2.UnitY && path[i].Direction == Vector2.UnitX))
                    spriteBatch.Draw(lPath, offset, null, Color.White, (float)((Math.PI / 2) * 2), new Vector2(lPath.Width / 2, lPath.Height / 2), SpriteEffects.None, 1);
                else if ((path[i - 1].Direction == Vector2.UnitX && path[i].Direction == Vector2.UnitY) || (path[i - 1].Direction == -Vector2.UnitY && path[i].Direction == -Vector2.UnitX) )
                    spriteBatch.Draw(lPath, offset, null, Color.White, (float)((Math.PI / 2) * 3), new Vector2(lPath.Width / 2, lPath.Height / 2), SpriteEffects.None, 1);
            }
        }

        public void Print()
        {
            for (int i = 0; i < sizeX * sizeY; i++)
            {
                Debug.WriteIf(i % sizeX == 0, '\n');
                Debug.WriteIf(map[i].Direction == Vector2.Zero, '.');
                Debug.WriteIf(map[i].Direction == -Vector2.UnitX, '\u2190');
                Debug.WriteIf(map[i].Direction == -Vector2.UnitY, '\u2191');
                Debug.WriteIf(map[i].Direction == Vector2.UnitX, '\u2192');
                Debug.WriteIf(map[i].Direction == Vector2.UnitY, '\u2193');
            }
            Debug.Write('\n');
        }
    }
}
