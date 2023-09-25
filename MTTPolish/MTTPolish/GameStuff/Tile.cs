using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MTTPolish.GameStuff
{
    [Flags]
    enum TileType
    {
        Left  = 1,
        Right = 2,
        Up    = 4,
        Down  = 8,
        Omni  = 16,
        Grass = 32,
    }
    internal class Tile
    {
        private Tile previousTile;
        private int xCoordinate;
        private int yCoordinate;
        private TileType nextPossibleTileTypes;
           
        public int XCoordinate { get { return xCoordinate; } }
        public int YCoordinate { get { return yCoordinate; } }
        public TileType TileType { get; set; }
        public TileType NextPossibleTileTypes { get { return nextPossibleTileTypes; } }
        public Tile PreviousTile { get { return previousTile; } }

        public Tile(int xCoordinate, int yCoordinate, Tile previousTile = null, TileType tileType = TileType.Right)
        {
            TileType = tileType;
            this.previousTile = previousTile;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
            nextPossibleTileTypes = TileType.Left | TileType.Right | TileType.Up | TileType.Down;
        }

        public void RemovePossibility(TileType possibility)
        {
            if ((int)possibility > (int)TileType.Down || !nextPossibleTileTypes.HasFlag(possibility))
                return;
            nextPossibleTileTypes &= ~possibility;
        }

        /*public enum Direction
        {
            Left,
            Right,
            Up,
            Down,
        }
        private const int width = 50;
        private const int height = 50;
        

        private Direction dir;
        private Rectangle box;
        private Texture2D tex;
        public Tile(Direction dir, Rectangle box)
        {
            this.dir = dir;
            this.box = box;
        }
        public Point Pos 
        { 
            set
            {
                box.X = value.X;
                box.Y = value.Y;
            } 
        }
        public Direction Dir { get { return dir; } }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, box, Color.White);
        }*/
    }
}
