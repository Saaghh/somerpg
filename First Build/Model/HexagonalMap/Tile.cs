using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Point = System.Drawing.Point;

namespace First_Build
{

    public class Tile : IEquatable<Tile>
    {
        public Point coord;
        public Terrain terrain;

        public Tile(Point coord)
        {
            this.coord = coord;
        }

        public virtual Bitmap Texture
        {
            get
            {
                return terrain.texture;
            }
        }

        public override string ToString()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(BattleTile));
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, this);
            return Encoding.Default.GetString((ms.ToArray()));
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Tile);
        }
        public bool Equals(Tile other)
        {
            return other != null &&
                   EqualityComparer<Point>.Default.Equals(coord, other.coord);
        }
        public override int GetHashCode()
        {
            return -1469483106 + EqualityComparer<Point>.Default.GetHashCode(coord);
        }
        public static bool operator ==(Tile left, Tile right)
        {
            return EqualityComparer<Tile>.Default.Equals(left, right);
        }
        public static bool operator !=(Tile left, Tile right)
        {
            return !(left == right);
        }
    }
}
