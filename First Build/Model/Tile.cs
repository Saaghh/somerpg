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
    [DataContract]
    public class Tile : IEquatable<Tile>
    {
        [DataMember]
        public Point coord;

        [DataMember]
        public Terrain terrain;

        public Character character;

        public bool ContainsCharacter
        {
            get
            {
                if (character != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Tile((int x, int y) coord)
        {
            this.coord = new Point(coord.x, coord.y);
        }

        //TODO: Сделать ивент, который заставит карту обновиться
        void ChangeTerrainFromJson(string json)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Terrain));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            terrain = (Terrain)serializer.ReadObject(ms);
        }

        public int GetEnterCost()
        {
            return terrain.moveCost;
        }

        public Tile CreateTileFromJson(string json)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Tile));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            return (Tile)serializer.ReadObject(ms);
        }

        public override string ToString()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Tile));
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, this);
            return Encoding.Default.GetString((ms.ToArray()));
        }

        public void Enter(Character actor)
        {
            if(!terrain.walkable) { throw new Exception("Tile can't be entered"); }
            character = actor;
            character.Died += Actor_Died;
            Console.WriteLine(actor.name + " has entered " + coord);
        }

        private void Actor_Died(object sender, EventArgs e)
        {
            Leave();
        }

        public void Leave()
        {
            if (character != null)
            {
                character.Died -= Actor_Died;
                character = null;
            }
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
