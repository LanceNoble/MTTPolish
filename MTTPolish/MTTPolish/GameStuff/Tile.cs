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

        public Tile(int positionX, int positionY)
        {
            x = positionX;
            y = positionY;
        }

        public bool Visited { get; set; } = false;

        public TileDirection Direction { get; set; } = TileDirection.None;
        public List<TileDirection> PossibleDirections { get; set; }

        public int X { get { return x; } }
        public int Y { get { return y; } }
    }
}
