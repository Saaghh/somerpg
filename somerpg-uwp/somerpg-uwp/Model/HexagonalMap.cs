using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;


namespace somerpg_uwp
{
    public abstract class HexagonalMap : IEnumerable
    {
        public const int HEXPIXELWIDTH = 200;
        public const int HEXPIXELHEIGHT = 120;
        public const int HEXPIXELHEIGHTOFFSET = 80;

        Tile[,] Tiles { get; set; }
        public Tile this[int x, int y]
        {
            get
            {
                return Tiles[x, y];
            }
            set
            {
                Tiles[x, y] = value;
            }
        }

        public HexagonalMap(int width, int height)
        {
            Tiles = new Tile[width, height];
        }

        public Point GetSize()
        {
            return new Point(Tiles.GetLength(0), Tiles.GetLength(1));
        }
        public Tile GetTileFromPoint(Point point)
        {
            return Tiles[point.X, point.Y];
        }
        public List<Tile> GetTilesFromPoints(List<Point> points)
        {
            List<Tile> result = new List<Tile>();
            foreach (Point item in points)
            {
                result.Add(Tiles[item.X, item.Y]);
            }
            return result;
        }
        public List<Tile> GetNeighbourTiles(Tile tile)
        {
            var points = GetNeighbourPoints(tile.Coord);
            var result = new List<Tile>();

            foreach (var point in points)
            {
                if (point.X < 0 || point.X >= Tiles.GetLength(0))
                    continue;
                if (point.Y < 0 || point.Y >= Tiles.GetLength(1))
                    continue;

                result.Add(GetTileFromPoint(point));
            }

            return result;
        }
        public IEnumerator GetEnumerator()
        {
            return Tiles.GetEnumerator();
        }

        public static List<Point> GetNeighbourPoints(Point coord)
        {
            List<Point> neighbourPoints = new List<Point>
            {
                new Point(coord.X + 1, coord.Y),
                new Point(coord.X - 1, coord.Y),
                new Point(coord.X, coord.Y + 1),
                new Point(coord.X, coord.Y - 1)
            };
            if (coord.X % 2 == 0) //если четный столблец
            {
                neighbourPoints.Add(new Point(coord.X + 1, coord.Y + 1));
                neighbourPoints.Add(new Point(coord.X - 1, coord.Y + 1));
            }
            else //если столбец нечетный
            {
                neighbourPoints.Add(new Point(coord.X - 1, coord.Y - 1));
                neighbourPoints.Add(new Point(coord.X + 1, coord.Y - 1));
            }

            return neighbourPoints;
        }
        public static double GetDistanceBetweenPoints(PointF point1, PointF point2)
        {
            float x1 = point1.X, x2 = point2.X, y1 = point1.X, y2 = point2.Y;

            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
        public static Point HexToPixel(Point hexCoord) => SquareToPixel(HexToSquare(hexCoord));
        public static PointF HexToSquare(Point hexCoord)
        {
            var p = new PointF
            {
                X = 0.75f * hexCoord.X,
                Y = hexCoord.Y
            };

            if (hexCoord.X % 2 != 1) //если столбец четный
            {
                p.Y += 0.5f;
            }

            return p;
        }
        public static Point SquareToPixel(PointF squareCoord)
        {
            Point p = new Point
            {
                X = Convert.ToInt32(squareCoord.X * HEXPIXELWIDTH),
                Y = Convert.ToInt32(squareCoord.Y * HEXPIXELHEIGHT)
            };

            return p;
        }
        public static (int width, int height) GetMapPixelSize((int x, int y) dataSize)
        {
            (int width, int height) result;
            result.width = ((HEXPIXELWIDTH / 4 * 3) * dataSize.x) + (HEXPIXELWIDTH / 4);
            result.height = (HEXPIXELHEIGHT * dataSize.y) + (HEXPIXELHEIGHT / 2) + HEXPIXELHEIGHTOFFSET;

            return result;
        }
        public static Point PixelToHex(Point coord)
        {
            //Console.WriteLine("-----------");

            const int quarter = (int)(HEXPIXELWIDTH * 0.25f);

            (int x, int y) pixelCoord =
                (Convert.ToInt32(coord.X),
                 Convert.ToInt32(coord.Y - HEXPIXELHEIGHTOFFSET));
            var n = HEXPIXELWIDTH - quarter;


            int x = pixelCoord.x / n;
            int xRem = pixelCoord.x % n;
            int quarterN = xRem / quarter;
            if (x % 2 == 0)
            {
                pixelCoord.y -= HEXPIXELHEIGHT / 2;
            }
            int y = pixelCoord.y / HEXPIXELHEIGHT;

            int yRem = pixelCoord.y % HEXPIXELHEIGHT;
            if (yRem < 0) { return Point.Empty; }



            if (quarterN == 0)
            {
                int x1, x2, y1, y2;
                x1 = quarter;
                y1 = 0;
                x2 = 0;
                y2 = HEXPIXELHEIGHT / 2;

                var linecheck1 = ((y1 - y2) * xRem) + ((x2 - x1) * yRem) + ((x1 * y2) - (x2 * y1));

                y1 = HEXPIXELHEIGHT;

                var linecheck2 = ((y1 - y2) * xRem) + ((x2 - x1) * yRem) + ((x1 * y2) - (x2 * y1));

                int trianglepart = 0;

                if (linecheck1 > 0) { trianglepart = -1; }
                else if (linecheck2 < 0) { trianglepart = 1; }

                switch (trianglepart)
                {
                    case -1:

                        if (x % 2 != 0)
                        {
                            y += -1;
                        }
                        x += -1;
                        break;
                    case 1:
                        if (x % 2 == 0)
                        {
                            y += 1;
                        }
                        x += -1;
                        break;
                }
            }
            return new Point(x, y);
        }
    }

    public class WorldMap : HexagonalMap
    {
        public WorldMap() : base(100, 100)
        {
            IniTiles();
        }

        void IniTiles()
        {
            Random r = new Random();
            for (int i = 0; i < GetSize().X; i++)
            {
                for (int j = 0; j < GetSize().Y; j++)
                {
                    if (r.Next(2) == 1)
                    {
                        this[i, j] = Tiles.FlatTile;
                    }
                    else
                    {
                        this[i, j] = Tiles.ForestTile;

                    }
                    this[i, j].Coord = new Point(i, j);

                }
            }
        }

        //void IniTiles(Point size)
        //{

        //    Tiles = new Tile[size.X, size.Y];
        //    for (int i = 0; i < Tiles.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < Tiles.GetLength(1); j++)
        //        {
        //            Tiles[i, j] = new WorldTile(new Point(i, j));
        //        }
        //    }
        //}

        //public Bitmap GetMapTexture()
        //{
        //    var size = GetMapPixelSize((Tiles.GetLength(0), Tiles.GetLength(0)));
        //    var bitmap = new Bitmap(size.width, size.height);

        //    Graphics g = Graphics.FromImage(bitmap);

        //    foreach (Tile item in Tiles)
        //    {
        //        var pixelCoord = HexToPixel(item.Coord);
        //        g.DrawImage(item.Terrain.bitmapTexture, pixelCoord);
        //    }

        //    return bitmap;
        //}
    }
}
//var s = Imaging.CreateBitmapSourceFromHBitmap(tiles.GetMapTexture().GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());