using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Point = System.Drawing.Point;

namespace First_Build
{
    public class BattleTile : Tile
    {
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
        public BattleTile(Point coord) : base(coord) { }

        //TODO: Сделать ивент, который заставит карту обновиться
        public int GetEnterCost()
        {
            return terrain.moveCost;
        }
        public BattleTile CreateTileFromJson(string json)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(BattleTile));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            return (BattleTile)serializer.ReadObject(ms);
        }
        public void Enter(Character actor)
        {
            if(!terrain.walkable || ContainsCharacter) { throw new Exception("Tile can't be entered"); }
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
    }
}
