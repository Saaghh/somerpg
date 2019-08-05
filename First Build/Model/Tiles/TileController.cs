using First_Build.Model.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build.Model.Tiles
{
    public class TileController : TileModel
    {
        public TileController((int x, int y) coord, string terrainJson, CharacterControl characterControl) : base(coord, terrainJson, characterControl) { }

        public void Enter(CharacterController actor)
        {
            character = actor;
            Console.WriteLine(actor.name + " has entered " + position);
        }

        public void Leave()
        {
            Console.WriteLine(character.name + " has left " + position);
            character = null;
        }
    }
}
