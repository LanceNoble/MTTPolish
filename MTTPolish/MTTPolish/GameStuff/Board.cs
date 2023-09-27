using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MTTPolish.GameStuff
{
    /*
     * The board class generates
     * To-Do:
     * Make the path 
     */
    internal class Board
    {
        private const char grass = '.';
        private const char left  = '\u2190';
        private const char right = '\u2192';
        private const char up    = '\u2191';
        private const char down  = '\u2193';
        //private const char omni = '+'; // omni tiles will determine their direction based on the previous tile in the path

        private Random randomNumberGenerator;

        private char[,] map;
        private Tile[,] map1;
        private LinkedList<(int x, int y, char dir, List<char> possibles)> path;

        private int dimensionsX;
        private int dimensionsY;

        private int startX;
        private int startY;

        private int endX;
        private int endY;

        public Board(Random randomNumberGenerator, int dimensionsX, int dimensionsY, int startX, int startY, int endX, int endY)
        {
            this.randomNumberGenerator = randomNumberGenerator;

            this.dimensionsX = dimensionsX;
            this.dimensionsY = dimensionsY;

            this.startX = startX;
            this.startY = startY;

            this.endX = endX;
            this.endY = endY;

            map = new char[this.dimensionsX, this.dimensionsY];
            map1 = new Tile[this.dimensionsX, this.dimensionsY];
            path = new LinkedList<(int x, int y, char dir, List<char> possibles)>();
        }

        /*
         * Known Issues: The Stack method does prevent the StackOverflowException. However, there are still issues where a higher dimension = a longer time to generate (and I mean unbearably long like basically forever)
         * The only difference with this solution is that an exception will not be thrown and the program will not crash. Instead, the loop will just keep going and going
         * Possible solutions: set a timer, simply clear the map and call generate again once this timer runs out to reset generation
         * Probably don't need the Tile class Left Right Below and Above references
         * Maybe add even more constraints to prevent long generation times
         */
        public void Generate()
        {
            for (int x = 0; x < dimensionsX; x++)
            {
                for (int y = 0; y < dimensionsY; y++)
                {
                    //map[x, y] = grass;

                    Tile tile = new Tile(x, y);
                    map1[x, y] = tile;
                    //if (x - 1 != -1)
                    //    tile.Above = map1[x - 1, y];
                    //if (x + 1 != dimensionsX)
                    //    tile.Below = map1[x + 1, y];
                    //if (y - 1 != -1)
                    //    tile.Left = map1[x, y - 1];
                    //if (y + 1 != dimensionsY)
                    //    tile.Right = map1[x, y + 1];
                }
            }

            for (int x = 0; x < dimensionsX; x++)
            {
                for (int y = 0; y < dimensionsY; y++)
                {
                    //map[x, y] = grass;

                    //Tile tile = new Tile(x, y);
                    //map1[x, y] = tile;
                    if (x - 1 != -1)
                        map1[x, y].Above = map1[x - 1, y];
                    if (x + 1 != dimensionsX)
                        map1[x, y].Below = map1[x + 1, y];
                    if (y - 1 != -1)
                        map1[x, y].Left = map1[x, y - 1];
                    if (y + 1 != dimensionsY)
                        map1[x, y].Right = map1[x, y + 1];
                }
            }

            //path.Clear();
            //Walk(new((randomNumberGenerator.Next(1, dimensionsX - 1), 0, right, new List<char>() { right })));

            Stack<Tile> path1 = new Stack<Tile>();
            Tile lastTile = map1[randomNumberGenerator.Next(1, dimensionsX - 1), 0];
            lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.East };
            lastTile.Direction = TileDirection.East;
            //lastTile.Visited = true;
            //path1.Push(lastTile);

            while (lastTile.PositionY != dimensionsY - 1)
            {
                //Print();
                if (lastTile.Visited && lastTile.PossibleDirections.Count == 0)
                {
                    lastTile.Direction = TileDirection.None;
                    lastTile = path1.Pop();
                    lastTile.PossibleDirections.Remove(lastTile.Direction);
                   
                    continue;
                }
                else if (lastTile.Visited && lastTile.PossibleDirections.Count > 0)
                {
                    path1.Push(lastTile);
                    lastTile.Direction = lastTile.PossibleDirections[randomNumberGenerator.Next(0, lastTile.PossibleDirections.Count)];
                }

                if (!lastTile.Visited)
                {
                    path1.Push(lastTile);
                    lastTile.Visited = true;
                }
                
                if (path1.Peek().Direction == TileDirection.East)
                {
                    lastTile = path1.Peek().Right;
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.North, TileDirection.East, TileDirection.South };
                } 
                else if (path1.Peek().Direction == TileDirection.West)
                {
                    lastTile = path1.Peek().Left;
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.North, TileDirection.West, TileDirection.South };
                }  
                else if (path1.Peek().Direction == TileDirection.South)
                {
                    lastTile = path1.Peek().Below;
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.West, TileDirection.South, TileDirection.East };
                } 
                else if (path1.Peek().Direction == TileDirection.North)
                {
                    lastTile = path1.Peek().Above;
                    lastTile.PossibleDirections = new List<TileDirection>() { TileDirection.West, TileDirection.North, TileDirection.East };
                }

                if (lastTile.Right == null)
                {
                    lastTile.Direction = TileDirection.East;
                    break;
                }

                if (lastTile.Left == null /*|| lastTile.Right == null*/ || lastTile.Above == null || lastTile.Below == null || 
                    (lastTile.Left != path1.Peek() && lastTile.Left.Direction != TileDirection.None) || 
                    (lastTile.Right != path1.Peek() && lastTile.Right.Direction != TileDirection.None) || 
                    (lastTile.Above != path1.Peek() && lastTile.Above.Direction != TileDirection.None) || 
                    (lastTile.Below != path1.Peek() && lastTile.Below.Direction != TileDirection.None)) 
                {
                    lastTile = path1.Pop();
                    lastTile.PossibleDirections.Remove(lastTile.Direction);
                    //lastTile.Direction = TileDirection.None;
                    continue;
                }

                lastTile.Direction = lastTile.PossibleDirections[randomNumberGenerator.Next(0, lastTile.PossibleDirections.Count)];
                //lastTile.Visited = true;
                //path1.Push(lastTile);
            }
        }

        public void Print()
        {
            for (int i = 0; i < dimensionsX; i++)
            {
                for (int j = 0; j < dimensionsY; j++)
                    Debug.Write((char)map1[i, j].Direction);
                Debug.Write('\n');
            }
            Debug.Write('\n');
        }

        /*
         * To-Do:
         * Enable path to make loops and add omni tiles
         * 
         * Known Issues:
         * 1. The probability of a StackOverflowException being thrown increases as the `dimensions` field increases (a dimension of >= 20 seems to start making this exception much more common)
         * Potential Solutions:
         * 1. try catch every Walk call (even the recursive ones) <-- this is not a very pogramming solution
         * 1. Add more constraints to the path generator
         * 1. Decrease either the x dimension, y dimension, or both
         * 1. Tree and Stack for Depth First Search
         * 1. Fishnet graph
         *      initialize map to be a 2-D array of the Tile class
         *      every Tile has a List of possible adjacent tiles
         *      initialize every tile's possible adjacent neighbors in the 2-D array before starting the walk
         *      next, start the walk and traverse the map and back track via a stack
         */
        private void Walk(LinkedListNode<(int x, int y, char dir, List<char> possibles)> lastTile)
        {
            map[lastTile.Value.x, lastTile.Value.y] = lastTile.Value.dir;

            if (lastTile.Value.y == dimensionsY - 1)
            {
                lastTile.ValueRef.dir = right;
                map[lastTile.Value.x, lastTile.Value.y] = lastTile.Value.dir;
                return;
            }

            if (lastTile.List != null && lastTile.Value.possibles.Count == 0)
            {
                map[lastTile.Value.x, lastTile.Value.y] = grass;
                lastTile = lastTile.Previous;
                lastTile.Value.possibles.Remove(lastTile.Value.dir);
                path.RemoveLast();
                Walk(lastTile);
                return;
            }
            else if (lastTile.List != null && lastTile.Value.possibles.Count > 0)
            {
                lastTile.ValueRef.dir = lastTile.Value.possibles[randomNumberGenerator.Next(0, lastTile.Value.possibles.Count)];
                map[lastTile.Value.x, lastTile.Value.y] = lastTile.Value.dir;
            }

            /*
             * If this lastTile was backtracked to instead of it being fresh new one, add it to the end of the path.
             * This condition must be added or else LinkedList.AddLast will throw an exception saying that this lastTile has already been added
             */
            if (lastTile.List == null)
                path.AddLast(lastTile);

            (int x, int y, char dir, List<char> possibles) next = lastTile.Value;

            // potential optimization: use enum flags
            if (lastTile.Value.dir == left)
            {
                next.y--;
                next.possibles = new() { up, left, down };
            }
            else if (lastTile.Value.dir == right)
            {
                next.y++;
                next.possibles = new() { up, right, down };
            }
            else if (lastTile.Value.dir == up)
            {
                next.x--;
                next.possibles = new() { left, up, right };
            }
            else if (lastTile.Value.dir == down)
            {
                next.x++;
                next.possibles = new() { left, down, right };
            }

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int futureX = next.x + i;
                    int futureY = next.y + j;
                    if ((i != 0 && j != 0) ||
                        (futureX == lastTile.Value.x && futureY == lastTile.Value.y) ||
                        (i == 0 && j == 0))
                        continue;
                  
                    if (futureY == -1 || futureX == -1 || futureX == dimensionsX || (futureY < dimensionsY && map[futureX, futureY] != grass))
                    {
                        lastTile.Value.possibles.Remove(lastTile.Value.dir);
                        Walk(lastTile);
                        return;
                    }
                }
            }

            next.dir = next.possibles[randomNumberGenerator.Next(0, next.possibles.Count)];

            Walk(new(next));
            return;
        }
    }
}
