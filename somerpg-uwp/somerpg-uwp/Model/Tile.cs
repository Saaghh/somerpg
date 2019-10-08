using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace somerpg_uwp
{
    public class Tile
    {
        Point coord;
        public virtual List<Uri> TextureUris
        {
            get
            {
                List<Uri> list = new List<Uri>();
                list.Add(Terrain.TextureUri);
                return list;
            }
        }

        public Point Coord { get => coord; set => coord = value; }
        public Terrain Terrain { get; set; }
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

    }

    public class WorldTile : Tile
    {
        public readonly WordlTileContent content;
        public bool HasContent
        {
            get
            {
                if (content == null) { return false; }
                return true;
            }
        }
        public override List<Uri> TextureUris
        {
            get
            {
                if (HasContent)
                {
                    var list = base.TextureUris;
                    list.Add(content.textureUri);
                    return list;
                }
                else
                {
                    return base.TextureUris;
                }
            }
        }
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
                    Name = "FlatWorldTerrain",
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
                    Name = "ForestWorldTerrain",
                    MoveCost = 2,
                    IsWalkable = true
                };
            }
        }

    }

    public class WordlTileContent : ContentObject
    {
        public Uri textureUri;
        public string name = "EmptyContent";

        public WordlTileContent()
        {
            name = "EmptyContent";
        }
    }

    public class Settlement : WordlTileContent
    {
        static protected int number = 0;

        public List<Building> buildings;

        public Settlement()
        {
            textureUri = new Uri("Resources/Village.png", UriKind.RelativeOrAbsolute);
            name = "Settlement #" + ++number;
        }
    }

    public class ContentObject : IEquatable<ContentObject>
    {
        private static int number = 0;
        protected static int NextID
        {
            get
            {
                return number++;
            }
        }

        public string name = "Empty Name";
        protected readonly int id;

        public ContentObject()
        {
            id = NextID;
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as ContentObject);
        }
        public bool Equals(ContentObject other)
        {
            return other != null &&
                   id == other.id;
        }
        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
        public override string ToString()
        {
            return name;
        }
        public static bool operator ==(ContentObject left, ContentObject right)
        {
            return EqualityComparer<ContentObject>.Default.Equals(left, right);
        }
        public static bool operator !=(ContentObject left, ContentObject right)
        {
            return !(left == right);
        }
    }

    public class TileResource : ContentObject
    {
        public TileResource(string name) : base()
        {
            this.name = name;
        }
    }

    public class Building : ContentObject
    {
        public Building() : base()
        {
            name = "Building " + id;
        }
    }

    public class City : Settlement
    {
        public static List<City> CitiesList = new List<City>();
        public City() : base()
        {
            name = "City #" + ++number;
            CitiesList.Add(this);
        }
    }
}