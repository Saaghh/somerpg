using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows;
using Color = Windows.UI.Color;

namespace somerpg_uwp
{
    public class Tile
    {
        static Random r = new Random(10000);

        Point coord;
        Point drawPoint;

        public Tile()
        {
            WorldLevel = r.Next(5) - 2;
        }

        public virtual List<Uri> TextureUris
        {
            get
            {
                List<Uri> list = new List<Uri>();
                list.Add(Terrain.TextureUri);
                return list;
            }
        }
        public Point DrawPoint
        {
            get
            {
                if (drawPoint == Point.Empty)
                {
                    drawPoint = HexagonalMap.HexToPixel(coord);
                }

                return drawPoint;
            }
        }
        public Point Coord { get => coord; set => coord = value; }
        public Terrain Terrain { get; set; }
        public int WorldLevel { get; set; }

    }

    public static class Tiles
    {
        public static Tile FlatTile
        {
            get
            {
                return new Tile
                {
                    Terrain = Terrain.FlatWorldTerrain
                };
            }
        }
        public static Tile ForestTile
        {
            get
            {
                return new Tile
                {
                    Terrain = Terrain.ForestWorldTerrain
                };
            }
        }
        public static Tile WaterTile
        {
            get
            {
                return new Tile
                {
                    Terrain = Terrain.WaterWorldTerrain
                };
            }
        }
        public static Tile MountainTile
        {
            get
            {
                return new Tile
                {
                    Terrain = Terrain.MountainWorldTerrain
                };
            }
        }
    }

    public class WorldTile : Tile
    {
        public WorldTile(Point coord)
        {
            Coord = coord;
            Terrain = Terrain.FlatWorldTerrain;

        }
        

    }

    public class Terrain
    {
        public Uri TextureUri { get; set; }
        public string Name { get; set; }
        public float MoveCost { get; set; }
        public bool IsWalkable { get; set; }

        public static Terrain FlatWorldTerrain
        {
            get
            {
                return new Terrain
                {
                    TextureUri = new Uri("ms-appx:///Textures/FlatTile.png", UriKind.RelativeOrAbsolute),
                    Name = "FlatTile",
                    MoveCost = 1,
                    IsWalkable = true
                };
            }
        }
        public static Terrain ForestWorldTerrain
        {
            get
            {
                return new Terrain
                {
                    TextureUri = new Uri("ms-appx:///Textures/Forest.png", UriKind.RelativeOrAbsolute),
                    Name = "Forest",
                    MoveCost = 2,
                    IsWalkable = true
                };
            }
        }
        public static Terrain WaterWorldTerrain
        {
            get
            {
                return new Terrain
                {
                    TextureUri = new Uri("ms-appx:///Textures/Water.png", UriKind.RelativeOrAbsolute),
                    Name = "Water",
                    MoveCost = 10,
                    IsWalkable = true
                };
            }
        }
        public static Terrain MountainWorldTerrain
        {
            get
            {
                return new Terrain
                {
                    TextureUri = new Uri("ms-appx:///Textures/Mountain.png", UriKind.RelativeOrAbsolute),
                    Name = "Mountain",
                    MoveCost = 100,
                    IsWalkable = false
                };
            }
        }
    }
}