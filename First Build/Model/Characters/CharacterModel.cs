using First_Build.Model.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace First_Build.Model.Characters
{
    public abstract class CharacterModel
    {
        static int amount = 0;
        public string name;

        public int health;
        public int actionPoints;

        public int weapon;
        public int offhand;
        public int[] addons;

        public int clothing;

        public TileModel position;

        public CharacterModel(TileModel position)
        {
            this.position = position;
            name = "Nameless Char - " + amount++;

            weapon = offhand = 10;

            clothing = 6;

            health = 200;
        }
    }
}
