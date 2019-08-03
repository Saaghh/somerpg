using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace First_Build.Model
{
    public class CharacterBase
    {
        static Random r = new Random();
        public string name = "Empty Name";
        public float health = 200f;
        public int weapon = 10;
        public int armor = 1;

        public CharacterBase()
        {
            name = "Char" + r.Next(100).ToString();
        }

        public virtual void GetHit(int damage)
        {
            health -= weapon - armor;
        }

        public virtual void Attack(CharacterBase target)
        {
            target.GetHit(weapon);
        }
        public static class ActionCommands
        {
            public const string ATTACK = "attack";
            public const string SKIP = "skip";
        }
    }
}
