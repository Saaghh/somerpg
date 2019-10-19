namespace somerpg_uwp
{
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
}