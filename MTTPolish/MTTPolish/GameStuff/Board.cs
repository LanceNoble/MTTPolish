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
        private Random randomNumberGenerator; // Puts the 'random' in random path

        private Tile[,] map; // Board layout showing what is on each and every single tile at all times
        private Tile[] path; // Path that the enemy will follow

        // Board dimensions
        private int sizeX;
        private int sizeY;

        public Board(Random randomNumberGenerator, int sizeX, int sizeY)
        {
            this.randomNumberGenerator = randomNumberGenerator;

            this.sizeX = sizeX;
            this.sizeY = sizeY;

            map = new Tile[this.sizeX, this.sizeY];
        }

        public Tile[] Path { get { return path; } }
        public Tile[,] Map { get { return map; } }

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
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                    map[x, y] = new Tile(x, y);
            }

            Stack<Tile> path = new Stack<Tile>();
            Tile lastTile = map[randomNumberGenerator.Next(1, sizeX - 1), 0];
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
                    lastTile.Direction = lastTile.PossibleDirections[randomNumberGenerator.Next(0, lastTile.PossibleDirections.Count)];
                }

                if (!lastTile.Visited)
                {
                    path.Push(lastTile);
                    lastTile.Visited = true;
                }
                
                if (path.Peek().Direction == TileDirection.East)
                {
                    lastTile = map[path.Peek().X, path.Peek().Y + 1];
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.North, TileDirection.East, TileDirection.South };
                } 
                else if (path.Peek().Direction == TileDirection.West)
                {
                    lastTile = map[path.Peek().X, path.Peek().Y - 1];
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.North, TileDirection.West, TileDirection.South };
                }  
                else if (path.Peek().Direction == TileDirection.South)
                {
                    lastTile = map[path.Peek().X + 1, path.Peek().Y];
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.West, TileDirection.South, TileDirection.East };
                } 
                else if (path.Peek().Direction == TileDirection.North)
                {
                    lastTile = map[path.Peek().X - 1, path.Peek().Y];
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.West, TileDirection.North, TileDirection.East };
                }

                if (lastTile.Y + 1 == sizeY)
                {
                    path.Push(lastTile);
                    lastTile.Direction = TileDirection.East;
                    break;
                }

                if (lastTile.Y - 1 == -1 || lastTile.X - 1 == -1 || lastTile.X + 1 == sizeX || 
                    (map[lastTile.X, lastTile.Y - 1] != path.Peek() && map[lastTile.X, lastTile.Y - 1].Direction != TileDirection.None) || 
                    (map[lastTile.X, lastTile.Y + 1] != path.Peek() && map[lastTile.X, lastTile.Y + 1].Direction != TileDirection.None) || 
                    (map[lastTile.X - 1, lastTile.Y] != path.Peek() && map[lastTile.X - 1, lastTile.Y].Direction != TileDirection.None) || 
                    (map[lastTile.X + 1, lastTile.Y] != path.Peek() && map[lastTile.X + 1, lastTile.Y].Direction != TileDirection.None)) 
                {
                    lastTile = path.Pop();
                    lastTile.PossibleDirections.Remove(lastTile.Direction);
                    continue;
                }

                lastTile.Direction = lastTile.PossibleDirections[randomNumberGenerator.Next(0, lastTile.PossibleDirections.Count)];
            }

            this.path = new Tile[path.Count];

            // I transferred the Stack data to an array so that any tile on the path can be accessed at anytime 
            for (int i = this.path.Length - 1; path.Count != 0; i--) // Reverse because Stack is FILO
                this.path[i] = path.Pop();
        }

        public void Print()
        {
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                    Debug.Write((char)map[i, j].Direction);
                Debug.Write('\n');
            }
            Debug.Write('\n');
        }
    }
}
