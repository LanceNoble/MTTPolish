using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MTTPolish.GameStuff
{
    internal class Tile
    {
        public enum Direction
        {
            Left,
            Right,
            Up,
            Down,
        }
        private enum Type
        {

        }
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
        }
    }
}
