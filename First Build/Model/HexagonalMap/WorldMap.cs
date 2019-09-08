using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build
{
    public class WorldMap : HexMap
    {
        List<City> cities = new List<City>();
        public WorldMap()
        {
            MapHeight = 50;
            MapWidth = 50;

            GenerateMap();
        }
        protected override void GenerateMap()
        {
            Tiles = new Tile[MapWidth, MapHeight];
            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapHeight; j++)
                {
                    Tiles[i, j] = new WorldTile(new System.Drawing.Point(i, j));
                    Tiles[i, j].terrain = Terrain.WorldTerrain.Field;
                }
            }
            GenerateCities();
        }

        private void GenerateCities()
        {
            foreach (WorldTile item in Tiles)
            {
                if (r.Next(100) == 1)
                {
                    var city = new City();
                    item.Content = city;
                    cities.Add(city);
                }
            }
        }
    }
}
