using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace First_Build
{
    public class HexMap : IEnumerable
    {
        public const int HEXPIXELWIDTH = 200;
        public const int HEXPIXELHEIGHT = 160;
        public readonly static int PIXELDISTANCE = Math.Abs(HEXPIXELWIDTH - HEXPIXELWIDTH / 2);

        public const int MAPWIDTH = 20;
        public const int MAPHEIGHT = 20;

        protected static Random r = new Random();

        public Tile[,] tiles;

        public Tile this[int x, int y]
        {
            get
            {
                return tiles[x, y];
            }
            set
            {
                tiles[x, y] = value;
            }
        }

        public HexMap()
        {
            GenerateMap();
        }

        protected void GenerateMap()
        {
            tiles = new Tile[MAPWIDTH, MAPHEIGHT];

            for (int i = 0; i < MAPWIDTH; i++)
            {
                for (int j = 0; j < MAPHEIGHT; j++)
                {
                    tiles[i, j] = new Tile((i, j));
                    if (r.Next(10) == 0)
                    {
                        tiles[i, j].terrain = Terrain.Forest;
                    }
                    else
                    {
                        tiles[i, j].terrain = Terrain.Flat;
                    }
                }
            }
        }
        public IEnumerator GetEnumerator()
        {
            return tiles.GetEnumerator();
        }
        public Tile GetTileFromPoint(Point point)
        {
            return tiles[point.X, point.Y];
        }
        public List<Tile> GetTilesFromPoints(List<Point> points)
        {
            List<Tile> result = new List<Tile>();
            foreach (Point item in points)
            {
                result.Add(tiles[item.X, item.Y]);
            }
            return result;
        }

        public static Point GetHexCoordinate(int x, int y)
        {
            int resultX, resultY;
            if (x % 2 != 1) //если четный столбец
            {
                resultX = (HEXPIXELWIDTH / 4 * 3) * x;
                resultY = (HEXPIXELHEIGHT / 2) + (HEXPIXELHEIGHT * y);
            }
            else //если нечетный столбец
            {
                resultX = (HEXPIXELWIDTH / 4 * 3) * x;
                resultY = HEXPIXELHEIGHT * y;
            }

            return new Point(resultX, resultY);
        }
        public static (int width, int height) GetMapPixelSize((int x, int y) dataSize)
        {
            (int width, int height) result;
            result.width = ((HEXPIXELWIDTH / 4 * 3) * dataSize.x) + (HEXPIXELWIDTH / 4);
            result.height = (HEXPIXELHEIGHT * dataSize.y) + (HEXPIXELHEIGHT / 2);

            return result;
        }
        public static (int x, int y) GetCharacterStarterTilePosition((int w, int h) mapSize, int order, int team)
        {
            (int x, int y) position;

            switch (team)
            {
                case 0:
                    position.x = mapSize.w / 8 * 3;
                    position.y = mapSize.h / 8 * 3 + order;
                    break;
                case 1:
                    position.x = mapSize.w / 8 * 5;
                    position.y = mapSize.h / 8 * 3 + order;
                    break;
                default:
                    throw new Exception("Сражаться могут лишь 2 команды");
            }

            return position;
        }
        public static int GetHexDistance(Tile tile1, Tile tile2, HexMap hexMap)
        {
            return AStar.FindPath(hexMap, tile1.coord, tile2.coord).Count;
        }
    }
}

