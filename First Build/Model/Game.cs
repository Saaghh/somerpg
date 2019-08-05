using First_Build.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build.Controller
{
    public static class Game
    {
        static public CharacterBase player;
        static public BattleUseless battle;


        public static void IniGame()
        {
            battle = new BattleUseless();
        }
    }

    public static class GameVisualiser
    {
        
    }
}
