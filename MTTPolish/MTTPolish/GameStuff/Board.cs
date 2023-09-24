using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MTTPolish.GameStuff
{
    internal class Board
    {
        /*[Flags]
        private enum Direction
        {
            Left  = 0b_0000_0000,
            Right = 0b_0000_0001,
            Up    = 0b_0000_0010,
            Down  = 0b_0000_0100,
        }*/

        private char[,] layout;
        // rules:
        // both the start and end tile are guaranteed to move right
        // every tile except for the start and end must be touching EXACTLY two other tiles OR 4
        // those two other adjacent tiles can be from any direction
        // the start tile can ONLY touch one other path tile and that tile must be from the right
        // for the end tile, the one path tile must come from the left instead
        // if there are 3 adjacent tiles, that means the path is bunching up and snaking
        // that means users cannot place towers inbetween the snaking paths
        // giving the enemies a safe space for traversing
        // the enemies must ALWAYS be in danger of being shot
        // dilemma: should i allow the path to bunch up and snake???

        private Random rndGen = new Random();

        private const int dimensions = 10;

        private const char grass = '.';
        private const char left = '\u2190';
        private const char right = '\u2192';
        private const char up = '\u2191';
        private const char down = '\u2193';
        private const char omni = '+'; // omni tiles will determine their direction based on the previous tile in the path

        public Board()
        {
            layout = new char[dimensions, dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                    layout[i, j] = '.';
            }

            // Decrease the range of possible starting X positions to ensure that the starting tile isn't spawned on the upper and lower borders of the map
            (int x, int y, char dir) currentTile = (rndGen.Next(1, dimensions - 1), 0, right);

            layout[currentTile.x, currentTile.y] = currentTile.dir;

            while (currentTile.y < dimensions - 1 && currentTile.y >= 0 && currentTile.x >= 0 && currentTile.x < dimensions - 1)
            {
                for (int i = 0; i < dimensions; i++)
                {
                    for (int j = 0; j < dimensions; j++)
                    {
                        Debug.Write(layout[i, j]);
                    }
                    Debug.Write("\n");
                }
                Debug.Write("\n");

                List<char> possibilities = new List<char>() { left, right, up, down };

                if (currentTile.dir == right)
                    currentTile.y++;
                else if (currentTile.dir == up)
                    currentTile.x--;
                else if (currentTile.dir == down)
                    currentTile.x++;
                else if (currentTile.dir == left)
                    currentTile.y--;

                if (currentTile.y - 1 > -1 && currentTile.y - 1 < dimensions  &&currentTile.x > -1 && currentTile.x < dimensions && layout[currentTile.x, currentTile.y - 1] != grass)
                    possibilities.RemoveAt(possibilities.IndexOf(left));
                if (currentTile.y + 1 < dimensions && currentTile.y + 1 > -1 && currentTile.x > -1 && currentTile.x < dimensions && layout[currentTile.x, currentTile.y + 1] != grass)
                    possibilities.RemoveAt(possibilities.IndexOf(right));
                if (currentTile.x - 1 > -1 && currentTile.x - 1 < dimensions && currentTile.y > -1 && currentTile.y < dimensions && layout[currentTile.x - 1, currentTile.y] != grass)
                    possibilities.RemoveAt(possibilities.IndexOf(up));
                if (currentTile.x + 1 < dimensions && currentTile.x > -1 && currentTile.y > -1 && currentTile.y < dimensions && layout[currentTile.x + 1, currentTile.y] != grass)
                    possibilities.RemoveAt(possibilities.IndexOf(down));

                /*if (layout[currentTile.x, currentTile.y] != grass)
                {
                    layout[currentTile.x, currentTile.y] = omni;
                    continue;
                }*/

                if (currentTile.x > -1 && currentTile.x < dimensions && currentTile.y > -1 && currentTile.y < dimensions)
                {
                    currentTile.dir = possibilities[rndGen.Next(0, possibilities.Count)];
                    layout[currentTile.x, currentTile.y] = currentTile.dir;
                }
                  
            }
        }

        // There are two factors for the next tile:
        // its possible locations and its possible directions

        /*
         * Possible algorithm solution for procedurally generating a path: Big Head method
         * This works if you want to have a tiled path where each tile is adjacent to either TWO or FOUR other tiles
         * Imagine the path as a snake with a slim body but a big head
         * The snake will always seek the next tile where its head has the most amount of space
         */
        //private (int x, int y, char dir) Walk((int x, int y, char dir) currentTile)
        //{
        //    (int x, int y, char dir) nextTile = currentTile;
        //    // possible optimization: Use enums mapped to numbers or bit flags so all you have to do is remove the possibility via division
        //    //List<char> possibleDirections = new List<char>() { pathLeft, pathRight, pathUp, pathDown };
        //
        //
        //
        //    /*
        //     * If the current tile is a right tile, then the next tile must always be to the very right of it.
        //     * The next tile's possible directions are: Up, Right, Down.
        //     */
        //    if (layout[x, y] == start || layout[x, y] == right)
        //    {
        //        nextTile.y++;
        //        List<char> possibleDirections = new List<char>() { up, right, down };
        //        if (!CheckSurroundings(nextTile.x - 1, nextTile.y))
        //            possibleDirections.RemoveAt(possibleDirections.IndexOf(up));
        //        if (!CheckSurroundings(nextTile.x, nextTile.y + 1))
        //            possibleDirections.RemoveAt(possibleDirections.IndexOf(right));
        //        if (!CheckSurroundings(nextTile.x + 1, nextTile.y))
        //            possibleDirections.RemoveAt(possibleDirections.IndexOf(down));
        //        if (possibleDirections.Count > 0)
        //            nextTile.dir = possibleDirections[rndGen.Next(0, possibleDirections.Count)];
        //    }
        //    /*
        //     * If the current tile is a left tile, then the next tile must always be to the very left of it.
        //     * The next tile's possible directions are: Up, Left, Down.
        //     */
        //    else if (layout[x, y] == left)
        //    {
        //        //nextTile.x = x;
        //        nextTile.y--;
        //        List<char> possibleDirections = new List<char>() { up, left, down };
        //        if (!CheckSurroundings(nextTile.x - 1, nextTile.y))
        //            possibleDirections.RemoveAt(possibleDirections.IndexOf(up));
        //        if (!CheckSurroundings(nextTile.x, nextTile.y - 1))
        //            possibleDirections.RemoveAt(possibleDirections.IndexOf(left));
        //        if (!CheckSurroundings(nextTile.x + 1, nextTile.y))
        //            possibleDirections.RemoveAt(possibleDirections.IndexOf(down));
        //        if (possibleDirections.Count > 0)
        //            nextTile.dir = possibleDirections[rndGen.Next(0, possibleDirections.Count)];
        //    }
        //    /*
        //     * If the current tile is an up tile, then the next tile must always be right above it.
        //     * The next tile's possible directions are: Left, Up, Right.
        //     */
        //    else if (layout[x, y] == up)
        //    {
        //        nextTile.x--;
        //        List<char> possibleDirections = new List<char>() { left, up, right };
        //        if (!CheckSurroundings(nextTile.x, nextTile.y - 1))
        //            possibleDirections.RemoveAt(possibleDirections.IndexOf(left));
        //        if (!CheckSurroundings(nextTile.x - 1, nextTile.y))
        //            possibleDirections.RemoveAt(possibleDirections.IndexOf(up));
        //        if (!CheckSurroundings(nextTile.x, nextTile.y + 1))
        //            possibleDirections.RemoveAt(possibleDirections.IndexOf(right));
        //        if (possibleDirections.Count > 0)
        //            nextTile.dir = possibleDirections[rndGen.Next(0, possibleDirections.Count)];
        //    }
        //    /*
        //     * If the current tile is a down tile, then the next tile must always be right below it.
        //     * The next tile's possible directions are: Left, Down, Right.
        //     */
        //    else if (layout[x, y] == down)
        //    {
        //        nextTile.x++;
        //        List<char> possibleDirections = new List<char>() { left, down, right };
        //        if (!CheckSurroundings(nextTile.x, nextTile.y - 1))
        //            possibleDirections.RemoveAt(possibleDirections.IndexOf(left));
        //        if (!CheckSurroundings(nextTile.x + 1, nextTile.y))
        //            possibleDirections.RemoveAt(possibleDirections.IndexOf(down));
        //        if (!CheckSurroundings(nextTile.x, nextTile.y + 1))
        //            possibleDirections.RemoveAt(possibleDirections.IndexOf(right));
        //        if (possibleDirections.Count > 0)
        //            nextTile.dir = possibleDirections[rndGen.Next(0, possibleDirections.Count)];
        //    }
        //
        //    if (layout[nextTile.x, nextTile.y] != '.')
        //    {
        //        layout[nextTile.x, nextTile.y] = omni;
        //        nextTile.dir = layout[x, y];
        //    }
        //    /*
        //     * If the current tile is an omni tile, then the next tile's location depends on its direction
        //     * and the omni tile's direction depends on the very previous tile's in the path
        //     * but then again, omni tiles are only created when the newest tile of the path intersects with a tile that was already paved previously in the path
        //     * that means that there can only be one possibility for the next tile's location once the current tile transforms into an omni tile
        //     * and that location depends on the direction of the tile BEFORE that omni tile
        //     * if the direction of that tile is right, then the next tile must be to the very right of the omni tile and its only possible direction is right
        //     * same idea if the direction of that tile is left, up, or down
        //     */
        //    return nextTile;
        //}
        //
        //private bool CheckSurroundings(int x, int y)
        //{
        //    if (x <= 0 || x >= dimensions || y <= 0 || y >= dimensions)
        //        return false;
        //
        //   
        //    if (((layout[x, y + 1] != grass && layout[x, y + 1] != 'X') && (layout[x + 1, y] != grass && layout[x + 1, y] != 'X') && (layout[x + 1, y + 1] != grass && layout[x + 1, y + 1] != 'X')) ||
        //        ((layout[x, y + 1] != grass && layout[x, y + 1] != 'X') && (layout[x - 1, y] != grass && layout[x - 1, y] != 'X') && (layout[x - 1, y + 1] != grass && layout[x - 1, y + 1] != 'X')) ||
        //        ((layout[x, y - 1] != grass && layout[x, y - 1] != 'X') && (layout[x + 1, y] != grass && layout[x + 1, y] != 'X') && (layout[x + 1, y - 1] != grass && layout[x + 1, y - 1] != 'X')) ||
        //        ((layout[x, y - 1] != grass && layout[x, y - 1] != 'X') && (layout[x - 1, y] != grass && layout[x - 1, y] != 'X') && (layout[x - 1, y - 1] != grass && layout[x - 1, y - 1] != 'X')))
        //        return false;
        //   
        //    return true;
        //}
    }
}
