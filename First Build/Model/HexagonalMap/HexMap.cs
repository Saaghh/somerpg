using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Windows;
using Point = System.Drawing.Point;

namespace First_Build
{

    public class HexMap : IEnumerable
    {
        public const int HEXPIXELWIDTH = 200;
        public const int HEXPIXELHEIGHT = 120;

        public int MapWidth = 20;
        public int MapHeight = 20;

        protected static Random r = new Random();

        protected Tile[,] tiles;

        public virtual Tile[,] Tiles { get => tiles; set => tiles = value; }

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

        public HexMap()
        {
            GenerateMap();
        }

        protected virtual void GenerateMap()
        {

        }

        public virtual Bitmap GetMapTexture()
        {
            var (width, height) = GetMapPixelSize((MapWidth, MapHeight));
            var texture = new Bitmap(width, height);

            Graphics g = Graphics.FromImage(texture);
            for (int i = 0; i < Tiles.GetLength(1); i++)
            {
                for (int j = 0; j < Tiles.GetLength(0); j++)
                {
                    var item = Tiles[i, j];
                    var pixelCoord = HexToPixel(new Point(i, j));
                    g.DrawImage(item.Texture, pixelCoord);
                }
            }

            texture.Save("Map" + ".png");

            return texture;
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
            var points = GetNeighbourPoints(tile.coord);
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
        public int GetDistanceBetweenTiles(Tile tile1, Tile tile2)
        {
            var x = AStar.FindPath(this, tile1.coord, tile2.coord, true);

            return x.Count - 1;
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
            result.height = (HEXPIXELHEIGHT * dataSize.y) + (HEXPIXELHEIGHT / 2);

            return result;
        }
    }
}

