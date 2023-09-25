using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MTTPolish.GameStuff
{
    internal class Board
    {
        private Tile[,] map;
        private Random rndGen = new();

        private const int dimensions = 10;

        private const char grass = '.';
        private const char left = '\u2190';
        private const char right = '\u2192';
        private const char up = '\u2191';
        private const char down = '\u2193';
        private const char omni = '+'; // omni tiles will determine their direction based on the previous tile in the path

        public Board()
        {
            map = new Tile[dimensions, dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    map[i, j] = new Tile(i, j);
                    map[i, j].TileType = TileType.Grass;
                }
            }
            /*layout = new char[dimensions, dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                    layout[i, j] = '.';
            }

            // Decrease the range of possible starting X positions to ensure that the starting tile isn't spawned on the upper and lower borders of the map
            (int x, int y, char dir) currentTile = (rndGen.Next(1, dimensions - 1), 0, right);

            layout[currentTile.x, currentTile.y] = currentTile.dir;

            currentTile.y++;
            currentTile.dir = right;
            layout[currentTile.x, currentTile.y] = currentTile.dir;

            while (currentTile.y < dimensions - 1)
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

                List<char> possibilities = new() { left, right, up, down };

                if (currentTile.dir == right)
                    currentTile.y++;
                else if (currentTile.dir == up)
                    currentTile.x--;
                else if (currentTile.dir == down)
                    currentTile.x++;
                else if (currentTile.dir == left)
                    currentTile.y--;

                if (currentTile.y == 0 || currentTile.x == 0 || currentTile.x == dimensions - 1)

                if (currentTile.y - 1 > -1 && currentTile.y - 1 < dimensions  && currentTile.x > -1 && currentTile.x < dimensions && layout[currentTile.x, currentTile.y - 1] != grass)
                    possibilities.RemoveAt(possibilities.IndexOf(left));
                if (currentTile.y + 1 < dimensions && currentTile.y + 1 > -1 && currentTile.x > -1 && currentTile.x < dimensions && layout[currentTile.x, currentTile.y + 1] != grass)
                    possibilities.RemoveAt(possibilities.IndexOf(right));
                if (currentTile.x - 1 > -1 && currentTile.x - 1 < dimensions && currentTile.y > -1 && currentTile.y < dimensions && layout[currentTile.x - 1, currentTile.y] != grass)
                    possibilities.RemoveAt(possibilities.IndexOf(up));
                if (currentTile.x + 1 < dimensions && currentTile.x > -1 && currentTile.y > -1 && currentTile.y < dimensions && layout[currentTile.x + 1, currentTile.y] != grass)
                    possibilities.RemoveAt(possibilities.IndexOf(down));

                if (currentTile.x > -1 && currentTile.x < dimensions && currentTile.y > -1 && currentTile.y < dimensions)
                {
                    currentTile.dir = possibilities[rndGen.Next(0, possibilities.Count)];
                    layout[currentTile.x, currentTile.y] = currentTile.dir;
                }
            }*/


            Tile currentTile = new Tile(rndGen.Next(1, dimensions - 1), 0);
            map[currentTile.XCoordinate, currentTile.YCoordinate] = currentTile;
            while (currentTile.YCoordinate < dimensions - 1)
            {
                Print();

                int nextTileXCoordinate = currentTile.XCoordinate;
                int nextTileYCoordinate = currentTile.YCoordinate;
                if (currentTile.TileType == TileType.Left)
                    nextTileYCoordinate--;
                else if (currentTile.TileType == TileType.Right)
                    nextTileYCoordinate++;
                else if (currentTile.TileType == TileType.Up)
                    nextTileXCoordinate--;
                else if (currentTile.TileType == TileType.Down)
                    nextTileXCoordinate++;

                if (nextTileXCoordinate < 0)
                {

                }

                Tile nextTile = new Tile(nextTileXCoordinate, nextTileYCoordinate, currentTile);

                if (nextTile.YCoordinate - 1 == -1)
                    nextTile.RemovePossibility(TileType.Left);
                if (nextTile.YCoordinate + 1 == dimensions)
                    nextTile.RemovePossibility(TileType.Right);
                if (nextTile.XCoordinate - 1 == -1)
                    nextTile.RemovePossibility(TileType.Up);
                if (nextTile.XCoordinate + 1 == dimensions)
                    nextTile.RemovePossibility(TileType.Down);

                if (nextTile.NextPossibleTileTypes.HasFlag(TileType.Left) && map[nextTile.XCoordinate, nextTile.YCoordinate - 1].TileType != TileType.Grass)
                    nextTile.RemovePossibility(TileType.Left);
                if (nextTile.NextPossibleTileTypes.HasFlag(TileType.Right) && map[nextTile.XCoordinate, nextTile.YCoordinate + 1].TileType != TileType.Grass)
                    nextTile.RemovePossibility(TileType.Right);
                if (nextTile.NextPossibleTileTypes.HasFlag(TileType.Up) && map[nextTile.XCoordinate - 1, nextTile.YCoordinate].TileType != TileType.Grass)
                    nextTile.RemovePossibility(TileType.Up);
                if (nextTile.NextPossibleTileTypes.HasFlag(TileType.Down) && map[nextTile.XCoordinate + 1, nextTile.YCoordinate].TileType != TileType.Grass)
                    nextTile.RemovePossibility(TileType.Down);

                List<TileType> possibles = Enum.GetValues<TileType>().Where(tileType => { 
                    if (nextTile.NextPossibleTileTypes.HasFlag(tileType))
                    {
                        return true;
                    }
                    return false;
                }).ToList(); 

                nextTile.TileType = possibles[rndGen.Next(0, possibles.Count)];
                map[nextTile.XCoordinate, nextTile.YCoordinate] = nextTile;

                currentTile = nextTile;
            }
        } 

        private void Print()
        {
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    switch (map[i, j].TileType)
                    {
                        case (TileType.Left):
                            Debug.Write(left);
                            break;
                        case (TileType.Right):
                            Debug.Write(right);
                            break;
                        case (TileType.Up):
                            Debug.Write(up);
                            break;
                        case (TileType.Down):
                            Debug.Write(down);
                            break;
                        case (TileType.Omni):
                            Debug.Write(omni);
                            break;
                        case (TileType.Grass):
                            Debug.Write(grass);
                            break;
                    }
                }
                Debug.Write("\n");
            }
            Debug.Write("\n");
        }

        /*private bool Walk(Tile currentTile)
        {


            if (!Walk(currentTile))
            {

            }
            else
            {
                return true;
            }
            if (currentTile.Position.Y == dimensions - 1)
                return currentTile;

            if (currentTile.dir == right)
                currentTile.y++;
            else if (currentTile.dir == up)
                currentTile.x--;
            else if (currentTile.dir == down)
                currentTile.x++;
            else if (currentTile.dir == left)
                currentTile.y--;

            return Walk(currentTile);
        }*/
    }
}
