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
            lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.East };
            lastTile.Direction = TileDirection.East;

            while (true) // I put the condition `lastTile.Y + 1 != sizeY` inside the loop, so it gets checked before the potentially program crashing conditional occurs
            {
                if (lastTile.Visited && lastTile.PossibleDirections.Count == 0)
                {
                    lastTile.Direction = TileDirection.None;
                    lastTile = path.Pop();
                    lastTile.PossibleDirections.Remove(lastTile.Direction);
                    continue;
                }
                else if (lastTile.Visited && lastTile.PossibleDirections.Count > 0)
                {
                    path.Push(lastTile);
                    lastTile.Direction = lastTile.PossibleDirections[rng.Next(0, lastTile.PossibleDirections.Count)];
                }

                if (!lastTile.Visited)
                {
                    path.Push(lastTile);
                    lastTile.Visited = true;
                }
                
                if (path.Peek().Direction == TileDirection.East)
                {
                    lastTile = map[(path.Peek().X + 1) + (sizeX * path.Peek().Y)];
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.North, TileDirection.East, TileDirection.South };
                } 
                else if (path.Peek().Direction == TileDirection.West)
                {
                    lastTile = map[(path.Peek().X - 1) + (sizeX * path.Peek().Y)];
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.North, TileDirection.West, TileDirection.South };
                }  
                else if (path.Peek().Direction == TileDirection.South)
                {
                    lastTile = map[path.Peek().X + (sizeX * (path.Peek().Y + 1))];
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.West, TileDirection.South, TileDirection.East };
                } 
                else if (path.Peek().Direction == TileDirection.North)
                {
                    lastTile = map[path.Peek().X + (sizeX * (path.Peek().Y - 1))];
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.West, TileDirection.North, TileDirection.East };
                }

                if (lastTile.X + 1 == sizeX)
                {
                    path.Push(lastTile);
                    lastTile.Direction = TileDirection.East;
                    break;
                }

                if (lastTile.Y - 1 == -1 || lastTile.X - 1 == -1 || lastTile.Y + 1 == sizeY || 
                    (map[(lastTile.X - 1) + (sizeX * lastTile.Y)] != path.Peek() && map[(lastTile.X - 1) + (sizeX * lastTile.Y)].Direction != TileDirection.None) || 
                    (map[(lastTile.X + 1) + (sizeX * lastTile.Y)] != path.Peek() && map[(lastTile.X + 1) + (sizeX * lastTile.Y)].Direction != TileDirection.None) || 
                    (map[lastTile.X + (sizeX * (lastTile.Y - 1))] != path.Peek() && map[lastTile.X + (sizeX * (lastTile.Y - 1))].Direction != TileDirection.None) || 
                    (map[lastTile.X + (sizeX * (lastTile.Y + 1))] != path.Peek() && map[lastTile.X + (sizeX * (lastTile.Y + 1))].Direction != TileDirection.None)) 
                {
                    lastTile = path.Pop();
                    lastTile.PossibleDirections.Remove(lastTile.Direction);
                    continue;
                }

                lastTile.Direction = lastTile.PossibleDirections[rng.Next(0, lastTile.PossibleDirections.Count)];
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
               
            }
        }

        public void Print()
        {
            for (int i = 0; i < sizeX * sizeY; i++)
            {
                if (i % sizeX == 0)
                    Debug.Write('\n');
                Debug.Write((char)map[i].Direction);  
            }
        }
    }
}
