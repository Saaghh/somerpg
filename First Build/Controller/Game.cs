using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build.Model
{
    public class Game
    {
        Character player = new Character();
        Character mob1 = new Character();

        public Game()
        {
            mob1.name = "Zombie";
        }
    }
}
