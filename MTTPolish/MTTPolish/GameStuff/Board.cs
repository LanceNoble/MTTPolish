using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTPolish.GameStuff
{
    internal class Board
    {
        private string path;
        private char[][] layout;

        public Board(string path)
        {
            this.path = path;
            //layout = new char[9][9];
        }
    }
}
