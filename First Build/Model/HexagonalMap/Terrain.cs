using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace First_Build
{
    [DataContract]
    public class Terrain
    {
        [DataMember]
        public int moveCost;
        [DataMember]
        public string type;
        [DataMember]
        public bool walkable;
        [DataMember]
        public Bitmap texture = Properties.Resources.TestTile;

        public struct BattleTerrain
        {
            public static Terrain Stone
            {
                get
                {
                    var x = new Terrain
                    {
                        moveCost = 1000,
                        type = "Stone",
                        walkable = true,
                        texture = Properties.Resources.StoneTile
                    };
                    return x;
                }
            }
            public static Terrain Water
            {
                get
                {
                    var x = new Terrain
                    {
                        moveCost = 4,
                        type = "Water",
                        walkable = false,
                        texture = Properties.Resources.WaterTile
                    };
                    return x;
                }
            }
            public static Terrain Flat
            {
                get
                {
                    var x = new Terrain
                    {
                        moveCost = 1,
                        type = "Flat",
                        walkable = true,
                        texture = Properties.Resources.FlatTile
                    };
                    return x;
                }
            }
        }
        public struct WorldTerrain
        {
            public static Terrain Field
            {
                get
                {
                    var x = new Terrain
                    {
                        moveCost = 10,
                        type = "Field",
                        walkable = true,
                        texture = Properties.Resources.WorldFlatTile
                    };
                    return x;
                }
            }
        }
    }
}
