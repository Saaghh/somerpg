﻿namespace somerpg_uwp
{
    public class TileLayer
    {
        public bool IsWalkable { get; set; } = true;
        public int MoveCost { get; set; } = 1;
        public ResourceKey DrawingResourceKey { get; set; } = ResourceKey.ErrorImage;
    }
    
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