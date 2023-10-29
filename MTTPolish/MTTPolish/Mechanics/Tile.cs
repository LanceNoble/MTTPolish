using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MTTPolish.GameStuff
{
    /*
     * Represents an object that has its own bounding box with a unique position
     * Tells entities standing on it what direction they should move in
     * Should be used to draw the environment
     * Should be used in a 2D array to create a tile-based board
     */
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
            int dimensionsX = 80;
            int dimensionsY = 80;
            box = new Rectangle(this.x * dimensionsX, this.y * dimensionsY, dimensionsX, dimensionsY);
            possibleDirections = new HashSet<Vector2>() { -Vector2.UnitX, -Vector2.UnitY, Vector2.UnitX, Vector2.UnitY };
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }
        public Rectangle Box { get { return box; } }
        public HashSet<Vector2> PossibleDirections { get { return possibleDirections; } }

        //public Texture2D Texture { get; set; } = null;
        //public float Rotation { get; set; } = 0;
        //public SpriteEffects Flip { get; set; } = SpriteEffects.None;
        public Vector2 Direction { get; set; } = Vector2.Zero;
        public bool Visited { get; set; } = false;

        /*
         * This method prevents the path generation algorithm from getting stuck
         * If the path has to backtrack, it should reset the tile it backtracked from to ensure that it is able to visit it again later
         * If it doesn't, it will never be able to traverse it again, leaving it with even less options to path towards the more it backtracks
         * this will go on until it has no options to path towards since it backtracked way too much, thus it gets stuck
         * this problem gets even worse with larger map dimensions
         * this was the cause of the infinite loop problem or stack overflow exceptions you got while backtracking
         * Now, thanks to this solution, you can make the map as big as you want and it'll never get stuck! (i think...)
         */
        public void Reset()
        {
            if (possibleDirections.Count != 0)
                return;
            possibleDirections.Add(-Vector2.UnitX);
            possibleDirections.Add(-Vector2.UnitY);
            possibleDirections.Add(Vector2.UnitX);
            possibleDirections.Add(Vector2.UnitY);
            Direction = Vector2.Zero;
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
            Direction = iterator.Current;
        }
    }
}
