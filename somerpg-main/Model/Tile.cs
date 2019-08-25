using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace somerpg_main
{
    public class Tile
    {
        Point coord;
        Terrain terrain;

        public Terrain Terrain { get => terrain; set => terrain = value; }
        public Point Coord { get => coord; protected set => coord = value; }
    }

    public class Terrain
    {
        public Uri textureUri;
        public string name;
        public float moveCost;
        public bool isWalkable;
        public Bitmap bitmapTexture;

        public static readonly Bitmap WorldFlatTile = Properties.Resources.WorldFlatTile;

        public static Terrain FlatWorldTerrain
        {
            get
            {
                return new Terrain
                {
                    textureUri = new Uri("Resources/WorldFlatTile.png", UriKind.RelativeOrAbsolute),
                    name = "FlatWorldTerrain",
                    moveCost = 1,
                    isWalkable = true,
                    bitmapTexture = WorldFlatTile
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
}
