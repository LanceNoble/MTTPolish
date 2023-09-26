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
        private char[,] map;
        private Random rndGen = new Random();

        //private const int dimensions = 20;

        private const int dimensionsX = 10;
        private const int dimensionsY = 30;

        private const char grass = '.';
        private const char left  = '\u2190';
        private const char right = '\u2192';
        private const char up    = '\u2191';
        private const char down  = '\u2193';
        //private const char omni = '+'; // omni tiles will determine their direction based on the previous tile in the path

        private LinkedList<(int x, int y, char dir, List<char> possibles)> path;

        public Board()
        {
            map = new char[dimensionsX, dimensionsY];
            path = new LinkedList<(int x, int y, char dir, List<char> possibles)>();

            Reset();
            Walk(new((rndGen.Next(1, dimensionsX - 1), 0, right, new List<char>() { right })));
            Print();
        }

        private void Reset()
        {
            for (int i = 0; i < dimensionsX; i++)
            {
                for (int j = 0; j < dimensionsY; j++)
                    map[i, j] = grass;
            }
            path.Clear();
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
                lastTile.ValueRef.dir = lastTile.Value.possibles[rndGen.Next(0, lastTile.Value.possibles.Count)];
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

            next.dir = next.possibles[rndGen.Next(0, next.possibles.Count)];

            Walk(new(next));
            return;
        }

        private void Print()
        {
            for (int i = 0; i < dimensionsX; i++)
            {
                for (int j = 0; j < dimensionsY; j++)
                    Debug.Write(map[i, j]);
                Debug.Write('\n');
            }
            Debug.Write('\n');
        }
    }
}
