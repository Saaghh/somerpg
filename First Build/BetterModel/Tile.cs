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

namespace First_Build.BetterModel
{
    [DataContract]
    public class Tile
    {
        [DataMember]
        public (int x, int y) coord;

        [DataMember]
        public Terrain terrain;

        public Character character;


        public Tile((int x, int y) coord)
        {
            this.coord = coord;
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
            Console.WriteLine(actor.name + " has entered " + coord);
        }

        public void Leave()
        {
            character = null;
        }
    }
}
