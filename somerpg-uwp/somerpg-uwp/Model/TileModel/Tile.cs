using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows;
using Color = Windows.UI.Color;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace somerpg_uwp
{
    public class Tile : IDrawableObject
    {
        Point drawPoint;

        Underground underground;
        Terrain terrain;
        Water water;
        Landscape landscape;
        Buildings buildings;

        public Point DrawPoint
        {
            get
            {
                if (drawPoint == Point.Empty)
                {
                    drawPoint = HexagonalMap.HexToPixel(Coord);
                }

                return drawPoint;
            }
        }
        public Point Coord { get; set; }
        
        /// <summary>
        /// Returns -1 if tile isn't walkable
        /// </summary>
        public int MoveCost
        {
            get
            {
                int x = 0;

                if (underground.IsWalkable) { x += underground.MoveCost; }  else { return -1; }
                if (terrain.IsWalkable)     { x += terrain.MoveCost; }      else { return -1; }
                if (water.IsWalkable)       { x += water.MoveCost; }        else { return -1; }
                if (landscape.IsWalkable)   { x += landscape.MoveCost; }    else { return -1; }
                if (buildings.IsWalkable)   { x += buildings.MoveCost; }    else { return -1; }

                return x;
            }
        }

        public int WorldLevel { get; set; } = 0;

        public Tile()
        {
            underground = new Underground();
            terrain = new Terrain();
            water = new Water();
            landscape = new Landscape();
            buildings = new Buildings();
        }

        public void Draw(CanvasAnimatedDrawEventArgs args, int offsetX, int offsetY)
        {
            int x = DrawPoint.X + offsetX;
            int y = DrawPoint.Y + offsetY;

            underground.Draw(args, x, y);
            terrain.Draw(args, x, y);
            water.Draw(args, x, y);
            landscape.Draw(args, x, y);
            buildings.Draw(args, x, y);
        }
    }

    public enum TrianglePosition { TopLeft, Top, TopRight, BottomRight, Bottom, BottomLeft }
    public enum TriangleType { FlatTop, FlatBottom }

    //public class Terrain
    //{
    //    public Uri TextureUri { get; set; }
    //    public string Name { get; set; }
    //    public float MoveCost { get; set; }
    //    public bool IsWalkable { get; set; }

    //    public static Terrain FlatWorldTerrain
    //    {
    //        get
    //        {
    //            return new Terrain
    //            {
    //                TextureUri = new Uri("ms-appx:///Textures/FlatTile.png", UriKind.RelativeOrAbsolute),
    //                Name = "FlatTile",
    //                MoveCost = 1,
    //                IsWalkable = true
    //            };
    //        }
    //    }
    //    public static Terrain ForestWorldTerrain
    //    {
    //        get
    //        {
    //            return new Terrain
    //            {
    //                TextureUri = new Uri("ms-appx:///Textures/Forest.png", UriKind.RelativeOrAbsolute),
    //                Name = "Forest",
    //                MoveCost = 2,
    //                IsWalkable = true
    //            };
    //        }
    //    }
    //    public static Terrain WaterWorldTerrain
    //    {
    //        get
    //        {
    //            return new Terrain
    //            {
    //                TextureUri = new Uri("ms-appx:///Textures/Water.png", UriKind.RelativeOrAbsolute),
    //                Name = "Water",
    //                MoveCost = 10,
    //                IsWalkable = true
    //            };
    //        }
    //    }
    //    public static Terrain MountainWorldTerrain
    //    {
    //        get
    //        {
    //            return new Terrain
    //            {
    //                TextureUri = new Uri("ms-appx:///Textures/Mountain.png", UriKind.RelativeOrAbsolute),
    //                Name = "Mountain",
    //                MoveCost = 100,
    //                IsWalkable = false
    //            };
    //        }
    //    }
    //}
}