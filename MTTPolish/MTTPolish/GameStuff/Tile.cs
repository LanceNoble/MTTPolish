using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MTTPolish.GameStuff
{
    internal class Tile
    {
        private int x;
        private int y;
        private Rectangle box;
        private HashSet<Vector2> possibleDirections;

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
            int dimensionsX = 40;
            int dimensionsY = 40;
            box = new Rectangle(this.x * dimensionsX, this.y * dimensionsY, dimensionsX, dimensionsY);
            possibleDirections = new HashSet<Vector2>() { -Vector2.UnitX, -Vector2.UnitY, Vector2.UnitX, Vector2.UnitY };
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }
        public Rectangle Box { get { return box; } }
        public HashSet<Vector2> PossibleDirections { get { return possibleDirections; } }

        public Vector2 CurrentDirection { get; set; } = Vector2.Zero;
        public bool Visited { get; set; } = false;

        public void Reset()
        {
            if (possibleDirections.Count != 0)
                return;
            possibleDirections.Add(-Vector2.UnitX);
            possibleDirections.Add(-Vector2.UnitY);
            possibleDirections.Add(Vector2.UnitX);
            possibleDirections.Add(Vector2.UnitY);
            CurrentDirection = Vector2.Zero;
            Visited = false;
        }

        public void SetRandomDirection(Random rng)
        {
            if (possibleDirections.Count == 0)
                return;
            HashSet<Vector2>.Enumerator iterator = possibleDirections.GetEnumerator();
            iterator.MoveNext(); // Iterators in C# do not initially start at the first element in the data container, you have to make it move to the first element yourself
            int end = rng.Next(0, possibleDirections.Count);
            for (int i = 0; i < end; i++)
                iterator.MoveNext();
            CurrentDirection = iterator.Current;
        }
    }
}
