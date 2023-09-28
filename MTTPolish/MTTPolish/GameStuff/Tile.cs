using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        private int width;
        private int height;

        private Rectangle box;

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;

            width = 40;
            height = 20;

            box = new Rectangle(this.y * width, this.x * height + 360, width, height); // The 360 is an offset shifting the path downwards more
        }

        public Rectangle Box { get { return box; } }
        public bool Visited { get; set; } = false;

        public TileDirection Direction { get; set; } = TileDirection.None;
        public List<TileDirection> PossibleDirections { get; set; }

        public int X { get { return x; } }
        public int Y { get { return y; } }
         
        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, box, Color.White);
        }
    }
}
