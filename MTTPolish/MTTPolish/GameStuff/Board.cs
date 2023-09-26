using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MTTPolish.GameStuff
{
    internal class Board
    {
        private char[,] map;
        private Random rndGen = new();

        private const int dimensions = 10;

        private const char grass = '.';
        private const char left  = '\u2190';
        private const char right = '\u2192';
        private const char up    = '\u2191';
        private const char down  = '\u2193';
        //private const char omni = '+'; // omni tiles will determine their direction based on the previous tile in the path

        LinkedList<(int x, int y, char dir, List<char> possibles)> path;

        // To-Do: optimize this path generation, it gets mad slow once the dimensions are 30 x 30
        // possible solution: implement a fish net graph
        public Board()
        {
            map = new char[dimensions, dimensions];

            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                    map[i, j] = grass;
            }

            path = new();

            Walk(new((rndGen.Next(1, dimensions - 1), 0, right, new List<char>() { right })));

            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                    Debug.Write(map[i, j]);
                Debug.Write('\n');
            }
            Debug.Write('\n');
        }

        private void Walk(LinkedListNode<(int x, int y, char dir, List<char> possibles)> lastTile)
        {
            if (lastTile.Value.y == dimensions - 1)
                return;

            if (lastTile.ValueRef.possibles.Count == 0)
            {
                map[lastTile.ValueRef.x, lastTile.ValueRef.y] = grass;
                lastTile = lastTile.Previous;
                lastTile.ValueRef.possibles.Remove(lastTile.ValueRef.dir);
                path.RemoveLast();
                Walk(lastTile);
                return;
            }
            else
            {
                lastTile.ValueRef.dir = lastTile.ValueRef.possibles[rndGen.Next(0, lastTile.ValueRef.possibles.Count)];
                map[lastTile.ValueRef.x, lastTile.ValueRef.y] = lastTile.ValueRef.dir;
            }

            if (lastTile.List == null)
                path.AddLast(lastTile);

            map[lastTile.Value.x, lastTile.Value.y] = lastTile.Value.dir;

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

            if (next.y == -1 || next.y == dimensions || next.x == -1 || next.x == dimensions || map[next.x, next.y] != grass)
            {
                lastTile.ValueRef.possibles.Remove(lastTile.ValueRef.dir);
                Walk(lastTile);
                return;
            }

            next.dir = next.possibles[rndGen.Next(0, next.possibles.Count)];

            Walk(new(next));
        }
    }
}
