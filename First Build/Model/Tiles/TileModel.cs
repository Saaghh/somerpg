using First_Build.Model.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace First_Build.Model.Tiles
{
    public abstract class TileModel
    {
        public (int x, int y) position;
        public CharacterModel character;
        public CharacterControl characterControl;

        public Terrain terrain;
        
        public TileModel((int x, int y) coord, string terrainJson, CharacterControl characterControl)
        {
            this.characterControl = characterControl;
            position = coord;
            LoadTerrainFromJson(terrainJson);
        }

        void LoadTerrainFromJson(string json)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Terrain));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            terrain = (Terrain)serializer.ReadObject(ms);
        }

        public int GetEnterCost()
        {
            return terrain.moveCost;
        }

        [DataContract]
        public class Terrain
        {
            [DataMember]
            public int moveCost;
            [DataMember]
            public string type;
            [DataMember]
            public bool walkable;
        }

    }
}
