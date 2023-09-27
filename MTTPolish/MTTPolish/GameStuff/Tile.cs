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
    }
    internal class Tile
    {
        private int positionX;
        private int positionY;

        public Tile(int positionX, int positionY)
        {
            this.positionX = positionX;
            this.positionY = positionY;
        }

        public bool Visited { get; set; } = false;

        public TileDirection Direction { get; set; } = TileDirection.None;
        public List<TileDirection> PossibleDirections { get; set; } = new List<TileDirection>() { TileDirection.West, TileDirection.North, TileDirection.East, TileDirection.South };

        public int PositionX { get { return positionX; } }
        public int PositionY { get { return positionY; } }

        public Tile Left  { get; set; } = null;
        public Tile Right { get; set; } = null;
        public Tile Above { get; set; } = null;
        public Tile Below { get; set; } = null;
    }
}
