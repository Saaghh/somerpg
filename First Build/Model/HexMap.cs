using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build
{
    public class HexMap
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

        public static (int x, int y) GetHexCoordinate(int x, int y)
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

            var result = (resultX, resultY);

            return result;
        }

        public static Point GetHexCoordinate(int x, int y, Point isPoint)
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

            var result = new Point(resultX, resultY);

            return result;
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

        public static int GetHexDistance(Tile tile1, Tile tile2)
        {
            var (width1, height1) = GetMapPixelSize(tile1.coord);
            //width1 += HEXPIXELWIDTH / 2;
            //height1 += HEXPIXELHEIGHT / 2;

            var (width2, height2) = GetMapPixelSize(tile2.coord);
            //width2 += HEXPIXELWIDTH / 2;
            //height2 += HEXPIXELHEIGHT / 2;

            var x = Math.Pow((width1 - width2), 2);
            var y = Math.Pow((height1 - height2), 2);

            var z = x + y;

            var t = Math.Sqrt(z);

            //var pixelDistance = Math.Sqrt(((width1 - width2) ^ 2) + ((height1 - height2) ^ 2));

            return (int)Math.Round(t / PIXELDISTANCE, 0);
        }

        public Tile[,] GetArray()
        {
            return tiles;
        }
    }
}
