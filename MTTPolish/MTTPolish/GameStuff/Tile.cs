using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MTTPolish.GameStuff
{
    enum TileDirection
    {
        None  = '.',
        West  = '\u2190',
        North = '\u2191',
        East  = '\u2192',
        South = '\u2193',
        Omni  = '+',
    }
    internal class Tile
    {
        private int x;
        private int y;
        private Rectangle box;

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
            int dimensions = 40;
            box = new Rectangle(this.x * dimensions, this.y * dimensions, dimensions, dimensions);
        }

        public Rectangle Box { get { return box; } }
        public bool Visited { get; set; } = false;
        public TileDirection Direction { get; set; } = TileDirection.None;
        public List<TileDirection> PossibleDirections { get; set; }
        public int X { get { return x; } }
        public int Y { get { return y; } }
    }
}
