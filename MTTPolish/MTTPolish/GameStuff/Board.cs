using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MTTPolish.GameStuff
{
    /*
     * Generates a map with a path that enemies must follow and keeps track of what is on each and every single tile at all times
     */
    internal class Board
    {
        private Random rng; // Puts the 'random' in random path
        private Tile[] map, path; // Board layout showing what is on each and every single tile at all times
        private float[] grassRotations;
        private SpriteEffects[] grassFlips;
        private SpriteEffects[] flipOptions;
        private int sizeX, sizeY; // Board dimensions

        public Board(Random rng, int sizeX, int sizeY)
        {
            this.rng = rng;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            
            map = new Tile[this.sizeX * this.sizeY];

            grassRotations = new float[this.sizeX * this.sizeY];
            for (int i = 0; i < grassRotations.Length; i++)
                grassRotations[i] = rng.Next(0, 4) * (float)(Math.PI / 2);

            flipOptions = new SpriteEffects[3] { SpriteEffects.None, SpriteEffects.FlipVertically, SpriteEffects.FlipHorizontally};

            grassFlips = new SpriteEffects[this.sizeX * this.sizeY];
            for (int i = 0; i < grassFlips.Length; i++)
                grassFlips[i] = flipOptions[rng.Next(0, flipOptions.Length)];
        }

        public Tile[] Path { get { return path; } }
        public Tile[] Map { get { return map; } }

        /*
         * Generates the path
         * 
         * Known Issues:
         * 1. Larger size = larger chance this method gets stuck
         * 
         * Potential Solutions:
         * 1. More constraints
         * 1. Recall after timer expires
         * 1. BFS
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
            lastTile.CurrentDirection = Vector2.UnitX;

            while (true) // I put the condition `lastTile.Y + 1 != sizeY` inside the loop, so it gets checked before the potentially program crashing conditional occurs
            {
                if (lastTile.Visited && lastTile.PossibleDirections.Count == 0)
                {
                    lastTile.Reset();
                    lastTile = path.Pop();
                    lastTile.PossibleDirections.Remove(lastTile.CurrentDirection);
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

                lastTile = map[(path.Peek().X + (int)path.Peek().CurrentDirection.X) + (sizeX * (path.Peek().Y + (int)path.Peek().CurrentDirection.Y))];
                //lastTile = map[(path.Peek().X + (int)path.Peek().CurrentDirection.X) * (path.Peek().Y + (int)path.Peek().CurrentDirection.Y)];
                lastTile.PossibleDirections.Remove(-path.Peek().CurrentDirection);
                //lastTile.PossibleDirections.Remove(Vector2.Zero);
                //Debug.Write(lastTile.PossibleDirections.Count);
                //Debug.Write('\n');

                /*if (path.Peek().DirectionVectors == TileDirection.East)
                {
                    lastTile = map[(path.Peek().X + 1) + (sizeX * path.Peek().Y)];
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.North, TileDirection.East, TileDirection.South };
                } 
                else if (path.Peek().DirectionVectors == TileDirection.West)
                {
                    lastTile = map[(path.Peek().X - 1) + (sizeX * path.Peek().Y)];
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.North, TileDirection.West, TileDirection.South };
                }  
                else if (path.Peek().DirectionVectors == TileDirection.South)
                {
                    lastTile = map[path.Peek().X + (sizeX * (path.Peek().Y + 1))];
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.West, TileDirection.South, TileDirection.East };
                } 
                else if (path.Peek().DirectionVectors == TileDirection.North)
                {
                    lastTile = map[path.Peek().X + (sizeX * (path.Peek().Y - 1))];
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.West, TileDirection.North, TileDirection.East };
                }*/

                if (lastTile.X + 1 == sizeX)
                {
                    path.Push(lastTile);
                    lastTile.CurrentDirection = Vector2.UnitX;
                    //lastTile.PossibleDirections = TileDirection.East;
                    break;
                }

                if (lastTile.Y - 1 == -1 || lastTile.X - 1 == -1 || lastTile.Y + 1 == sizeY || 
                    (map[(lastTile.X - 1) + (sizeX * lastTile.Y)] != path.Peek() && map[(lastTile.X - 1) + (sizeX * lastTile.Y)].CurrentDirection != Vector2.Zero) || 
                    (map[(lastTile.X + 1) + (sizeX * lastTile.Y)] != path.Peek() && map[(lastTile.X + 1) + (sizeX * lastTile.Y)].CurrentDirection != Vector2.Zero) || 
                    (map[lastTile.X + (sizeX * (lastTile.Y - 1))] != path.Peek() && map[lastTile.X + (sizeX * (lastTile.Y - 1))].CurrentDirection != Vector2.Zero) || 
                    (map[lastTile.X + (sizeX * (lastTile.Y + 1))] != path.Peek() && map[lastTile.X + (sizeX * (lastTile.Y + 1))].CurrentDirection != Vector2.Zero)) 
                {
                    lastTile = path.Pop();
                    lastTile.PossibleDirections.Remove(lastTile.CurrentDirection);
                    //Debug.Write(lastTile.PossibleDirections.Remove(lastTile.CurrentDirection));
                    //Debug.Write(lastTile.PossibleDirections.Count);
                    //Debug.Write('\n');
                    continue;
                }

                lastTile.SetRandomDirection(rng);

                //Print();
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

                /*if (i == 0 || i == path.Length - 1 ||
                    (path[i - 1].Direction == TileDirection.East && path[i].Direction == TileDirection.East) ||
                    (path[i - 1].Direction == TileDirection.West && path[i].Direction == TileDirection.West))
                    spriteBatch.Draw(straightPath, path[i].Box, Color.White);
                else if ((path[i - 1].Direction == TileDirection.North && path[i].Direction == TileDirection.North) || (path[i - 1].Direction == TileDirection.South && path[i].Direction == TileDirection.South))
                    spriteBatch.Draw(straightPath, offset, null, Color.White, (float)((Math.PI / 2)), new Vector2(straightPath.Width / 2, straightPath.Height / 2), SpriteEffects.None, 1);
                else if ((path[i - 1].Direction == TileDirection.East && path[i].Direction == TileDirection.North) || (path[i - 1].Direction == TileDirection.South && path[i].Direction == TileDirection.West))
                    spriteBatch.Draw(lPath, path[i].Box, Color.White);
                else if ((path[i - 1].Direction == TileDirection.West && path[i].Direction == TileDirection.North) || (path[i - 1].Direction == TileDirection.South && path[i].Direction == TileDirection.East))
                    spriteBatch.Draw(lPath, offset, null, Color.White, (float)((Math.PI / 2) * 1), new Vector2(lPath.Width / 2, lPath.Height / 2), SpriteEffects.None, 1);
                else if ((path[i - 1].Direction == TileDirection.West && path[i].Direction == TileDirection.South) || (path[i - 1].Direction == TileDirection.North && path[i].Direction == TileDirection.East))
                    spriteBatch.Draw(lPath, offset, null, Color.White, (float)((Math.PI / 2) * 2), new Vector2(lPath.Width / 2, lPath.Height / 2), SpriteEffects.None, 1);
                else if ((path[i - 1].Direction == TileDirection.East && path[i].Direction == TileDirection.South) || (path[i - 1].Direction == TileDirection.North && path[i].Direction == TileDirection.West) )
                    spriteBatch.Draw(lPath, offset, null, Color.White, (float)((Math.PI / 2) * 3), new Vector2(lPath.Width / 2, lPath.Height / 2), SpriteEffects.None, 1);*/
            }
        }

        public void Print()
        {
            for (int i = 0; i < sizeX * sizeY; i++)
            {
                Debug.WriteIf(i % sizeX == 0, '\n');
                Debug.WriteIf(map[i].CurrentDirection == Vector2.Zero, '.');
                Debug.WriteIf(map[i].CurrentDirection == -Vector2.UnitX, '\u2190');
                Debug.WriteIf(map[i].CurrentDirection == -Vector2.UnitY, '\u2191');
                Debug.WriteIf(map[i].CurrentDirection == Vector2.UnitX, '\u2192');
                Debug.WriteIf(map[i].CurrentDirection == Vector2.UnitY, '\u2193');
                // Debug.WriteIf(map[i].Direction == Vector2.One, '+');
            }
            Debug.Write('\n');
        }
    }
}
